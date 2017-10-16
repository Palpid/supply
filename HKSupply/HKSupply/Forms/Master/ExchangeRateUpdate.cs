using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using HKSupply.Helpers.CurrencyConverter;
using System;
using System.Threading;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class ExchangeRateUpdate : Form
    {
        public ExchangeRateUpdate()
        {
            OpenWaitFormToUpdate();
            InitializeComponent();
        }

        private void ExchangeRateUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.CloseForm(false);
                BeginInvoke(new MethodInvoker(Close)); //No se puede hacer directamente el Close, da un error
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void OpenWaitFormToUpdate()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                SplashScreenManager.Default.SetWaitFormCaption("Please wait");
                SplashScreenManager.Default.SetWaitFormDescription("Updating Exchange Rates...");

                UpdateRates();
                Thread.Sleep(3000); //Fake Delay (to show de wait form)
                XtraMessageBox.Show("Exchange Rates updated");
            }
            catch
            {
                throw;
            }

        }

        private void UpdateRates()
        {
            try
            {
                CurrencyConverter ratetable = new CurrencyConverter();
                ratetable.UpdateDbExchangeRates();
            }
            catch
            {
                throw;
            }
        }
    }
}
