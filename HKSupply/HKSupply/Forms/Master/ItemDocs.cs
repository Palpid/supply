using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using HKSupply.DB;
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
using DevExpress.XtraBars;

namespace HKSupply.Forms.Master
{
    public partial class ItemDocs : RibbonFormBase
    {
        #region Private Members

        List<Model> _modelList;
        List<ItemEy> _itemEyList;
        List<ItemEy> _itemsModelList;
        List<DocType> _docsTypeList;

        #endregion

        #region Constructor
        public ItemDocs()
        {
            InitializeComponent();

            try
            {
                ConfigureRibbonActions();
                LoadAuxList();
                SetUpSlueModel();
                SetUpCheckedListBoxControlItems();
                SetUpLueDocType();
                //SetUpGrdBomBreakdown();
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
                EnableExportExcel = false;
                EnableExportCsv = false;
                ConfigurePrintExportOptions();
                //Layout
                EnableLayoutOptions = false;
                ConfigureLayoutOptions();
                //Excepcionalmente esta pantalla se abre directamente en modo edición
                CurrentState = ActionsStates.Edit;

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
                ResetForm();
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

                if (IsValid() == false)
                    return;

                DialogResult result = MessageBox.Show(GlobalSetting.ResManager.GetString("SaveChanges"), "", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                Cursor = Cursors.WaitCursor;

                res = UpdateItemsDoc();

                if (res == true)
                {
                    MessageBox.Show(GlobalSetting.ResManager.GetString("SaveSuccessfully"));
                    ResetForm();
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
        private void ItemDocs_Load(object sender, EventArgs e)
        {

        }

        private void SlueModel_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string idModel = slueModel.EditValue as string;
                _itemsModelList = new List<ItemEy>();
                if (string.IsNullOrEmpty(idModel) == false)
                    _itemsModelList = _itemEyList.Where(a => a.IdModel.Equals(idModel)).ToList();
                LoadCheckedList();

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckedListBoxControlItems_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            try
            {
                if (e.Index == 0)
                {
                    if (e.State == CheckState.Checked)
                    {
                        checkedListBoxControlItems.CheckAll();
                        checkedListBoxControlItems.Items[0].Description = "Uncheck All";
                    }
                    else
                    {
                        checkedListBoxControlItems.UnCheckAll();
                        checkedListBoxControlItems.Items[0].Description = "Check All"; }
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

        #region Private Members

        #region Loads

        private void LoadAuxList()
        {
            try
            {
                _modelList = GlobalSetting.ModelService.GetModels();
                _itemEyList = GlobalSetting.ItemEyService.GetItems();
                _docsTypeList = GlobalSetting.DocTypeService.GetDocsType(Constants.ITEM_GROUP_EY);
            }
            catch
            {
                throw;
            }
        }

        private void LoadCheckedList()
        {
            try
            {
                checkedListBoxControlItems.Items.Clear();
                checkedListBoxControlItems.Items.Add("Check All", false);

                foreach (var item in _itemsModelList)
                {
                    checkedListBoxControlItems.Items.Add(item.IdItemBcn, false);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region SetUp Form Objects

        private void SetUpSlueModel()
        {
            try
            {
                slueModel.Properties.DataSource = _modelList;
                slueModel.Properties.ValueMember = nameof(Model.IdModel);
                slueModel.Properties.DisplayMember = nameof(Model.Description);
                slueModel.Properties.View.Columns.AddField(nameof(Model.Description)).Visible = true;
                slueModel.Properties.NullText = "Select model...";
                slueModel.EditValueChanged += SlueModel_EditValueChanged;
            }
            catch
            {
                throw;
            }
        }

        private void SetUpCheckedListBoxControlItems()
        {
            try
            {
                checkedListBoxControlItems.CheckOnClick = true;
                checkedListBoxControlItems.ItemCheck += CheckedListBoxControlItems_ItemCheck;
            }
            catch
            {
                throw;
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
                lueDocType.Properties.NullText = "Select Type...";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CRUD

        private bool IsValid()
        {
            try
            {

                if((checkedListBoxControlItems.CheckedItems.Count == 0) 
                    || (checkedListBoxControlItems.CheckedItems.Count == 1 && checkedListBoxControlItems.Items[0].CheckState == CheckState.Checked))
                {
                    XtraMessageBox.Show("Select Items", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (string.IsNullOrEmpty(txtPathNewDoc.Text) == true)
                {
                    XtraMessageBox.Show("Select a file", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

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

                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool UpdateItemsDoc()
        {
            try
            {
                foreach(CheckedListBoxItem item in checkedListBoxControlItems.CheckedItems)
                {
                    ItemEy itemTmp = _itemsModelList.Where(a => a.IdItemBcn.Equals(item.Value)).SingleOrDefault();
                    //Nota: No hay transacción entre item/archivo
                    if (itemTmp != null)
                        UpdateItemWithDoc(itemTmp);
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool UpdateItemWithDoc(ItemEy itemUpdate, bool newVersion = false)
        {
            try
            {
                string fileName = System.IO.Path.GetFileName(txtPathNewDoc.Text);
                string fileNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(txtPathNewDoc.Text);
                string extension = System.IO.Path.GetExtension(txtPathNewDoc.Text);

                //Creamos los directorios si no existen
                new System.IO.FileInfo(Constants.ITEMS_DOCS_PATH + itemUpdate.IdItemBcn + "\\" + lueDocType.EditValue.ToString() + "\\").Directory.Create();


                ItemDoc itemDoc = new ItemDoc();
                itemDoc.IdItemBcn = itemUpdate.IdItemBcn;
                itemDoc.IdItemGroup = Constants.ITEM_GROUP_EY;
                itemDoc.IdDocType = lueDocType.EditValue.ToString();
                itemDoc.FileName = fileNameNoExtension + "_" + (itemUpdate.IdVer.ToString()) + "." + ((itemUpdate.IdSubVer + 1).ToString()) + extension;
                itemDoc.FilePath = itemUpdate.IdItemBcn + "\\" + lueDocType.EditValue.ToString() + "\\" + itemDoc.FileName;

                //move to file server
                System.IO.File.Copy(txtPathNewDoc.Text, Constants.ITEMS_DOCS_PATH + itemDoc.FilePath, overwrite: true);

                //update database
                return GlobalSetting.ItemEyService.UpdateItemWithDoc(itemUpdate, itemDoc, newVersion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void ResetForm()
        {
            try
            {
                txtPathNewDoc.Text = string.Empty;
                slueModel.EditValue = null;
                lueDocType.EditValue = null;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
