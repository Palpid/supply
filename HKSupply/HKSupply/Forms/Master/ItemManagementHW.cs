using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using HKSupply.General;
using HKSupply.Helpers;
using HKSupply.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Forms.Master
{
    public partial class ItemManagementHW : RibbonFormBase
    {
        #region Enums
        private enum eItemColumns
        {
            IdVer,
            IdSubVer,
            Timestamp,
            IdDefaultSupplier,
            IdPrototype,
            Prototype,
            IdModel,
            Model,
            Caliber,
            IdColor1,
            IdColor2,
            IdItemBcn,
            IdItemHK,
            ItemDescription,
            IdHwTypeL1,
            IdHwTypeL2,
            IdHwTypeL3,
            Comments,
            LaunchDate,
            RemovalDate,
            IdStatusCial,
            IdStatusProd,
            IdUserAttri1,
            IdUserAttri2,
            IdUserAttri3,
        }

        private enum eItemDocColumns
        {
            IdVerItem,
            IdSubVerItem,
            IdDocType,
            DocType,
            FileName,
            FilePath,
            CreateDate,
            View,
        }

        #endregion

        #region Private Members

        ItemHw _itemUpdate;
        ItemHw _itemOriginal;
        ItemHwHistory _itemHistory;

        List<ItemHw> _itemsList;
        List<ItemHwHistory> _itemsHistoryList;
        List<Supplier> _supplierList;
        List<StatusHK> _statusProdList;
        List<UserAttrDescription> _userAttrDescriptionList;
        List<DocType> _docsTypeList;
        List<ItemDoc> _itemDocsList;
        List<ItemDoc> _itemLastDocsList;

        string[] _editingFields = { "lueIdDefaultSupplier", "lueIdStatusProd", "txtIdUserAttri1", "txtIdUserAttri2", "txtIdUserAttri3" };

        int _currentHistoryNumList;
        bool _itemImageChanged = false;

        #endregion

        #region Constructor
        public ItemManagementHW()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                SetUpGrdItems();
                SetUpGrdLastDocs();
                SetUpGrdDocsHistory();
                SetUpTexEdit();
                SetUpLueDefaultSupplier();
                SetUpLueStatusProd();
                SetUpLueDocType();
                SetUpLabelNameUserAttributes();
                SetUpPictureEditItemImage();
                ResetItemUpdate();
                SetFormBinding();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Ribbon
        private void ConfigureRibbonActions()
        {
            try
            {
                var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));
                Read = actions.Read;
                New = actions.New;
                Modify = actions.Modify;
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);

            try
            {
                _itemOriginal = null;
                ResetItemUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpDocs.PageVisible = false;
                xtpList.PageVisible = true;
                peItemImage.Properties.ShowMenu = false;
                LoadItemsList();
                SetNonCreatingFieldsVisibility(LayoutVisibility.Always);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                if (_itemOriginal == null)
                {
                    MessageBox.Show("No item selected");
                    RestoreInitState();
                }
                else
                {
                    ConfigureRibbonActionsEditing();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiNew_ItemClick(sender, e);
        }

        public override void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);
            try
            {
                bool res = false;

                if (IsValidItem() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (_itemUpdate.Equals(_itemOriginal) && string.IsNullOrEmpty(txtPathNewDoc.Text))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (CurrentState == ActionsStates.Edit)
                {
                    if (string.IsNullOrEmpty(txtPathNewDoc.Text))
                        res = UpdateItem();
                    else
                        res = UpdateItemWithDoc();
                }

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    ActionsAfterCU();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Form Events
        private void ItemManagementHW_Load(object sender, EventArgs e)
        {
            try
            {
                xtpForm.PageVisible = false;
                xtpDocs.PageVisible = false;
                SetUpLueStatusProd();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewItems_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                ItemHw item = view.GetRow(view.FocusedRowHandle) as ItemHw;
                if (item != null)
                {
                    LoadItemForm(item);
                    LoadItemHistory();
                    LoadItemDocs();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbLoad_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                LoadItemsList();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        void lueIdDefaultSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Delete)
                {
                    lueIdDefaultSupplier.EditValue = null;
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbForward_Click(object sender, EventArgs e)
        {
            try
            {
                SetCurrentItemHistory(_currentHistoryNumList + 1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sbBackward_Click(object sender, EventArgs e)
        {
            try
            {
                SetCurrentItemHistory(_currentHistoryNumList - 1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void repButtonHistDoc_Click(object sender, EventArgs e)
        {
            try
            {
                ItemDoc itemDoc = gridViewDocsHistory.GetRow(gridViewDocsHistory.FocusedRowHandle) as ItemDoc;

                if (itemDoc != null)
                {
                    DocHelper.OpenDoc(Constants.DOCS_PATH + itemDoc.FilePath);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void repButtonLastDoc_Click(object sender, EventArgs e)
        {
            try
            {
                ItemDoc itemDoc = gridViewLastDocs.GetRow(gridViewDocsHistory.FocusedRowHandle) as ItemDoc;

                if (itemDoc != null)
                {
                    DocHelper.OpenDoc(Constants.DOCS_PATH + itemDoc.FilePath);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void sbViewNewDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPathNewDoc.Text) == false)
                {
                    DocHelper.OpenDoc(txtPathNewDoc.Text);
                }
                else
                {
                    XtraMessageBox.Show("No file selected", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void sbOpenFileNewDoc_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|JPG files(*.jpg)|*.jpg|PNG files (*.png)|*.png";
                openFileDialog.Multiselect = false;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtPathNewDoc.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Resetear el objeto item que usamos para la actualización
        /// </summary>
        private void ResetItemUpdate()
        {
            _itemUpdate = new ItemHw();
        }

        private void SetUpGrdItems()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewItems.OptionsView.ColumnAutoWidth = false;
                rootGridViewItems.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                rootGridViewItems.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdVer = new GridColumn() { Caption = "Version Id", Visible = true, FieldName = eItemColumns.IdVer.ToString(), Width = 70 };
                GridColumn colIdSubVer = new GridColumn() { Caption = "Subversion Id", Visible = true, FieldName = eItemColumns.IdSubVer.ToString(), Width = 85 };
                GridColumn colTimestamp = new GridColumn() { Caption = "Timestamp", Visible = true, FieldName = eItemColumns.Timestamp.ToString(), Width = 130 };
                GridColumn colIdDefaultSupplier = new GridColumn() { Caption = "Default Supplier", Visible = true, FieldName = eItemColumns.IdDefaultSupplier.ToString(), Width = 110 };
                GridColumn colIdPrototype = new GridColumn() { Caption = "Id Prototype", Visible = true, FieldName = eItemColumns.IdPrototype.ToString(), Width = 150 };

                GridColumn colPrototypeName = new GridColumn() { Caption = "Prototype Name", Visible = true, FieldName = eItemColumns.Prototype.ToString() + ".PrototypeName", Width = 150 };
                GridColumn colPrototypeDescription = new GridColumn() { Caption = "Prototype Description", Visible = true, FieldName = eItemColumns.Prototype.ToString() + ".PrototypeDescription", Width = 150 };
                GridColumn colPrototypeStatus = new GridColumn() { Caption = "Prototype Status", Visible = true, FieldName = eItemColumns.Prototype.ToString() + ".PrototypeStatus", Width = 150 };
                
                GridColumn colIdModel = new GridColumn() { Caption = "Id Model", Visible = false, FieldName = eItemColumns.IdModel.ToString(), Width = 0 };
                GridColumn colModel = new GridColumn() { Caption = "Model", Visible = true, FieldName = eItemColumns.Model.ToString() + ".Description", Width = 120 };
                GridColumn colCaliber = new GridColumn() { Caption = "Caliber", Visible = true, FieldName = eItemColumns.Caliber.ToString(), Width = 70 };
                GridColumn colIdColor1 = new GridColumn() { Caption = "Color 1", Visible = true, FieldName = eItemColumns.IdColor1.ToString(), Width = 60 };
                GridColumn colIdColor2 = new GridColumn() { Caption = "Color 2", Visible = true, FieldName = eItemColumns.IdColor2.ToString(), Width = 60 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = "Item BCN", Visible = true, FieldName = eItemColumns.IdItemBcn.ToString(), Width = 160 };
                GridColumn colIdItemHK = new GridColumn() { Caption = "Item HK", Visible = true, FieldName = eItemColumns.IdItemHK.ToString(), Width = 160 };
                GridColumn colItemDescription = new GridColumn() { Caption = "Item Description", Visible = true, FieldName = eItemColumns.ItemDescription.ToString(), Width = 300 };
                GridColumn colIdHwTypeL1 = new GridColumn() { Caption = "Hw Type L1", Visible = true, FieldName = eItemColumns.IdHwTypeL1.ToString(), Width = 100 };
                GridColumn colIdHwTypeL2 = new GridColumn() { Caption = "Hw Type L2", Visible = true, FieldName = eItemColumns.IdHwTypeL2.ToString(), Width = 100 };
                GridColumn colIdHwTypeL3 = new GridColumn() { Caption = "Hw Type L3", Visible = true, FieldName = eItemColumns.IdHwTypeL3.ToString(), Width = 100 };

                GridColumn colComments = new GridColumn() { Caption = "Comments", Visible = true, FieldName = eItemColumns.Comments.ToString(), Width = 300 }; //No aparece?

                GridColumn colLaunchedDate = new GridColumn() { Caption = "Launch Date", Visible = true, FieldName = eItemColumns.LaunchDate.ToString(), Width = 90 };
                GridColumn colRemovalDate = new GridColumn() { Caption = "Removal Date", Visible = true, FieldName = eItemColumns.RemovalDate.ToString(), Width = 90 };
                GridColumn colIdStatusCial = new GridColumn() { Caption = "Status Cial", Visible = true, FieldName = eItemColumns.IdStatusCial.ToString(), Width = 90 };
                GridColumn colIdStatusProd = new GridColumn() { Caption = "Status Prod", Visible = true, FieldName = eItemColumns.IdStatusProd.ToString(), Width = 90 };
                GridColumn colIdUserAttri1 = new GridColumn() { Caption = "User Attri. 1", Visible = true, FieldName = eItemColumns.IdUserAttri1.ToString(), Width = 90 };
                GridColumn colIdUserAttri2 = new GridColumn() { Caption = "User Attri. 2", Visible = true, FieldName = eItemColumns.IdUserAttri2.ToString(), Width = 90 };
                GridColumn colIdUserAttri3 = new GridColumn() { Caption = "User Attri. 3", Visible = true, FieldName = eItemColumns.IdUserAttri3.ToString(), Width = 90 };


                //Display Format
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                colCaliber.DisplayFormat.FormatType = FormatType.Numeric;
                colCaliber.DisplayFormat.FormatString = "n2";

                //Add columns to grid root view
                rootGridViewItems.Columns.Add(colIdVer);
                rootGridViewItems.Columns.Add(colIdSubVer);
                rootGridViewItems.Columns.Add(colTimestamp);
                rootGridViewItems.Columns.Add(colIdDefaultSupplier);
                rootGridViewItems.Columns.Add(colIdPrototype);
                rootGridViewItems.Columns.Add(colPrototypeName);
                rootGridViewItems.Columns.Add(colPrototypeDescription);
                rootGridViewItems.Columns.Add(colPrototypeStatus);
                rootGridViewItems.Columns.Add(colIdModel);
                rootGridViewItems.Columns.Add(colModel);
                rootGridViewItems.Columns.Add(colCaliber);
                rootGridViewItems.Columns.Add(colIdColor1);
                rootGridViewItems.Columns.Add(colIdColor2);
                rootGridViewItems.Columns.Add(colIdItemBcn);
                rootGridViewItems.Columns.Add(colIdItemHK);
                rootGridViewItems.Columns.Add(colItemDescription);
                rootGridViewItems.Columns.Add(colIdHwTypeL1);
                rootGridViewItems.Columns.Add(colIdHwTypeL2);
                rootGridViewItems.Columns.Add(colIdHwTypeL3);
                rootGridViewItems.Columns.Add(colComments);
                rootGridViewItems.Columns.Add(colLaunchedDate);
                rootGridViewItems.Columns.Add(colRemovalDate);
                rootGridViewItems.Columns.Add(colIdStatusCial);
                rootGridViewItems.Columns.Add(colIdStatusProd);
                rootGridViewItems.Columns.Add(colIdUserAttri1);
                rootGridViewItems.Columns.Add(colIdUserAttri2);
                rootGridViewItems.Columns.Add(colIdUserAttri3);

                //Events
                rootGridViewItems.DoubleClick += rootGridViewItems_DoubleClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdLastDocs()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewLastDocs.OptionsView.ColumnAutoWidth = false;
                gridViewLastDocs.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                //gridViewLastDocs.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdVerItem = new GridColumn() { Caption = "Item Ver", Visible = true, FieldName = eItemDocColumns.IdVerItem.ToString(), Width = 60 };
                GridColumn colIdSubVerItem = new GridColumn() { Caption = "Item Subver", Visible = true, FieldName = eItemDocColumns.IdSubVerItem.ToString(), Width = 75 };
                GridColumn colIdDocType = new GridColumn() { Caption = "IdDocType", Visible = false, FieldName = eItemDocColumns.IdDocType.ToString(), Width = 10 };
                GridColumn colDocType = new GridColumn() { Caption = "Doc Type", Visible = true, FieldName = eItemDocColumns.DocType.ToString() + ".Description", Width = 100 };
                GridColumn colFileName = new GridColumn() { Caption = "File Name", Visible = true, FieldName = eItemDocColumns.FileName.ToString(), Width = 250 };
                GridColumn colFilePath = new GridColumn() { Caption = "FilePath", Visible = false, FieldName = eItemDocColumns.FilePath.ToString(), Width = 10 };
                GridColumn colCreateDate = new GridColumn() { Caption = "Create Date", Visible = true, FieldName = eItemDocColumns.CreateDate.ToString(), Width = 150 };
                GridColumn colViewButton = new GridColumn() { Caption = "View", Visible = true, FieldName = eItemDocColumns.View.ToString(), Width = 50 };

                //Display Format
                colCreateDate.DisplayFormat.FormatType = FormatType.DateTime;

                //View Button
                colViewButton.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repButtonLastDoc = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                repButtonLastDoc.Name = "btnViewLastDoc";
                repButtonLastDoc.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                repButtonLastDoc.Click += repButtonLastDoc_Click;
                colViewButton.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                colViewButton.ColumnEdit = repButtonLastDoc;

                //Only button allow to edit to allow click
                colIdVerItem.OptionsColumn.AllowEdit = false;
                colIdSubVerItem.OptionsColumn.AllowEdit = false;
                colIdDocType.OptionsColumn.AllowEdit = false;
                colDocType.OptionsColumn.AllowEdit = false;
                colFileName.OptionsColumn.AllowEdit = false;
                colFilePath.OptionsColumn.AllowEdit = false;
                colCreateDate.OptionsColumn.AllowEdit = false;
                colViewButton.OptionsColumn.AllowEdit = true;

                //Add columns to grid root view
                gridViewLastDocs.Columns.Add(colIdVerItem);
                gridViewLastDocs.Columns.Add(colIdSubVerItem);
                gridViewLastDocs.Columns.Add(colIdDocType);
                gridViewLastDocs.Columns.Add(colDocType);
                gridViewLastDocs.Columns.Add(colFileName);
                gridViewLastDocs.Columns.Add(colFilePath);
                gridViewLastDocs.Columns.Add(colCreateDate);
                gridViewLastDocs.Columns.Add(colViewButton);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpGrdDocsHistory()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                gridViewDocsHistory.OptionsView.ColumnAutoWidth = false;
                gridViewDocsHistory.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                //gridViewDocsHistory.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdVerItem = new GridColumn() { Caption = "Item Ver", Visible = true, FieldName = eItemDocColumns.IdVerItem.ToString(), Width = 60 };
                GridColumn colIdSubVerItem = new GridColumn() { Caption = "Item Subver", Visible = true, FieldName = eItemDocColumns.IdSubVerItem.ToString(), Width = 75 };
                GridColumn colIdDocType = new GridColumn() { Caption = "IdDocType", Visible = false, FieldName = eItemDocColumns.IdDocType.ToString(), Width = 10 };
                GridColumn colDocType = new GridColumn() { Caption = "Doc Type", Visible = true, FieldName = eItemDocColumns.DocType.ToString() + ".Description", Width = 100 };
                GridColumn colFileName = new GridColumn() { Caption = "File Name", Visible = true, FieldName = eItemDocColumns.FileName.ToString(), Width = 280 };
                GridColumn colFilePath = new GridColumn() { Caption = "FilePath", Visible = false, FieldName = eItemDocColumns.FilePath.ToString(), Width = 10 };
                GridColumn colCreateDate = new GridColumn() { Caption = "Create Date", Visible = true, FieldName = eItemDocColumns.CreateDate.ToString(), Width = 120 };
                GridColumn colViewButton = new GridColumn() { Caption = "View", Visible = true, FieldName = eItemDocColumns.View.ToString(), Width = 50 };

                //Display Format
                colCreateDate.DisplayFormat.FormatType = FormatType.DateTime;

                //View Button
                colViewButton.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repButtonHistDoc = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                repButtonHistDoc.Name = "btnViewHistoryDoc";
                repButtonHistDoc.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                repButtonHistDoc.Click += repButtonHistDoc_Click;
                colViewButton.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                colViewButton.ColumnEdit = repButtonHistDoc;

                //Only button allow to edit to allow click
                colIdVerItem.OptionsColumn.AllowEdit = false;
                colIdSubVerItem.OptionsColumn.AllowEdit = false;
                colIdDocType.OptionsColumn.AllowEdit = false;
                colDocType.OptionsColumn.AllowEdit = false;
                colFileName.OptionsColumn.AllowEdit = false;
                colFilePath.OptionsColumn.AllowEdit = false;
                colCreateDate.OptionsColumn.AllowEdit = false;
                colViewButton.OptionsColumn.AllowEdit = true;

                //Add columns to grid root view
                gridViewDocsHistory.Columns.Add(colIdVerItem);
                gridViewDocsHistory.Columns.Add(colIdSubVerItem);
                gridViewDocsHistory.Columns.Add(colIdDocType);
                gridViewDocsHistory.Columns.Add(colDocType);
                gridViewDocsHistory.Columns.Add(colFileName);
                gridViewDocsHistory.Columns.Add(colFilePath);
                gridViewDocsHistory.Columns.Add(colCreateDate);
                gridViewDocsHistory.Columns.Add(colViewButton);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpTexEdit()
        {
            try
            {
                txtLaunchDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                txtLaunchDate.Properties.Mask.EditMask = "dd/MM/yyyy";
                txtLaunchDate.Properties.Mask.UseMaskAsDisplayFormat = true;

                txtRemovalDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                txtRemovalDate.Properties.Mask.EditMask = "dd/MM/yyyy";
                txtRemovalDate.Properties.Mask.UseMaskAsDisplayFormat = true;

                //History
                txtHLaunchDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                txtHLaunchDate.Properties.Mask.EditMask = "dd/MM/yyyy";
                txtHLaunchDate.Properties.Mask.UseMaskAsDisplayFormat = true;

                txtHRemovalDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                txtHRemovalDate.Properties.Mask.EditMask = "dd/MM/yyyy";
                txtHRemovalDate.Properties.Mask.UseMaskAsDisplayFormat = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetFormBinding()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((TextEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(CheckEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((CheckEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(DateEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((DateEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((LookUpEdit)ctl).ReadOnly = true;
                    }
                }

                //para evitar problemas al bindear nested properties
                if (_itemUpdate.Model == null) _itemUpdate.Model = new Model();
                if (_itemUpdate.Prototype == null) _itemUpdate.Prototype = new Prototype(); 

                //TextEdit
                txtIdVersion.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdVer);
                txtIdSubversion.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdSubVer);
                txtTimestamp.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.Timestamp);
                txtIdPrototype.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdPrototype);
                txtPrototypeName.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeName");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtPrototypeDescription.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeDescription");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtPrototypeStatus.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeStatus");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtModel.DataBindings.Add("Text", _itemUpdate, "Model.Description");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtIdColor1.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdColor1);
                txtIdColor2.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdColor2);
                txtIdItemBcn.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdItemBcn);
                txtIdItemHK.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdItemHK);
                txtItemDescription.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.ItemDescription);

                txtIdHwTypeL1.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdHwTypeL1);
                txtIdHwTypeL2.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdHwTypeL2);
                txtIdHwTypeL3.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdHwTypeL3);

                txtComments.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.Comments);
                txtLaunchDate.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.LaunchDate);
                txtRemovalDate.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.RemovalDate);
                txtIdStatusCial.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdStatusCial);
                txtUnit.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.Unit);
                txtDocsLink.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.DocsLink);
                txtCreateDate.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.CreateDate);

                txtIdUserAttri1.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri1);
                txtIdUserAttri2.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri2);
                txtIdUserAttri3.DataBindings.Add<ItemHw>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri3);

                //LookUpEdit
                lueIdDefaultSupplier.DataBindings.Add<ItemHw>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdDefaultSupplier);
                lueIdStatusProd.DataBindings.Add<ItemHw>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdStatusProd);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetHistoryBinding()
        {
            try
            {
                foreach (Control ctl in layoutControlHistory.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((TextEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(CheckEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((CheckEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(DateEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((DateEdit)ctl).ReadOnly = true;
                    }
                    else if (ctl.GetType() == typeof(LookUpEdit))
                    {
                        ctl.DataBindings.Clear();
                        ((LookUpEdit)ctl).ReadOnly = true;
                    }
                }

                //TextEdit
                txtHIdVersion.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdVer);
                txtHIdSubversion.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdSubVer);
                txtHTimestamp.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.Timestamp);
                txtHIdPrototype.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdPrototype);
                txtHModel.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdModel);
                txtHIdColor1.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdColor1);
                txtHIdColor2.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdColor2);
                txtHIdItemBcn.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdItemBcn);
                txtHIdItemHK.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdItemHK);
                txtHItemDescription.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.ItemDescription);
                txtHIdHwTypeL1.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdHwTypeL1);
                txtHIdHwTypeL2.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdHwTypeL1);
                txtHIdHwTypeL3.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdHwTypeL1);
                txtHComments.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.Comments);
                txtHLaunchDate.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.LaunchDate);
                txtHRemovalDate.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.RemovalDate);
                txtHIdStatusCial.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdStatusCial);
                txtHUnit.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.Unit);
                txtHDocsLink.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.DocsLink);
                txtHCreateDate.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.CreateDate);

                txtHIdUserAttri1.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri1);
                txtHIdUserAttri2.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri2);
                txtHIdUserAttri3.DataBindings.Add<ItemHwHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri3);

                //LookUpEdit
                lueHIdDefaultSupplier.DataBindings.Add<ItemHwHistory>(_itemHistory, (LookUpEdit e) => e.EditValue, item => item.IdDefaultSupplier);
                lueHIdStatusProd.DataBindings.Add<ItemHwHistory>(_itemHistory, (LookUpEdit e) => e.EditValue, item => item.IdStatusProd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Cargar la colección de suppliers del sistema
        /// </summary>
        private void SetUpLueDefaultSupplier()
        {
            try
            {
                _supplierList = GlobalSetting.SupplierService.GetSuppliers();

                lueIdDefaultSupplier.Properties.DataSource = _supplierList;
                lueIdDefaultSupplier.Properties.DisplayMember = "IdSupplier";
                lueIdDefaultSupplier.Properties.ValueMember = "IdSupplier";
                lueIdDefaultSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdSupplier", 20, "Id Supplier"));
                lueIdDefaultSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", 100, "Name"));

                //De esta manera se activa el limpiar el combo pulsado Ctrl + Supr. Es poco intuitivo, lo controlamos por el evento
                //lueIdDefaultSupplier.Properties.AllowNullInput = DefaultBoolean.True; 
                lueIdDefaultSupplier.KeyDown += lueIdDefaultSupplier_KeyDown;

                //History
                lueHIdDefaultSupplier.Properties.DataSource = _supplierList;
                lueHIdDefaultSupplier.Properties.DisplayMember = "IdSupplier";
                lueHIdDefaultSupplier.Properties.ValueMember = "IdSupplier";
                lueHIdDefaultSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdSupplier", 20, "Id Supplier"));
                lueHIdDefaultSupplier.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", 100, "Name"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar la colección de Status Prod. del sistema y configurar el lookupEdit correspondiente
        /// </summary>
        private void SetUpLueStatusProd()
        {
            try
            {
                _statusProdList = GlobalSetting.StatusProdService.GetStatusProd();

                lueIdStatusProd.Properties.DataSource = _statusProdList;
                lueIdStatusProd.Properties.DisplayMember = "Description";
                lueIdStatusProd.Properties.ValueMember = "IdStatusProd";
                //lueIdStatusProd.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdSupplier", 20, "Id Supplier"));
                //lueIdStatusProd.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", 100, "Name"));

                lueHIdStatusProd.Properties.DataSource = _statusProdList;
                lueHIdStatusProd.Properties.DisplayMember = "Description";
                lueHIdStatusProd.Properties.ValueMember = "IdStatusProd";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpLueDocType()
        {
            try
            {
                _docsTypeList = GlobalSetting.DocTypeService.GetDocsType(Constants.ITEM_GROUP_HW);

                lueDocType.Properties.DataSource = _docsTypeList;
                lueDocType.Properties.DisplayMember = "Description";
                lueDocType.Properties.ValueMember = "IdDocType";
                lueDocType.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", 100, "Description"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpLabelNameUserAttributes()
        {
            try
            {
                _userAttrDescriptionList = GlobalSetting.UserAttrDescriptionService.GetUserAttrsDescription(Constants.ITEM_GROUP_HW);

                //TODO: hacer esto de una manera un poco mas elegante
                lciIdUserAttri1.Text = lciHIdUserAttri1.Text = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals("HWATTR01")).Select(a => a.Description).SingleOrDefault();
                lciIdUserAttri2.Text = lciHIdUserAttri2.Text =_userAttrDescriptionList.Where(u => u.IdUserAttr.Equals("HWATTR02")).Select(a => a.Description).SingleOrDefault();
                lciIdUserAttri3.Text = lciHIdUserAttri3.Text = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals("HWATTR03")).Select(a => a.Description).SingleOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetUpPictureEditItemImage()
        {
            try
            {
                //Quitamos el menú contextual. Sólo estará disponble en edición
                peItemImage.Properties.ShowMenu = false;
                //Events
                peItemImage.EditValueChanged += peItemImage_EditValueChanged;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void peItemImage_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _itemImageChanged = (peItemImage.EditValue == null ? false : true );
                Bitmap bmp = peItemImage.EditValue as Bitmap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// cargar la colección de Items del sistema
        /// </summary>
        /// <remarks></remarks>
        private void LoadItemsList()
        {
            try
            {
                _itemsList = GlobalSetting.ItemHwService.GetItems();
                xgrdItems.DataSource = _itemsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargar los datos de un customer en concreto
        /// </summary>
        /// <param name="item"></param>
        private void LoadItemForm(ItemHw item)
        {
            try
            {
                _itemUpdate = item.Clone();
                _itemOriginal = _itemUpdate.Clone();
                SetFormBinding(); //refresh binding
                xtpForm.PageVisible = true;
                xtcGeneral.SelectedTabPage = xtpForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadItemHistory()
        {
            try
            {
                _itemsHistoryList = GlobalSetting.ItemHwService.GetItemHwHistory(_itemUpdate.IdItemBcn);
                SetCurrentItemHistory(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetCurrentItemHistory(int historyNum)
        {
            try
            {
                if (historyNum >= 0 && historyNum < _itemsHistoryList.Count())
                {
                    _itemHistory = _itemsHistoryList[historyNum];
                    SetHistoryBinding();
                    _currentHistoryNumList = historyNum;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadItemDocs()
        {
            try
            {
                _itemDocsList = GlobalSetting.ItemDocService.GetItemsDocs(_itemUpdate.IdItemBcn, Constants.ITEM_GROUP_HW);
                xgrdDocsHistory.DataSource = _itemDocsList;

                _itemLastDocsList = GlobalSetting.ItemDocService.GetLastItemsDocs(_itemUpdate.IdItemBcn, Constants.ITEM_GROUP_HW);
                xgrdLastDocs.DataSource = _itemLastDocsList;

                xtpDocs.PageVisible = true;
                gbNewDoc.Enabled = false;

                //Item Image
                //Quitamos el menú contextual ya que no nos interesa en este momento
                //peItemImage.Properties.ShowMenu = false;
                ////Cargamos la imagen de la cache.
                //AddToPhotoCache(_itemUpdate.PhotoUrl);
                //Bitmap im = null;
                //im = photosCache[_itemUpdate.PhotoUrl];
                //peItemImage.Image = im;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                xtpList.PageVisible = true;
                gbNewDoc.Enabled = true;
                SetEditingFieldsEnabled();
                peItemImage.Properties.ShowMenu = true; //activamos el menú contextual en el picture edit
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Poner como editables los campos para el modo de edición
        /// </summary>
        private void SetEditingFieldsEnabled()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (Array.IndexOf(_editingFields, ctl.Name) >= 0)
                    {
                        if (ctl.GetType() == typeof(TextEdit))
                        {
                            ((TextEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(CheckEdit))
                        {
                            ((CheckEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(DateEdit))
                        {
                            ((DateEdit)ctl).ReadOnly = false;
                        }
                        else if (ctl.GetType() == typeof(LookUpEdit))
                        {
                            ((LookUpEdit)ctl).ReadOnly = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetCreatingFieldsEnabled()
        {
            try
            {
                foreach (Control ctl in layoutControlForm.Controls)
                {
                    if (ctl.GetType() == typeof(TextEdit))
                    {
                        ((TextEdit)ctl).ReadOnly = false;
                    }
                    else if (ctl.GetType() == typeof(CheckEdit))
                    {
                        ((CheckEdit)ctl).ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetNonCreatingFieldsVisibility(LayoutVisibility visibility)
        {
            try
            {
                lciIdVersion.Visibility = visibility;
                lciIdSubversion.Visibility = visibility;
                lciTimestamp.Visibility = visibility;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mover la fila activa a un item en concreto
        /// </summary>
        /// <param name="itemCode"></param>
        private void MoveGridToItem(string itemCode)
        {
            try
            {
                //TODO
                //GridColumn column = rootGridViewItems.Columns[eItemColumns.ItemCode.ToString()];
                //if (column != null)
                //{
                //    // locating the row 
                //    int rhFound = rootGridViewItems.LocateByDisplayText(rootGridViewItems.FocusedRowHandle + 1, column, itemCode);
                //    // focusing the cell 
                //    if (rhFound != GridControl.InvalidRowHandle)
                //    {
                //        rootGridViewItems.FocusedRowHandle = rhFound;
                //        rootGridViewItems.FocusedColumn = column;
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //TODO
        private bool IsValidItem()
        {
            try
            {
                //foreach (Control ctl in layoutControlForm.Controls)
                //{
                //    if (ctl.GetType() == typeof(TextEdit))
                //    {
                //        if (string.IsNullOrEmpty(((TextEdit)ctl).Text))
                //        {
                //            MessageBox.Show(string.Format(GlobalSetting.ResManager.GetString("NullArgument"), ctl.Name));
                //            return false;
                //        }
                //    }
                //}

                if (string.IsNullOrEmpty(txtPathNewDoc.Text) == false)
                {
                    if (System.IO.File.Exists(txtPathNewDoc.Text) == false)
                    {
                        XtraMessageBox.Show("New doc file doesn't exist", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (lueDocType.EditValue == null)
                    {
                        XtraMessageBox.Show("Select doc type", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        /// <summary>
        /// Actualizar los datos de un item
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateItem(bool newVersion = false)
        {
            try
            {
                return GlobalSetting.ItemHwService.UpdateItem(_itemUpdate, newVersion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateItemWithDoc(bool newVersion = false)
        {
            try
            {
                string fileName = System.IO.Path.GetFileName(txtPathNewDoc.Text);
                string fileNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(txtPathNewDoc.Text);
                string extension = System.IO.Path.GetExtension(txtPathNewDoc.Text);

                //Creamos los directorios si no existen
                new System.IO.FileInfo(Constants.DOCS_PATH + _itemUpdate.IdItemBcn + "\\" + lueDocType.EditValue.ToString() + "\\").Directory.Create();


                ItemDoc itemDoc = new ItemDoc();
                itemDoc.IdItemBcn = _itemUpdate.IdItemBcn;
                itemDoc.IdItemGroup = Constants.ITEM_GROUP_HW;
                itemDoc.IdDocType = lueDocType.EditValue.ToString();
                itemDoc.FileName = fileNameNoExtension + "_" + (_itemUpdate.IdVer.ToString()) + "." + ((_itemUpdate.IdSubVer + 1).ToString()) + extension;
                itemDoc.FilePath = _itemUpdate.IdItemBcn + "\\" + lueDocType.EditValue.ToString() + "\\" + itemDoc.FileName;

                //move to file server
                System.IO.File.Copy(txtPathNewDoc.Text, Constants.DOCS_PATH + itemDoc.FilePath, overwrite: true);

                //update database
                return GlobalSetting.ItemHwService.UpdateItemWithDoc(_itemUpdate, itemDoc, newVersion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ActionsAfterCU()
        {
            try
            {
                string itemCode = _itemOriginal.IdItemBcn;
                _itemOriginal = null;
                ResetItemUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpDocs.PageVisible = false;
                xtpList.PageVisible = true;
                peItemImage.Properties.ShowMenu = false;
                LoadItemsList();
                MoveGridToItem(itemCode);
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
