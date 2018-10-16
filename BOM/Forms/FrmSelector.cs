using BOM.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BOM.Forms
{
    public partial class FrmSelector : Form
    {
        #region Private Members
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructor
        public FrmSelector()
        {
            InitializeComponent();

            try
            {
                SetUpPictureEdit();
                SetupEvents();
                SetUpToolTip();
                SetUpForm();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Form Events
        private void PeMassiveUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsItUser())
                {
                    MassiveUpdateChangeItem form = new MassiveUpdateChangeItem();
                    form.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("No tiene permisos para ejecutar esta opción");
                }
                    
                
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PeImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                BomImport form = new BomImport();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PeBomManagement_Click(object sender, EventArgs e)
        {
            try
            {
                BomManagement form = new BomManagement();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Private Members

        #region Setup Form Events

        private void SetUpForm()
        {
            try
            {
                BackColor = Color.White;
                ShowIcon = false;
                Text = string.Empty;
                FormBorderStyle = FormBorderStyle.FixedSingle;
                MaximizeBox = false;
                MinimizeBox = false;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpPictureEdit()
        {
            try
            {
                peBomManagement.Image = Properties.Resources.bill_of_materials;
                peBomManagement.Properties.SizeMode = PictureSizeMode.Zoom;
                peBomManagement.Properties.ShowMenu = false;
                peBomManagement.BorderStyle = BorderStyles.NoBorder;

                peImportExcel.Image = Properties.Resources.excel_import;
                peImportExcel.Properties.SizeMode = PictureSizeMode.Zoom;
                peImportExcel.Properties.ShowMenu = false;
                peImportExcel.BorderStyle = BorderStyles.NoBorder;

                peMassiveUpdate.Image = Properties.Resources.update;
                peMassiveUpdate.Properties.SizeMode = PictureSizeMode.Zoom;
                peMassiveUpdate.Properties.ShowMenu = false;
                peMassiveUpdate.BorderStyle = BorderStyles.NoBorder;
            }
            catch
            {
                throw;
            }
        }

        private void SetupEvents()
        {
            try
            {
                peBomManagement.Click += PeBomManagement_Click;
                peImportExcel.Click += PeImportExcel_Click;
                peMassiveUpdate.Click += PeMassiveUpdate_Click;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpToolTip()
        {
            try
            {
                toolTipController1.SetToolTip(peBomManagement, "BOM Management");
                toolTipController1.SetToolTip(peImportExcel, "Import Excel");
                toolTipController1.SetToolTip(peMassiveUpdate, "Massive update");
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Aux
        private bool IsItUser()
        {
            try
            {



                using (var adContext = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                {
                    UserPrincipal user = UserPrincipal.Current;
                    PrincipalSearchResult<Principal> results = user.GetAuthorizationGroups();
                    foreach (var u in results)
                    {
                        if (u.Name == "1006_IT")
                            return true;
                    }
                }

                return false;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #endregion
    }
}
