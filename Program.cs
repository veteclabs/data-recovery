using Plate.Resources;
using Plate.UI.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Plate
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
                var koKR = new System.Globalization.CultureInfo("ko-KR");
                Thread.CurrentThread.CurrentCulture = koKR;
                Thread.CurrentThread.CurrentUICulture = koKR;
                DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Segoe UI", 8);
                UserLookAndFeel.Default.SetSkinStyle(SkinSvgPalette.WXICompact.OfficeColorful);

                SplashScreenManager.ShowForm(typeof(SplashScreen1));
                Application.Run(new frmMain());
        }
    }
}
