using System.Windows.Forms;
using DevExpress.XtraEditors;
using Serilog.Events;
using Serilog;
using Serilog.Core;
using System;
using Microsoft.Extensions.Logging;
using Plate.Modules;

namespace Plate
{
    public partial class ucOutput : BaseModule
    {
        public ucOutput() {
            InitializeComponent();
            comboBox.SelectedIndex = 0;
            textBox.ContextMenuStrip = new ContextMenuStrip();
           
        }
        internal override void ShowModule(bool firstShow)
        {
           

            AppendLog("Start Application...");
        }

        public delegate void AppendLogDelegate(string log, string daq);

        public void AppendLog(string log)
        {
            AppendLog(log, string.Empty);
            Log.Debug(log);

        }
        public void AppendLog(string log, string daq)
        {
            //if (_logPaused && daq != TagResources.OUTPUT)
            //    return;
            if (InvokeRequired)
            {
                BeginInvoke(new AppendLogDelegate(AppendLog), new object[] { log, daq });
                return;
            }
            var now = DateTime.Now;
            var tmpStr = ">" + now.ToString("HH:mm:ss") + ": " + log + Environment.NewLine;


            // Add the new entry at the end
            textBox.AppendText(tmpStr);

            // Remove the first line if the line count exceeds the limit
            while (textBox.Lines.Length > 10000)
            {
                int firstLineLength = textBox.GetFirstCharIndexFromLine(1);
                textBox.Text = textBox.Text.Remove(0, firstLineLength);
            }

            // Scroll to the end of the text box
            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();

        }
        void comboBox_SelectedIndexChanged(object sender, System.EventArgs e) {
            //if(comboBox.SelectedIndex == 0)
            //    textBox.Text = " ------ Build started: Project: VisualStudioInspiredUIDemo, Configuration: Debug .NET ------\r\n\r\n Preparing resources...\r\n Updating references...\r\n Performing main compilation...\r\n\r\n Build complete -- 0 errors, 0 warnings\r\n Building satellite assemblies...\r\n\r\n\r\n ---------------------- Done ----------------------\r\n\r\n     Build: 1 succeeded, 0 failed, 0 skipped";
            //else textBox.Text = " 'DefaultDomain': Loaded 'd:\\winnt\\microsoft.net\\framework\\v1.0.3705\\mscorlib.dll', No symbols loaded.\r\n 'VisualStudioInspiredUIDemo': Loaded 'C:\\BarDemos\\CS\\DockingDemo\\bin\\Debug\\DockingDemo.exe', Symbols loaded.";
        }
        void textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
            e.Handled = true;
        }
        
    }
}
