using DataRecorvery.Configurations;
using DataRecorvery.Domain.Interfaces;
using DataRecorvery.Domain.Models;
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

            _settings = ConfigManager.LoadConfig();
            LoadTagsToGrid();
            gvTags.OptionsSelection.MultiSelect = true;
            gvTags.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvTags.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            gvTags.OptionsBehavior.Editable = false; // 체크박스만 활성화, 편집은 비활성화
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
    }
}