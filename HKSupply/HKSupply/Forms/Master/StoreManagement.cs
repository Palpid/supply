using HKSupply.General;
using HKSupply.Models;
using HKSupply.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class StoreManagement : Form, IActionsStackView
    {
        #region enums
        private enum eStoreColumns
        {
            IdStore,
            Name,
            Active
        }
        #endregion

        #region Private Members
        CustomControls.StackView actionsStackView;

        List<Store> _modifiedStores = new List<Store>();
        List<Store> _createdStores = new List<Store>();
        #endregion

        #region Constructor
        public StoreManagement()
        {
            InitializeComponent();
        }
        #endregion

        #region Action Toolbar

        public void actionsStackView_EditButtonClick(object sender, EventArgs e)
        {
            try
            {
                ConfigureActionsStackViewEditing();
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
                //indicamos que ha dejado de editar el grid, por si modifica una celda y sin salir pulsa sobre guardar
                grdStores.EndEdit();

                if (IsValidStores() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    if (_modifiedStores.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (UpdateStores())
                        {
                            res = true;
                        }
                    }
                }
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                {
                    if (CreateStore())
                    {
                        res = true;
                    }
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllStores();
                    ConfigureRolesGridDefaultStyles();
                    actionsStackView.RestoreInitState();
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
                LoadAllStores();
                SetupStoreGrid();
                actionsStackView.RestoreInitState();
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

        private void StoreManagement_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ConfigureActionsStackView();
                SetupStoreGrid();
                LoadAllStores();
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

        #region Grid Events

        private void grdStores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    Store tmpStore = new Store();
                    tmpStore.IdStore = grdStores.Rows[e.RowIndex].Cells[(int)eStoreColumns.IdStore].Value.ToString();
                    tmpStore.Name = (grdStores.Rows[e.RowIndex].Cells[(int)eStoreColumns.Name].Value ?? string.Empty).ToString();
                    tmpStore.Active = (bool)grdStores.Rows[e.RowIndex].Cells[(int)eStoreColumns.Active].Value;
                    AddModifiedStoresToList(tmpStore);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdStores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)eStoreColumns.Name)
                {
                    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void grdStores_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //show this message when the user enter incorrect value in a cell
            MessageBox.Show(GlobalSetting.ResManager.GetString("CellDataError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #endregion

        #region Private Methods

        private void SetupStoreGrid()
        {
            try
            {
                grdStores.CellValueChanged += grdStores_CellValueChanged;
                grdStores.CellValidating += grdStores_CellValidating;
                grdStores.DataError +=grdStores_DataError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureRolesGridDefaultStyles()
        {
            try
            {
                grdStores.Columns[(int)eStoreColumns.IdStore].Width = 100;
                grdStores.Columns[(int)eStoreColumns.Name].Width = 200;
                grdStores.Columns[(int)eStoreColumns.Active].Width = 100;

                grdStores.Columns[(int)eStoreColumns.IdStore].DefaultCellStyle.ForeColor = Color.Black;

                grdStores.ColumnHeadersDefaultCellStyle.BackColor = AppStyles.EtniaRed;
                grdStores.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grdStores.EnableHeadersVisualStyles = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAllStores()
        {
            try
            {
                _modifiedStores.Clear();
                _createdStores.Clear();
                IEnumerable<Store> stores = GlobalSetting.StoreService.GetAllStores();
                grdStores.DataSource = stores;
                grdStores.ReadOnly = true;

                ConfigureRolesGridDefaultStyles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureActionsStackViewEditing()
        {
            try
            {
                grdStores.ReadOnly = false;
                grdStores.Columns[(int)eStoreColumns.IdStore].ReadOnly = true; //make the id column readonly
                grdStores.Columns[(int)eStoreColumns.IdStore].DefaultCellStyle.ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureActionsStackViewCreating()
        {
            try
            {
                _createdStores.Add(new Store());
                grdStores.DataSource = null;
                grdStores.Rows.Clear();
                grdStores.DataSource = _createdStores;
                grdStores.ReadOnly = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedStoresToList(Store modifiedStore)
        {
            try
            {
                var store = _modifiedStores.FirstOrDefault(s => s.IdStore.Equals(modifiedStore.IdStore));
                if (store == null)
                {
                    _modifiedStores.Add(modifiedStore);
                }
                else
                {
                    store.Name = modifiedStore.Name;
                    store.Active = modifiedStore.Active;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddCreatedStoreToList(Store createdStore)
        {
            try
            {
                var store = _createdStores.FirstOrDefault(s => s.IdStore.Equals(createdStore.IdStore));
                if (store == null)
                {
                    _createdStores.Add(createdStore);
                }
                else
                {
                    store.Name = createdStore.Name;
                    store.Active = createdStore.Active;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidStores()
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                    return IsValidModifiedStores();
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                    return IsValidCreatedStores();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedStores()
        {
            try
            {
                foreach (var store in _modifiedStores)
                {
                    if (string.IsNullOrEmpty(store.Name))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidCreatedStores()
        {
            try
            {
                //Expresión regular, sólo letras (sin la ñ) y números
                Regex val = new Regex("^[A-Z0-9a-z]*$");

                foreach (var store in _createdStores)
                {
                    if (string.IsNullOrEmpty(store.Name) || string.IsNullOrEmpty(store.IdStore))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (val.IsMatch(store.IdStore) == false)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("InvalidId"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateStores()
        {
            try
            {
                return GlobalSetting.StoreService.UpdateStore(_modifiedStores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateStore()
        {
            try
            {
                GlobalSetting.StoreService.NewStore(_createdStores.FirstOrDefault());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


    }
}
