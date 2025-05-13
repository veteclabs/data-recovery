namespace Plate.Modules
{
    partial class ucGridControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucGridControl));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcCompare = new DevExpress.XtraGrid.GridControl();
            this.gvCompare = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcInterpolation = new DevExpress.XtraGrid.GridControl();
            this.gvInterpolation = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.editBar = new DevExpress.XtraRichEdit.UI.CommonBar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.iStartDate = new DevExpress.XtraBars.BarEditItem();
            this.deStartDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.iEndDate = new DevExpress.XtraBars.BarEditItem();
            this.deEndDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.iSearch = new DevExpress.XtraBars.BarButtonItem();
            this.iInterpolation = new DevExpress.XtraBars.BarButtonItem();
            this.iSave = new DevExpress.XtraBars.BarButtonItem();
            this.iInterval = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.fileSaveItem1 = new DevExpress.XtraRichEdit.UI.FileSaveItem();
            this.undoItem1 = new DevExpress.XtraRichEdit.UI.UndoItem();
            this.redoItem1 = new DevExpress.XtraRichEdit.UI.RedoItem();
            this.pasteItem1 = new DevExpress.XtraRichEdit.UI.PasteItem();
            this.cutItem1 = new DevExpress.XtraRichEdit.UI.CutItem();
            this.copyItem1 = new DevExpress.XtraRichEdit.UI.CopyItem();
            this.findItem1 = new DevExpress.XtraRichEdit.UI.FindItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.repositoryItemRichEditFontSizeEdit1 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit();
            this.repositoryItemRichEditStyleEdit1 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInterpolation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInterpolation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditStyleEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcCompare);
            this.layoutControl1.Controls.Add(this.gcInterpolation);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 24);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1135, 878);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcCompare
            // 
            this.gcCompare.Location = new System.Drawing.Point(22, 29);
            this.gcCompare.MainView = this.gvCompare;
            this.gcCompare.Name = "gcCompare";
            this.gcCompare.Size = new System.Drawing.Size(547, 837);
            this.gcCompare.TabIndex = 13;
            this.gcCompare.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCompare});
            // 
            // gvCompare
            // 
            this.gvCompare.GridControl = this.gcCompare;
            this.gvCompare.Name = "gvCompare";
            // 
            // gcInterpolation
            // 
            this.gcInterpolation.Location = new System.Drawing.Point(573, 29);
            this.gcInterpolation.MainView = this.gvInterpolation;
            this.gcInterpolation.Name = "gcInterpolation";
            this.gcInterpolation.Size = new System.Drawing.Size(550, 837);
            this.gcInterpolation.TabIndex = 14;
            this.gcInterpolation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInterpolation});
            // 
            // gvInterpolation
            // 
            this.gvInterpolation.GridControl = this.gcInterpolation;
            this.gvInterpolation.Name = "gvInterpolation";
            this.gvInterpolation.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.True;
            this.gvInterpolation.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.gvInterpolation.OptionsClipboard.ShowProgress = DevExpress.Export.ProgressMode.Always;
            this.gvInterpolation.OptionsSelection.MultiSelect = true;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.splitterItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1135, 878);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcCompare;
            this.layoutControlItem1.Location = new System.Drawing.Point(10, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(551, 858);
            this.layoutControlItem1.Text = "Comparison";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(70, 14);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(10, 858);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcInterpolation;
            this.layoutControlItem2.Location = new System.Drawing.Point(561, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(554, 858);
            this.layoutControlItem2.Text = "Interpolation";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(70, 14);
            // 
            // barManager1
            // 
            this.barManager1.AllowGlyphSkinning = true;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.editBar});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.fileSaveItem1,
            this.undoItem1,
            this.redoItem1,
            this.pasteItem1,
            this.cutItem1,
            this.copyItem1,
            this.findItem1,
            this.barEditItem1,
            this.iInterpolation,
            this.iSave,
            this.barButtonItem3,
            this.iStartDate,
            this.barEditItem3,
            this.iEndDate,
            this.barStaticItem1,
            this.barStaticItem2,
            this.iSearch,
            this.iInterval});
            this.barManager1.MaxItemId = 69;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemFontEdit1,
            this.repositoryItemRichEditFontSizeEdit1,
            this.repositoryItemRichEditStyleEdit1,
            this.repositoryItemTextEdit1,
            this.deStartDate,
            this.repositoryItemTextEdit2,
            this.deEndDate,
            this.repositoryItemSpinEdit1});
            // 
            // editBar
            // 
            this.editBar.BarName = "Edit";
            this.editBar.Control = null;
            this.editBar.DockCol = 0;
            this.editBar.DockRow = 0;
            this.editBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.editBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.iStartDate, "", false, true, true, 185),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.iEndDate, "", false, true, true, 164),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSearch, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iInterpolation, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iInterval, true)});
            this.editBar.Text = "Edit";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Start:";
            this.barStaticItem1.Id = 65;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // iStartDate
            // 
            this.iStartDate.Caption = "barEditItem2";
            this.iStartDate.Edit = this.deStartDate;
            this.iStartDate.Id = 62;
            this.iStartDate.Name = "iStartDate";
            // 
            // deStartDate
            // 
            this.deStartDate.AutoHeight = false;
            this.deStartDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartDate.Name = "deStartDate";
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "End:";
            this.barStaticItem2.Id = 66;
            this.barStaticItem2.Name = "barStaticItem2";
            // 
            // iEndDate
            // 
            this.iEndDate.Caption = "barEditItem4";
            this.iEndDate.Edit = this.deEndDate;
            this.iEndDate.Id = 64;
            this.iEndDate.Name = "iEndDate";
            // 
            // deEndDate
            // 
            this.deEndDate.AutoHeight = false;
            this.deEndDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Name = "deEndDate";
            // 
            // iSearch
            // 
            this.iSearch.Caption = "Search";
            this.iSearch.Hint = "Search";
            this.iSearch.Id = 67;
            this.iSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("iSearch.ImageOptions.SvgImage")));
            this.iSearch.Name = "iSearch";
            this.iSearch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.iSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iSearch_ItemClick);
            // 
            // iInterpolation
            // 
            this.iInterpolation.Caption = "Interpolation";
            this.iInterpolation.Id = 59;
            this.iInterpolation.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("iInterpolation.ImageOptions.SvgImage")));
            this.iInterpolation.Name = "iInterpolation";
            this.iInterpolation.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.iInterpolation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iInterpolation_ItemClick);
            // 
            // iSave
            // 
            this.iSave.Caption = "Save";
            this.iSave.Id = 60;
            this.iSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("iRecovery.ImageOptions.SvgImage")));
            this.iSave.Name = "iSave";
            this.iSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.iSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iSave_ItemClick);
            // 
            // iInterval
            // 
            this.iInterval.Caption = "Interval (min.)";
            this.iInterval.Edit = this.repositoryItemSpinEdit1;
            this.iInterval.EditValue = ((short)(15));
            this.iInterval.Id = 68;
            this.iInterval.Name = "iInterval";
            this.iInterval.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlTop.Size = new System.Drawing.Size(1135, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 902);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlBottom.Size = new System.Drawing.Size(1135, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 878);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1135, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(2);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 878);
            // 
            // fileSaveItem1
            // 
            this.fileSaveItem1.Id = 2;
            this.fileSaveItem1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.fileSaveItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("fileSaveItem1.ImageOptions.SvgImage")));
            this.fileSaveItem1.Name = "fileSaveItem1";
            // 
            // undoItem1
            // 
            this.undoItem1.Id = 7;
            this.undoItem1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.undoItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("undoItem1.ImageOptions.SvgImage")));
            this.undoItem1.Name = "undoItem1";
            // 
            // redoItem1
            // 
            this.redoItem1.Id = 8;
            this.redoItem1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.redoItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("redoItem1.ImageOptions.SvgImage")));
            this.redoItem1.Name = "redoItem1";
            // 
            // pasteItem1
            // 
            this.pasteItem1.Id = 9;
            this.pasteItem1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.pasteItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("pasteItem1.ImageOptions.SvgImage")));
            this.pasteItem1.Name = "pasteItem1";
            // 
            // cutItem1
            // 
            this.cutItem1.Id = 10;
            this.cutItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cutItem1.ImageOptions.SvgImage")));
            this.cutItem1.Name = "cutItem1";
            // 
            // copyItem1
            // 
            this.copyItem1.Id = 11;
            this.copyItem1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.copyItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("copyItem1.ImageOptions.SvgImage")));
            this.copyItem1.Name = "copyItem1";
            // 
            // findItem1
            // 
            this.findItem1.Id = 56;
            this.findItem1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.findItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("findItem1.ImageOptions.SvgImage")));
            this.findItem1.Name = "findItem1";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "barEditItem1";
            this.barEditItem1.Edit = this.repositoryItemTextEdit1;
            this.barEditItem1.Id = 58;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 61;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "barEditItem3";
            this.barEditItem3.Edit = this.repositoryItemTextEdit2;
            this.barEditItem3.Id = 63;
            this.barEditItem3.Name = "barEditItem3";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // repositoryItemRichEditFontSizeEdit1
            // 
            this.repositoryItemRichEditFontSizeEdit1.AutoHeight = false;
            this.repositoryItemRichEditFontSizeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditFontSizeEdit1.Control = null;
            this.repositoryItemRichEditFontSizeEdit1.Name = "repositoryItemRichEditFontSizeEdit1";
            // 
            // repositoryItemRichEditStyleEdit1
            // 
            this.repositoryItemRichEditStyleEdit1.AutoHeight = false;
            this.repositoryItemRichEditStyleEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditStyleEdit1.Control = null;
            this.repositoryItemRichEditStyleEdit1.Name = "repositoryItemRichEditStyleEdit1";
            // 
            // ucGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucGridControl";
            this.Size = new System.Drawing.Size(1135, 902);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInterpolation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInterpolation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditStyleEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraRichEdit.UI.CommonBar editBar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraRichEdit.UI.FileSaveItem fileSaveItem1;
        private DevExpress.XtraRichEdit.UI.UndoItem undoItem1;
        private DevExpress.XtraRichEdit.UI.RedoItem redoItem1;
        private DevExpress.XtraRichEdit.UI.PasteItem pasteItem1;
        private DevExpress.XtraRichEdit.UI.CutItem cutItem1;
        private DevExpress.XtraRichEdit.UI.CopyItem copyItem1;
        private DevExpress.XtraRichEdit.UI.FindItem findItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit repositoryItemRichEditFontSizeEdit1;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit repositoryItemRichEditStyleEdit1;
        private DevExpress.XtraBars.BarButtonItem iInterpolation;
        private DevExpress.XtraBars.BarButtonItem iSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarEditItem iStartDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit deStartDate;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarEditItem iEndDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit deEndDate;
        private DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.BarButtonItem iSearch;
        private DevExpress.XtraGrid.GridControl gcCompare;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCompare;
        private DevExpress.XtraGrid.GridControl gcInterpolation;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInterpolation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraBars.BarEditItem iInterval;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
    }
}
