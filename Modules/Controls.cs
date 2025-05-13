using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.Modules
{
    public class BaseControl : XtraUserControl
    {
        public BaseControl()
        {
            this.VisibleChanged += new EventHandler(BaseControl_VisibleChanged);
        }
        void BaseControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ShowControlFirstTime();
                this.VisibleChanged -= new EventHandler(BaseControl_VisibleChanged);
            }
        }
        internal virtual void ShowControlFirstTime() { }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //if (!DesignTimeTools.IsDesignMode)
            //    LookAndFeelStyleChanged();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
    public class BaseModule : BaseControl
    {
        public BaseModule() { }
        private frmMain instance;
        public IOverlaySplashScreenHandle ShowProgressPanel()
        {
            return SplashScreenManager.ShowOverlayForm(this, OverlayWindowOptions.Default);
        }
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            SplashScreenManager.CloseOverlayForm(handle);
        }
        internal frmMain OwnerForm
        {
            get
            {
                if (instance == null)
                    instance = this.FindForm() as frmMain;
                return instance;
            }
            set
            {
                this.instance = value;
            }
        }
        internal virtual void ShowModule(bool firstShow)
        {
            if (OwnerForm == null)
                return;
        }
        public virtual string ModuleName { get { return this.GetType().Name; } }
        public virtual GridControl GridControl { get { return null; } }
        public virtual GridView GridView { get { return null; } }


    }

}
