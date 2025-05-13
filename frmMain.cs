using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Plate.Configurations;
using Plate.Domain.Models;
using Plate.Infrastructure.Scada;
using Plate.Modules;
using Plate.UI.Component;
using Plate.UI.Forms;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraSplashScreen;
using Serilog.Events;
using Serilog;
using System.Threading.Tasks;

namespace Plate
{
    public partial class frmMain : DevExpress.XtraBars.ToolbarForm.ToolbarForm {
        private AccessTagRepository _repo;
        private SettingsConfig _settings;
        public frmMain() {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.File("./logs/general/log.txt", rollingInterval: RollingInterval.Day) // 일반 로그
.WriteTo.File("./logs/errors/error.txt", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day) // 에러 로그 전용 파일
.CreateLogger();
            this.Text = "Plate Support Application";
            barStaticItem1.Caption = this.Text;
            Icon = DevExpress.Utils.ResourceImageHelperCore.CreateIconFromResourcesEx("Plate.Resources.AppIcon.ico", typeof(frmMain).Assembly);
            ApplyHeaderStaticItemAppearance();
        }
    
        internal SettingsConfig CurrentSettings => _settings;
        protected override void OnLookAndFeelChangedCore() {
            base.OnLookAndFeelChangedCore();
            ApplyHeaderStaticItemAppearance();
        }
        void ApplyHeaderStaticItemAppearance() {
            this.barStaticItem1.ItemAppearance.Normal.BackColor = BarSkins.GetSkin(LookAndFeel)[BarSkins.SkinBar].Color.BackColor;
            this.barStaticItem1.ItemAppearance.Normal.Options.UseBackColor = true;
        }
        internal void LoadConfig()
        {
            _settings = ConfigManager.LoadConfig();

            // UI 먼저 띄움
            if (File.Exists(_settings.Scada.FilePath))
            {
                _repo = new AccessTagRepository(_settings.Scada);
                webAccessExplorer.Repository = _repo;

                // TreeView 로딩은 백그라운드에서 처리
                _ = Task.Run(() => webAccessExplorer.PopulateTreeSafeAsync());
            }
            else
            {
                iSettings_ItemClick(null, null);
            }
        }
        void frmMain_Load(object sender, System.EventArgs e) {
            LoadConfig();

            BeginInvoke(new MethodInvoker(InitApp));
        }
        void barManager1_Merge(object sender, BarManagerMergeEventArgs e) {
            barManager.Bars["Edit"].Merge(e.ChildManager.Bars["Edit"]);
        }
        void barManager1_UnMerge(object sender, BarManagerMergeEventArgs e) {
            barManager.Bars["Edit"].UnMerge();
        }
        void InitApp() {
            SplashScreenManager.CloseForm();

        }
   
        internal List<string> CheckedTags;
        internal void AppendLog(string log)
        {
            ucOutput1.AppendLog(log);
        }
        void AddNewDocument(string fileName, Stream content) {
            tabbedView.BeginUpdate();
            ucCodeEditor control = new ucCodeEditor();
            control.Name = fileName;
            control.Text = fileName;
            BaseDocument document = tabbedView.AddDocument(control);
            document.Footer = Directory.GetCurrentDirectory();
            control.LoadCode(content);
            tabbedView.EndUpdate();
            tabbedView.Controller.Activate(document);
        }
        void repositoryItemComboBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter && eFind.EditValue != null)
                repositoryItemComboBox1.Items.Add(eFind.EditValue.ToString());
        }
        int projectIndex = 0;
        void iNewItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //AddNewDocument(string.Format("File{0}.cs", ++projectIndex));
        }
        void iAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //BarManager.About();
        }
        void iSolutionExplorer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel1.Show();
        }
        void iProperties_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //dockPanel2.Show();
        }
        void iTaskList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel3.Show();
        }
        void iFindResults_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel4.Show();
        }
        void iOutput_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel5.Show();
        }
        void iToolbox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            dockPanel6.Show();
        }
        void solutionExplorer_PropertiesItemClick(object sender, System.EventArgs e) {
            //dockPanel2.Show();
        }
        internal void SetProgressbar(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => iProgressbar.EditValue = value));
            }
            else
            {
                iProgressbar.EditValue = value;
            }
        }
        void solutionExplorer_TreeViewItemClick(object sender, System.EventArgs e) {
            DevExpress.XtraTreeList.TreeList treeView = sender as DevExpress.XtraTreeList.TreeList;
            string focusedNodeText = treeView.FocusedNode.GetDisplayText(0);
            if(!focusedNodeText.Contains(".cs")) return;
            string fileName = focusedNodeText.Replace(".cs", string.Empty);
            foreach(BaseDocument document in tabbedView.Documents) {
                if(document.Caption == (fileName + ".cs")) {
                    tabbedView.Controller.Activate(document);
                    return;
                }
            }
        }
        void iSaveLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(SaveFileDialog dlg = new SaveFileDialog()) {
                dlg.Filter = "XML files (*.xml)|*.xml";
                dlg.Title = "Save Layout";
                if(dlg.ShowDialog() == DialogResult.OK) {
                    Refresh(true);
                    barManager.SaveToXml(dlg.FileName);
                    Refresh(false);
                }
            }
        }
        void iLoadLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(OpenFileDialog dlg = new OpenFileDialog()) {
                dlg.Filter = "XML files (*.xml)|*.xml|All files|*.*";
                dlg.Title = "Restore Layout";
                if(dlg.ShowDialog() == DialogResult.OK) {
                    Refresh(true);
                    try {
                        barManager.RestoreFromXml(dlg.FileName);
                    }
                    catch { }
                    Refresh(false);
                }
            }
        }
        Cursor currentCursor;
        void Refresh(bool isWait) {
            if(isWait) {
                currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
            }
            else Cursor.Current = currentCursor;
            this.Refresh();
        }
        void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.Close();
        }

        private void iStart_ItemClick(object sender, ItemClickEventArgs e)
        {
            tabbedView.BeginUpdate();
            ucGridControl control = new ucGridControl();
            
            BaseDocument document = tabbedView.AddDocument(control);
            control.Name = iStart.Caption;
            control.Text = iStart.Caption;
            document.Footer = Directory.GetCurrentDirectory();
            tabbedView.EndUpdate();
            tabbedView.Controller.Activate(document);
        }

        private void iOpenSolution_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void iSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (frmSettings frm = new frmSettings())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    LoadConfig();
                };
            }
        }

        private void webAccessExplorer_RefereshItemClick(object sender, System.EventArgs e)
        {
            LoadConfig();
        }
    }
}
