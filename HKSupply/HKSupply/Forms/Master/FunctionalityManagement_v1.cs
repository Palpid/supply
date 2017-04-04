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
    public partial class FunctionalityManagement_v1 : Form, IActionsStackView
    {
        #region Enums
        private enum eFunctionalityColumns
        {
            FunctionalityId,
            FunctionalityName,
            Category,
            FormName
        }
        #endregion

        #region Private members

        CustomControls.StackView actionsStackView;

        List<KeyValuePair<string, string>> _categoryList = new List<KeyValuePair<string, string>>() 
        { 
            new KeyValuePair<string, string>("Masters", "Masters"), 
            new KeyValuePair<string, string>("Others", "Others"),
            new KeyValuePair<string, string>("Help", "Help") 
        };

        List<Functionality> _modifiedFunctionalities = new List<Functionality>();
        List<Functionality> _createdFunctionalities = new List<Functionality>();
        #endregion

        #region Constructor
        public FunctionalityManagement_v1()
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
                grdFunctionalities.EndEdit();

                if (IsValidFunctionalities() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    if (_modifiedFunctionalities.Count() == 0)
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (UpdateRoles())
                        {
                            res = true;
                        }
                    }
                }
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                {
                    if (CreateRol())
                    {
                        res = true;
                    }
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    LoadAllFunctionalities();
                    ConfigureRolesGridStyles();
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
                LoadAllFunctionalities();
                ConfigureRolesGridStyles();
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

                //CustomControls.StackView actionsStackView = new CustomControls.StackView(actions.Read, actions.New, actions.Modify);
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
        private void FunctionalityManagement_Load(object sender, EventArgs e)
        {
            ConfigureActionsStackView();
            SetupFunctionalitiesGrid();
            LoadAllFunctionalities();
        }

        

        #region Grid events

        private void grdFunctionalities_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                {
                    Functionality tmpFunctionality = new Functionality();
                    tmpFunctionality.FunctionalityId = (int)grdFunctionalities.Rows[e.RowIndex].Cells[(int)eFunctionalityColumns.FunctionalityId].Value;
                    tmpFunctionality.FunctionalityName = (grdFunctionalities.Rows[e.RowIndex].Cells[(int)eFunctionalityColumns.FunctionalityName].Value ?? string.Empty).ToString();
                    tmpFunctionality.Category = grdFunctionalities.Rows[e.RowIndex].Cells[(int)eFunctionalityColumns.Category].Value.ToString();
                    tmpFunctionality.FormName = (grdFunctionalities.Rows[e.RowIndex].Cells[(int)eFunctionalityColumns.FormName].Value ?? String.Empty).ToString();
                    AddModifiedFunctionalityToList(tmpFunctionality);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdFunctionalities_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                switch (e.ColumnIndex)
                {
                    case (int)eFunctionalityColumns.FunctionalityName:
                    case (int)eFunctionalityColumns.FormName:
                        if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show(GlobalSetting.ResManager.GetString("FieldRequired"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void grdRoles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //show this message when the user enter incorrect value in a cell
            MessageBox.Show(GlobalSetting.ResManager.GetString("CellDataError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #endregion

        #region Private Methods

        private void SetupFunctionalitiesGrid()
        {
            try
            {
                //Events
                grdFunctionalities.CellValueChanged += grdFunctionalities_CellValueChanged;
                grdFunctionalities.CellValidating += grdFunctionalities_CellValidating;

                //Columns

                //adding Functionality Id TextBox
                DataGridViewTextBoxColumn columnFunctionalityId = new DataGridViewTextBoxColumn();
                columnFunctionalityId.HeaderText = eFunctionalityColumns.FunctionalityId.ToString();
                columnFunctionalityId.Width = 100;
                columnFunctionalityId.DataPropertyName = eFunctionalityColumns.FunctionalityId.ToString();

                grdFunctionalities.Columns.Add(columnFunctionalityId);

                //adding Functionality Name TextBox
                DataGridViewTextBoxColumn columnFunctionalityName = new DataGridViewTextBoxColumn();
                columnFunctionalityName.HeaderText = eFunctionalityColumns.FunctionalityName.ToString();
                columnFunctionalityName.Width = 300;
                columnFunctionalityName.DataPropertyName = eFunctionalityColumns.FunctionalityName.ToString();

                grdFunctionalities.Columns.Add(columnFunctionalityName);

                //adding Category ComboBox
                DataGridViewComboBoxColumn columnCategory = new DataGridViewComboBoxColumn();
                columnCategory.HeaderText = eFunctionalityColumns.Category.ToString();
                columnCategory.Width = 200;
                columnCategory.DataPropertyName = eFunctionalityColumns.Category.ToString();
                columnCategory.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                columnCategory.DataSource = _categoryList;
                columnCategory.ValueMember = "Value";
                columnCategory.DisplayMember = "Key";

                grdFunctionalities.Columns.Add(columnCategory);

                //adding FormName TextBox
                DataGridViewTextBoxColumn columnFormName = new DataGridViewTextBoxColumn();
                columnFormName.HeaderText = eFunctionalityColumns.FormName.ToString();
                columnFormName.Width = 300;
                columnFormName.DataPropertyName = eFunctionalityColumns.FormName.ToString();

                grdFunctionalities.Columns.Add(columnFormName);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureRolesGridStyles()
        {
            try
            {
                grdFunctionalities.Columns[(int)eFunctionalityColumns.FunctionalityId].DefaultCellStyle.ForeColor = Color.Black;
                grdFunctionalities.ColumnHeadersDefaultCellStyle.BackColor = AppStyles.EtniaRed;
                grdFunctionalities.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grdFunctionalities.EnableHeadersVisualStyles = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void LoadAllFunctionalities()
        {
            try
            {
                _modifiedFunctionalities.Clear();
                _createdFunctionalities.Clear();
                IEnumerable<Functionality> appFunctionalities = GlobalSetting.FunctionalityService.GetAllFunctionalities();
                grdFunctionalities.DataSource = appFunctionalities;
                grdFunctionalities.ReadOnly = true;
                
                ConfigureRolesGridStyles();
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
                grdFunctionalities.ReadOnly = false;
                grdFunctionalities.Columns[(int)eFunctionalityColumns.FunctionalityId].ReadOnly = true; //make the id column readonly
                grdFunctionalities.Columns[(int)eFunctionalityColumns.FunctionalityId].DefaultCellStyle.ForeColor = Color.Gray;
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
                _createdFunctionalities.Add(
                    new Functionality 
                    { 
                        Category = _categoryList.Select(c => c.Key).FirstOrDefault()
                    });
                grdFunctionalities.DataSource = null;
                grdFunctionalities.Rows.Clear();
                SetupFunctionalitiesGrid();
                grdFunctionalities.DataSource = _createdFunctionalities;
                grdFunctionalities.ReadOnly = false;
                grdFunctionalities.Columns[(int)eFunctionalityColumns.FunctionalityId].ReadOnly = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedFunctionalityToList(Functionality modifiedFunctionality)
        {
            try
            {
                var functionality = _modifiedFunctionalities.FirstOrDefault(f => f.FunctionalityId.Equals(modifiedFunctionality.FunctionalityId));
                if (functionality == null)
                {
                    _modifiedFunctionalities.Add(modifiedFunctionality);
                }
                else
                {
                    functionality.FunctionalityName = modifiedFunctionality.FunctionalityName;
                    functionality.Category = modifiedFunctionality.Category;
                    functionality.FormName = modifiedFunctionality.FormName;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddCreatedFunctionalityToList(Functionality createdFunctionality)
        {
            try
            {
                var functionality = _createdFunctionalities.FirstOrDefault(f => f.FunctionalityId.Equals(createdFunctionality.FunctionalityId));
                if (functionality == null)
                {
                    _createdFunctionalities.Add(createdFunctionality);
                }
                else
                {
                    functionality.FunctionalityName = createdFunctionality.FunctionalityName;
                    functionality.Category = createdFunctionality.Category;
                    functionality.FormName = createdFunctionality.FormName;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidFunctionalities()
        {
            try
            {
                if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.Edit)
                    return IsValidModifiedFunctionalities();
                else if (actionsStackView.CurrentState == CustomControls.StackView.ToolbarStates.New)
                    return IsValidCreatedFunctionalities();

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidModifiedFunctionalities()
        {
            try
            {
                foreach (var func in _modifiedFunctionalities)
                {
                    if (string.IsNullOrEmpty(func.FunctionalityName) || string.IsNullOrEmpty(func.FormName))
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

        private bool IsValidCreatedFunctionalities()
        {
            try
            {
                foreach (var func in _createdFunctionalities)
                {
                    if (string.IsNullOrEmpty(func.FunctionalityName) || string.IsNullOrEmpty(func.FormName))
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

        private bool UpdateRoles()
        {
            try
            {
                return GlobalSetting.FunctionalityService.UpdateFunctionalities(_modifiedFunctionalities);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CreateRol()
        {

            try
            {
                GlobalSetting.FunctionalityService.NewFunctionality(_createdFunctionalities.FirstOrDefault());
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
