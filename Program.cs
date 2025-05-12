using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataRecorvery
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Globalization.CultureInfo koKR = new System.Globalization.CultureInfo("ko-KR");
            System.Threading.Thread.CurrentThread.CurrentCulture = koKR;
            System.Threading.Thread.CurrentThread.CurrentUICulture = koKR;
            DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Segoe UI", 8);
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);

            var splashScreenImage = DevExpress.Utils.ResourceImageHelper.CreateImageFromResourcesEx("DataRecorvery.Resources.SplashScreenNew.png", CurrentAssembly);
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowImage(splashScreenImage, true, false);
            Application.Run(new frmMain());
        }
        static Assembly currentAssemblyCore;
        static Assembly CurrentAssembly
        {
            get
            {
                if (currentAssemblyCore == null)
                    currentAssemblyCore = Assembly.GetExecutingAssembly();
                return currentAssemblyCore;
            }
        }
    }
}
