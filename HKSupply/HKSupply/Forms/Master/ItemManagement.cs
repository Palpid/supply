using HKSupply.Helpers;
using HKSupply.General;
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
using CustomControls;

namespace HKSupply.Forms.Master
{
    public partial class ItemManagement : Form, IActionsStackView
    {
        #region Enums

        private enum eItemColumns
        {
            IdVer,
            IdSubVer,
            Timestamp,
            ItemCode,
            ItemName,
            Model,
            Active,
            IdStatus,
            Launched,
            Retired,
            MmFront,
            Size,
            CategoryName,
            Caliber,
        }

        private enum eItemColumnsFilter
        {
            ItemCode,
            ItemName,
            Model,
            Active,
        }

        private enum eColumnsFilterType
        {
            Text = 0,
            CheckBox = 1,
        }

        private Dictionary<string, int> _filterDic = new Dictionary<string, int>() 
        { 
            {eItemColumns.ItemCode.ToString(), (int)eColumnsFilterType.Text },
            {eItemColumns.ItemName.ToString(), (int)eColumnsFilterType.Text },
            {eItemColumns.Model.ToString(), (int)eColumnsFilterType.Text },
            {eItemColumns.Active.ToString(), (int)eColumnsFilterType.CheckBox }
        };

        #endregion

        #region Private Members

        CustomControls.StackView actionsStackView;

        Item _itemUpdate;
        Item _itemOriginal;

        List<Item> _itemsList;

        string[] _nonEditingFields = { "txtItemCode", "txtIdVersion", "txtIdSubversion", "txtTimestamp" };
        string[] _nonCreatingFields = { "txtIdVersion", "txtIdSubversion", "txtTimestamp" };
        int[] _nonCreatingFieldsRows = { 1, 2, 3 };

        bool _sortDescending = true;

        #endregion

        #region Constructor
        public ItemManagement()
        {
            InitializeComponent();
            ResetItemUpdate();
        }
        #endregion

        #region Action toolbar
        public void actionsStackView_EditButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (_itemOriginal == null)
                {
                    MessageBox.Show("No item selected");
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
            try
            {
                bool res = false;
                //El toolstrip no lanza el validate, lo lanzamos a mano por si acaso hay algún elemento que lo tiene pendiente
                Validate();

                if (IsValidItem() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;
                try
                {
                    if (_itemUpdate.Equals(_itemOriginal))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                        return;
                    }

                    if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                    {
                        res = UpdateItem();
                    }
                    else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                    {
                        res = CreateItem();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void actionsStackView_CancelButtonClick(object sender, EventArgs e)
        {
            try
            {
                _itemOriginal = null;
                ResetItemUpdate();
                SetFormBinding();
                tcGeneral.TabPages.Remove(tpForm);
                tcGeneral.TabPages.Add(tpGrid);
                btnNewVersion.Visible = false;
                LoadItemsList();
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

        private void ItemManagement_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigureActionsStackView();
                SetFormBinding();
                tcGeneral.TabPages.Remove(tpForm);
                btnNewVersion.Visible = false;
                LoadComboFilters();

                //TODO Mejor llevarlo a otro lado y meter el texto el resources.
                lblDatesRemarks.Text = "* For dates" + Environment.NewLine + "Press Delete to set empty value";
                ndtpLaunched.ValueChanged += ndtpLaunched_ValueChanged;
                ndtpRetired.ValueChanged += ndtpRetired_ValueChanged;
                
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
                LoadItemsList();
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

                if (_itemUpdate.Equals(_itemOriginal))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (UpdateItem(true))
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

                var type = _filterDic[((eItemColumnsFilter)cmbColFilter.SelectedIndex).ToString()];
                ShowFilterField((eColumnsFilterType)type);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// No queremos la parte de la hora que te pone el control por defecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ndtpRetired_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ndtpRetired.Value != null)
                {
                    DateTime tmp = (DateTime)ndtpRetired.Value;
                    ndtpRetired.Value = new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// No queremos la parte de la hora que te pone el control por defecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ndtpLaunched_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ndtpLaunched.Value != null)
                {
                    DateTime tmp = (DateTime)ndtpLaunched.Value;
                    ndtpLaunched.Value = new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Grid events

        /// <summary>
        /// Cargamos los datos del registro de la fila que se hace doble click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdItems_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                LoadItemForm(grdItems.Rows[e.RowIndex].Cells[(int)eItemColumns.ItemCode].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ordenamos si ha pulsado sobre algún header de columna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 && e.ColumnIndex > -1)
                {
                    LoadItemsSortedList((eItemColumns)e.ColumnIndex);
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
        /// Resetear el objeto item que usamos para la actualización
        /// </summary>
        private void ResetItemUpdate()
        {
            _itemUpdate = new Item();
            //_itemUpdate = new Item
            //{
            //    ItemCode = string.Empty,
            //    ItemName = string.Empty,
            //    Model = string.Empty,
            //    Active = false,
            //    Launched = new DateTime(),
            //    Retired = new DateTime(),
            //    Size = string.Empty,
            //    CategoryName = string.Empty,
            //};
        }

        /// <summary>
        /// Crear los bindings de los campos del formulario
        /// </summary>
        private void SetFormBinding()
        {
            try
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
                    else if (ctl.GetType() == typeof(NullableDateTimePicker))
                    {
                        ctl.DataBindings.Clear();
                        ((NullableDateTimePicker)ctl).Enabled = false;
                    }
                }

                //NullableDateTimePicker
                //Nota: El "OnPropertyChanged" es necesario para que el binding sea en los dos sentidos.
                ndtpLaunched.DataBindings.Add("Value", _itemUpdate, "Launched", true, DataSourceUpdateMode.OnPropertyChanged);
                ndtpRetired.DataBindings.Add("Value", _itemUpdate, "Retired", true, DataSourceUpdateMode.OnPropertyChanged);
                //TextBox
                txtItemCode.DataBindings.Add("Text", _itemUpdate, "ItemCode");
                txtIdVersion.DataBindings.Add("Text", _itemUpdate, "idVer");
                txtIdSubversion.DataBindings.Add("Text", _itemUpdate, "idSubVer");
                txtTimestamp.DataBindings.Add("Text", _itemUpdate, "Timestamp");
                txtItemName.DataBindings.Add("Text", _itemUpdate, "ItemName");
                txtModel.DataBindings.Add("Text", _itemUpdate, "Model");
                txtStatus.DataBindings.Add("Text", _itemUpdate, "IdStatus");
                txtMmFront.DataBindings.Add("Text", _itemUpdate, "MmFront");
                txtSize.DataBindings.Add("Text", _itemUpdate, "Size");
                txtCategoryName.DataBindings.Add("Text", _itemUpdate, "CategoryName");
                txtCaliber.DataBindings.Add("Text", _itemUpdate, "Caliber");
                //CheckBox
                chkActive.DataBindings.Add("Checked", _itemUpdate, "Active");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar el combo con los campos que se puede filtrar
        /// </summary>
        private void LoadComboFilters()
        {
            try
            {
                cmbColFilter.DataSource = Enum.GetNames(typeof(eItemColumnsFilter));
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
        private void ShowFilterField(eColumnsFilterType type)
        {
            try
            {
                txtFilter.Location = new Point(223, 14);
                chkFilter.Location = new Point(223, 18);

                switch (type)
                {
                    case eColumnsFilterType.Text:
                        txtFilter.Visible = true;
                        chkFilter.Visible = false;
                        break;
                    case eColumnsFilterType.CheckBox:
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
        private void LoadItemsList()
        {
            try
            {
                _itemsList = GlobalSetting.ItemService.GetItems();

                //Filtramos si es necesario
                //MRM.Notes: Ahora obtenemos todo de la bbdd y sobre esa colección filtramos. Si al final se implanta esto se tendría que hacer la llamada
                //a la bbdd con las condiciones y que la propia consulta filtre
                //Columnas tipo Texto
                if ((eItemColumnsFilter)cmbColFilter.SelectedIndex != eItemColumnsFilter.Active &&
                    cmbColFilter.SelectedIndex > -1 && string.IsNullOrEmpty(txtFilter.Text) == false)
                {
                    //Lo pasamos antes a minúsculas todo, ya que el Contains es case sensitive 
                    _itemsList = _itemsList.Where(cmbColFilter.SelectedItem.ToString() + ".ToLower().Contains(@0)", txtFilter.Text.ToLower()).ToList();
                }
                //Columnas bit
                if ((eItemColumnsFilter)cmbColFilter.SelectedIndex == eItemColumnsFilter.Active)
                {
                    _itemsList = _itemsList.Where(cmbColFilter.SelectedItem.ToString() + " = @0", chkFilter.Checked).ToList();
                }

                grdItems.CellDoubleClick += grdItems_CellDoubleClick;
                grdItems.CellClick += grdItems_CellClick;
                grdItems.DataSource = null;
                grdItems.Rows.Clear();
                grdItems.DataSource = _itemsList;
                grdItems.ReadOnly = true;
                //Para poder cambiar el color del header cuando se ordena hay que desactivar los efectos visuales del header que coge por defecto
                grdItems.EnableHeadersVisualStyles = false;

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
        private void LoadItemsSortedList(eItemColumns sortedColumn)
        {
            try
            {
                string order = _sortDescending ? " ASC" : " DESC";
                _itemsList = _itemsList.OrderBy(sortedColumn.ToString() + order).ToList();

                _sortDescending = !_sortDescending; //See Remarks

                grdItems.DataSource = null;
                grdItems.Rows.Clear();
                grdItems.DataSource = _itemsList;
                grdItems.ReadOnly = true;
                grdItems.Columns[(int)sortedColumn].HeaderCell.Style.BackColor = Color.Tomato;
                grdItems.Columns[(int)sortedColumn].HeaderText += "*";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar los datos de un customer en concreto
        /// </summary>
        /// <param name="itemCode"></param>
        private void LoadItemForm(string itemCode)
        {
            try
            {
                if (tcGeneral.TabPages.Contains(tpForm) == false)
                    tcGeneral.TabPages.Add(tpForm);
                tcGeneral.SelectedTab = tpForm;

                _itemUpdate = GlobalSetting.ItemService.GetItemByItemCode(itemCode);
                _itemOriginal = _itemUpdate.Clone();
                SetFormBinding();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizar los datos de un item
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateItem(bool newVersion = false)
        {
            try
            {
                return GlobalSetting.ItemService.UpdateItem(_itemUpdate, newVersion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crear un nuevo item
        /// </summary>
        /// <returns></returns>
        private bool CreateItem()
        {
            try
            {
                _itemOriginal = _itemUpdate.Clone();
                return GlobalSetting.ItemService.newItem(_itemUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mover la celda activa a la de un customer en concreto
        /// </summary>
        /// <param name="itemCode"></param>
        private void MoveGridToItem(string itemCode)
        {
            try
            {
                foreach (DataGridViewRow row in grdItems.Rows)
                {
                    if (row.Cells[(int)eItemColumns.ItemCode].Value.ToString() == itemCode)
                    {
                        grdItems.CurrentCell = row.Cells[(int)eItemColumns.IdVer];
                        grdItems.FirstDisplayedScrollingRowIndex = row.Index;
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

            ResetItemUpdate();
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
                        else if (ctl.GetType() == typeof(NullableDateTimePicker))
                        {
                            ((NullableDateTimePicker)ctl).Enabled = true;
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
                        else if (ctl.GetType() == typeof(NullableDateTimePicker))
                        {
                            ((NullableDateTimePicker)ctl).Enabled = true;
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
                string itemCode = _itemOriginal.ItemCode;
                _itemOriginal = null;
                ResetItemUpdate();
                SetFormBinding();
                tcGeneral.TabPages.Remove(tpForm);
                tcGeneral.TabPages.Add(tpGrid);
                btnNewVersion.Visible = false;
                LoadItemsList();
                MoveGridToItem(itemCode);
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

        private bool IsValidItem()
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                    return IsValidModifiedItem();
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                    return IsValidCreatedItem();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// TODO Implementar las validaciones reales.
        /// </summary>
        /// <returns></returns>
        private bool IsValidModifiedItem()
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
        /// TODO Implementar las validaciones reales.
        /// </summary>
        /// <returns></returns>
        private bool IsValidCreatedItem()
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
