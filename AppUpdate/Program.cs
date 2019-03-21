using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AppUpdate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Utitls.PropertsUtils configPes = new Utitls.PropertsUtils();
            Application.Run(new UpdateForm(configPes));
        }
    }
}
