
using HKSupply.General;
using HKSupply.PRJ_Stocks.DB;
using HKSupply.PRJ_Stocks.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HKSupply.DB;


namespace HKSupply.PRJ_Stocks.Forms
{
    public partial class StockManagement : Form
    {
        private Stocks STKAct = new Stocks();

        public StockManagement()
            
        {            
            InitializeComponent();
                      
        }

        private void StockManagement_Load(object sender, EventArgs e)
        {
            try
            {
                this.Show();

                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));
                // Permisos que te l'usuari
                bool CanRead = actions.Read;
                bool CanNew = actions.New;
                bool CanModify = actions.Modify;

                Call_DB_Stocks CallDBS = new Call_DB_Stocks();
                STKAct = CallDBS.CallCargaStocks();

                GC_Stocks.DataSource = STKAct.LstStocks;

                this.Refresh();
            }
            catch
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
