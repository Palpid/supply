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

using System.Data.Entity;
using DevExpress.Utils.Menu;
using HKSupply.Classes;

namespace HKSupply.Forms.Master
{
    public partial class ItemManagementEY : RibbonFormBase
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
            IdMaterialL1,
            IdMaterialL2,
            IdMaterialL3,
            Comments,
            Segment,
            Category,
            Age,
            LaunchDate,
            RemovalDate,
            IdStatusCial,
            IdStatusProd,
            IdUserAttri1,
            IdUserAttri2,
            IdUserAttri3,
            PhotoUrl,
            Photo,
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

        ItemEy _itemUpdate;
        ItemEy _itemOriginal;
        ItemEyHistory _itemHistory;

        List<ItemEy> _itemsList;
        List<ItemEyHistory> _itemsHistoryList;
        List<Supplier> _supplierList;
        List<StatusHK> _statusProdList;
        List<UserAttrDescription> _userAttrDescriptionList;
        List<DocType> _docsTypeList;
        List<ItemDoc> _itemDocsList;
        List<ItemDoc> _itemLastDocsList;

        string[] _editingFields = { "lueIdDefaultSupplier", "lueIdStatusProd", "txtIdUserAttri1", "txtIdUserAttri2", "txtIdUserAttri3" };
        
        Dictionary<String, Bitmap> photosCache = new Dictionary<string, Bitmap>();

        int _currentHistoryNumList;
        #endregion

        #region Constructor
        public ItemManagementEY()
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
                ResetItemUpdate();
                SetFormBinding();

                //Test
                xgrdItems.ToolTipController = this.toolTipController1;
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
                    if(string.IsNullOrEmpty(txtPathNewDoc.Text))
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

        private void ItemManagement_Load(object sender, EventArgs e)
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
                ItemEy  item = view.GetRow(view.FocusedRowHandle) as ItemEy;
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

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != xgrdItems) return;
            ToolTipControlInfo info = null;

            SuperToolTip sTooltip1 = new SuperToolTip();


            try
            {
                GridView view = xgrdItems.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);

                if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
                {
                    //info para debug
                    //info = new ToolTipControlInfo(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowIndicator.ToString() + hi.RowHandle.ToString(), "Row Handle: " + hi.RowHandle.ToString());

                    ToolTipTitleItem titleItem1 = new ToolTipTitleItem();

                    if (hi.Column.FieldName != eItemColumns.Photo.ToString())
                        return;

                    string url = view.GetRowCellValue(hi.RowHandle, eItemColumns.PhotoUrl.ToString()).ToString();
                    AddToPhotoCache(url);
                    //if (photosCache.ContainsKey(url) == false)
                    //{
                    //    var request = System.Net.WebRequest.Create(url);
                    //    using (var response = request.GetResponse())
                    //    {
                    //        using (var stream = response.GetResponseStream())
                    //        {
                    //            Image p = Bitmap.FromStream(stream);
                    //            photosCache.Add(url, (Bitmap)p);
                    //        }
                    //    }
                    //}
                    Bitmap im = null;
                    im = photosCache[url.ToString()];
                    ToolTipItem item1 = new ToolTipItem();
                    item1.Image = im;
                    sTooltip1.Items.Add(item1);

                    //END.MRM
                }
                info = new ToolTipControlInfo(hi.HitTest, "");
                info.SuperTip = sTooltip1;
            }
            finally
            {
                e.Info = info;
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

        void rootGridViewItems_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            try
            {
                //DataRow dr = (e.Row as DataRowView).Row;
                //string url = dr[eItemColumns.PhotoUrl.ToString()].ToString();
                string url = (e.Row as ItemEy).PhotoUrl;
                if (photosCache.ContainsKey(url))
                {
                    e.Value = photosCache[url];
                    return;
                }
                var request = System.Net.WebRequest.Create(url);
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        e.Value = Bitmap.FromStream(stream);
                        photosCache.Add(url, (Bitmap)e.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void rootGridViewItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (e.MenuType == GridMenuType.Row)
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    e.Menu.Items.Add(CreateMenuPriceList(view, rowHandle));

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void OnMenuItemViewItemPriceListClick(object sender, EventArgs e)
        {
            try
            {
                DXMenuItem item = sender as DXMenuItem;
                RowInfo info = item.Tag as RowInfo;

                ItemEy itemEy = info.View.GetRow(info.View.FocusedRowHandle) as ItemEy;
                OpenPriceListForm(itemEy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Resetear el objeto item que usamos para la actualización
        /// </summary>
        private void ResetItemUpdate()
        {
            _itemUpdate = new ItemEy();
            _itemDocsList = null;
            txtPathNewDoc.Text = string.Empty;
        }

        #region SetUp Form Object

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
                
                GridColumn colPrototypeName = new GridColumn() { Caption = "Prototype Name", Visible = true, FieldName = eItemColumns.Prototype.ToString() +  ".PrototypeName", Width = 150 };
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

                GridColumn colIdMaterialL1 = new GridColumn() { Caption = "Material L1", Visible = true, FieldName = eItemColumns.IdMaterialL1.ToString(), Width = 100 };
                GridColumn colIdMaterialL2 = new GridColumn() { Caption = "Material L2", Visible = true, FieldName = eItemColumns.IdMaterialL2.ToString(), Width = 100 };
                GridColumn colIdMaterialL3 = new GridColumn() { Caption = "Material L3", Visible = true, FieldName = eItemColumns.IdMaterialL3.ToString(), Width = 100 };

                GridColumn colComments = new GridColumn() { Caption = "Comments", Visible = true, FieldName = eItemColumns.Comments.ToString(), Width = 300 };
                
                GridColumn colSegment = new GridColumn() { Caption = "Segment", Visible = true, FieldName = eItemColumns.Segment.ToString(), Width = 70 };
                GridColumn colCategory = new GridColumn() { Caption = "Category", Visible = true, FieldName = eItemColumns.Category.ToString(), Width = 70 };
                GridColumn colAge = new GridColumn() { Caption = "Age", Visible = true, FieldName = eItemColumns.Age.ToString(), Width = 70 };

                GridColumn colLaunchedDate = new GridColumn() { Caption = "Launch Date", Visible = true, FieldName = eItemColumns.LaunchDate.ToString(), Width = 90 };
                GridColumn colRemovalDate = new GridColumn() { Caption = "Removal Date", Visible = true, FieldName = eItemColumns.RemovalDate.ToString(), Width = 90 };
                GridColumn colIdStatusCial = new GridColumn() { Caption = "Status Cial", Visible = true, FieldName = eItemColumns.IdStatusCial.ToString(), Width = 90 };
                GridColumn colIdStatusProd = new GridColumn() { Caption = "Status Prod", Visible = true, FieldName = eItemColumns.IdStatusProd.ToString(), Width = 90 };
                GridColumn colIdUserAttri1 = new GridColumn() { Caption = "User Attri. 1", Visible = true, FieldName = eItemColumns.IdUserAttri1.ToString(), Width = 90 };
                GridColumn colIdUserAttri2 = new GridColumn() { Caption = "User Attri. 2", Visible = true, FieldName = eItemColumns.IdUserAttri2.ToString(), Width = 90 };
                GridColumn colIdUserAttri3 = new GridColumn() { Caption = "User Attri. 3", Visible = true, FieldName = eItemColumns.IdUserAttri3.ToString(), Width = 90 };

                GridColumn colPhotoUrl = new GridColumn() { Caption = "Photo URL", Visible = false, FieldName = eItemColumns.PhotoUrl.ToString(), Width = 90 };
                GridColumn colPhoto = new GridColumn() { Caption = "Photo", Visible = true, FieldName = eItemColumns.Photo.ToString(), Width = 90 };

                //Display Format
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                colCaliber.DisplayFormat.FormatType = FormatType.Numeric;
                colCaliber.DisplayFormat.FormatString = "n2";

                //Photo
                colPhoto.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit pictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
                //pictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
                pictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                pictureEdit.ShowZoomSubMenu = DefaultBoolean.True;
                colPhoto.ColumnEdit = pictureEdit;

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
                rootGridViewItems.Columns.Add(colIdMaterialL1);
                rootGridViewItems.Columns.Add(colIdMaterialL2);
                rootGridViewItems.Columns.Add(colIdMaterialL3);
                rootGridViewItems.Columns.Add(colComments);
                rootGridViewItems.Columns.Add(colSegment);
                rootGridViewItems.Columns.Add(colCategory);
                rootGridViewItems.Columns.Add(colAge);
                rootGridViewItems.Columns.Add(colLaunchedDate);
                rootGridViewItems.Columns.Add(colRemovalDate);
                rootGridViewItems.Columns.Add(colIdStatusCial);
                rootGridViewItems.Columns.Add(colIdStatusProd);
                rootGridViewItems.Columns.Add(colIdUserAttri1);
                rootGridViewItems.Columns.Add(colIdUserAttri2);
                rootGridViewItems.Columns.Add(colIdUserAttri3);

                rootGridViewItems.Columns.Add(colPhotoUrl);
                rootGridViewItems.Columns.Add(colPhoto);

                //Events
                rootGridViewItems.DoubleClick += rootGridViewItems_DoubleClick;
                rootGridViewItems.CustomUnboundColumnData += rootGridViewItems_CustomUnboundColumnData;

                rootGridViewItems.PopupMenuShowing += rootGridViewItems_PopupMenuShowing;

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
                txtCaliber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtCaliber.Properties.Mask.EditMask = "F2"; //Dos decimales
                txtCaliber.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtCaliber.EditValue = "0.00";

                txtLaunchDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                txtLaunchDate.Properties.Mask.EditMask = "dd/MM/yyyy";
                txtLaunchDate.Properties.Mask.UseMaskAsDisplayFormat = true;

                txtRemovalDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                txtRemovalDate.Properties.Mask.EditMask = "dd/MM/yyyy";
                txtRemovalDate.Properties.Mask.UseMaskAsDisplayFormat = true;

                //History

                txtHCaliber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtHCaliber.Properties.Mask.EditMask = "F2"; //Dos decimales
                txtHCaliber.Properties.Mask.UseMaskAsDisplayFormat = true;
                txtHCaliber.EditValue = "0.00";

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
                txtIdVersion.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdVer);
                txtIdSubversion.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdSubVer);
                txtTimestamp.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.Timestamp);
                txtIdPrototype.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdPrototype);
                txtPrototypeName.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeName");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtPrototypeDescription.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeDescription");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtPrototypeStatus.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeStatus");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtModel.DataBindings.Add("Text", _itemUpdate, "Model.Description");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtCaliber.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.Caliber);
                txtIdColor1.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdColor1);
                txtIdColor2.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdColor2);
                txtIdItemBcn.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdItemBcn);
                txtIdItemHK.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdItemHK);
                txtItemDescription.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.ItemDescription);
                txtIdMaterialL1.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdMaterialL1);
                txtIdMaterialL2.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdMaterialL2);
                txtIdMaterialL3.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdMaterialL3);
                txtComments.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.Comments);
                txtSegment.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.Segment);
                txtCategory.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.Category);
                txtAge.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.Age);
                txtLaunchDate.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.LaunchDate);
                txtRemovalDate.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.RemovalDate);
                txtIdStatusCial.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdStatusCial);
                txtUnit.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.Unit);
                txtDocsLink.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.DocsLink);
                txtCreateDate.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.CreateDate);

                txtIdUserAttri1.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri1);
                txtIdUserAttri2.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri2);
                txtIdUserAttri3.DataBindings.Add<ItemEy>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri3);

                //LookUpEdit
                lueIdDefaultSupplier.DataBindings.Add<ItemEy>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdDefaultSupplier);
                lueIdStatusProd.DataBindings.Add<ItemEy>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdStatusProd);

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
                txtHIdVersion.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdVer);
                txtHIdSubversion.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdSubVer);
                txtHTimestamp.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.Timestamp);
                txtHIdPrototype.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdPrototype);
                txtHModel.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdModel);
                txtHCaliber.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.Caliber);
                txtHIdColor1.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdColor1);
                txtHIdColor2.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdColor2);
                txtHIdItemBcn.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdItemBcn);
                txtHIdItemHK.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdItemHK);
                txtHItemDescription.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.ItemDescription);
                txtHIdMaterialL1.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdMaterialL1);
                txtHIdMaterialL2.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdMaterialL2);
                txtHIdMaterialL3.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdMaterialL3);
                txtHComments.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.Comments);
                txtHSegment.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.Segment);
                txtHCategory.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.Category);
                txtHAge.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.Age);
                txtHLaunchDate.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.LaunchDate);
                txtHRemovalDate.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.RemovalDate);
                txtHIdStatusCial.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdStatusCial);
                txtHUnit.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.Unit);
                txtHDocsLink.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.DocsLink);
                txtHCreateDate.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.CreateDate);

                txtHIdUserAttri1.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri1);
                txtHIdUserAttri2.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri2);
                txtHIdUserAttri3.DataBindings.Add<ItemEyHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri3);

                //LookUpEdit
                lueHIdDefaultSupplier.DataBindings.Add<ItemEyHistory>(_itemHistory, (LookUpEdit e) => e.EditValue, item => item.IdDefaultSupplier);
                lueHIdStatusProd.DataBindings.Add<ItemEyHistory>(_itemHistory, (LookUpEdit e) => e.EditValue, item => item.IdStatusProd);
            
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
                _docsTypeList = GlobalSetting.DocTypeService.GetDocsType(Constants.ITEM_GROUP_EY);

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
                _userAttrDescriptionList = GlobalSetting.UserAttrDescriptionService.GetUserAttrsDescription(Constants.ITEM_GROUP_EY);

                //TODO: hacer esto de una manera un poco mas elegante
                lciIdUserAttri1.Text = lciHIdUserAttri1.Text = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals("EYATTR01")).Select(a => a.Description).SingleOrDefault();
                lciIdUserAttri2.Text = lciHIdUserAttri2.Text = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals("EYATTR02")).Select(a => a.Description).SingleOrDefault();
                lciIdUserAttri3.Text = lciHIdUserAttri3.Text = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals("EYATTR03")).Select(a => a.Description).SingleOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        // <summary>
        /// cargar la colección de Items del sistema
        /// </summary>
        /// <remarks></remarks>
        private void LoadItemsList()
        {
            try
            {
                _itemsList = GlobalSetting.ItemEyService.GetItems();
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
        private void LoadItemForm(ItemEy item)
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
                _itemsHistoryList = GlobalSetting.ItemEyService.GetItemEyHistory(_itemUpdate.IdItemBcn);
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
                _itemDocsList = GlobalSetting.ItemDocService.GetItemsDocs(_itemUpdate.IdItemBcn, Constants.ITEM_GROUP_EY); 
                xgrdDocsHistory.DataSource = _itemDocsList;

                _itemLastDocsList = GlobalSetting.ItemDocService.GetLastItemsDocs(_itemUpdate.IdItemBcn, Constants.ITEM_GROUP_EY);
                xgrdLastDocs.DataSource = _itemLastDocsList;

                xtpDocs.PageVisible = true;
                gbNewDoc.Enabled = false;

                //Item Image
                //Quitamos el menú contextual ya que no nos interesa en este momento
                peItemImage.Properties.ShowMenu = false;
                //Cargamos la imagen de la cache.
                AddToPhotoCache(_itemUpdate.PhotoUrl);
                Bitmap im = null;
                im = photosCache[_itemUpdate.PhotoUrl];
                peItemImage.Image = im;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Agregar una imagen al cache de fotos si no está ya en él
        /// </summary>
        /// <param name="url"></param>
        private void AddToPhotoCache(string url)
        {
            try
            {
                if (photosCache.ContainsKey(url) == false)
                {
                    var request = System.Net.WebRequest.Create(url);
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            Image p = Bitmap.FromStream(stream);
                            photosCache.Add(url, (Bitmap)p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Popup menu

        DXMenuItem CreateMenuPriceList(GridView view, int rowHandle)
        {
            DXMenuItem menuItem = new DXMenuItem("View Item price list",
                new EventHandler(OnMenuItemViewItemPriceListClick));
            menuItem.Tag = new RowInfo(view, rowHandle);
            return menuItem;
        }

        private void OpenPriceListForm(ItemEy itemEy)
        {
            try
            {
                //Check if is already open
                HKSupply.Forms.Master.SupplierPriceListManagement priceListForm =
                    Application.OpenForms.OfType<HKSupply.Forms.Master.SupplierPriceListManagement>()
                    .Where(pre => pre.Name == "SupplierPriceListManagement")
                    .SingleOrDefault<HKSupply.Forms.Master.SupplierPriceListManagement>();

                if (priceListForm != null)
                    priceListForm.Close();

                priceListForm = new HKSupply.Forms.Master.SupplierPriceListManagement();
                priceListForm.InitData(itemEy.IdDefaultSupplier, itemEy.IdItemBcn);

                priceListForm.MdiParent = this.MdiParent;
                priceListForm.ShowIcon = false;
                priceListForm.Dock = DockStyle.Fill;
                priceListForm.ControlBox = false;
                priceListForm.Show();
                priceListForm.WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                xtpList.PageVisible = true;
                gbNewDoc.Enabled = true;
                SetEditingFieldsEnabled();
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
        private void MoveGridToItem(string idPrototype, string idItemBcn)
        {
            try
            {
                //Buscar por un valor
                GridColumn column = rootGridViewItems.Columns[eItemColumns.IdItemBcn.ToString()];
                if (column != null)
                {
                    // locating the row 
                    int rhFound = rootGridViewItems.LocateByDisplayText(rootGridViewItems.FocusedRowHandle + 1, column, idItemBcn);
                    // focusing the cell 
                    if (rhFound != GridControl.InvalidRowHandle)
                    {
                        rootGridViewItems.FocusedRowHandle = rhFound;
                        rootGridViewItems.FocusedColumn = column;
                    }
                }

                //Buscar por varios valores
                //int row = GridControl.InvalidRowHandle;

                //for (int i = 0; i < rootGridViewItems.RowCount; i++)
                //{
                //    if (rootGridViewItems.GetDataRow(i)[eItemColumns.IdPrototype.ToString()].Equals(idPrototype) &&
                //        rootGridViewItems.GetDataRow(i)[eItemColumns.IdItemBcn.ToString()].Equals(idItemBcn))
                //    {
                //        row = i;
                //    }
                //}

                //if (row != GridControl.InvalidRowHandle)
                //{
                //    rootGridViewItems.FocusedRowHandle = row;
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
                return GlobalSetting.ItemEyService.UpdateItem(_itemUpdate, newVersion);
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
                new System.IO.FileInfo(Constants.DOCS_PATH + _itemUpdate.IdItemBcn + "\\" + lueDocType.EditValue.ToString()+"\\").Directory.Create();

                
                ItemDoc itemDoc = new ItemDoc();
                itemDoc.IdItemBcn = _itemUpdate.IdItemBcn;
                itemDoc.IdItemGroup = Constants.ITEM_GROUP_EY; 
                itemDoc.IdDocType = lueDocType.EditValue.ToString();
                itemDoc.FileName = fileNameNoExtension + "_" + (_itemUpdate.IdVer.ToString()) + "." + ((_itemUpdate.IdSubVer + 1).ToString()) + extension;
                itemDoc.FilePath = _itemUpdate.IdItemBcn + "\\" + lueDocType.EditValue.ToString() + "\\" + itemDoc.FileName;

                //move to file server
                System.IO.File.Copy(txtPathNewDoc.Text, Constants.DOCS_PATH + itemDoc.FilePath, overwrite: true);

                //update database
                return GlobalSetting.ItemEyService.UpdateItemWithDoc(_itemUpdate, itemDoc, newVersion);


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
                string idPrototype = _itemOriginal.IdPrototype;
                string idItemBcn = _itemOriginal.IdItemBcn;

                _itemOriginal = null;
                ResetItemUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpDocs.PageVisible = false;
                xtpList.PageVisible = true;
                LoadItemsList();
                MoveGridToItem(idPrototype, idItemBcn);
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region Read XML Currency Echange
        private void ReadEuroCurrencyExchange()
        {
            try
            {
                string xmlString = "<?xml version='1.0' encoding='UTF-8'?>";
                xmlString += "<gesmesEnvelope>";
                xmlString += "  <gesmessubject>Reference rates</gesmessubject>";
                xmlString += "  <gesmesSender>";
                xmlString += "      <gesmesname>European Central Bank</gesmesname>";
                xmlString += "  </gesmesSender>";
                xmlString += "  <Cube>";
                xmlString += "      <Cube2 time='2017-04-24'>";
                xmlString += "          <Cube3 currency='USD' rate='1.0848'/>";
                xmlString += "          <Cube3 currency='JPY' rate='119.67'/>";
                xmlString += "      </Cube2>";
                xmlString += "  </Cube>";
                xmlString += "</gesmesEnvelope>";

                //---------------------------------------------------------------------------------------------
                string xmlUrl = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
                
                System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
                xml.Load(xmlUrl);

                foreach (System.Xml.XmlNode xnode in xml)
                {
                    string name = xnode.Name;
                    string value = xnode.InnerText;
                    string nv = name + "|" + value;

                    foreach (System.Xml.XmlNode xnode2 in xnode)
                    {
                        string name2 = xnode2.Name;
                        string value2 = xnode2.InnerText;

                        foreach (System.Xml.XmlNode xnode3 in xnode2)
                        {
                            string name3 = xnode3.Name;
                            string value3 = xnode3.InnerText;
                            var a = xnode3.Attributes;

                            foreach (System.Xml.XmlNode xnode4 in xnode3)
                            {
                                string name4 = xnode4.Name;
                                string value4 = xnode4.InnerText;
                                var aa = xnode4.Attributes;
                            }
                        }
                    }
                }

                //------------------------------------------------------------------------------
                System.Xml.Linq.XElement xml2 = System.Xml.Linq.XElement.Load(xmlUrl);
                //var cu2s = from cu2 in xml2.Descendants("Cube")
                //           select new
                //           {
                //               Currency = cu2.Attribute("currency").Value,
                //               Rate = cu2.Attribute("rate").Value,
                //           };

                //System.Xml.Linq.XNamespace ns = "http://www.gesmes.org/xml/2002-08-01";
                System.Xml.Linq.XNamespace ns = System.Xml.Linq.XNamespace.Get("gesmes");
                var cu3s = from cu3 in xml2.Descendants(ns + "Cube")
                           select new
                           {

                               Currency = cu3.Name
                               //Children = cu1.Descendants("level2")
                           };

                //------------------ Funciona si nos 3 niveles no se llaman igual (Cube)
                System.Xml.Linq.XElement xml4 = System.Xml.Linq.XElement.Parse(xmlString);
                var cu1s = from cu1 in xml4.Descendants("Cube3")
                           select new
                           {
                               Currency = cu1.Attribute("currency").Value,
                               Rate = cu1.Attribute("rate").Value,
                               //Children = cu1.Descendants("level2")
                           };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }

}
