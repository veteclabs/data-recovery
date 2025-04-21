using DataRecorvery.App.Services;
using DataRecorvery.Configurations;
using DataRecorvery.Domain.Interfaces;
using DataRecorvery.Domain.Models;
using DataRecorvery.Infrastructure.Database;
using DataRecorvery.Infrastructure.InfluxDb;
using DataRecorvery.Infrastructure.Scada;
using DataRecorvery.UI.Forms;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataRecorvery
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {

        SettingsConfig _settings;

        private DateTime StartDate => deStartDate.DateTime;
        private DateTime EndDate => deEndDate.DateTime;
        private int ProjNodeId => int.Parse(_settings.Scada.ProjNodeId);
        public MainForm()
        {
            InitializeComponent();
            Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 오늘 날짜 기준
            DateTime today = DateTime.Today;

            // 시작일: 오늘
            deStartDate.EditValue = today;

            // 종료일: 일주일 뒤
            deEndDate.EditValue = today.AddDays(7);

            btnSettingReload_Click(null, null);
            gvTags.OptionsSelection.MultiSelect = true;
            gvTags.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvTags.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gvTags.OptionsBehavior.Editable = false; // 체크박스만 활성화, 편집은 비활성화

            btnRecovery.Enabled = false;

        }
        private void LoadTagsToGrid()
        {
            try
            {
                ITagRepository tagRepository = new AccessTagRepository(_settings.Scada);
                var tags = tagRepository.GetTags();
                gcTags.DataSource = tags;
                gvTags.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"태그 로딩 중 오류가 발생했습니다.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (frmSettings frm = new frmSettings())
            {
                frm.ShowDialog();
            }
        }

        private void btnSettingReload_Click(object sender, EventArgs e)
        {
            _settings = ConfigManager.LoadConfig();
            LoadTagsToGrid();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnSearch.Text = "조회 중...";
            try
            {
                var influxTask = LoadInfluxTagDataAsync();
                var mariaTask = LoadMariaTagDataAsync();

                await Task.WhenAll(influxTask, mariaTask);

                var influxData = influxTask.Result;
                var mariaData = mariaTask.Result;

                LoadCompareData(influxData, mariaData);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"데이터 조회 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
                btnSearch.Text = "조회";
            }
        }
        private Task<List<InfluxDataPoint>> LoadInfluxTagDataAsync()
        {
            return Task.Run(() =>
            {
                var influx = _settings.Influxdb;
                var repo = new InfluxTagRepository(influx.Url, influx.Token, influx.Organizations, influx.Buckets);
                var dataPoints = repo.GetTagData(GetSelectedTagNames(), StartDate, EndDate);

                BindDataToGridSafe(gcInfluxdb, gvInfluxdb, dataPoints);


                return dataPoints;
            });
        }


        private Task<List<MariaDataPoint>> LoadMariaTagDataAsync()
        {
            return Task.Run(() =>
            {
                var repo = new MariaTagRepository(_settings.DatabaseConfig.ConnectionString);
                var dataPoints = repo.GetTagData(GetSelectedTagNames(), StartDate, EndDate, ProjNodeId);

                BindDataToGridSafe(gcDatabase, gvDatabase, dataPoints);


                return dataPoints;
            });
        }
        private List<string> GetSelectedTagNames()
        {
            if (gcTags.InvokeRequired)
            {
                return (List<string>)gcTags.Invoke(new Func<List<string>>(() =>
                    GetCheckedTags().Select(t => t.TagName).ToList()
                ));
            }
            else
            {
                return GetCheckedTags().Select(t => t.TagName).ToList();
            }
        }

        private List<Tags> GetCheckedTags()
        {
            var selectedRows = gvTags.GetSelectedRows();
            var result = new List<Tags>();

            foreach (var rowHandle in selectedRows)
            {
                var tag = gvTags.GetRow(rowHandle) as Tags;
                if (tag != null)
                    result.Add(tag);
            }

            return result;
        }
        private void BindDataToGridSafe<T>(
    DevExpress.XtraGrid.GridControl gridControl,
    DevExpress.XtraGrid.Views.Grid.GridView gridView,
    List<T> data)
        {
            if (gridControl.InvokeRequired)
            {
                gridControl.Invoke(new Action(() =>
                {
                    BindDataToGrid(gridControl, gridView, data);
                }));
            }
            else
            {
                BindDataToGrid(gridControl, gridView, data);
            }
        }

     
        private void BindDataToGrid<T>(DevExpress.XtraGrid.GridControl gridControl, DevExpress.XtraGrid.Views.Grid.GridView gridView, List<T> data)
        {
            gridControl.DataSource = data;

            if (gridView.Columns["TimeStamp"] != null)
            {
                gridView.Columns["TimeStamp"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView.Columns["TimeStamp"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            }
        }
        private void LoadInterpolatedData()
        {
            btnRecovery.Enabled = false;

            try
            {
                var data = _compared; // Influx + Maria 머지된 리스트
                var interpolated = new InterpolationService().Interpolate(data);
                gcInterpolation.DataSource = interpolated;

                gvInterpolation.BestFitColumns();
                gvInterpolation.Columns["TimeStamp"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gvInterpolation.Columns["TimeStamp"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
                btnRecovery.Enabled = true;

            }
            catch (Exception e)
            {
                XtraMessageBox.Show($"데이터 보간 중 오류 발생: {e.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }
        private async void btnSaveInterpolated_Click(object sender, EventArgs e)
        {
            btnRecovery.Enabled = false;
            btnRecovery.Text = "복구 중...";
            try
            {
                // 1) 보간된 데이터
                var interpolated = gvInterpolation.DataSource as List<TagComparisonPoint>;
                if (interpolated == null || !interpolated.Any())
                    return;

                // 2) Influx에 쓰기
                var influx = _settings.Influxdb;
                using (var repo = new InfluxTagRepository(
                   influx.Url, influx.Token, influx.Organizations, influx.Buckets))
                {
                    // 동기 쓰기를 백그라운드 스레드에서 실행
                    await Task.Run(() => repo.WriteInterpolatedData(interpolated));
                }

                XtraMessageBox.Show(
                  "Interpolated data has been written to InfluxDB.",
                  "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                  $"InfluxDB write failed:\n{ex.Message}",
                  "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRecovery.Enabled = true;
                btnRecovery.Text = "복구";
            }
        }


        private void LoadCompareData(List<InfluxDataPoint> influxData, List<MariaDataPoint> mariaData)
        {
            _compared = CompareTagData(influxData, mariaData);
            BindDataToGrid(gcCompare,gvCompare,_compared);
            gvCompare.RowStyle -= gvCompare_RowStyle;
            gvCompare.RowStyle += gvCompare_RowStyle;

            int firstDiffRowHandle = _compared.FindIndex(r => r.Diff.HasValue && Math.Abs(r.Diff.Value) > 0.0001);
            if (firstDiffRowHandle >= 0)
            {
                gvCompare.FocusedRowHandle = firstDiffRowHandle;
                gvCompare.TopRowIndex = firstDiffRowHandle;
            }
        }
        List<TagComparisonPoint> _compared;

        public List<TagComparisonPoint> CompareTagData(
    List<InfluxDataPoint> influxData,
    List<MariaDataPoint> mariaData)
        {
            var merged = (from influx in influxData
                          join maria in mariaData
                          on new { influx.TimeStamp, influx.TagName }
                          equals new { maria.TimeStamp, maria.TagName }
                          into mj
                          from maria in mj.DefaultIfEmpty()
                          select new TagComparisonPoint
                          {
                              TimeStamp = influx.TimeStamp,
                              TagName = influx.TagName,
                              Value_Influx = influx.Value,
                              Value_Maria = maria?.Value,
                              Diff = maria != null ? influx.Value - maria.Value : (double?)null
                          }).ToList();

            return merged;
        }
        private void gvCompare_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            var diffValue = view.GetRowCellValue(e.RowHandle, "Diff");

            if (diffValue != null && diffValue != DBNull.Value)
            {
                if (double.TryParse(diffValue.ToString(), out double diff) && Math.Abs(diff) > 0.0001)
                {
                    e.Appearance.BackColor = Color.MistyRose;  // 또는 Color.LightCoral 등
                    e.HighPriority = true;
                }
            }
        }

        private  void btnRecovery_Click(object sender, EventArgs e)
        {
            btnSaveInterpolated_Click(sender, e);
        }

        private void btnInterpolation_Click(object sender, EventArgs e)
        {
            LoadInterpolatedData();
        }

        private void gvCompare_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ManualValue")
            {
                var row = gvInterpolation.GetRow(e.RowHandle) as TagComparisonPoint;
                if (row != null)
                {
                    row.InterpolationStatus = "Manual Fix";
                }
            }
        }
    }
}