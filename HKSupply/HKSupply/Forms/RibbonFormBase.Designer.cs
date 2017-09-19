namespace HKSupply.Forms
{
    partial class RibbonFormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.Animation.PushTransition pushTransition1 = new DevExpress.Utils.Animation.PushTransition();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiPrintPreview = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancel = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportExcel = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportCsv = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSaveLayout = new DevExpress.XtraBars.BarButtonItem();
            this.bsiRestoreLayout = new DevExpress.XtraBars.BarSubItem();
            this.bwmiLayouts = new DevExpress.XtraBars.BarWorkspaceMenuItem();
            this.workspaceManager1 = new DevExpress.Utils.WorkspaceManager();
            this.barStaticItemUser = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemDbServer = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemDb = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupLayout = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.barStaticItemAppVersion = new DevExpress.XtraBars.BarStaticItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.bbiPrintPreview,
            this.bbiNew,
            this.bbiEdit,
            this.bbiCancel,
            this.bbiSave,
            this.bbiClose,
            this.bbiExportExcel,
            this.bbiExportCsv,
            this.bbiSaveLayout,
            this.bsiRestoreLayout,
            this.bwmiLayouts,
            this.barStaticItemUser,
            this.barStaticItemDbServer,
            this.barStaticItemDb,
            this.barStaticItemAppVersion});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 30;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.Size = new System.Drawing.Size(790, 143);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bbiPrintPreview
            // 
            this.bbiPrintPreview.Caption = "Print Preview";
            this.bbiPrintPreview.Id = 14;
            this.bbiPrintPreview.ImageOptions.ImageUri.Uri = "Preview";
            this.bbiPrintPreview.Name = "bbiPrintPreview";
            // 
            // bbiNew
            // 
            this.bbiNew.Caption = "New";
            this.bbiNew.Id = 16;
            this.bbiNew.ImageOptions.ImageUri.Uri = "New";
            this.bbiNew.Name = "bbiNew";
            // 
            // bbiEdit
            // 
            this.bbiEdit.Caption = "Edit";
            this.bbiEdit.Id = 17;
            this.bbiEdit.ImageOptions.ImageUri.Uri = "Edit";
            this.bbiEdit.Name = "bbiEdit";
            // 
            // bbiCancel
            // 
            this.bbiCancel.Caption = "Cancel";
            this.bbiCancel.Id = 18;
            this.bbiCancel.ImageOptions.ImageUri.Uri = "Delete";
            this.bbiCancel.Name = "bbiCancel";
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "Save";
            this.bbiSave.Id = 19;
            this.bbiSave.ImageOptions.ImageUri.Uri = "Save";
            this.bbiSave.Name = "bbiSave";
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "Close";
            this.bbiClose.Id = 20;
            this.bbiClose.ImageOptions.ImageUri.Uri = "Close";
            this.bbiClose.Name = "bbiClose";
            // 
            // bbiExportExcel
            // 
            this.bbiExportExcel.Caption = "Export to Excel";
            this.bbiExportExcel.Id = 21;
            this.bbiExportExcel.ImageOptions.ImageUri.Uri = "ExportToXLSX";
            this.bbiExportExcel.Name = "bbiExportExcel";
            // 
            // bbiExportCsv
            // 
            this.bbiExportCsv.Caption = "Export to CSV";
            this.bbiExportCsv.Id = 22;
            this.bbiExportCsv.ImageOptions.ImageUri.Uri = "ExportToCSV";
            this.bbiExportCsv.Name = "bbiExportCsv";
            // 
            // bbiSaveLayout
            // 
            this.bbiSaveLayout.Caption = "Save Layout";
            this.bbiSaveLayout.Id = 23;
            this.bbiSaveLayout.ImageOptions.ImageUri.Uri = "ExportFile";
            this.bbiSaveLayout.Name = "bbiSaveLayout";
            // 
            // bsiRestoreLayout
            // 
            this.bsiRestoreLayout.Caption = "Restore Layout";
            this.bsiRestoreLayout.Id = 24;
            this.bsiRestoreLayout.ImageOptions.ImageUri.Uri = "SaveAndNew";
            this.bsiRestoreLayout.Name = "bsiRestoreLayout";
            // 
            // bwmiLayouts
            // 
            this.bwmiLayouts.Caption = "Workspace Manager";
            this.bwmiLayouts.Id = 25;
            this.bwmiLayouts.ImageOptions.ImageUri.Uri = "ExportFile";
            this.bwmiLayouts.Name = "bwmiLayouts";
            this.bwmiLayouts.WorkspaceManager = this.workspaceManager1;
            // 
            // workspaceManager1
            // 
            this.workspaceManager1.TargetControl = this;
            this.workspaceManager1.TransitionType = pushTransition1;
            // 
            // barStaticItemUser
            // 
            this.barStaticItemUser.Caption = "User: XX";
            this.barStaticItemUser.Id = 26;
            this.barStaticItemUser.Name = "barStaticItemUser";
            // 
            // barStaticItemDbServer
            // 
            this.barStaticItemDbServer.Caption = "Db Server: XXX";
            this.barStaticItemDbServer.Id = 27;
            this.barStaticItemDbServer.Name = "barStaticItemDbServer";
            // 
            // barStaticItemDb
            // 
            this.barStaticItemDb.Caption = "DB: XX";
            this.barStaticItemDb.Id = 28;
            this.barStaticItemDb.Name = "barStaticItemDb";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroupLayout,
            this.ribbonPageGroup3});
            this.ribbonPage1.MergeOrder = 0;
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiNew);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiEdit);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiCancel);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiSave);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Tasks";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.AllowTextClipping = false;
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiPrintPreview);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiExportExcel);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbiExportCsv);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.ShowCaptionButton = false;
            this.ribbonPageGroup2.Text = "Print and Export";
            // 
            // ribbonPageGroupLayout
            // 
            this.ribbonPageGroupLayout.ItemLinks.Add(this.bbiSaveLayout);
            this.ribbonPageGroupLayout.ItemLinks.Add(this.bsiRestoreLayout);
            this.ribbonPageGroupLayout.ItemLinks.Add(this.bwmiLayouts);
            this.ribbonPageGroupLayout.Name = "ribbonPageGroupLayout";
            this.ribbonPageGroupLayout.Text = "Layout";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.bbiClose);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemAppVersion);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemUser);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemDbServer);
            this.ribbonStatusBar.ItemLinks.Add(this.barStaticItemDb);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 568);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(790, 31);
            // 
            // barStaticItemAppVersion
            // 
            this.barStaticItemAppVersion.Caption = "App. Version: X.X";
            this.barStaticItemAppVersion.Id = 29;
            this.barStaticItemAppVersion.Name = "barStaticItemAppVersion";
            // 
            // RibbonFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "RibbonFormBase";
            this.Ribbon = this.ribbonControl;
            this.StatusBar = this.ribbonStatusBar;
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        public DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        public DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        public DevExpress.XtraBars.BarButtonItem bbiPrintPreview;
        public DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        public DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        public DevExpress.XtraBars.BarButtonItem bbiNew;
        public DevExpress.XtraBars.BarButtonItem bbiEdit;
        public DevExpress.XtraBars.BarButtonItem bbiCancel;
        public DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraBars.BarButtonItem bbiExportExcel;
        private DevExpress.XtraBars.BarButtonItem bbiExportCsv;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupLayout;
        private DevExpress.XtraBars.BarButtonItem bbiSaveLayout;
        private DevExpress.XtraBars.BarSubItem bsiRestoreLayout;
        private DevExpress.XtraBars.BarWorkspaceMenuItem bwmiLayouts;
        private DevExpress.Utils.WorkspaceManager workspaceManager1;
        private DevExpress.XtraBars.BarStaticItem barStaticItemUser;
        private DevExpress.XtraBars.BarStaticItem barStaticItemDbServer;
        private DevExpress.XtraBars.BarStaticItem barStaticItemDb;
        private DevExpress.XtraBars.BarStaticItem barStaticItemAppVersion;
    }
}