﻿using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
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
using DevExpress.XtraBars;
using System.IO;

namespace HKSupply.Forms.Master
{
    public partial class ItemManagementHF : RibbonFormBase
    {
        #region Constants
        private const string PHOTO_COLUMN = "Photo";
        private const string VIEW_COLUMN = "View";
        #endregion

        #region Private Members


        ItemHf _itemUpdate;
        ItemHf _itemOriginal;
        ItemHfHistory _itemHistory;

        List<ItemHf> _itemsList;
        List<ItemHfHistory> _itemsHistoryList;
        List<ItemHf> _modifiedItemsEy = new List<ItemHf>();
        List<Supplier> _supplierList;
        List<StatusHK> _statusProdList;
        List<FamilyHK> _familiesHkList;
        List<UserAttrDescription> _userAttrDescriptionList;
        List<DocType> _docsTypeList;
        List<ItemDoc> _itemDocsList;
        List<ItemDoc> _itemLastDocsList;

        string[] _editingFields = { "lueIdDefaultSupplier", "lueIdStatusProd", "lueIdFamilyHK", "txtIdUserAttri1", "txtIdUserAttri2", "txtIdUserAttri3" };
        string[] _editingCols = { nameof(ItemHf.IdDefaultSupplier), nameof(ItemHf.IdUserAttri1), nameof(ItemHf.IdUserAttri2), nameof(ItemHf.IdUserAttri3), nameof(ItemHf.IdStatusProd), nameof(ItemHf.IdFamilyHK) };

        Dictionary<String, Bitmap> photosCache = new Dictionary<string, Bitmap>();

        int _currentHistoryNumList;

        #endregion

        #region Constructor
        public ItemManagementHF()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                LoadAuxList();
                SetUpGrdItems();
                SetUpGrdLastDocs();
                SetUpGrdDocsHistory();
                SetUpTexEdit();
                SetUpLueStatusProd();
                SetUpLueFamiliesHk();
                SetUpLueDefaultSupplier();
                SetUpLueDocType();
                SetUpLabelNameUserAttributes();
                ResetItemUpdate();
                SetFormBinding();
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
                //Task Buttons
                SetActions();
                RestoreInitState();
                //Print and export buttons
                EnablePrintPreview = false;
                EnableExportExcel = true;
                EnableExportCsv = true;
                ConfigurePrintExportOptions();
                //Layout
                EnableLayoutOptions = true;
                ConfigureLayoutOptions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiCancel_ItemClick(object sender, ItemClickEventArgs e)
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
                SetItemGridStylesByState();
                //suscribirse de nuevo a los eventos y hacer el grid no editable
                rootGridViewItems.DoubleClick += rootGridViewItems_DoubleClick;
                rootGridViewItems.PopupMenuShowing += rootGridViewItems_PopupMenuShowing;
                rootGridViewItems.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiEdit_ItemClick(sender, e);

            try
            {
                if (xtcGeneral.SelectedTabPage == xtpList && rootGridViewItems.DataRowCount == 0)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
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

        public override void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
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

                if (xtcGeneral.SelectedTabPage == xtpForm)
                {
                    if (_itemUpdate.Equals(_itemOriginal) && string.IsNullOrEmpty(txtPathNewDoc.Text))
                    {
                        MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                        return;
                    }
                    else if (xtcGeneral.SelectedTabPage == xtpList)
                    {
                        if (_modifiedItemsEy.Count == 0)
                        {
                            MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                            return;
                        }
                    }
                }

                if (CurrentState == ActionsStates.Edit)
                {
                    if (xtcGeneral.SelectedTabPage == xtpForm || xtcGeneral.SelectedTabPage == xtpDocs)
                    {
                        if (string.IsNullOrEmpty(txtPathNewDoc.Text))
                            res = UpdateItem();
                        else
                            res = UpdateItemWithDoc();
                    }
                    else if (xtcGeneral.SelectedTabPage == xtpList)
                    {
                        res = UpdateItems();
                    }
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

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (rootGridViewItems.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            base.bbiExportCsv_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportCsvFile) == false)
                {
                    rootGridViewItems.ExportToCsv(ExportCsvFile);

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ExportCsvFile);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (rootGridViewItems.DataRowCount == 0)
            {
                MessageBox.Show(GlobalSetting.ResManager.GetString("NoDataSelected"));
                return;
            }

            base.bbiExportExcel_ItemClick(sender, e);

            try
            {
                if (string.IsNullOrEmpty(ExportExcelFile) == false)
                {
                    rootGridViewItems.ExportToXlsx(ExportExcelFile);

                    DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("OpenFileQuestion"), "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(ExportExcelFile);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Form Events

        private void ItemManagementHF_Load(object sender, EventArgs e)
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
                ItemHf item = view.GetRow(view.FocusedRowHandle) as ItemHf;
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

        private void RootGridViewItems_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (CurrentState == ActionsStates.Edit)
                {
                    GridView view = sender as GridView;
                    ItemHf item = view.GetRow(view.FocusedRowHandle) as ItemHf;
                    if (item != null)
                    {
                        AddModifiedItemToList(item);
                    }
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
                    DocHelper.OpenDoc(Constants.ITEMS_DOCS_PATH + itemDoc.FilePath);
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
                ItemDoc itemDoc = gridViewLastDocs.GetRow(gridViewLastDocs.FocusedRowHandle) as ItemDoc;

                if (itemDoc != null)
                {
                    DocHelper.OpenDoc(Constants.ITEMS_DOCS_PATH + itemDoc.FilePath);
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
                    XtraMessageBox.Show(GlobalSetting.ResManager.GetString("NoFileSelected"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "PDF files (*.pdf)|*.pdf|JPG files(*.jpg)|*.jpg|PNG files (*.png)|*.png",
                    Multiselect = false,
                    RestoreDirectory = true,
                };
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

                    if (hi.Column.FieldName != PHOTO_COLUMN)
                        return;

                    string url = (view.GetRowCellValue(hi.RowHandle, nameof(ItemHf.PhotoPath)) ?? string.Empty).ToString();

                    if (string.IsNullOrEmpty(url)) return;

                    AddToPhotoCache(url);

                    Bitmap im = null;
                    if (photosCache.ContainsKey(url.ToString()))
                    {
                        im = photosCache[url.ToString()];
                    }
                    ToolTipItem item1 = new ToolTipItem() { Image = im };
                    sTooltip1.Items.Add(item1);
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
                ItemHf itemTemp = e.Row as ItemHf;

                string url = itemTemp.PhotoPath;
                if (string.IsNullOrEmpty(url)) return;

                if (photosCache.ContainsKey(url))
                {
                    e.Value = photosCache[url];
                    return;
                }
                var request = System.Net.WebRequest.Create(url);
                try
                {
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            e.Value = Bitmap.FromStream(stream);
                            photosCache.Add(url, (Bitmap)e.Value);
                        }
                    }
                }
                catch (System.Net.WebException wex)
                {
                    if (((System.Net.HttpWebResponse)(wex.Response)).StatusCode != System.Net.HttpStatusCode.NotFound)
                    {
                        throw;
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

                ItemHf itemHf = info.View.GetRow(info.View.FocusedRowHandle) as ItemHf;
                OpenPriceListForm(itemHf);
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
            _itemUpdate = new ItemHf();
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

                //Obtenemos los nombres de los atributos de usuario
                string userAtt01 = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals(Constants.HF_USER_ATTR_01)).Select(a => a.Description).SingleOrDefault();
                string userAtt02 = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals(Constants.HF_USER_ATTR_02)).Select(a => a.Description).SingleOrDefault();
                string userAtt03 = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals(Constants.HF_USER_ATTR_03)).Select(a => a.Description).SingleOrDefault();

                //Columns definition
                GridColumn colIdVer = new GridColumn() { Caption = "Version Id", Visible = false, FieldName = nameof(ItemHf.IdVer), Width = 70 };
                GridColumn colIdSubVer = new GridColumn() { Caption = "Subversion Id", Visible = false, FieldName = nameof(ItemHf.IdSubVer), Width = 85 };
                GridColumn colTimestamp = new GridColumn() { Caption = "Timestamp", Visible = false, FieldName = nameof(ItemHf.Timestamp), Width = 130 };
                GridColumn colIdDefaultSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("DefaultSupplier"), Visible = true, FieldName = nameof(ItemHf.IdDefaultSupplier), Width = 110 };
                GridColumn colIdPrototype = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("IdPrototype"), Visible = true, FieldName = nameof(ItemHf.IdPrototype), Width = 150 };
                GridColumn colPrototypeName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("PrototypeName"), Visible = true, FieldName = nameof(ItemHf.Prototype) + "." + nameof(Prototype.PrototypeName), Width = 150 };
                GridColumn colPrototypeDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("PrototypeDescription"), Visible = true, FieldName = nameof(ItemHf.Prototype) + "." + nameof(Prototype.PrototypeDescription), Width = 150 };
                GridColumn colPrototypeStatus = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("PrototypeStatus"), Visible = true, FieldName = nameof(ItemHf.Prototype) + "." + nameof(Prototype.PrototypeStatus), Width = 100 };
                GridColumn colIdModel = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("IdModel"), Visible = false, FieldName = nameof(ItemHf.IdModel), Width = 0 };
                GridColumn colModel = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Model"), Visible = true, FieldName = nameof(ItemHf.Model) + "." + nameof(Model.Description), Width = 120 };
                GridColumn colFamilyHK = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FamilyHk"), Visible = true, FieldName = nameof(ItemHf.IdFamilyHK), Width = 90 };
                GridColumn colCaliber = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Caliber"), Visible = true, FieldName = nameof(ItemHf.Caliber), Width = 70 };
                GridColumn colIdColor1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Color1"), Visible = true, FieldName = nameof(ItemHf.IdColor1), Width = 60 };
                GridColumn colIdColor2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Color2"), Visible = true, FieldName = nameof(ItemHf.IdColor2), Width = 60 };
                GridColumn colIdItemBcn = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemBCN"), Visible = true, FieldName = nameof(ItemHf.IdItemBcn), Width = 160 };
                GridColumn colIdItemHK = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemHK"), Visible = true, FieldName = nameof(ItemHf.IdItemHK), Width = 160 };
                GridColumn colItemDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemDescription"), Visible = true, FieldName = nameof(ItemHf.ItemDescription), Width = 300 };
                GridColumn colIdMaterialL1 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL1"), Visible = true, FieldName = nameof(ItemHf.IdMaterialL1), Width = 100 };
                GridColumn colIdMaterialL2 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL2"), Visible = true, FieldName = nameof(ItemHf.IdMaterialL2), Width = 100 };
                GridColumn colIdMaterialL3 = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("MaterialL3"), Visible = true, FieldName = nameof(ItemHf.IdMaterialL3), Width = 100 };
                GridColumn colComments = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Comments"), Visible = true, FieldName = nameof(ItemHf.Comments), Width = 300 };
                GridColumn colSegment = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Segment"), Visible = true, FieldName = nameof(ItemHf.Segment), Width = 70 };
                GridColumn colCategory = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Category"), Visible = true, FieldName = nameof(ItemHf.Category), Width = 70 };
                GridColumn colAge = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Age"), Visible = true, FieldName = nameof(ItemHf.Age), Width = 70 };
                GridColumn colLaunchedDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("LaunchDate"), Visible = true, FieldName = nameof(ItemHf.LaunchDate), Width = 90 };
                GridColumn colRemovalDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("RemovalDate"), Visible = true, FieldName = nameof(ItemHf.RemovalDate), Width = 90 };
                GridColumn colIdStatusCial = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("StatusCial"), Visible = true, FieldName = nameof(ItemHf.IdStatusCial), Width = 90 };
                GridColumn colIdStatusProd = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("StatusProd"), Visible = true, FieldName = nameof(ItemHf.IdStatusProd), Width = 90 };
                GridColumn colIdUserAttri1 = new GridColumn() { Caption = userAtt01, Visible = true, FieldName = nameof(ItemHf.IdUserAttri1), Width = 90 };
                GridColumn colIdUserAttri2 = new GridColumn() { Caption = userAtt02, Visible = true, FieldName = nameof(ItemHf.IdUserAttri2), Width = 90 };
                GridColumn colIdUserAttri3 = new GridColumn() { Caption = userAtt03, Visible = true, FieldName = nameof(ItemHf.IdUserAttri3), Width = 90 };
                GridColumn colPhotoPath = new GridColumn() { Caption = "Photo URL", Visible = false, FieldName = nameof(ItemHf.PhotoPath), Width = 90 };
                GridColumn colPhoto = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Photo"), Visible = true, FieldName = PHOTO_COLUMN, Width = 90 };

                //Display Format
                colTimestamp.DisplayFormat.FormatType = FormatType.DateTime;

                colCaliber.DisplayFormat.FormatType = FormatType.Numeric;
                colCaliber.DisplayFormat.FormatString = "n2";

                //Photo
                colPhoto.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                RepositoryItemPictureEdit pictureEdit = new RepositoryItemPictureEdit()
                {
                    SizeMode = PictureSizeMode.Zoom,
                    ShowZoomSubMenu = DefaultBoolean.True
                };
                colPhoto.ColumnEdit = pictureEdit;

                //Edit repositories
                RepositoryItemLookUpEdit riStatusProd = new RepositoryItemLookUpEdit()
                {
                    DataSource = _statusProdList,
                    DisplayMember = nameof(StatusHK.IdStatusProd), //nameof(StatusHK.Description), //Tenemos las descripciones en blanco de momento
                    ValueMember = nameof(StatusHK.IdStatusProd),
                };

                colIdStatusProd.ColumnEdit = riStatusProd;

                RepositoryItemLookUpEdit riFamiliesHk = new RepositoryItemLookUpEdit()
                {
                    DataSource = _familiesHkList,
                    DisplayMember = nameof(FamilyHK.Description),
                    ValueMember = nameof(FamilyHK.IdFamilyHk),
                    NullText = string.Empty
                };

                colFamilyHK.ColumnEdit = riFamiliesHk;

                RepositoryItemSearchLookUpEdit riDefaultSupplier = new RepositoryItemSearchLookUpEdit()
                {
                    DataSource = _supplierList,
                    ValueMember = nameof(Supplier.IdSupplier),
                    DisplayMember = nameof(Supplier.SupplierName),
                    ShowClearButton = false,
                };
                riDefaultSupplier.View.Columns.AddField(nameof(Supplier.IdSupplier)).Visible = true;
                riDefaultSupplier.View.Columns.AddField(nameof(Supplier.SupplierName)).Visible = true;

                colIdDefaultSupplier.ColumnEdit = riDefaultSupplier;

                //Alignment
                colPrototypeStatus.AppearanceCell.Options.UseTextOptions = true;
                colPrototypeStatus.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                colIdStatusCial.AppearanceCell.Options.UseTextOptions = true;
                colIdStatusCial.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                colIdStatusProd.AppearanceCell.Options.UseTextOptions = true;
                colIdStatusProd.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;


                //Add columns to grid root view
                rootGridViewItems.Columns.Add(colIdVer);
                rootGridViewItems.Columns.Add(colIdSubVer);
                rootGridViewItems.Columns.Add(colTimestamp);
                rootGridViewItems.Columns.Add(colIdDefaultSupplier);
                rootGridViewItems.Columns.Add(colIdPrototype);
                rootGridViewItems.Columns.Add(colPrototypeName);
                rootGridViewItems.Columns.Add(colPrototypeDescription);
                rootGridViewItems.Columns.Add(colPrototypeStatus);
                rootGridViewItems.Columns.Add(colCaliber);
                rootGridViewItems.Columns.Add(colIdModel);
                rootGridViewItems.Columns.Add(colModel);
                rootGridViewItems.Columns.Add(colIdColor1);
                rootGridViewItems.Columns.Add(colIdColor2);
                rootGridViewItems.Columns.Add(colIdItemBcn);
                rootGridViewItems.Columns.Add(colIdItemHK);
                rootGridViewItems.Columns.Add(colItemDescription);
                rootGridViewItems.Columns.Add(colFamilyHK);
                rootGridViewItems.Columns.Add(colIdStatusCial);
                rootGridViewItems.Columns.Add(colIdStatusProd);
                rootGridViewItems.Columns.Add(colSegment);
                rootGridViewItems.Columns.Add(colCategory);
                rootGridViewItems.Columns.Add(colAge);
                rootGridViewItems.Columns.Add(colIdMaterialL1);
                rootGridViewItems.Columns.Add(colIdMaterialL2);
                rootGridViewItems.Columns.Add(colIdMaterialL3);
                rootGridViewItems.Columns.Add(colLaunchedDate);
                rootGridViewItems.Columns.Add(colRemovalDate);
                rootGridViewItems.Columns.Add(colIdUserAttri1);
                rootGridViewItems.Columns.Add(colIdUserAttri2);
                rootGridViewItems.Columns.Add(colIdUserAttri3);
                rootGridViewItems.Columns.Add(colComments);
                rootGridViewItems.Columns.Add(colPhotoPath);
                rootGridViewItems.Columns.Add(colPhoto);

                //Events
                rootGridViewItems.DoubleClick += rootGridViewItems_DoubleClick;
                rootGridViewItems.CustomUnboundColumnData += rootGridViewItems_CustomUnboundColumnData;

                rootGridViewItems.PopupMenuShowing += rootGridViewItems_PopupMenuShowing;
                rootGridViewItems.CellValueChanged += RootGridViewItems_CellValueChanged;

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
                GridColumn colIdVerItem = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemVer"), Visible = true, FieldName = nameof(ItemDoc.IdVerItem), Width = 60 };
                GridColumn colIdSubVerItem = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemSubver"), Visible = true, FieldName = nameof(ItemDoc.IdSubVerItem), Width = 75 };
                GridColumn colIdDocType = new GridColumn() { Caption = "IdDocType", Visible = false, FieldName = nameof(ItemDoc.IdDocType), Width = 10 };
                GridColumn colDocType = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("DocType"), Visible = true, FieldName = $"{nameof(ItemDoc.DocType)}.{nameof(DocType.Description)}", Width = 100 };
                GridColumn colFileName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FileName"), Visible = true, FieldName = nameof(ItemDoc.FileName), Width = 250 };
                GridColumn colFilePath = new GridColumn() { Caption = "FilePath", Visible = false, FieldName = nameof(ItemDoc.FilePath), Width = 10 };
                GridColumn colCreateDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CreateDate"), Visible = true, FieldName = nameof(ItemDoc.CreateDate), Width = 150 };
                GridColumn colViewButton = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("View"), Visible = true, FieldName = VIEW_COLUMN, Width = 50 };

                //Display Format
                colCreateDate.DisplayFormat.FormatType = FormatType.DateTime;

                //View Button
                colViewButton.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                RepositoryItemButtonEdit repButtonLastDoc = new RepositoryItemButtonEdit()
                {
                    Name = "btnViewLastDoc",
                    TextEditStyle = TextEditStyles.HideTextEditor,
                };
                repButtonLastDoc.Click += repButtonLastDoc_Click;

                colViewButton.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
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
                GridColumn colIdVerItem = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemVer"), Visible = true, FieldName = nameof(ItemDoc.IdVerItem), Width = 60 };
                GridColumn colIdSubVerItem = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemSubver"), Visible = true, FieldName = nameof(ItemDoc.IdSubVerItem), Width = 75 };
                GridColumn colIdDocType = new GridColumn() { Caption = "IdDocType", Visible = false, FieldName = nameof(ItemDoc.IdDocType), Width = 10 };
                GridColumn colDocType = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("DocType"), Visible = true, FieldName = $"{nameof(ItemDoc.DocType)}.{nameof(DocType.Description)}", Width = 100 };
                GridColumn colFileName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FileName"), Visible = true, FieldName = nameof(ItemDoc.FileName), Width = 280 };
                GridColumn colFilePath = new GridColumn() { Caption = "FilePath", Visible = false, FieldName = nameof(ItemDoc.FilePath), Width = 10 };
                GridColumn colCreateDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CreateDate"), Visible = true, FieldName = nameof(ItemDoc.CreateDate), Width = 120 };
                GridColumn colViewButton = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("View"), Visible = true, FieldName = VIEW_COLUMN, Width = 50 };

                //Display Format
                colCreateDate.DisplayFormat.FormatType = FormatType.DateTime;

                //View Button
                colViewButton.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                RepositoryItemButtonEdit repButtonHistDoc = new RepositoryItemButtonEdit()
                {
                    Name = "btnViewHistoryDoc",
                    TextEditStyle = TextEditStyles.HideTextEditor,
                };
                repButtonHistDoc.Click += repButtonHistDoc_Click;

                colViewButton.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
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
                txtIdVersion.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdVer);
                txtIdSubversion.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdSubVer);
                txtTimestamp.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.Timestamp);
                txtIdPrototype.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdPrototype);
                txtPrototypeName.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeName");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtPrototypeDescription.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeDescription");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtPrototypeStatus.DataBindings.Add("Text", _itemUpdate, "Prototype.PrototypeStatus");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtModel.DataBindings.Add("Text", _itemUpdate, "Model.Description");//La custom extension que hice no funciona con propiedades que son clases, bindeo a la antigua
                txtCaliber.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.Caliber);
                txtIdColor1.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdColor1);
                txtIdColor2.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdColor2);
                txtIdItemBcn.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdItemBcn);
                txtIdItemHK.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdItemHK);
                txtItemDescription.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.ItemDescription);
                txtIdMaterialL1.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdMaterialL1);
                txtIdMaterialL2.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdMaterialL2);
                txtIdMaterialL3.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdMaterialL3);
                txtComments.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.Comments);
                txtSegment.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.Segment);
                txtCategory.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.Category);
                txtAge.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.Age);
                txtLaunchDate.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.LaunchDate);
                txtRemovalDate.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.RemovalDate);
                txtIdStatusCial.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdStatusCial);
                txtUnit.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.Unit);
                txtDocsLink.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.DocsLink);
                txtCreateDate.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.CreateDate);

                txtIdUserAttri1.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri1);
                txtIdUserAttri2.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri2);
                txtIdUserAttri3.DataBindings.Add<ItemHf>(_itemUpdate, (Control c) => c.Text, item => item.IdUserAttri3);

                //LookUpEdit
                lueIdDefaultSupplier.DataBindings.Add<ItemHf>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdDefaultSupplier);
                lueIdStatusProd.DataBindings.Add<ItemHf>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdStatusProd);
                lueIdFamilyHK.DataBindings.Add<ItemHf>(_itemUpdate, (LookUpEdit e) => e.EditValue, item => item.IdFamilyHK);

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
                txtHIdVersion.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdVer);
                txtHIdSubversion.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdSubVer);
                txtHTimestamp.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.Timestamp);
                txtHIdPrototype.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdPrototype);
                txtHModel.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdModel);
                txtHCaliber.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.Caliber);
                txtHIdColor1.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdColor1);
                txtHIdColor2.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdColor2);
                txtHIdItemBcn.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdItemBcn);
                txtHIdItemHK.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdItemHK);
                txtHItemDescription.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.ItemDescription);
                txtHIdMaterialL1.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdMaterialL1);
                txtHIdMaterialL2.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdMaterialL2);
                txtHIdMaterialL3.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdMaterialL3);
                txtHComments.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.Comments);
                txtHSegment.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.Segment);
                txtHCategory.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.Category);
                txtHAge.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.Age);
                txtHLaunchDate.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.LaunchDate);
                txtHRemovalDate.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.RemovalDate);
                txtHIdStatusCial.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdStatusCial);
                txtHUnit.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.Unit);
                txtHDocsLink.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.DocsLink);
                txtHCreateDate.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.CreateDate);

                txtHIdUserAttri1.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri1);
                txtHIdUserAttri2.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri2);
                txtHIdUserAttri3.DataBindings.Add<ItemHfHistory>(_itemHistory, (Control c) => c.Text, item => item.IdUserAttri3);

                //LookUpEdit
                lueHIdDefaultSupplier.DataBindings.Add<ItemHfHistory>(_itemHistory, (LookUpEdit e) => e.EditValue, item => item.IdDefaultSupplier);
                lueHIdStatusProd.DataBindings.Add<ItemHfHistory>(_itemHistory, (LookUpEdit e) => e.EditValue, item => item.IdStatusProd);
                lueHIdFamilyHK.DataBindings.Add<ItemHfHistory>(_itemHistory, (LookUpEdit e) => e.EditValue, item => item.IdFamilyHK);

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
                lueIdDefaultSupplier.Properties.DataSource = _supplierList;
                lueIdDefaultSupplier.Properties.DisplayMember = nameof(Supplier.IdSupplier);
                lueIdDefaultSupplier.Properties.ValueMember = nameof(Supplier.IdSupplier);
                lueIdDefaultSupplier.Properties.Columns.Add(new LookUpColumnInfo(nameof(Supplier.IdSupplier), 20, GlobalSetting.ResManager.GetString("Supplier")));
                lueIdDefaultSupplier.Properties.Columns.Add(new LookUpColumnInfo(nameof(Supplier.SupplierName), 100, GlobalSetting.ResManager.GetString("Name")));

                //De esta manera se activa el limpiar el combo pulsado Ctrl + Supr. Es poco intuitivo, lo controlamos por el evento
                //lueIdDefaultSupplier.Properties.AllowNullInput = DefaultBoolean.True; 
                lueIdDefaultSupplier.KeyDown += lueIdDefaultSupplier_KeyDown;

                //History
                lueHIdDefaultSupplier.Properties.DataSource = _supplierList;
                lueHIdDefaultSupplier.Properties.DisplayMember = nameof(Supplier.IdSupplier);
                lueHIdDefaultSupplier.Properties.ValueMember = nameof(Supplier.IdSupplier);
                lueHIdDefaultSupplier.Properties.Columns.Add(new LookUpColumnInfo(nameof(Supplier.IdSupplier), 20, GlobalSetting.ResManager.GetString("Supplier")));
                lueHIdDefaultSupplier.Properties.Columns.Add(new LookUpColumnInfo(nameof(Supplier.SupplierName), 100, GlobalSetting.ResManager.GetString("Name")));

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
                lueIdStatusProd.Properties.DataSource = _statusProdList;
                lueIdStatusProd.Properties.DisplayMember = nameof(StatusHK.Description);
                lueIdStatusProd.Properties.ValueMember = nameof(StatusHK.IdStatusProd);
                //lueIdStatusProd.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdSupplier", 20, "Id Supplier"));
                //lueIdStatusProd.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", 100, "Name"));

                lueHIdStatusProd.Properties.DataSource = _statusProdList;
                lueHIdStatusProd.Properties.DisplayMember = nameof(StatusHK.Description);
                lueHIdStatusProd.Properties.ValueMember = nameof(StatusHK.IdStatusProd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetUpLueFamiliesHk()
        {
            try
            {
                lueIdFamilyHK.Properties.DataSource = _familiesHkList;
                lueIdFamilyHK.Properties.DisplayMember = nameof(FamilyHK.Description);
                lueIdFamilyHK.Properties.ValueMember = nameof(FamilyHK.IdFamilyHk);

                lueHIdFamilyHK.Properties.DataSource = _familiesHkList;
                lueHIdFamilyHK.Properties.DisplayMember = nameof(FamilyHK.Description);
                lueHIdFamilyHK.Properties.ValueMember = nameof(FamilyHK.IdFamilyHk);
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
                lueDocType.Properties.DataSource = _docsTypeList;
                lueDocType.Properties.DisplayMember = nameof(DocType.Description);
                lueDocType.Properties.ValueMember = nameof(DocType.IdDocType);
                lueDocType.Properties.Columns.Add(new LookUpColumnInfo(nameof(DocType.Description), 100, GlobalSetting.ResManager.GetString("Description")));
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
                lciIdUserAttri1.Text = lciHIdUserAttri1.Text = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals(Constants.HF_USER_ATTR_01)).Select(a => a.Description).SingleOrDefault();
                lciIdUserAttri2.Text = lciHIdUserAttri2.Text = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals(Constants.HF_USER_ATTR_02)).Select(a => a.Description).SingleOrDefault();
                lciIdUserAttri3.Text = lciHIdUserAttri3.Text = _userAttrDescriptionList.Where(u => u.IdUserAttr.Equals(Constants.HF_USER_ATTR_03)).Select(a => a.Description).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        private void LoadAuxList()
        {
            try
            {
                _statusProdList = GlobalSetting.StatusProdService.GetStatusProd();
                _familiesHkList = GlobalSetting.FamilyHKService.GetFamiliesHK();
                _supplierList = GlobalSetting.SupplierService.GetSuppliers();
                _docsTypeList = GlobalSetting.DocTypeService.GetDocsType(Constants.ITEM_GROUP_HF);
                _userAttrDescriptionList = GlobalSetting.UserAttrDescriptionService.GetUserAttrsDescription(Constants.ITEM_GROUP_HF);
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
                _itemsList = GlobalSetting.ItemHfService.GetItems();
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
        private void LoadItemForm(ItemHf item)
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
                _itemsHistoryList = GlobalSetting.ItemHfService.GetItemHfHistory(_itemUpdate.IdItemBcn);
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
                _itemDocsList = GlobalSetting.ItemDocService.GetItemsDocs(_itemUpdate.IdItemBcn, Constants.ITEM_GROUP_HF);
                xgrdDocsHistory.DataSource = _itemDocsList;

                _itemLastDocsList = GlobalSetting.ItemDocService.GetLastItemsDocs(_itemUpdate.IdItemBcn, Constants.ITEM_GROUP_HF);
                xgrdLastDocs.DataSource = _itemLastDocsList;

                xtpDocs.PageVisible = true;
                gbNewDoc.Enabled = false;

                //Item Image
                //Quitamos el menú contextual ya que no nos interesa en este momento
                peItemImage.Properties.ShowMenu = false;
                peItemImage.Properties.SizeMode = PictureSizeMode.Zoom;

                if (string.IsNullOrEmpty(_itemUpdate.PhotoPath)) return;

                //Cargamos la imagen de la cache.
                AddToPhotoCache(_itemUpdate.PhotoPath);
                Bitmap im = null;
                if (photosCache.ContainsKey(_itemUpdate.PhotoPath))
                {
                    im = photosCache[_itemUpdate.PhotoPath];
                }
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
                    try
                    {
                        using (var response = request.GetResponse())
                        {
                            using (var stream = response.GetResponseStream())
                            {
                                Image p = Bitmap.FromStream(stream);
                                photosCache.Add(url, (Bitmap)p);
                            }
                        }
                    }
                    catch (System.Net.WebException wex)
                    {
                        if (((System.Net.HttpWebResponse)(wex.Response)).StatusCode != System.Net.HttpStatusCode.NotFound)
                        {
                            throw;
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
            DXMenuItem menuItem = new DXMenuItem(GlobalSetting.ResManager.GetString("ViewItemPriceList"),
                new EventHandler(OnMenuItemViewItemPriceListClick));
            menuItem.Tag = new RowInfo(view, rowHandle);
            return menuItem;
        }

        private void OpenPriceListForm(ItemHf ItemHf)
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
                priceListForm.InitData(ItemHf.IdDefaultSupplier, ItemHf.IdItemBcn);

                priceListForm.MdiParent = this.MdiParent;
                priceListForm.ShowIcon = false;
                //priceListForm.Dock = DockStyle.Fill;
                //priceListForm.ControlBox = false;
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
                //En función de si está en seleccionada la tab del formulario o del grid activaremos la edición de esa tab
                if (xtcGeneral.SelectedTabPage == xtpForm || xtcGeneral.SelectedTabPage == xtpDocs)
                {
                    xtpList.PageVisible = false;
                    gbNewDoc.Enabled = true;
                    SetEditingFieldsEnabled();
                }
                else if (xtcGeneral.SelectedTabPage == xtpList)
                {
                    xtpForm.PageVisible = false;
                    xtpDocs.PageVisible = false;

                    //Hacemos el grid editable y marcamos a mano qué columnas se puede editar
                    rootGridViewItems.OptionsBehavior.Editable = true;

                    foreach (GridColumn col in rootGridViewItems.Columns)
                    {
                        if (Array.IndexOf(_editingCols, col.FieldName) >= 0)
                        {
                            col.OptionsColumn.AllowEdit = true;
                        }
                        else
                        {
                            col.OptionsColumn.AllowEdit = false;
                        }
                    }
                    //desuscribirse de los eventos mientras editamos el grid
                    rootGridViewItems.DoubleClick -= rootGridViewItems_DoubleClick;
                    rootGridViewItems.PopupMenuShowing -= rootGridViewItems_PopupMenuShowing;

                    //cambiar los estilos del grid
                    SetItemGridStylesByState();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddModifiedItemToList(ItemHf modifiedItem)
        {
            try
            {
                var item = _modifiedItemsEy.FirstOrDefault(a => a.IdItemBcn.Equals(modifiedItem.IdItemBcn));
                if (item == null)
                {
                    _modifiedItemsEy.Add(modifiedItem);
                }
                else
                {
                    item.IdDefaultSupplier = modifiedItem.IdDefaultSupplier;
                    item.IdUserAttri1 = modifiedItem.IdUserAttri1;
                    item.IdUserAttri2 = modifiedItem.IdUserAttri2;
                    item.IdUserAttri3 = modifiedItem.IdUserAttri3;
                    item.IdStatusProd = modifiedItem.IdStatusProd;
                }
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
        private void MoveGridToItem(string idItemBcn)
        {
            try
            {
                //Buscar por un valor
                GridColumn column = rootGridViewItems.Columns[nameof(ItemHf.IdItemBcn)];
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

        private bool IsValidItem()
        {
            try
            {
                if (xtcGeneral.SelectedTabPage == xtpList)
                    return IsValidItemGrid();
                else if (xtcGeneral.SelectedTabPage == xtpForm || xtcGeneral.SelectedTabPage == xtpDocs)
                    return IsValidItemForm();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidItemGrid()
        {
            try
            {
                foreach (var item in _modifiedItemsEy)
                {
                    if (string.IsNullOrEmpty(item.IdDefaultSupplier))
                    {
                        XtraMessageBox.Show("Default Supplier Field Required", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private bool IsValidItemForm()
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
                        XtraMessageBox.Show(GlobalSetting.ResManager.GetString("FileDoesntExist"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (lueDocType.EditValue == null)
                    {
                        XtraMessageBox.Show(GlobalSetting.ResManager.GetString("SelectDocType"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        #region Update Item

        /// <summary>
        /// Actualizar los datos de un item
        /// </summary>
        /// <param name="newVersion">Si es una versión nueva o una actualización de la existente</param>
        /// <returns></returns>
        private bool UpdateItem(bool newVersion = false)
        {
            try
            {
                return GlobalSetting.ItemHfService.UpdateItem(_itemUpdate, newVersion);
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
                new System.IO.FileInfo(Constants.ITEMS_DOCS_PATH + _itemUpdate.IdItemBcn + "\\" + lueDocType.EditValue.ToString() + "\\").Directory.Create();


                ItemDoc itemDoc = new ItemDoc();
                itemDoc.IdItemBcn = _itemUpdate.IdItemBcn;
                itemDoc.IdItemGroup = Constants.ITEM_GROUP_HF;
                itemDoc.IdDocType = lueDocType.EditValue.ToString();
                itemDoc.FileName = fileNameNoExtension + "_" + (_itemUpdate.IdVer.ToString()) + "." + ((_itemUpdate.IdSubVer + 1).ToString()) + extension;
                itemDoc.FilePath = _itemUpdate.IdItemBcn + "\\" + lueDocType.EditValue.ToString() + "\\" + itemDoc.FileName;

                //move to file server
                System.IO.File.Copy(txtPathNewDoc.Text, Constants.ITEMS_DOCS_PATH + itemDoc.FilePath, overwrite: true);

                //update database
                return GlobalSetting.ItemHfService.UpdateItemWithDoc(_itemUpdate, itemDoc, newVersion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool UpdateItems()
        {
            try
            {
                return GlobalSetting.ItemHfService.UpdateItems(_modifiedItemsEy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void ActionsAfterCU()
        {
            try
            {
                string idItemBcn = (_itemOriginal == null ? string.Empty : _itemOriginal.IdItemBcn);
                _itemOriginal = null;
                ResetItemUpdate();
                SetFormBinding();
                xtpForm.PageVisible = false;
                xtpDocs.PageVisible = false;
                xtpList.PageVisible = true;
                LoadItemsList();
                MoveGridToItem(idItemBcn);
                SetItemGridStylesByState();
                //suscribirse de nuevo a los eventos y hacer el grid no editable
                rootGridViewItems.DoubleClick += rootGridViewItems_DoubleClick;
                rootGridViewItems.PopupMenuShowing += rootGridViewItems_PopupMenuShowing;
                rootGridViewItems.OptionsBehavior.Editable = false;

                RestoreInitState();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetItemGridStylesByState()
        {
            try
            {
                foreach (GridColumn col in rootGridViewItems.Columns)
                {
                    switch (CurrentState)
                    {
                        case ActionsStates.Edit:

                            if (Array.IndexOf(_editingCols, col.FieldName) >= 0)
                                col.AppearanceCell.BackColor = Color.White;
                            else
                                col.AppearanceCell.BackColor = Color.GhostWhite;
                            break;

                        default:
                            col.AppearanceCell.BackColor = Color.White;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}