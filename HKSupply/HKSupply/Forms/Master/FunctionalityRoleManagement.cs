using HKSupply.Exceptions;
using HKSupply.General;
using HKSupply.Models;
using HKSupply.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class FunctionalityRoleManagement : Form, IActionsStackView
    {
        #region Enums
        private enum eFunctionalityRoleColumns
        {
            FunctionalityId,
            RoleId,
            Read,
            New,
            Modify,
            Functionality,
            Role
        }
        #endregion

        #region Private members

        CustomControls.StackView actionsStackView;

        List<Role> _roleList;
        List<Functionality> _functionalityList;

        List<FunctionalityRole> _modifiedFunctionalityRole = new List<FunctionalityRole>();
        List<FunctionalityRole> _createdFunctionalityRole = new List<FunctionalityRole>();
        #endregion

        #region Constructor
        public FunctionalityRoleManagement()
        {
            InitializeComponent();
        }
        #endregion

        #region Action toolbar

        public void actionsStackView_EditButtonClick(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Edit Button");
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
            try
            {
                bool res = false;
                //indicamos que ha dejado de editar el grid, por si modifica una celda y sin salir pulsa sobre guardar
                grdFuncRoles.EndEdit();

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    if (_modifiedFunctionalityRole.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (IsValidModifiedFunctionalitiesRoles())
                        {
                            if (UpdateUsers())
                            {
                                res = true;
                            }
                        }

                    }
                }
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                {
                    if (IsValidCreatedRoles())
                    {
                        if (CreateUser())
                        {
                            res = true;
                        }
                    }
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllFunctionalitiesRoles();
                    ConfigureRolesColorsStyles();
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
                LoadAllFunctionalitiesRoles();
                //SetupFunctionalitiesRolesGrid();
                ConfigureRolesColorsStyles();
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
        private void FunctionalityRoleManagement_Load(object sender, EventArgs e)
        {
            ConfigureActionsStackView();
            LoadRoles();
            LoadFunctionalities();
            SetupFunctionalitiesRolesGrid();
            LoadAllFunctionalitiesRoles();
        }

        

        #region Grid events

        private void grdFuncRoles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    FunctionalityRole tmpFunctionalityRole = new FunctionalityRole();

                    tmpFunctionalityRole.FunctionalityId = (int)grdFuncRoles.Rows[e.RowIndex].Cells[(int)eFunctionalityRoleColumns.FunctionalityId].Value;
                    tmpFunctionalityRole.RoleId = grdFuncRoles.Rows[e.RowIndex].Cells[(int)eFunctionalityRoleColumns.RoleId].Value.ToString();
                    tmpFunctionalityRole.Read = (bool)grdFuncRoles.Rows[e.RowIndex].Cells[(int)eFunctionalityRoleColumns.Read].Value;
                    tmpFunctionalityRole.New = (bool)grdFuncRoles.Rows[e.RowIndex].Cells[(int)eFunctionalityRoleColumns.New].Value;
                    tmpFunctionalityRole.Modify = (bool)grdFuncRoles.Rows[e.RowIndex].Cells[(int)eFunctionalityRoleColumns.Modify].Value;
                    AddModifiedFuncRoleToList(tmpFunctionalityRole);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdFuncRoles_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void grdFuncRoles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //show this message when the user enter incorrect value in a cell
            MessageBox.Show(GlobalSetting.ResManager.GetString("CellDataError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #endregion

        #region Private Methods

        private void SetupFunctionalitiesRolesGrid()
        {
            try
            {
                //Events
                grdFuncRoles.CellValueChanged += grdFuncRoles_CellValueChanged;
                grdFuncRoles.CellValidating += grdFuncRoles_CellValidating;
                grdFuncRoles.DataError +=grdFuncRoles_DataError;

                //Columns

                //adding Functionality Id Combo
                DataGridViewComboBoxColumn columnFunctionalityId = new DataGridViewComboBoxColumn();
                columnFunctionalityId.HeaderText = eFunctionalityRoleColumns.FunctionalityId.ToString();
                columnFunctionalityId.Width = 200;
                columnFunctionalityId.DataPropertyName = eFunctionalityRoleColumns.FunctionalityId.ToString();
                columnFunctionalityId.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                columnFunctionalityId.DataSource = _functionalityList;
                columnFunctionalityId.ValueMember = "FunctionalityId";
                columnFunctionalityId.DisplayMember = "FunctionalityName";

                grdFuncRoles.Columns.Add(columnFunctionalityId);

                //Adding  Role Id Combo
                DataGridViewComboBoxColumn columnRoleId = new DataGridViewComboBoxColumn();
                columnRoleId.HeaderText = eFunctionalityRoleColumns.RoleId.ToString();
                columnRoleId.DataPropertyName = eFunctionalityRoleColumns.RoleId.ToString();
                columnRoleId.Width = 200;
                columnRoleId.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                columnRoleId.DataSource = _roleList;
                columnRoleId.ValueMember = "RoleId";
                columnRoleId.DisplayMember = "Description";

                grdFuncRoles.Columns.Add(columnRoleId);

                //Adding  Read CheckBox
                DataGridViewCheckBoxColumn columnRead = new DataGridViewCheckBoxColumn();
                columnRead.HeaderText = eFunctionalityRoleColumns.Read.ToString();
                columnRead.Width = 60;
                columnRead.DataPropertyName = eFunctionalityRoleColumns.Read.ToString();

                grdFuncRoles.Columns.Add(columnRead);

                //Adding  New CheckBox
                DataGridViewCheckBoxColumn columnNew = new DataGridViewCheckBoxColumn();
                columnNew.HeaderText = eFunctionalityRoleColumns.New.ToString();
                columnNew.Width = 60;
                columnNew.DataPropertyName = eFunctionalityRoleColumns.New.ToString();

                grdFuncRoles.Columns.Add(columnNew);

                //Adding  Modify CheckBox
                DataGridViewCheckBoxColumn columnModify = new DataGridViewCheckBoxColumn();
                columnModify.HeaderText = eFunctionalityRoleColumns.Modify.ToString();
                columnModify.Width = 60;
                columnModify.DataPropertyName = eFunctionalityRoleColumns.Modify.ToString();

                grdFuncRoles.Columns.Add(columnModify);

                //Adding Functionality
                DataGridViewTextBoxColumn columnFunctionality = new DataGridViewTextBoxColumn();
                columnFunctionality.HeaderText = eFunctionalityRoleColumns.Functionality.ToString();
                columnFunctionality.Width = 10;
                columnFunctionality.DataPropertyName = eFunctionalityRoleColumns.Functionality.ToString();
                columnFunctionality.Visible = false;

                grdFuncRoles.Columns.Add(columnFunctionality);

                //Adding  Role
                DataGridViewTextBoxColumn columnRole = new DataGridViewTextBoxColumn();
                columnRole.HeaderText = eFunctionalityRoleColumns.Role.ToString();
                columnRole.Width = 10;
                columnRole.DataPropertyName = eFunctionalityRoleColumns.Role.ToString();
                columnRole.Visible = false;

                grdFuncRoles.Columns.Add(columnRole);

                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureRolesColorsStyles()
        {
            try
            {
                foreach (DataGridViewColumn col in grdFuncRoles.Columns)
                    col.DefaultCellStyle.ForeColor = Color.Black;

                grdFuncRoles.ColumnHeadersDefaultCellStyle.BackColor = AppStyles.EtniaRed;
                grdFuncRoles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grdFuncRoles.EnableHeadersVisualStyles = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadRoles()
        {
            try
            {
                _roleList = GlobalSetting.RoleService.GetRoles(false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadFunctionalities()
        {
            try
            {
                _functionalityList = GlobalSetting.FunctionalityService.GetAllFunctionalities().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LoadAllFunctionalitiesRoles()
        {
            try
            {
                _modifiedFunctionalityRole.Clear();
                _createdFunctionalityRole.Clear();
                IEnumerable<FunctionalityRole> funcRoles = GlobalSetting.FunctionalityRoleService.GetAllFunctionalitiesRole();
                grdFuncRoles.DataSource = funcRoles;
                grdFuncRoles.ReadOnly = true;

                ConfigureRolesColorsStyles();
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
                grdFuncRoles.ReadOnly = false;
                //make columns readonly
                grdFuncRoles.Columns[(int)eFunctionalityRoleColumns.FunctionalityId].ReadOnly = true;
                grdFuncRoles.Columns[(int)eFunctionalityRoleColumns.RoleId].ReadOnly = true;

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
                _createdFunctionalityRole.Add(
                    new FunctionalityRole 
                    { 
                        FunctionalityId = _functionalityList.Select(f => f.FunctionalityId).FirstOrDefault(),
                        RoleId = _roleList.Select(r => r.RoleId).FirstOrDefault(),
                    });
                grdFuncRoles.DataSource = null;
                grdFuncRoles.Rows.Clear();
                SetupFunctionalitiesRolesGrid();
                grdFuncRoles.DataSource = _createdFunctionalityRole;
                grdFuncRoles.ReadOnly = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedFuncRoleToList(FunctionalityRole modifiedFuncRole)
        {
            try
            {
                var funcRole = _modifiedFunctionalityRole.Where(fr => fr.FunctionalityId.Equals(modifiedFuncRole.FunctionalityId) &&
                    fr.RoleId.Equals(modifiedFuncRole.RoleId)).FirstOrDefault();

                if (funcRole == null)
                {
                    _modifiedFunctionalityRole.Add(modifiedFuncRole);
                }
                else
                {
                    funcRole.Read = modifiedFuncRole.Read;
                    funcRole.New = modifiedFuncRole.New;
                    funcRole.Modify = modifiedFuncRole.Modify;
              
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddCreatedFunctionalityRoleToList(FunctionalityRole createdFunctionalityRole)
        {
            try
            {
                var funcRole = _createdFunctionalityRole.FirstOrDefault(fr => fr.FunctionalityId.Equals(createdFunctionalityRole.FunctionalityId) &&
                    fr.RoleId.Equals(createdFunctionalityRole.RoleId));
                if (funcRole == null)
                {
                    _createdFunctionalityRole.Add(createdFunctionalityRole);
                }
                else
                {
                    funcRole.FunctionalityId = createdFunctionalityRole.FunctionalityId;
                    funcRole.RoleId = createdFunctionalityRole.RoleId;
                    funcRole.Read = createdFunctionalityRole.Read;
                    funcRole.New = createdFunctionalityRole.New;
                    funcRole.Modify = createdFunctionalityRole.Modify;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedFunctionalitiesRoles()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidCreatedRoles()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateUsers()
        {
            try
            {
                return GlobalSetting.FunctionalityRoleService.UpdateFunctionalitiesRoles(_modifiedFunctionalityRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateUser()
        {
            try
            {
                GlobalSetting.FunctionalityRoleService.NewFunctionalityRole(_createdFunctionalityRole.FirstOrDefault());
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
