using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using HKSupply.General;
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
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using HKSupply.Helpers;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace HKSupply.Forms.Master
{
    public partial class PrototypeManagement : RibbonFormBase
    {
        #region Constants
        private const string VIEW_COLUMN = "View";
        #endregion

        #region Private Members
        List<Prototype> _prototypesList;
        Prototype _prototypeCurrent;

        List<DocType> _docsTypeList;
        List<PrototypeDoc> _prototypeDocsList;
        List<PrototypeDoc> _prototypeLastDocsList;
        #endregion

        #region Constructor
        public PrototypeManagement()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                LoadAuxList();
                SetUpGrdProtos();
                SetUpGrdLastDocs();
                SetUpGrdDocsHistory();
                SetUpLueDocType();
                LoadPrototypesList();
            }
            catch(Exception ex)
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiExportExcel_ItemClick(sender, e);
        }

        public override void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiExportCsv_ItemClick(sender, e);
        }

        public override void bbiCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiCancel_ItemClick(sender, e);

            try
            {
                _prototypeCurrent = null;
                _prototypeDocsList = null;
                _prototypeLastDocsList = null;
                xtpDocs.PageVisible = false;
                xtpList.PageVisible = true;
                LoadPrototypesList();
                txtPathNewDoc.Text = string.Empty;
                rootGridViewProtos.OptionsBehavior.Editable = false;
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
                if (_prototypeCurrent == null)
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

        public override void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.bbiSave_ItemClick(sender, e);

            try
            {
                bool res = false;

                if (IsValidProto() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                if (string.IsNullOrEmpty(txtPathNewDoc.Text))
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("NoPendingChanges"));
                    return;
                }

                if (CurrentState == ActionsStates.Edit)
                {
                    res = AddPrototypeDoc();
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

        private void PrototypesManagement_Load(object sender, EventArgs e)
        {
            try
            {
                xtpDocs.PageVisible = false;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RootGridViewProtos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.GetRow(view.FocusedRowHandle) is Prototype proto)
                {
                    LoadProtoDocs(proto);
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RepButtonLastDoc_Click(object sender, EventArgs e)
        {
            try
            {
                PrototypeDoc protoDoc = gridViewLastDocs.GetRow(gridViewLastDocs.FocusedRowHandle) as PrototypeDoc;

                if (protoDoc != null)
                {
                    DocHelper.OpenDoc(Constants.PROTO_DOCS_PATH + protoDoc.FilePath);
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
                PrototypeDoc protoDoc = gridViewDocsHistory.GetRow(gridViewDocsHistory.FocusedRowHandle) as PrototypeDoc;

                if (protoDoc != null)
                {
                    DocHelper.OpenDoc(Constants.PROTO_DOCS_PATH + protoDoc.FilePath);
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

        #endregion

        #region Private Methods

        #region SetUp Form Object

        private void SetUpGrdProtos()
        {
            try
            {
                //Para que aparezca el scroll horizontal hay que desactivar el auto width y poner a mano el width de cada columna
                rootGridViewProtos.OptionsView.ColumnAutoWidth = false;
                rootGridViewProtos.HorzScrollVisibility = ScrollVisibility.Auto;

                //hacer todo el grid no editable
                rootGridViewProtos.OptionsBehavior.Editable = false;

                //Columns definition
                GridColumn colIdPrototype = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Id"), Visible = true, FieldName = nameof(Prototype.IdPrototype), Width = 70 };
                GridColumn colPrototypeName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Name"), Visible = true, FieldName = nameof(Prototype.PrototypeName), Width = 150 };
                GridColumn colIdPrototypeDescription = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Description"), Visible = true, FieldName = nameof(Prototype.PrototypeDescription), Width = 200 };
                GridColumn colPrototypeStatus = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Status"), Visible = true, FieldName = nameof(Prototype.PrototypeStatus), Width = 70 };
                GridColumn colDefaultSupplier = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("DefaultSupplier"), Visible = true, FieldName = $"{nameof(Prototype.DefaultSupplier)}.{nameof(Supplier.ContactName)}", Width = 150 };
                GridColumn colCaliber = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("Caliber"), Visible = true, FieldName = nameof(Prototype.Caliber), Width = 70 };
                GridColumn colLaunchDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("LaunchDate"), Visible = true, FieldName = nameof(Prototype.LaunchDate), Width = 90 };
                GridColumn colCreateDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CreateDate"), Visible = true, FieldName = nameof(Prototype.CreateDate), Width = 120 };

                //Display Format
                colLaunchDate.DisplayFormat.FormatType = FormatType.DateTime;
                colCreateDate.DisplayFormat.FormatType = FormatType.DateTime;

                colCaliber.DisplayFormat.FormatType = FormatType.Numeric;
                colCaliber.DisplayFormat.FormatString = "N2";

                //Addd columns to grid root view;
                rootGridViewProtos.Columns.Add(colIdPrototype);
                rootGridViewProtos.Columns.Add(colPrototypeName);
                rootGridViewProtos.Columns.Add(colIdPrototypeDescription);
                rootGridViewProtos.Columns.Add(colPrototypeStatus);
                rootGridViewProtos.Columns.Add(colDefaultSupplier);
                rootGridViewProtos.Columns.Add(colCaliber);
                rootGridViewProtos.Columns.Add(colLaunchDate);
                rootGridViewProtos.Columns.Add(colCreateDate);

                //Events
                rootGridViewProtos.DoubleClick += RootGridViewProtos_DoubleClick;

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
                //GridColumn colIdVerItem = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemVer"), Visible = true, FieldName = nameof(ItemDoc.IdVerItem), Width = 60 };
                //GridColumn colIdSubVerItem = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemSubver"), Visible = true, FieldName = nameof(ItemDoc.IdSubVerItem), Width = 75 };
                //GridColumn colIdDocType = new GridColumn() { Caption = "IdDocType", Visible = false, FieldName = nameof(ItemDoc.IdDocType), Width = 10 };
                GridColumn colDocType = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("DocType"), Visible = true, FieldName = $"{nameof(PrototypeDoc.DocType)}.{nameof(DocType.Description)}", Width = 100 };
                GridColumn colFileName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FileName"), Visible = true, FieldName = nameof(PrototypeDoc.FileName), Width = 250 };
                GridColumn colFilePath = new GridColumn() { Caption = "FilePath", Visible = false, FieldName = nameof(PrototypeDoc.FilePath), Width = 10 };
                GridColumn colCreateDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CreateDate"), Visible = true, FieldName = nameof(PrototypeDoc.CreateDate), Width = 150 };
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
                repButtonLastDoc.Click += RepButtonLastDoc_Click;

                colViewButton.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
                colViewButton.ColumnEdit = repButtonLastDoc;

                ////Only button allow to edit to allow click
                //colIdVerItem.OptionsColumn.AllowEdit = false;
                //colIdSubVerItem.OptionsColumn.AllowEdit = false;
                //colIdDocType.OptionsColumn.AllowEdit = false;
                colDocType.OptionsColumn.AllowEdit = false;
                colFileName.OptionsColumn.AllowEdit = false;
                colFilePath.OptionsColumn.AllowEdit = false;
                colCreateDate.OptionsColumn.AllowEdit = false;
                colViewButton.OptionsColumn.AllowEdit = true;

                //Add columns to grid root view
                //gridViewLastDocs.Columns.Add(colIdVerItem);
                //gridViewLastDocs.Columns.Add(colIdSubVerItem);
                //gridViewLastDocs.Columns.Add(colIdDocType);
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

                ////Columns definition
                //GridColumn colIdVerItem = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemVer"), Visible = true, FieldName = nameof(ItemDoc.IdVerItem), Width = 60 };
                //GridColumn colIdSubVerItem = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("ItemSubver"), Visible = true, FieldName = nameof(ItemDoc.IdSubVerItem), Width = 75 };
                //GridColumn colIdDocType = new GridColumn() { Caption = "IdDocType", Visible = false, FieldName = nameof(ItemDoc.IdDocType), Width = 10 };
                GridColumn colDocType = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("DocType"), Visible = true, FieldName = $"{nameof(PrototypeDoc.DocType)}.{nameof(DocType.Description)}", Width = 100 };
                GridColumn colFileName = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("FileName"), Visible = true, FieldName = nameof(PrototypeDoc.FileName), Width = 280 };
                GridColumn colFilePath = new GridColumn() { Caption = "FilePath", Visible = false, FieldName = nameof(PrototypeDoc.FilePath), Width = 10 };
                GridColumn colCreateDate = new GridColumn() { Caption = GlobalSetting.ResManager.GetString("CreateDate"), Visible = true, FieldName = nameof(PrototypeDoc.CreateDate), Width = 120 };
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
                //colIdVerItem.OptionsColumn.AllowEdit = false;
                //colIdSubVerItem.OptionsColumn.AllowEdit = false;
                //colIdDocType.OptionsColumn.AllowEdit = false;
                colDocType.OptionsColumn.AllowEdit = false;
                colFileName.OptionsColumn.AllowEdit = false;
                colFilePath.OptionsColumn.AllowEdit = false;
                colCreateDate.OptionsColumn.AllowEdit = false;
                colViewButton.OptionsColumn.AllowEdit = true;

                //Add columns to grid root view
                //gridViewDocsHistory.Columns.Add(colIdVerItem);
                //gridViewDocsHistory.Columns.Add(colIdSubVerItem);
                //gridViewDocsHistory.Columns.Add(colIdDocType);
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

        #endregion

        #region Loads

        private void LoadAuxList()
        {
            try
            {
                _docsTypeList = GlobalSetting.DocTypeService.GetDocsType(Constants.ITEM_GROUP_PROTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadPrototypesList()
        {
            try
            {
                _prototypesList = GlobalSetting.PrototypeService.GetPrototypes();
                xgrdProtos.DataSource = _prototypesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadProtoDocs(Prototype proto)
        {
            try
            {
                _prototypeCurrent = proto.Clone();
                xtpDocs.PageVisible = true;
                xtcGeneral.SelectedTabPage = xtpDocs;

                lblIdProto.DataBindings.Clear();
                lblIdProto.DataBindings.Add<Prototype>(_prototypeCurrent, (Control c) => c.Text, p => p.IdPrototype);

                lblProtoName.DataBindings.Clear();
                lblProtoName.DataBindings.Add<Prototype>(_prototypeCurrent, (Control c) => c.Text, p => p.PrototypeName);

                _prototypeDocsList = GlobalSetting.PrototypeDocService.GetPrototypeDocs(_prototypeCurrent.IdPrototype);
                _prototypeLastDocsList = GlobalSetting.PrototypeDocService.GetLastPrototypeDocs(_prototypeCurrent.IdPrototype);

                xgrdDocsHistory.DataSource = _prototypeDocsList;
                xgrdLastDocs.DataSource = _prototypeLastDocsList;

                gbNewDoc.Enabled = false;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        private void ConfigureRibbonActionsEditing()
        {
            try
            {
                gbNewDoc.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidProto()
        {
            try
            {
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void MoveGridToProto(string idProto)
        {
            try
            {
                GridColumn column = rootGridViewProtos.Columns[nameof(Prototype.IdPrototype)];
                if (column != null)
                {
                    // locating the row 
                    int rhFound = rootGridViewProtos.LocateByDisplayText(rootGridViewProtos.FocusedRowHandle + 1, column, idProto);
                    // focusing the cell 
                    if (rhFound != GridControl.InvalidRowHandle)
                    {
                        rootGridViewProtos.FocusedRowHandle = rhFound;
                        rootGridViewProtos.FocusedColumn = column;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region CU

        private bool AddPrototypeDoc(bool newVersion = false)
        {
            try
            {
                string fileName = System.IO.Path.GetFileName(txtPathNewDoc.Text);
                string fileNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(txtPathNewDoc.Text);
                string extension = System.IO.Path.GetExtension(txtPathNewDoc.Text);

                //Creamos los directorios si no existen
                new System.IO.FileInfo(Constants.PROTO_DOCS_PATH
                    + String.Concat(_prototypeCurrent.IdPrototype.Split(System.IO.Path.GetInvalidFileNameChars())) //sanitize path
                    + "\\" + lueDocType.EditValue.ToString() + "\\")
                    .Directory.Create();

                PrototypeDoc protoDoc = new PrototypeDoc();
                protoDoc.IdPrototype = _prototypeCurrent.IdPrototype;
                protoDoc.IdDocType = lueDocType.EditValue.ToString();
                protoDoc.FileName = fileNameNoExtension + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + extension;
                protoDoc.FilePath = String.Concat(_prototypeCurrent.IdPrototype.Split(System.IO.Path.GetInvalidFileNameChars())) //sanitize file name
                    + "\\" + lueDocType.EditValue.ToString() + "\\" + protoDoc.FileName;

                //move to file server
                System.IO.File.Copy(txtPathNewDoc.Text, Constants.PROTO_DOCS_PATH + protoDoc.FilePath, overwrite: true);

                //update database
                return GlobalSetting.PrototypeService.AddPrototypeDoc(protoDoc);

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
                txtPathNewDoc.Text = string.Empty;
                string idProto = (_prototypeCurrent == null ? string.Empty : _prototypeCurrent.IdPrototype);
                _prototypeCurrent = null;
                _prototypeDocsList = null;
                _prototypeLastDocsList = null;
                xtpDocs.PageVisible = false;
                xtpList.PageVisible = true;
                LoadPrototypesList();
                MoveGridToProto(idProto);
                RestoreInitState();
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
