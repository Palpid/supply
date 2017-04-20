using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms
{
    /// <summary>
    /// Visor de pdf integrado en la aplicación. No permitimos guardar/abrir, pero dejo los botones por si en el futuro 
    /// se quiere dar esta funcionalidad en algunos casos
    /// </summary>
    public partial class PDFViewer : Form
    {
        #region Public Properties
        public string pdfFile { get; set; }
        #endregion

        #region Constructor
        public PDFViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void PDFViewer_Load(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(pdfFile))
                    pdfViewer1.LoadDocument(pdfFile);
                else
                    XtraMessageBox.Show("File not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        #endregion
    }
}
