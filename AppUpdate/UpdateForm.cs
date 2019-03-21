using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using AppUpdate.Utitls;
using LayeredSkin.DirectUI;
using LayeredSkin.Forms;
namespace AppUpdate
{
    public partial class UpdateForm : LayeredForm
    {

        public PropertsUtils pes = new PropertsUtils();
        System.Timers.Timer st = new System.Timers.Timer();
        Controls.CustomHxjdtControl customHxjdt = new Controls.CustomHxjdtControl();
        public UpdateForm(PropertsUtils cps)
        {
            pes = cps;
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btn_close_MouseEnter(object sender, EventArgs e)
        {

        }

        private void layeredPanel_close_MouseLeave(object sender, EventArgs e)
        {

        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            st.Interval = 100;
            st.Enabled = true;
            st.Elapsed += St_Elapsed;
            customHxjdt.Size = new Size(250, 250);
            customHxjdt.Location = new Point((lbc.Width-250)/2, 0);
            customHxjdt.Value = 0;
            customHxjdt.Lcp = System.Drawing.Drawing2D.LineCap.Round;
            customHxjdt.CircularWidth = 25;
            customHxjdt.BackColor = Color.Transparent;
            customHxjdt.MainColor = pes.BackColor;
            lbc.DUIControls.Add(customHxjdt);
            this.BackColor = Color.FromArgb(185, pes.BackColor);
            foreach (DuiBaseControl item in lbc.DUIControls)
            {
                if (item is DuiTextBox)
                {
                    DuiTextBox gxnr = item as DuiTextBox;
                    gxnr.Text = pes.VerNo + "\r\n" + pes.UpdateContent.Replace("---", "\r\n");
                }
            }
            HttpDldFile fileDownload = new HttpDldFile();
            Thread thread = new Thread(() => fileDownload.Download(pes.DownloadUrl, AppDomain.CurrentDomain.BaseDirectory + @"\"+pes.MainApplicationName+".exe", customHxjdt));
            thread.Start();
        }

        private void St_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (customHxjdt.Value == 100)
            {
                //System.Windows.Forms.Application.Restart();
                st.Enabled = false;
                //打开更新后的主窗体
                Process myProcess = new Process();
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"\"+pes.MainApplicationName+".exe";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
                //关闭程序
                Dispose();
                System.Environment.Exit(0);
            }
        }
    }
}
