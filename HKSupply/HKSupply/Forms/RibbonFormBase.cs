using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using HKSupply.Models;
using HKSupply.General;
using System.IO;

namespace HKSupply.Forms
{
    public partial class RibbonFormBase : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        #region Enums
        public enum ActionsStates
        {
            OnlyRead,
            OnlyEdit,
            OnlyEditNew,
            Edit,
            New,
            OnlyNew
        }
        #endregion

        #region Private Members

        private ActionsStates _currentState;

        private bool isLoadingWorkspace;

        #endregion

        #region Public Properties

        public bool Read { get; set; }
        public bool New { get; set; }
        public bool Modify { get; set; }

        public bool EnablePrintPreview { get; set; }
        public bool EnableExportExcel { get; set; }
        public bool EnableExportCsv { get; set; }
        public bool EnableLayoutOptions { get; set; }

        public string ExportExcelFile { get; set; }
        public string ExportCsvFile { get; set; }

        public string CurrentLayout { get; set; }

        public ActionsStates CurrentState 
        { 
            get { return _currentState; }
            set 
            {
                _currentState = value;
                ConfigureByState(_currentState);
            }
        }

        #endregion

        #region Constructor
        public RibbonFormBase()
        {
            InitializeComponent();
            ConfigureRibbonEvents();
            ConfigurePrintExportOptions();
            ConfigureLayoutOptions();
            ConfigureRibbonStyles();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Lee las acciones que tiene permitido un formulario.
        /// </summary>
        /// <remarks>
        /// No hay que llamarlo desde dentro porque el "Name" es el de "RibbonFormBase", hay que llamarlo desde los que lo herendan
        /// para que coja su nombre
        /// </remarks>
        public void SetActions()
        {
            var actions = GlobalSetting.FunctionalitiesRoles.FirstOrDefault(fr => fr.Functionality.FormName.Equals(Name));
            //Task Buttons
            Read = actions.Read;
            New = actions.New;
            Modify = actions.Modify;
            //"Breadcrumb"
            SetRibbonText($"{actions.Functionality.Category} > {actions.Functionality.FunctionalityName}");

            //Reports
            GetFormReports(actions.FunctionalityId);

        }

        public void RestoreInitState()
        {
            ConfigureActions();
        }

        public void ConfigurePrintExportOptions()
        {
            //bbiPrintPreview.Visibility = (ShowPrintPreview ? BarItemVisibility.Always : BarItemVisibility.Never);
            //bbiExportExcel.Visibility = (ShowExportExcel ? BarItemVisibility.Always : BarItemVisibility.Never);
            //bbiExportCsv.Visibility = (ShowExportCsv ? BarItemVisibility.Always : BarItemVisibility.Never);

            //if (ShowPrintPreview == false && ShowExportExcel == false && ShowExportCsv == false)
            //    ribbonPageGroup2.Visible = false;
            //else
            //    ribbonPageGroup2.Visible = true;

            bbiPrintPreview.Enabled = EnablePrintPreview;
            bbiExportExcel.Enabled = EnableExportExcel;
            bbiExportCsv.Enabled = EnableExportCsv;
        }

        public void ConfigureLayoutOptions()
        {
            //v1, los oculto ya que usamos el workspace manager
            //bbiSaveLayout.Enabled = EnableLayoutOptions;
            //bsiRestoreLayout.Enabled = EnableLayoutOptions;
            bbiSaveLayout.Visibility = BarItemVisibility.Never;
            bsiRestoreLayout.Visibility = BarItemVisibility.Never;

            bwmiLayouts.Enabled = EnableLayoutOptions;

            if (EnableLayoutOptions)
            {
                LoadWorkspacesLayouts();
            }

            bwmiLayouts.WorkspaceManager.TransitionType = new DevExpress.Utils.Animation.FadeTransition();
        }

        public void SetRibbonText(string title)
        {
            ribbonPage1.Text = $"Home > {title}";
        }

        public void AddRestoreLayoutItems(List<Layout> layouts)
        {
            try
            {
                BarButtonItem layoutButton;

                bsiRestoreLayout.ClearLinks();

                //Puedem existir más de un objeto que hayamos guardado layput en un formulario, pero comparten nombre
                var distinctLayouts = layouts.GroupBy(a => a.LayoutName).Select(group => group.First());

                foreach (var layout in distinctLayouts)
                {
                    layoutButton = new BarButtonItem() { Caption = layout.LayoutName, Tag = layout.LayoutName };
                    layoutButton.ItemClick += LayoutButton_ItemClick;
                    bsiRestoreLayout.AddItem(layoutButton);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Private Functions

        private void ConfigureActions()
        {
            if (Read == true && New == false && Modify == false)
                ConfigureByState(ActionsStates.OnlyRead);
            else if (Read == true && New == true && Modify == true)
                ConfigureByState(ActionsStates.OnlyEditNew);
            else if (Read == true && New == false && Modify == true)
                ConfigureByState(ActionsStates.OnlyEdit);
            else if (Read == true && New == true && Modify == false)
                ConfigureByState(ActionsStates.OnlyNew);
        }

        private void ConfigureByState(ActionsStates state)
        {
            _currentState = state;
            switch (state)
            {
                case ActionsStates.OnlyRead:
                    bbiEdit.Enabled = false;
                    bbiNew.Enabled = false;
                    bbiSave.Enabled = false;
                    bbiCancel.Enabled = false;
                    break;
                case ActionsStates.OnlyEdit:
                    bbiEdit.Enabled = true;
                    bbiNew.Enabled = false;
                    bbiSave.Enabled = false;
                    bbiCancel.Enabled = false;
                    break;
                case ActionsStates.OnlyEditNew:
                    bbiEdit.Enabled = true;
                    bbiNew.Enabled = true;
                    bbiSave.Enabled = false;
                    bbiCancel.Enabled = false;
                    break;
                case ActionsStates.Edit:
                case ActionsStates.New:
                    bbiEdit.Enabled = false;
                    bbiNew.Enabled = false;
                    bbiSave.Enabled = true;
                    bbiCancel.Enabled = true;
                    break;
                case ActionsStates.OnlyNew:
                    bbiEdit.Enabled = false;
                    bbiNew.Enabled = true;
                    bbiSave.Enabled = false;
                    bbiCancel.Enabled = false;
                    break;
            }
        }

        private void ConfigureRibbonEvents()
        {
            try
            {
                FormClosing += RibbonFormBase_FormClosing;
                Load += RibbonFormBase_Load;

                //Task buttons
                bbiEdit.ItemClick += bbiEdit_ItemClick;
                bbiNew.ItemClick += bbiNew_ItemClick;
                bbiCancel.ItemClick += bbiCancel_ItemClick;
                bbiSave.ItemClick += bbiSave_ItemClick;
                bbiClose.ItemClick += bbiClose_ItemClick;

                //Print and export button
                bbiPrintPreview.ItemClick += bbiPrintPreview_ItemClick;
                bbiExportExcel.ItemClick += bbiExportExcel_ItemClick;
                bbiExportCsv.ItemClick += bbiExportCsv_ItemClick;

                //Layouts options
                bbiSaveLayout.ItemClick += BbiSaveLayout_ItemClick;

                bwmiLayouts.Popup += BwmiLayouts_Popup;
                bwmiLayouts.WorkspaceManager.WorkspaceCollectionChanged += WorkspaceManager_WorkspaceCollectionChanged;

                //test
                ribbonControl.Paint += RibbonControl_Paint;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Draw Etnia Logo on right side of the ribbon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonControl_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                DevExpress.XtraBars.Ribbon.ViewInfo.RibbonViewInfo ribbonViewInfo = ribbonControl.ViewInfo;
                if (ribbonViewInfo == null)
                    return;
                DevExpress.XtraBars.Ribbon.ViewInfo.RibbonPanelViewInfo panelViewInfo = ribbonViewInfo.Panel;
                if (panelViewInfo == null)
                    return;
                Rectangle bounds = panelViewInfo.Bounds;
                int minX = bounds.X;
                DevExpress.XtraBars.Ribbon.ViewInfo.RibbonPageGroupViewInfoCollection groups = panelViewInfo.Groups;
                if (groups == null)
                    return;
                if (groups.Count > 0)
                    minX = groups[groups.Count - 1].Bounds.Right;
                //Image image = DevExpress.Utils.Frames.ApplicationCaption8_1.GetImageLogoEx(LookAndFeel);
                //Image image = Image.FromFile(@"Resources\Images\logo_etnia-barcelona.png");
                Image image = Properties.Resources.logo_etnia_barcelona;

                if (bounds.Height < image.Height)
                    return;
                int offset = (bounds.Height - image.Height) / 2;
                int width = image.Width + 15;
                bounds.X = bounds.Width - width;
                if (bounds.X < minX)
                    return;
                bounds.Width = width;
                bounds.Y += offset;
                bounds.Height = image.Height;
                e.Graphics.DrawImage(image, bounds.Location);



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RibbonFormBase_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigureStatusBar();
            }
            catch
            {
                throw;
            }
        }

        private void RibbonFormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_currentState == ActionsStates.Edit)
            {
                //DialogResult result = MessageBox.Show("Pending changes. Close?", "", MessageBoxButtons.YesNo);

                //if (result != DialogResult.Yes)
                //    e.Cancel = true;

                DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction action = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutAction() { Caption = "Confirm", Description = "Pending changes. Close?" };
                Predicate<DialogResult> predicate = canCloseFunc;
                DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand command1 = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand() { Text = "Close", Result = DialogResult.Yes };
                DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand command2 = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutCommand() { Text = "Cancel", Result = DialogResult.No };
                action.Commands.Add(command1);
                action.Commands.Add(command2);
                DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutProperties properties = new DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutProperties();
                properties.ButtonSize = new Size(100, 40);
                properties.Style = DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutStyle.MessageBox;

                if (DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties, predicate) == DialogResult.Yes)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        private static bool canCloseFunc(DialogResult parameter)
        {
            return parameter != DialogResult.Cancel;
        }

        private void ConfigureRibbonStyles()
        {
            //Cambiar a un estilo más minimalista en lugar del estilo Office 2010 de ribbon con iconos grandes
            //ribbonControl.RibbonStyle = RibbonControlStyle.OfficeUniversal;

            ribbonControl.ToolbarLocation = RibbonQuickAccessToolbarLocation.Hidden;
            ribbonControl.DrawGroupCaptions = DefaultBoolean.False;

            ribbonPage1.Appearance.Font = new Font(ribbonPage1.Appearance.Font, FontStyle.Bold);

            //Aplicar styles Etnia al skin por defecto
            Styles.AppStyles.SetEtniaSkinStyles(); 
        }

        private void ConfigureStatusBar()
        {
            try
            {
                //Si estamos diseñando no tiene que pasar por aquí. Al ser un form que se hereda se llama al constructor y peta el diseñador al no tener acceso estos parámetros
                //if (DesignMode) return; //No detecta que está en modo diseño
                if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                    return;
                else if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"))
                    return;

                

                //Recuperamos los datos de la conexión de la DB
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[GlobalSetting.ConnStringName].ConnectionString;
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);

                barStaticItemAppVersion.Caption = $"v{Application.ProductVersion}";
                barStaticItemUser.Caption = $"{GlobalSetting.ResManager.GetString("User")}: {GlobalSetting.LoggedUser.UserLogin}";
                barStaticItemDbServer.Caption = $"{GlobalSetting.ResManager.GetString("DbServer")}: {builder.DataSource}";
                barStaticItemDb.Caption = $"{GlobalSetting.ResManager.GetString("DataBase")}: {builder.InitialCatalog}";
            }
            catch
            {
                throw;
            }
        }

        private void SaveWorkspace(string name, string base64stringLayout)
        {
            try
            {
                if (isLoadingWorkspace) return;

                int funcId = GlobalSetting.FunctionalitiesRoles.Where(fr => fr.Functionality.FormName.Equals(Name)).Select(a => a.FunctionalityId).FirstOrDefault();

                Layout tmpLayout = new Layout()
                {
                    FunctionalityId = funcId,
                    UserLogin = GlobalSetting.LoggedUser.UserLogin,
                    ObjectName = nameof(bwmiLayouts),
                    LayoutString = base64stringLayout,
                    LayoutName = name
                };

                GlobalSetting.LayoutService.SaveLayout(tmpLayout);
            }
            catch
            {
                throw;
            }
        }

        private void UpdateWorkspace(string name, string base64stringLayout)
        {
            try
            {
                int funcId = GlobalSetting.FunctionalitiesRoles.Where(fr => fr.Functionality.FormName.Equals(Name)).Select(a => a.FunctionalityId).FirstOrDefault();

                Layout tmpLayout = new Layout()
                {
                    FunctionalityId = funcId,
                    UserLogin = GlobalSetting.LoggedUser.UserLogin,
                    ObjectName = nameof(bwmiLayouts),
                    LayoutString = base64stringLayout,
                    LayoutName = name
                };

                GlobalSetting.LayoutService.UpdateLayout(tmpLayout);
            }
            catch
            {
                throw;
            }
        }

        private void LoadWorkspacesLayouts()
        {
            try
            {
                isLoadingWorkspace = true;

                List<Layout> layouts = Helpers.LayoutHelper.GetRibbonWorkSpaceLayouts(Name, nameof(bwmiLayouts));

                foreach (var wks in layouts)
                {
                    byte[] rawData = Convert.FromBase64String(wks.LayoutString);
                    using (MemoryStream ms = new MemoryStream(rawData))
                    {
                        bwmiLayouts.WorkspaceManager.LoadWorkspace(wks.LayoutName, ms);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                isLoadingWorkspace = false;
            }
        }

        private void GetFormReports(int idFunctionality)
        {
            try
            {
                List<FunctionalityReport> reports = GlobalSetting.FunctionalityReportService.GetFunctionalityReports(idFunctionality);

                List<BarButtonItem> reportList = new List<BarButtonItem>();

                if(reports.Count() > 0)
                {
                    foreach (var report in reports)
                    {
                        BarButtonItem barButtonItem = new BarButtonItem();
                        barButtonItem.Caption = report.ReportNameEn;
                        barButtonItem.Tag = report;
                        barButtonItem.ItemClick += BarButtonItemReport_ItemClick;

                        reportList.Add(barButtonItem);
                    }

                    barLinkContainerItemReports.AddItems(reportList.ToArray());
                    ribbonPageGroupReports.Visible = true;
                }
                else
                {
                    ribbonPageGroupReports.Visible = false;
                }
                
            }
            catch
            {
                throw;
            }
        }



        #endregion

        #region Events

        #region Task Buttons

        public virtual void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //Lanzamos el validate para finalizar cualquier accion de edición del formulario, ya que este control no tiene focus nunca y no lo lanza por si mismo
                Validate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                RestoreInitState();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                CurrentState = ActionsStates.New;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                CurrentState = ActionsStates.Edit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                //if (_currentState == ActionsStates.Edit)
                //{
                //    DialogResult result = MessageBox.Show("Pending changes. Close?", "", MessageBoxButtons.YesNo);

                //    if (result != DialogResult.Yes)
                //        return;
                //    else
                //        Close();
                //}
                //else
                //{
                //    Close();
                //}
                Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Print and Export Buttons

        public virtual void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        public virtual void bbiExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "xlsx files (*.xlsx)|*.xlsx",
                    FilterIndex = 1,
                    RestoreDirectory = true,
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportExcelFile = saveFileDialog.FileName;
                }
                else
                {
                    ExportExcelFile = string.Empty;
                }
            }
            catch
            {
                throw;
            }
        }

        public virtual void bbiExportCsv_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = "csv files (*.csv)|*.csv",
                    FilterIndex = 1,
                    RestoreDirectory = true,
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportCsvFile = saveFileDialog.FileName;
                }
                else
                {
                    ExportCsvFile = string.Empty;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Layout
        public virtual void BbiSaveLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        public virtual void LayoutButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            CurrentLayout = e.Item.Tag.ToString();
        }

        private void BwmiLayouts_Popup(object sender, EventArgs e)
        {
            //ocultamos el item de gestion de workspace del propio devexpress
            bwmiLayouts.ItemLinks[1].Visible = false;
        }

        private void WorkspaceManager_WorkspaceCollectionChanged(object sender, WorkspaceCollectionChangedEventArgs ea)
        {
            switch (ea.Action)
            {
                case WorkspaceCollectionChangedAction.WorkspaceAdded:
                    using (MemoryStream ms = new MemoryStream())
                    {
                        string layoutAsBase64String;
                        bwmiLayouts.WorkspaceManager.SaveWorkspace(ea.Workspace.Name, ms);
                        layoutAsBase64String = Convert.ToBase64String(ms.ToArray());
                        SaveWorkspace(ea.Workspace.Name, layoutAsBase64String);
                    }
                    break;

                case WorkspaceCollectionChangedAction.WorkspaceRemoved:
                    //de momento no damos opción a borrar
                    break;

                case WorkspaceCollectionChangedAction.WorkspaceUpdated:
                    using (MemoryStream ms = new MemoryStream())
                    {
                        string layoutAsBase64String;
                        bwmiLayouts.WorkspaceManager.SaveWorkspace(ea.Workspace.Name, ms);
                        layoutAsBase64String = Convert.ToBase64String(ms.ToArray());
                        UpdateWorkspace(ea.Workspace.Name, layoutAsBase64String);
                    }
                    break;
            }
        }

        #endregion

        #region Reports
        public virtual void BarButtonItemReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }
        #endregion

        #endregion

        #region Overrides
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F1)
            {

                string pdfHelp = string.Empty;

                //Documentos de ayuda por formulario
                switch (Name)
                {
                    case nameof(Master.BomManagement):
                        pdfHelp = $"{Application.StartupPath}\\HelpDocs\\EN\\BOM Flow and Application Help.pdf";
                        break;
                }

                //Documentos de ayuda por Categoría.
                var category = GlobalSetting.FunctionalitiesRoles
                    .Where(a => a.Functionality.FormName.Equals(Name))
                    .Select(b => b.Functionality.Category).FirstOrDefault();

                switch(category)
                {
                    case "Supply":
                        pdfHelp = $"{Application.StartupPath}\\HelpDocs\\EN\\SUPPLY Flow and Application Help.pdf";
                        break;
                }

                if (string.IsNullOrEmpty(pdfHelp) == false && File.Exists(pdfHelp))
                    Helpers.DocHelper.OpenDoc(pdfHelp, showDialog: false);

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}