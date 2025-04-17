namespace DataRecorvery
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcCompare = new DevExpress.XtraGrid.GridControl();
            this.gvCompare = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcTags = new DevExpress.XtraGrid.GridControl();
            this.gvTags = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDatabase = new DevExpress.XtraGrid.GridControl();
            this.gvDatabase = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcInfluxdb = new DevExpress.XtraGrid.GridControl();
            this.gvInfluxdb = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.deStartDate = new DevExpress.XtraEditors.DateEdit();
            this.deEndDate = new DevExpress.XtraEditors.DateEdit();
            this.btnRecovery = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnInterpolation = new DevExpress.XtraEditors.SimpleButton();
            this.btnSettingReload = new DevExpress.XtraEditors.SimpleButton();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gcInterpolation = new DevExpress.XtraGrid.GridControl();
            this.gvInterpolation = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInfluxdb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInfluxdb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInterpolation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInterpolation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(979, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(572, 22);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "설정";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcCompare);
            this.layoutControl1.Controls.Add(this.gcTags);
            this.layoutControl1.Controls.Add(this.gcDatabase);
            this.layoutControl1.Controls.Add(this.gcInfluxdb);
            this.layoutControl1.Controls.Add(this.deStartDate);
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.deEndDate);
            this.layoutControl1.Controls.Add(this.btnRecovery);
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.btnInterpolation);
            this.layoutControl1.Controls.Add(this.btnSettingReload);
            this.layoutControl1.Controls.Add(this.checkEdit1);
            this.layoutControl1.Controls.Add(this.checkEdit2);
            this.layoutControl1.Controls.Add(this.gcInterpolation);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1563, 715);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcCompare
            // 
            this.gcCompare.Location = new System.Drawing.Point(978, 153);
            this.gcCompare.MainView = this.gvCompare;
            this.gcCompare.Name = "gcCompare";
            this.gcCompare.Size = new System.Drawing.Size(305, 540);
            this.gcCompare.TabIndex = 12;
            this.gcCompare.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCompare});
            // 
            // gvCompare
            // 
            this.gvCompare.GridControl = this.gcCompare;
            this.gvCompare.Name = "gvCompare";
            // 
            // gcTags
            // 
            this.gcTags.Location = new System.Drawing.Point(12, 29);
            this.gcTags.MainView = this.gvTags;
            this.gcTags.Name = "gcTags";
            this.gcTags.Size = new System.Drawing.Size(346, 674);
            this.gcTags.TabIndex = 10;
            this.gcTags.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTags});
            // 
            // gvTags
            // 
            this.gvTags.GridControl = this.gcTags;
            this.gvTags.Name = "gvTags";
            this.gvTags.OptionsSelection.MultiSelect = true;
            this.gvTags.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvTags.OptionsView.ShowAutoFilterRow = true;
            this.gvTags.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gvTags.OptionsView.ShowFooter = true;
            this.gvTags.OptionsView.ShowGroupPanel = false;
            this.gvTags.OptionsView.ShowIndicator = false;
            // 
            // gcDatabase
            // 
            this.gcDatabase.Location = new System.Drawing.Point(670, 153);
            this.gcDatabase.MainView = this.gvDatabase;
            this.gcDatabase.Name = "gcDatabase";
            this.gcDatabase.Size = new System.Drawing.Size(304, 540);
            this.gcDatabase.TabIndex = 9;
            this.gcDatabase.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDatabase});
            // 
            // gvDatabase
            // 
            this.gvDatabase.GridControl = this.gcDatabase;
            this.gvDatabase.Name = "gvDatabase";
            // 
            // gcInfluxdb
            // 
            this.gcInfluxdb.Location = new System.Drawing.Point(362, 153);
            this.gcInfluxdb.MainView = this.gvInfluxdb;
            this.gcInfluxdb.Name = "gcInfluxdb";
            this.gcInfluxdb.Size = new System.Drawing.Size(304, 540);
            this.gcInfluxdb.TabIndex = 5;
            this.gcInfluxdb.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInfluxdb});
            // 
            // gvInfluxdb
            // 
            this.gvInfluxdb.GridControl = this.gcInfluxdb;
            this.gvInfluxdb.Name = "gvInfluxdb";
            // 
            // deStartDate
            // 
            this.deStartDate.EditValue = null;
            this.deStartDate.Location = new System.Drawing.Point(444, 38);
            this.deStartDate.Name = "deStartDate";
            this.deStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Properties.MaskSettings.Set("mask", "yyyy-MM-dd HH:mm");
            this.deStartDate.Properties.UseMaskAsDisplayFormat = true;
            this.deStartDate.Size = new System.Drawing.Size(531, 20);
            this.deStartDate.StyleController = this.layoutControl1;
            this.deStartDate.TabIndex = 4;
            // 
            // deEndDate
            // 
            this.deEndDate.EditValue = null;
            this.deEndDate.Location = new System.Drawing.Point(1061, 38);
            this.deEndDate.Name = "deEndDate";
            this.deEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.MaskSettings.Set("mask", "yyyy-MM-dd HH:mm");
            this.deEndDate.Properties.UseMaskAsDisplayFormat = true;
            this.deEndDate.Size = new System.Drawing.Size(490, 20);
            this.deEndDate.StyleController = this.layoutControl1;
            this.deEndDate.TabIndex = 4;
            // 
            // btnRecovery
            // 
            this.btnRecovery.Location = new System.Drawing.Point(1185, 62);
            this.btnRecovery.Name = "btnRecovery";
            this.btnRecovery.Size = new System.Drawing.Size(366, 22);
            this.btnRecovery.StyleController = this.layoutControl1;
            this.btnRecovery.TabIndex = 6;
            this.btnRecovery.Text = "복구";
            this.btnRecovery.Click += new System.EventHandler(this.btnRecovery_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(362, 62);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(408, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnInterpolation
            // 
            this.btnInterpolation.Location = new System.Drawing.Point(774, 62);
            this.btnInterpolation.Name = "btnInterpolation";
            this.btnInterpolation.Size = new System.Drawing.Size(407, 22);
            this.btnInterpolation.StyleController = this.layoutControl1;
            this.btnInterpolation.TabIndex = 8;
            this.btnInterpolation.Text = "데이터 보간";
            this.btnInterpolation.Click += new System.EventHandler(this.btnInterpolation_Click);
            // 
            // btnSettingReload
            // 
            this.btnSettingReload.Location = new System.Drawing.Point(362, 12);
            this.btnSettingReload.Name = "btnSettingReload";
            this.btnSettingReload.Size = new System.Drawing.Size(613, 22);
            this.btnSettingReload.StyleController = this.layoutControl1;
            this.btnSettingReload.TabIndex = 11;
            this.btnSettingReload.Text = "설정 동기화";
            this.btnSettingReload.Click += new System.EventHandler(this.btnSettingReload_Click);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(362, 88);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "checkEdit1";
            this.checkEdit1.Size = new System.Drawing.Size(1189, 20);
            this.checkEdit1.StyleController = this.layoutControl1;
            this.checkEdit1.TabIndex = 13;
            // 
            // checkEdit2
            // 
            this.checkEdit2.Location = new System.Drawing.Point(362, 112);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "checkEdit2";
            this.checkEdit2.Size = new System.Drawing.Size(1189, 20);
            this.checkEdit2.StyleController = this.layoutControl1;
            this.checkEdit2.TabIndex = 14;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem5,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem7,
            this.layoutControlItem14,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1563, 715);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButton1;
            this.layoutControlItem1.Location = new System.Drawing.Point(967, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(576, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.deStartDate;
            this.layoutControlItem2.Location = new System.Drawing.Point(350, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(617, 24);
            this.layoutControlItem2.Text = "시작일:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(70, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.deEndDate;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem3.Location = new System.Drawing.Point(967, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(576, 24);
            this.layoutControlItem3.Text = "종료일:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(70, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcInfluxdb;
            this.layoutControlItem4.Location = new System.Drawing.Point(350, 124);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(308, 561);
            this.layoutControlItem4.Text = "Influxdb";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(70, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSearch;
            this.layoutControlItem6.Location = new System.Drawing.Point(350, 50);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(412, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnInterpolation;
            this.layoutControlItem7.Location = new System.Drawing.Point(762, 50);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(411, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnRecovery;
            this.layoutControlItem5.Location = new System.Drawing.Point(1173, 50);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(370, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.gcDatabase;
            this.layoutControlItem8.Location = new System.Drawing.Point(658, 124);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(308, 561);
            this.layoutControlItem8.Text = "Mariadb";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(70, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(350, 685);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1193, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.gcTags;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(350, 695);
            this.layoutControlItem9.Text = "Tags";
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(70, 14);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnSettingReload;
            this.layoutControlItem10.Location = new System.Drawing.Point(350, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(617, 26);
            this.layoutControlItem10.Text = "Setting Reload";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcCompare;
            this.layoutControlItem11.Location = new System.Drawing.Point(966, 124);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(309, 561);
            this.layoutControlItem11.Text = "Comparison";
            this.layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(70, 14);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.checkEdit1;
            this.layoutControlItem12.Location = new System.Drawing.Point(350, 76);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(1193, 24);
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.checkEdit2;
            this.layoutControlItem13.Location = new System.Drawing.Point(350, 100);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(1193, 24);
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextVisible = false;
            // 
            // gcInterpolation
            // 
            this.gcInterpolation.Location = new System.Drawing.Point(1287, 153);
            this.gcInterpolation.MainView = this.gvInterpolation;
            this.gcInterpolation.Name = "gcInterpolation";
            this.gcInterpolation.Size = new System.Drawing.Size(264, 540);
            this.gcInterpolation.TabIndex = 12;
            this.gcInterpolation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInterpolation});
            // 
            // gvInterpolation
            // 
            this.gvInterpolation.GridControl = this.gcInterpolation;
            this.gvInterpolation.Name = "gvInterpolation";
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.gcInterpolation;
            this.layoutControlItem14.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem14.CustomizationFormText = "Comparison";
            this.layoutControlItem14.Location = new System.Drawing.Point(1275, 124);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(268, 561);
            this.layoutControlItem14.Text = "Interpolation";
            this.layoutControlItem14.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(70, 14);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1563, 715);
            this.Controls.Add(this.layoutControl1);
            this.Name = "MainForm";
            this.Text = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInfluxdb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInfluxdb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInterpolation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInterpolation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit deStartDate;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit deEndDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl gcTags;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTags;
        private DevExpress.XtraGrid.GridControl gcDatabase;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDatabase;
        private DevExpress.XtraGrid.GridControl gcInfluxdb;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInfluxdb;
        private DevExpress.XtraEditors.SimpleButton btnRecovery;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnInterpolation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.SimpleButton btnSettingReload;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraGrid.GridControl gcCompare;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCompare;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraGrid.GridControl gcInterpolation;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInterpolation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
    }
}