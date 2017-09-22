using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace LuceneWinApp
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
            bool isCreateNew = false;
            Mutex m = new Mutex(false, "mainform", out isCreateNew);
            if (isCreateNew)
            {
                Application.Run(new MainForm());
            }
        }
    }
}
