using HKSupply.Forms;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!LogManager.GetRepository().Configured)
                throw new Exception("log4net no ha sido configurado.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Source Sans Pro", 10);

            //Test: change culture to English
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN"); //Chinese

            Login frmLogin = new Login();
            frmLogin.ShowDialog();

            if (frmLogin.DialogResult == DialogResult.OK)
                Application.Run(new Main());
        }
    }
}
