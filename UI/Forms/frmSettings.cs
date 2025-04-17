using DataRecorvery.Configurations;
using DataRecorvery.Domain.Models;
using DevExpress.XtraEditors;
using InfluxDB.Client;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataRecorvery.UI.Forms
{
    public partial class frmSettings : DevExpress.XtraEditors.XtraForm
    {
        private SettingsConfig _settings;
        private Dictionary<string, string> _testResults = new Dictionary<string, string>();

        public frmSettings()
        {
            InitializeComponent();
            LoadConfiguration();
            labelControl1.AllowHtmlString = true;

            // 초기화

        }
        private async Task UpdateTestStatusAsync(LabelControl labelItem, string targetName, string status)
        {
            string icon, color, message;
            //Application.DoEvents();

            switch (status)
            {
                case "pending":
                    icon = "🕘";
                    color = "black";
                    message = $"{targetName} 대기 중...";
                    break;
                case "success":
                    icon = "✅";
                    color = "green";
                    message = $"{targetName} 연결 성공";
                    break;
                case "fail":
                    icon = "❌";
                    color = "red";
                    message = $"{targetName} 연결 실패";
                    break;
                default:
                    icon = "⚪";
                    color = "gray";
                    message = $"{targetName} 상태 미정";
                    break;
            }

            // 모든 상태들을 병합해서 텍스트 업데이트
            string newLine = $"<b><color={color}>{icon} {message}</color></b>";

            // 기존 텍스트에 누적
            labelItem.Text += "<br>" + newLine;

            // UI 반영을 위해 필요 시
            //Application.DoEvents();
        }

        private void LoadConfiguration()
        {
            try
            {
                _settings = ConfigManager.LoadConfig();

                // DatabaseConfig 값들
                teDatabaseHost.Text = _settings.DatabaseConfig.Host;
                teDatabasePort.Text = _settings.DatabaseConfig.Port;
                teDatabaseUsername.Text = _settings.DatabaseConfig.User;
                teDatabasePassword.Text = _settings.DatabaseConfig.Password;
                teDatabase.Text = _settings.DatabaseConfig.Database;

                // Influxdb 값들
                teInfluxdbUrl.Text = _settings.Influxdb.Url;
                teInfluxdbToken.Text = _settings.Influxdb.Token;
                teInfluxdbUsername.Text = _settings.Influxdb.Username;
                teInfluxdbPassword.Text = _settings.Influxdb.Password;
                teInfluxdbOrganizations.Text = _settings.Influxdb.Organizations;
                teInfluxdbBuckets.Text = _settings.Influxdb.Buckets;

                // Scada 값들
                beScadaFilePath.Text = _settings.Scada.FilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정을 불러오는 중 오류 발생:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
          
            // 폼 컨트롤의 값을 Settings 객체에 업데이트
            _settings.DatabaseConfig.Host = teDatabaseHost.Text.Trim();
            _settings.DatabaseConfig.Port = teDatabasePort.Text.Trim();
            _settings.DatabaseConfig.User = teDatabaseUsername.Text.Trim();
            _settings.DatabaseConfig.Password = teDatabasePassword.Text;
            _settings.DatabaseConfig.Database = teDatabase.Text.Trim();

            _settings.Influxdb.Url = teInfluxdbUrl.Text.Trim();
            _settings.Influxdb.Username = teInfluxdbUsername.Text.Trim();
            _settings.Influxdb.Password = teInfluxdbPassword.Text;
            _settings.Influxdb.Organizations = teInfluxdbOrganizations.Text.Trim();
            _settings.Influxdb.Buckets = teInfluxdbBuckets.Text.Trim();
            _settings.Influxdb.Token = teInfluxdbToken.Text.Trim();

            _settings.Scada.FilePath = beScadaFilePath.Text.Trim();

            // 연결 테스트 수행
            if (!await TestConnections())
            {
                // 테스트 실패 시 저장 진행 중단
                return;
            }

            // 모든 테스트 성공 시 설정 저장
            ConfigManager.SaveConfig(_settings);
            MessageBox.Show("설정이 저장되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// InfluxDB, MDB, Database 연결 테스트를 수행.
        /// MDB 테스트 시 pNode 테이블의 첫 번째 행의 ProjNodeId 값을 읽어 Scada 설정에 대입합니다.
        /// </summary>
        /// <returns>모든 테스트가 성공하면 true, 실패하면 false</returns>
        private async Task<bool> TestConnections()
        {
            // 1. InfluxDB 연결 테스트
            try
            {
                // 연결 테스트
                bool influxSuccess = TestInfluxDbConnection(_settings.Influxdb);
                await UpdateTestStatusAsync(labelControl1, "InfluxDB", influxSuccess ? "success" : "fail");

                if (!influxSuccess)
                {
                    MessageBox.Show("InfluxDB 연결 테스트에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("InfluxDB 연결 테스트 오류:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 2. MDB 파일 연결 테스트 및 pNode 테이블 조회 (ProjNodeId)
            try
            {
                string projNodeId = TestMdbConnection(_settings.Scada.FilePath);
                // 연결 테스트
                bool mdbSuccess = !string.IsNullOrEmpty(projNodeId);
                await UpdateTestStatusAsync(labelControl1, "MDB 파일", mdbSuccess ? "success" : "fail");
                if (!mdbSuccess)
                {
                    MessageBox.Show("MDB 파일 연결 또는 pNode 테이블 조회에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                // 읽어온 ProjNodeId를 Scada 설정에 반영
                _settings.Scada.ProjNodeId = projNodeId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MDB 연결 테스트 오류:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 3. Database (MariaDB) 연결 테스트
            try
            {
                bool dbSuccess = TestDatabaseConnection(_settings.DatabaseConfig);
                await UpdateTestStatusAsync(labelControl1, "Database", dbSuccess ? "success" : "fail");

                if (!dbSuccess)
                {
                    MessageBox.Show("Database (MariaDB) 연결 테스트에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database 연결 테스트 오류:\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// InfluxDB 연결 테스트: InfluxDB.Client를 사용하여 서버의 Health를 체크
        /// </summary>
        /// <param name="influx">Influxdb 설정 정보</param>
        /// <returns>true: 연결 성공, false: 실패</returns>
        private bool TestInfluxDbConnection(Influxdb influx)
        {
            try
            {
                using (var influxClient = new InfluxDBClient(influx.Url, influx.Token))
                {
                    var queryApi = influxClient.GetQueryApi();
                    // 최근 1분 내의 데이터를 조회하는 간단한 Flux 쿼리입니다.
                    string fluxQuery = $"from(bucket:\"{influx.Buckets}\") |> range(start: -1m)   |> filter(fn: (r) => r._measurement == \"minute\") |> limit(n:1)";

                    // 비동기 쿼리 실행 (동기식으로 대기)
                    var tables = queryApi.QueryAsync(fluxQuery, influx.Organizations)
                                          .GetAwaiter().GetResult();

                    var hasRow = tables.Any(t => t.Records.Any());

                    if (!hasRow)
                    {
                        MessageBox.Show("InfluxDB 연결 테스트에 실패했습니다: 조회 결과가 없습니다.",
                                        "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("InfluxDB 연결 테스트에 실패했습니다:\n" + ex.Message,
                                "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;


        }

        /// <summary>
        /// MDB 파일 연결 테스트 및 pNode 테이블에서 첫 번째 ProjNodeId 값 조회
        /// </summary>
        /// <param name="filePath">MDB 파일 경로</param>
        /// <returns>ProjNodeId 문자열 (조회 실패 시 null 또는 empty)</returns>
        private string TestMdbConnection(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !System.IO.File.Exists(filePath))
                throw new Exception("지정된 MDB 파일이 존재하지 않습니다.");

            // Access 데이터베이스 연결 문자열 (Microsoft.ACE.OLEDB.12.0 프로바이더 사용)
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};";
            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                // pNode 테이블의 첫 번째 ProjNodeId 값을 읽어옵니다.
                string query = "SELECT TOP 1 [ProjNodeIdbw] FROM [pNode]";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))

                {
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        /// <summary>
        /// Database (MariaDB) 연결 테스트: MySqlConnection을 이용하여 연결을 시도합니다.
        /// </summary>
        /// <param name="dbConfig">Database 설정 정보</param>
        /// <returns>true: 연결 성공, false: 실패</returns>
        private bool TestDatabaseConnection(DatabaseConfig dbConfig)
        {
            // 예를 들어, DatabaseConfig에 연결 문자열을 미리 조합하지 않았다면 조합
            string connStr = $"Server={dbConfig.Host};Port={dbConfig.Port};Database={dbConfig.Database};Uid={dbConfig.User};Pwd={dbConfig.Password};";
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                return conn.State == ConnectionState.Open;
            }
        }

        private void beScadaFilePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var openFileDialog = new XtraOpenFileDialog
            {
                Title = "SCADA MDB 파일 선택",
                Filter = "Access DB (*.mdb)|*.mdb|모든 파일 (*.*)|*.*",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                beScadaFilePath.Text = openFileDialog.FileName;
            }
        }
    }
}