using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace HKSupply.Reports
{
    public partial class frmVisor : Form
    {

        public frmVisor()
        {
            InitializeComponent();

        }
 
        private void frmVisor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReportDocument oDoc = (ReportDocument)this.crystalReportViewer1.ReportSource;
            oDoc.Close();
            oDoc.Dispose();
        }

    
    }
}