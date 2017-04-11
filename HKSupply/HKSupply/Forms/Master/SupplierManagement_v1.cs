using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class SupplierManagement_v1 : Form, IActionsStackView
    {

        #region enums
        private enum eSupplierColumns
        {
            IdVer,
            idSubVer,
            Timestamp,
            IdSupplier,
            SupplierName,
            Active,
            VATNum,
            ShippingAddress,
            BillingAddress,
            ContactName,
            ContactPhone,
            IdIncoterm,
            IdPaymentTerms,
            Currency,
        }

        private enum eSupplierColumnsFilter
        {
            IdSupplier,
            SupplierName,
            Active,
            VATNum,
        }

        private enum eSupplierColumnsFilterType
        {
            Text = 0,
            CheckBox = 1,
        }

        private Dictionary<string, int> _filterDic = new Dictionary<string, int>() 
        { 
            {eSupplierColumnsFilter.IdSupplier.ToString(), (int)eSupplierColumnsFilterType.Text },
            {eSupplierColumnsFilter.SupplierName.ToString(), (int)eSupplierColumnsFilterType.Text },
            {eSupplierColumnsFilter.Active.ToString(), (int)eSupplierColumnsFilterType.CheckBox },
            {eSupplierColumnsFilter.VATNum.ToString(), (int)eSupplierColumnsFilterType.Text }
        };

        #endregion

        #region Private members
        CustomControls.StackView actionsStackView;

        Supplier _supplierUpdate;
        Supplier _supplierOriginal;

        List<Supplier> _suppliersList;

        string[] _nonEditingFields = { "txtIdSupplier", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };
        string[] _nonCreatingFields = { "txtIdVersion", "txtIdSubversion", "txtTimestamp" };
        int[] _nonCreatingFieldsRows = { 1, 2, 3 };

        bool _sortDescending = true;

        #endregion

        #region Constructor
        public SupplierManagement_v1()
        {
            InitializeComponent();
            ResetSupplierUpdate();
        }
        #endregion

        #region Action toolbar
        public void actionsStackView_EditButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (_supplierOriginal == null)
                {
                    MessageBox.Show("No supplier selected");
                    actionsStackView.RestoreInitState();
                }
                else
                {
                    ConfigureActionsStackViewEditing();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void actionsStackView_NewButtonClick(object sender, EventArgs e)
        {
            try
            {
                ConfigureActionsStackViewCreating();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void actionsStackView_SaveButtonClick(object sender, EventArgs e)
        {
            bool res = false;
            //El toolstrip no lanza el validate, lo lanzamos a mano por si acaso hay algún elemento que lo tiene pendiente
            Validate();

            if (IsValidSupplier() == false)
                return;

            DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            Cursor = Cursors.WaitCursor;
            try
            {
                if (_supplierUpdate.Equals(_supplierOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    res = UpdateSupplier();

                }
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                {
                    res = CreateSupplier();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    ActionsAfterCU();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        public void actionsStackView_CancelButtonClick(object sender, EventArgs e)
        {
            try
            {
                _supplierOriginal = null;
                ResetSupplierUpdate();
                SetFormBinding();
                tcGeneral.TabPages.Remove(tpForm);
                tcGeneral.TabPages.Add(tpGrid);
                btnNewVersion.Visible = false;
                LoadSuppliersList();
                actionsStackView.RestoreInitState();

                foreach (var n in _nonCreatingFieldsRows)
                {
                    tlpForm.RowStyles[n].SizeType = SizeType.AutoSize;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ConfigureActionsStackView()
        {
            try
            {
                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));

                actionsStackView = new CustomControls.StackView(actions.Read, actions.New, actions.Modify);
                actionsStackView.EditButtonClick += actionsStackView_EditButtonClick;
                actionsStackView.NewButtonClick += actionsStackView_NewButtonClick;
                actionsStackView.SaveButtonClick += actionsStackView_SaveButtonClick;
                actionsStackView.CancelButtonClick += actionsStackView_CancelButtonClick;

                Controls.Add(actionsStackView);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Form Events
        private void SupplierManagement_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigureActionsStackView();
                SetFormBinding();
                tcGeneral.TabPages.Remove(tpForm);
                btnNewVersion.Visible = false;
                LoadComboFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadSuppliersList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnNewVersion_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Validate();

                if (_supplierUpdate.Equals(_supplierOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (UpdateSupplier(true))
                {
                    ActionsAfterCU();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void cmbColFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbColFilter.SelectedIndex == -1)
                {
                    txtFilter.Visible = false;
                    chkFilter.Visible = false;
                    return;
                }

                var type = _filterDic[((eSupplierColumnsFilter)cmbColFilter.SelectedIndex).ToString()];
                ShowFilterField((eSupplierColumnsFilterType)type);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid events
        private void grdSuppliers_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                LoadSupplierForm(grdSuppliers.Rows[e.RowIndex].Cells[(int)eSupplierColumns.IdSupplier].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 && e.ColumnIndex > -1)
                {
                    LoadSupplierSortedList((eSupplierColumns)e.ColumnIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Resetear el objeto supplier que usamos para la actualización
        /// </summary>
        private void ResetSupplierUpdate()
        {
            _supplierUpdate = new Supplier
            {
                IdSupplier = string.Empty,
                SupplierName = string.Empty,
                Active = false,
                VATNum = string.Empty,
                //ShippingAddress = string.Empty,
                //BillingAddress = string.Empty,
                //ContactName = string.Empty,
                //ContactPhone = string.Empty,
                //Currency = string.Empty
            };
        }

        /// <summary>
        /// Crear los bindings de los campos del formulario
        /// </summary>
        private void SetFormBinding()
        {
            foreach (Control ctl in tlpForm.Controls)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    ctl.DataBindings.Clear();
                    ((TextBox)ctl).ReadOnly = true;
                }
                else if (ctl.GetType() == typeof(CheckBox))
                {
                    ctl.DataBindings.Clear();
                    ((CheckBox)ctl).Enabled = false;
                }
            }
            txtIdSupplier.DataBindings.Add("Text", _supplierUpdate, "IdSupplier");
            txtIdVersion.DataBindings.Add("Text", _supplierUpdate, "idVer");
            txtIdSubversion.DataBindings.Add("Text", _supplierUpdate, "idSubVer");
            txtTimestamp.DataBindings.Add("Text", _supplierUpdate, "Timestamp");
            txtName.DataBindings.Add("Text", _supplierUpdate, "SupplierName");
            chkActive.DataBindings.Add("Checked", _supplierUpdate, "Active");
            txtVatNumber.DataBindings.Add("Text", _supplierUpdate, "VATNum");
            txtShippingAddress.DataBindings.Add("Text", _supplierUpdate, "ShippingAddress");
            txtBillingAddress.DataBindings.Add("Text", _supplierUpdate, "BillingAddress");
            txtContactName.DataBindings.Add("Text", _supplierUpdate, "ContactName");
            txtContactPhone.DataBindings.Add("Text", _supplierUpdate, "ContactPhone");
            txtIntercom.DataBindings.Add("Text", _supplierUpdate, "IdIncoterm");
            txtPaymentTerms.DataBindings.Add("Text", _supplierUpdate, "IdPaymentTerms");
            txtCurreny.DataBindings.Add("Text", _supplierUpdate, "Currency");
        }

        /// <summary>
        /// Cargar el combo con los campos que se puede filtrar
        /// </summary>
        private void LoadComboFilters()
        {
            try
            {
                cmbColFilter.DataSource = Enum.GetNames(typeof(eSupplierColumnsFilter));
                cmbColFilter.SelectedIndexChanged += cmbColFilter_SelectedIndexChanged;
                cmbColFilter.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mostrar el campo de filtro en función del tipo del campo (text, boolen...)
        /// </summary>
        /// <param name="type"></param>
        private void ShowFilterField(eSupplierColumnsFilterType type)
        {
            try
            {
                txtFilter.Location = new Point(223, 14);
                chkFilter.Location = new Point(223, 18);

                switch (type)
                {
                    case eSupplierColumnsFilterType.Text:
                        txtFilter.Visible = true;
                        chkFilter.Visible = false;
                        break;
                    case eSupplierColumnsFilterType.CheckBox:
                        txtFilter.Visible = false;
                        chkFilter.Visible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// cargar la colección de Suppliers del sistema
        /// </summary>
        /// <remarks>Filtramos si es necesario</remarks>
        private void LoadSuppliersList()
        {
            try
            {
                _suppliersList = GlobalSetting.SupplierService.GetSuppliers();

                //Filtramos si es necesario
                //Columnas tipo Texto
                if ((eSupplierColumnsFilter)cmbColFilter.SelectedIndex != eSupplierColumnsFilter.Active && 
                    cmbColFilter.SelectedIndex > -1 && string.IsNullOrEmpty(txtFilter.Text) == false)
                {
                    //Lo pasamos antes a minúsculas todo, ya que el Contains es case sensitive, 
                    _suppliersList = _suppliersList.Where(cmbColFilter.SelectedItem.ToString() + ".ToLower().Contains(@0)", txtFilter.Text.ToLower()).ToList();
                }
                //Columnas bit
                if ((eSupplierColumnsFilter)cmbColFilter.SelectedIndex == eSupplierColumnsFilter.Active)
                {
                    _suppliersList = _suppliersList.Where(cmbColFilter.SelectedItem.ToString() + " = @0", chkFilter.Checked).ToList();
                }

                grdSuppliers.CellDoubleClick += grdSuppliers_CellDoubleClick;
                grdSuppliers.CellClick += grdSuppliers_CellClick;
                grdSuppliers.DataSource = null;
                grdSuppliers.Rows.Clear();
                grdSuppliers.DataSource = _suppliersList;
                grdSuppliers.ReadOnly = true;
                //Para poder cambiar el color del header cuando se filtra hay que desactivar los efectos visuales del header que coge por defecto
                grdSuppliers.EnableHeadersVisualStyles = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ordenar el grid según la columna 
        /// </summary>
        /// <param name="sortedColumn">Columna por la que se quiere ordenar</param>
        /// <remarks>
        /// Alterna entre el orden ascendente y descendente.
        /// OK, no es muy correcto porque se controla con una sola variable la ordenación (ascendente o descendente) 
        /// de todas las columnas, pero así es más fácil y en el peor de los casos el usuario pulsará dos veces 
        /// sobre la misma columna para tener el orden que quiere.
        /// </remarks>
        private void LoadSupplierSortedList(eSupplierColumns sortedColumn)
        {
            try
            {
                string order = _sortDescending ? " ASC" : " DESC";
                _suppliersList = _suppliersList.OrderBy(sortedColumn.ToString() + order).ToList();

                _sortDescending = !_sortDescending; //See Remarks

                grdSuppliers.DataSource = null;
                grdSuppliers.Rows.Clear();
                grdSuppliers.DataSource = _suppliersList;
                grdSuppliers.ReadOnly = true;
                grdSuppliers.Columns[(int)sortedColumn].HeaderCell.Style.BackColor = Color.Tomato;
                grdSuppliers.Columns[(int)sortedColumn].HeaderText += "*";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar los datos de un suppliers en concreto
        /// </summary>
        /// <param name="idSupplier"></param>
        private void LoadSupplierForm(string idSupplier)
        {
            try
            {
                if (tcGeneral.TabPages.Contains(tpForm) == false)
                    tcGeneral.TabPages.Add(tpForm);
                tcGeneral.SelectedTab = tpForm;

                _supplierUpdate = GlobalSetting.SupplierService.GetSupplierById(idSupplier);
                _supplierOriginal = _supplierUpdate.Clone();
                SetFormBinding();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizar los datos de un supplier
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateSupplier(bool newVersion = false)
        {
            try
            {

                return GlobalSetting.SupplierService.UpdateSupplier(_supplierUpdate, newVersion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crear un nuevo Supplier
        /// </summary>
        /// <returns></returns>
        private bool CreateSupplier()
        {
            try
            {
                _supplierOriginal = _supplierUpdate.Clone();
                return GlobalSetting.SupplierService.NewSupplier(_supplierUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mover la celda activa a la de un supplier en concreto
        /// </summary>
        /// <param name="idSupplier"></param>
        private void MoveGridToSupplier(string idSupplier)
        {
            try
            {
                foreach (DataGridViewRow row in grdSuppliers.Rows)
                {
                    if (row.Cells[(int)eSupplierColumns.IdSupplier].Value.ToString() == idSupplier)
                    {
                        grdSuppliers.CurrentCell = row.Cells[(int)eSupplierColumns.IdVer];
                        grdSuppliers.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Acciones cuando pulsamos el botón de editar
        /// </summary>
        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                tcGeneral.TabPages.Remove(tpGrid);

                btnNewVersion.Visible = true;

                SetEditingFieldsEnabled();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Acciones cuando pulsamos el botón de nuevo
        /// </summary>
        private void ConfigureActionsStackViewCreating()
        {
            tcGeneral.TabPages.Remove(tpGrid);
            if (tcGeneral.TabPages.Contains(tpForm) == false)
                tcGeneral.TabPages.Add(tpForm);

            tcGeneral.SelectedTab = tpForm;

            btnNewVersion.Visible = false;

            ResetSupplierUpdate();
            SetFormBinding();

            SetCreatingFieldsEnabled();

            foreach (var n in _nonCreatingFieldsRows)
            {
                tlpForm.RowStyles[n].SizeType = SizeType.Absolute;
                tlpForm.RowStyles[n].Height = 0;
            }


        }

        /// <summary>
        /// Poner como editables los campos para el modo de edición
        /// </summary>
        private void SetEditingFieldsEnabled()
        {
            try
            {
                foreach (Control ctl in tlpForm.Controls)
                {
                    if (Array.IndexOf(_nonEditingFields, ctl.Name) < 0)
                    {
                        if (ctl.GetType() == typeof(TextBox))
                        {
                            ((TextBox)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(CheckBox))
                        {
                            ((CheckBox)ctl).Enabled = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Poner como editables los campos para el modo de creación
        /// </summary>
        private void SetCreatingFieldsEnabled()
        {
            try
            {
                foreach (Control ctl in tlpForm.Controls)
                {
                    if (Array.IndexOf(_nonCreatingFields, ctl.Name) < 0)
                    {
                        if (ctl.GetType() == typeof(TextBox))
                        {
                            ((TextBox)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(CheckBox))
                        {
                            ((CheckBox)ctl).Enabled = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Acciones después de crear o updatar
        /// </summary>
        private void ActionsAfterCU()
        {
            try
            {
                string id = _supplierOriginal.IdSupplier;
                _supplierOriginal = null;
                ResetSupplierUpdate();
                SetFormBinding();
                tcGeneral.TabPages.Remove(tpForm);
                tcGeneral.TabPages.Add(tpGrid);
                btnNewVersion.Visible = false;
                LoadSuppliersList();
                MoveGridToSupplier(id);
                actionsStackView.RestoreInitState();

                foreach (var n in _nonCreatingFieldsRows)
                {
                    tlpForm.RowStyles[n].SizeType = SizeType.AutoSize;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region validate data

        private bool IsValidSupplier()
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                    return IsValidModifiedSupplier();
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                    return IsValidCreatedSupplier();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsValidModifiedSupplier()
        {
            try
            {
                foreach (Control ctl in tpForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextBox))
                    {
                        if (string.IsNullOrEmpty(((TextBox)ctl).Text))
                        {
                            MessageBox.Show(string.Format(GlobalSetting.ResManager.GetString("NullArgument"), ctl.Name));
                            return false;
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        private bool IsValidCreatedSupplier()
        {
            try
            {
                foreach (Control ctl in tlpForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextBox))
                    {
                        if (string.IsNullOrEmpty(((TextBox)ctl).Text))
                        {
                            MessageBox.Show(string.Format(GlobalSetting.ResManager.GetString("NullArgument"), ctl.Name));
                            return false;
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
    }
}
