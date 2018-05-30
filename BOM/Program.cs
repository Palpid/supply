using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOM
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 10);

            //Change DevExpress Look and Feel skin for all application
            UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Forms.FrmSelector());
            //Application.Run(new Forms.BomManagement());
            //Application.Run(new Forms.MassiveUpdateChangeItem());
            //Application.Run(new Forms.BomImport());
        }
    }
}
