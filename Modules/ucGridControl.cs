using Plate.App.Services;
using Plate.Domain.Models;
using Plate.Infrastructure.Database;
using Plate.Infrastructure.InfluxDb;
using Plate.UI.Component;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using InfluxData.Net.InfluxDb.Models.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plate.Modules
{
    public partial class ucGridControl : BaseModule
    {
        private SettingsConfig _settings => OwnerForm.CurrentSettings;
        private DateTime StartDate => Convert.ToDateTime(iStartDate.EditValue);

        private DateTime EndDate => Convert.ToDateTime(iEndDate.EditValue);
        private int ProjNodeId => int.Parse(_settings.Scada.ProjNodeId);
        public ucGridControl()
        {
            InitializeComponent();
            // 오늘 날짜 기준
            DateTime today = DateTime.Today;
            // 시작일: 오늘
            iStartDate.EditValue = today;
            // 종료일: 일주일 뒤
            iEndDate.EditValue = today.AddDays(7);
        }
        internal override void ShowModule(bool firstShow)
        {
            base.ShowModule(firstShow);

            
        }
        IOverlaySplashScreenHandle progressPanelHandle = null;

        void ShowProgress()
        {
            progressPanelHandle = ShowProgressPanel();
        }
        void CloseProgress()
        {
            if (progressPanelHandle != null)
                CloseProgressPanel(progressPanelHandle);

        }
        private async void iSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            iSearch.Enabled = false;
            iSearch.Caption = "📊 Loading data..";

            ShowProgress();
            OwnerForm.SetProgressbar(10);
            string result = string.Empty;
            try
            {
                if(_checkedTags is null || _checkedTags.Count == 0)
                {
                    XtraMessageBox.Show($"Please select a tag.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    OwnerForm.SetProgressbar(0);

                    return;
                }
                OwnerForm.SetProgressbar(20);

                var influxTask = LoadInfluxTagDataAsync();
                var mariaTask = LoadMariaTagDataAsync();

                OwnerForm.SetProgressbar(40);

                await Task.WhenAll(influxTask, mariaTask);

                OwnerForm.SetProgressbar(70);

                var influxData = influxTask.Result;
                var mariaData = mariaTask.Result;

                iSearch.Caption = "⏳ Fetching results...";

                LoadCompareData(influxData, mariaData);

                OwnerForm.SetProgressbar(100);
                result = "Successfully Load.";

            }
            catch (Exception ex)
            {
                result = $"An error occurred while retrieving data:\n{ex.ToString()}";
                XtraMessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                OwnerForm.AppendLog(result);
                iSearch.Enabled = true;
                iSearch.Caption = "Search";
                CloseProgress();
            }
        }
        private void LoadCompareData(List<InfluxDataPoint> influxData, List<MariaDataPoint> mariaData)
        {
            _compared = CompareTagData(influxData, mariaData);
            BindDataToGrid(gcCompare, gvCompare, _compared);
            gvCompare.RowStyle -= gvCompare_RowStyle;
            gvCompare.RowStyle += gvCompare_RowStyle;

            int firstDiffRowHandle = _compared.FindIndex(r => r.Diff.HasValue && Math.Abs(r.Diff.Value) > 0.0001);
            if (firstDiffRowHandle >= 0)
            {
                gvCompare.FocusedRowHandle = firstDiffRowHandle;
                gvCompare.TopRowIndex = firstDiffRowHandle;
            }
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

        private void BindDataToGrid<T>(DevExpress.XtraGrid.GridControl gridControl, DevExpress.XtraGrid.Views.Grid.GridView gridView, List<T> data)
        {
            gridControl.DataSource = data;

            if (gridView.Columns["TimeStamp"] != null)
            {
                gridView.Columns["TimeStamp"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView.Columns["TimeStamp"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
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
        private Task<List<InfluxDataPoint>> LoadInfluxTagDataAsync()
        {
            return Task.Run(() =>
            {
                var influx = _settings.Influxdb;
                var repo = new InfluxTagRepository(influx.Url, influx.Token, influx.Organizations, influx.Buckets);
                int interval = Convert.ToInt16(iInterval.EditValue);
                var dataPoints = repo.GetTagData(_checkedTags, StartDate, EndDate,interval);
                return dataPoints;
            });
        }

        List<string> _checkedTags => OwnerForm.CheckedTags;
        private Task<List<MariaDataPoint>> LoadMariaTagDataAsync()
        {
            return Task.Run(() =>
            {
                var repo = new MariaTagRepository(_settings.DatabaseConfig.ConnectionString);
                var dataPoints = repo.GetTagData(_checkedTags, StartDate, EndDate, ProjNodeId);

                //BindDataToGridSafe(gcDatabase, gvDatabase, dataPoints);
                // 각 TimeStamp에 15분씩 추가
                foreach (var dp in dataPoints)
                {
                    dp.TimeStamp = dp.TimeStamp.AddMinutes(15);
                }

                return dataPoints;
            });
        }

        private void iInterpolation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            iInterpolation.Enabled = false;
            iInterpolation.Caption = "⏳ Fetching results...";
            string result = string.Empty;

            try
            {
                var data = _compared; // Influx + Maria 머지된 리스트
                var interpolated = new InterpolationService().Interpolate(data);
                gcInterpolation.DataSource = interpolated;

                gvInterpolation.BestFitColumns();
                gvInterpolation.Columns["TimeStamp"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gvInterpolation.Columns["TimeStamp"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
                result = "Succefully interpolation.";
            }
            catch (Exception ex)
            {
                result = $"Error occurred during data interpolation:\r\n{ex.Message}";
                XtraMessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                OwnerForm.AppendLog(result);

                iInterpolation.Enabled = true;
                iInterpolation.Caption = "Interpolation";
            }
        }

        private async void iSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            iSave.Enabled = false;
            iSave.Caption = "⏳ Saving...";
            ShowProgress();
            string result = string.Empty;
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

                result = "Interpolated data has been written to InfluxDB.";
                
            }
            catch (Exception ex)
            {
                result = $"InfluxDB write failed:\n{ex.Message}";
                XtraMessageBox.Show(
                  result,
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                OwnerForm.AppendLog(result);
                iSave.Enabled = true;
                iSave.Caption = "Recovery";
                CloseProgress();
            }
        }
    }
}
