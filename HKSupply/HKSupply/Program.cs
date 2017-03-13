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
            
            //Application.Run(new Form1());

            Login frmLogin = new Login();
            frmLogin.ShowDialog();

            if (frmLogin.DialogResult == DialogResult.OK)
                Application.Run(new Main());
        }
    }
}
