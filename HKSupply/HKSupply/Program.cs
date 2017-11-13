using DevExpress.LookAndFeel;
using HKSupply.Forms;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            //Encriptar las connection strings
            //ToggleConfigEncryption(AppDomain.CurrentDomain.FriendlyName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Source Sans Pro", 10);

            //Change DevExpress Look and Feel skin for all application
            UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Test: change culture to English
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN"); //Chinese

            Login frmLogin = new Login();
            frmLogin.ShowDialog();

            if (frmLogin.DialogResult == DialogResult.OK)
                Application.Run(new Main());
        }

        static void ToggleConfigEncryption(string exeConfigName)
        {
            // Takes the executable file name without the
            // .config extension.
            try
            {
                // Open the configuration file and retrieve
                // the connectionStrings section.
                Configuration config = ConfigurationManager.OpenExeConfiguration(exeConfigName);

                ConnectionStringsSection section =
                    config.GetSection("connectionStrings") as ConnectionStringsSection;

                if (section.SectionInformation.IsProtected)
                {
                    // Remove encryption.
                    section.SectionInformation.UnprotectSection();
                    Console.WriteLine("connectionStrings is already protected... removing encryption");
                }
                else
                {
                    // Encrypt the section.
                    Console.WriteLine("connectionStrings is not protected... encrypting?");
                    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                }

                // Save the current configuration.
                config.Save();

                Console.WriteLine("Protected={0}", section.SectionInformation.IsProtected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
