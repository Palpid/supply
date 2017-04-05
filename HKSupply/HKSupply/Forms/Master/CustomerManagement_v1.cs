using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using HKSupply.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class CustomerManagement_v1 : Form, IActionsStackView
    {
        #region enums
        private enum eCustomerColumns
        {
            IdVer,
            idSubVer,
            Timestamp,
            IdCustomer,
            CustName,
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

        private enum eCustomerColumnsFilter
        {
            IdCustomer,
            CustName,
            Active,
            VATNum,
        }

        private enum eCustomerColumnsFilterType
        {
            Text = 0,
            CheckBox = 1,
        }

        private Dictionary<string, int> _filterDic = new Dictionary<string, int>() 
        { 
            {eCustomerColumns.IdCustomer.ToString(), (int)eCustomerColumnsFilterType.Text },
            {eCustomerColumns.CustName.ToString(), (int)eCustomerColumnsFilterType.Text },
            {eCustomerColumns.Active.ToString(), (int)eCustomerColumnsFilterType.CheckBox },
            {eCustomerColumns.VATNum.ToString(), (int)eCustomerColumnsFilterType.Text }
        };
        #endregion

        #region Private members
        CustomControls.StackView actionsStackView;

        Customer _customerUpdate;
        Customer _customerOriginal;

        List<Customer> _customersList;

        string[] _nonEditingFields = { "txtIdCustomer", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };
        string[] _nonCreatingFields = { "txtIdVersion", "txtIdSubversion", "txtTimestamp" };
        int[] _nonCreatingFieldsRows = { 1, 2, 3 };

        bool _sortDescending = true;

        #endregion

        #region Constructor
        public CustomerManagement_v1()
        {
            InitializeComponent();
            ResetCustomerUpdate();
        }
        #endregion

        #region Action toolbar
        public void actionsStackView_EditButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (_customerOriginal == null)
                {
                    MessageBox.Show("No customer selected");
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
                //MessageBox.Show("New Button");
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

            if (IsValidCustomer() == false)
                return;

            DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            Cursor = Cursors.WaitCursor;
            try
            {
                if (_customerUpdate.Equals(_customerOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    res = UpdateCustomer();
                }
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                {
                    res = CreateCustomer();
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
                _customerOriginal = null;
                ResetCustomerUpdate();
                SetFormBinding();
                tcGeneral.TabPages.Remove(tpForm);
                tcGeneral.TabPages.Add(tpGrid);
                btnNewVersion.Visible = false;
                LoadCustomersList();
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

        private void CustomerManagement_Load(object sender, System.EventArgs e)
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
                LoadCustomersList();
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

                if (_customerUpdate.Equals(_customerOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (UpdateCustomer(true))
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

                var type = _filterDic[((eCustomerColumnsFilter)cmbColFilter.SelectedIndex).ToString()];
                ShowFilterField((eCustomerColumnsFilterType)type);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region Grid events
        private void grdCustomers_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                LoadCustomerForm(grdCustomers.Rows[e.RowIndex].Cells[(int)eCustomerColumns.IdCustomer].Value.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 && e.ColumnIndex > -1)
                {
                    LoadCustomersSortedList((eCustomerColumns)e.ColumnIndex);
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
        /// Resetear el objeto customer que usamos para la actualización
        /// </summary>
        private void ResetCustomerUpdate()
        {
            _customerUpdate = new Customer
                    {
                        IdCustomer = string.Empty,
                        CustName = string.Empty,
                        Active = false,
                        VATNum = string.Empty,
                        ShippingAddress = string.Empty,
                        BillingAddress = string.Empty,
                        ContactName = string.Empty,
                        ContactPhone = string.Empty,
                        Currency = string.Empty
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
            txtIdCustomer.DataBindings.Add("Text", _customerUpdate, "idCustomer");
            txtIdVersion.DataBindings.Add("Text", _customerUpdate, "idVer");
            txtIdSubversion.DataBindings.Add("Text", _customerUpdate, "idSubVer");
            txtTimestamp.DataBindings.Add("Text", _customerUpdate, "Timestamp");
            txtName.DataBindings.Add("Text", _customerUpdate, "CustName");
            chkActive.DataBindings.Add("Checked",_customerUpdate,"Active");
            txtVatNumber.DataBindings.Add("Text", _customerUpdate, "VATNum");
            txtShippingAddress.DataBindings.Add("Text", _customerUpdate, "ShippingAddress");
            txtBillingAddress.DataBindings.Add("Text", _customerUpdate, "BillingAddress");
            txtContactName.DataBindings.Add("Text", _customerUpdate, "ContactName");
            txtContactPhone.DataBindings.Add("Text", _customerUpdate, "ContactPhone");
            txtIntercom.DataBindings.Add("Text", _customerUpdate, "idIncoterm");
            txtPaymentTerms.DataBindings.Add("Text", _customerUpdate, "idPaymentTerms");
            txtCurreny.DataBindings.Add("Text", _customerUpdate, "Currency");
        }

        /// <summary>
        /// Cargar el combo con los campos que se puede filtrar
        /// </summary>
        private void LoadComboFilters()
        {
            try
            {
                cmbColFilter.DataSource = Enum.GetNames(typeof(eCustomerColumnsFilter));
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
        private void ShowFilterField(eCustomerColumnsFilterType type)
        {
            try
            {
                txtFilter.Location = new Point(223, 14);
                chkFilter.Location = new Point(223, 18);

                switch (type)
                {
                    case eCustomerColumnsFilterType.Text:
                        txtFilter.Visible = true;
                        chkFilter.Visible = false;
                        break;
                    case eCustomerColumnsFilterType.CheckBox:
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
        /// cargar la colección de Customer del sistema
        /// </summary>
        /// <remarks></remarks>
        private void LoadCustomersList()
        {
            try
            {
                _customersList = GlobalSetting.CustomerService.GetCustomers();

                //Filtramos si es necesario
                //MRM.Notes: Ahora obtenemos todo de la bbdd y sobre esa colección filtramos. Si al final se implanta esto se tendría que hacer la llamada
                //a la bbdd con las condiciones y que la propia consulta filtre
                //Columnas tipo Texto
                if ((eCustomerColumnsFilter)cmbColFilter.SelectedIndex != eCustomerColumnsFilter.Active &&  
                    cmbColFilter.SelectedIndex > -1 && string.IsNullOrEmpty(txtFilter.Text) == false)
                {
                    //Lo pasamos antes a minúsculas todo, ya que el Contains es case sensitive, 
                    _customersList = _customersList.Where(cmbColFilter.SelectedItem.ToString() + ".ToLower().Contains(@0)", txtFilter.Text.ToLower()).ToList();
                }
                //Columnas bit
                if ((eCustomerColumnsFilter)cmbColFilter.SelectedIndex == eCustomerColumnsFilter.Active)
                {
                    _customersList = _customersList.Where(cmbColFilter.SelectedItem.ToString() + " = @0", chkFilter.Checked).ToList();
                }

                grdCustomers.CellDoubleClick += grdCustomers_CellDoubleClick;
                grdCustomers.CellClick +=grdCustomers_CellClick;
                grdCustomers.DataSource = null;
                grdCustomers.Rows.Clear();
                grdCustomers.DataSource = _customersList;
                grdCustomers.ReadOnly = true;
                //Para poder cambiar el color del header cuando se filtra hay que desactivar los efectos visuales del header que coge por defecto
                grdCustomers.EnableHeadersVisualStyles = false; 

            }
            catch(Exception ex)
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
        private void LoadCustomersSortedList(eCustomerColumns sortedColumn)
        {
            try
            {
                string order = _sortDescending ? " ASC" : " DESC";
                _customersList = _customersList.OrderBy(sortedColumn.ToString() + order).ToList();

                _sortDescending = !_sortDescending; //See Remarks

                grdCustomers.DataSource = null;
                grdCustomers.Rows.Clear();
                grdCustomers.DataSource = _customersList;
                grdCustomers.ReadOnly = true;
                grdCustomers.Columns[(int)sortedColumn].HeaderCell.Style.BackColor = Color.Tomato;
                grdCustomers.Columns[(int)sortedColumn].HeaderText += "*";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Cargar los datos de un customer en concreto
        /// </summary>
        /// <param name="idCustomer"></param>
        private void LoadCustomerForm(string idCustomer)
        {
            try
            {
                if (tcGeneral.TabPages.Contains(tpForm) == false)
                    tcGeneral.TabPages.Add(tpForm);
                tcGeneral.SelectedTab = tpForm;

                _customerUpdate = GlobalSetting.CustomerService.GetCustomerById(idCustomer);
                _customerOriginal = _customerUpdate.Clone();
                SetFormBinding();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizar los datos de un customer
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateCustomer(bool newVersion = false)
        {
            try
            {
                return GlobalSetting.CustomerService.UpdateCustomer(_customerUpdate, newVersion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crear un nuevo Customer
        /// </summary>
        /// <returns></returns>
        private bool CreateCustomer()
        {
            try
            {
                _customerOriginal = _customerUpdate.Clone();
                return GlobalSetting.CustomerService.NewCustomer(_customerUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Mover la celda activa a la de un customer en concreto
        /// </summary>
        /// <param name="idCustomer"></param>
        private void MoveGridToCustomer(string idCustomer)
        {
            try
            {
                foreach (DataGridViewRow row in grdCustomers.Rows)
                {
                    if (row.Cells[(int)eCustomerColumns.IdCustomer].Value.ToString() == idCustomer)
                    {
                        grdCustomers.CurrentCell = row.Cells[(int)eCustomerColumns.IdVer];
                        grdCustomers.FirstDisplayedScrollingRowIndex = row.Index;
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

            ResetCustomerUpdate();
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
                string id = _customerOriginal.IdCustomer;
                _customerOriginal = null;
                ResetCustomerUpdate();
                SetFormBinding();
                tcGeneral.TabPages.Remove(tpForm);
                tcGeneral.TabPages.Add(tpGrid);
                btnNewVersion.Visible = false;
                LoadCustomersList();
                MoveGridToCustomer(id);
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

        private bool IsValidCustomer()
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                    return IsValidModifiedCustomer();
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                    return IsValidCreatedCustomer();

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
        private bool IsValidModifiedCustomer()
        {
            try
            {
                foreach(Control ctl in tpForm.Controls)
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
        private bool IsValidCreatedCustomer()
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
