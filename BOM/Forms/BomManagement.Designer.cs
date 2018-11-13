namespace BOM.Forms
{
    partial class BomManagement
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer dockingContainer1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer();
            this.documentGroup1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(this.components);
            this.document1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.dockManagerItemBom = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelBom = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.lblCopyBomFromSku = new DevExpress.XtraEditors.LabelControl();
            this.btnCopyFromSku = new DevExpress.XtraEditors.SimpleButton();
            this.lblLegendF4 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAcciones = new DevExpress.XtraEditors.SimpleButton();
            this.lblCopyBom = new DevExpress.XtraEditors.LabelControl();
            this.btnCopyBom = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddBomFactory = new DevExpress.XtraEditors.SimpleButton();
            this.lblFactory = new DevExpress.XtraEditors.LabelControl();
            this.slueFactory = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdItemBom = new DevExpress.XtraGrid.GridControl();
            this.gridViewItemBom = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dockPanelItems = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.grdItems = new DevExpress.XtraGrid.GridControl();
            this.gridViewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.documentManagerBom = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManagerItemBom)).BeginInit();
            this.dockPanelBom.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueFactory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItemBom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemBom)).BeginInit();
            this.dockPanelItems.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManagerBom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // documentGroup1
            // 
            this.documentGroup1.Items.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document[] {
            this.document1});
            // 
            // document1
            // 
            this.document1.Caption = "BOM";
            this.document1.ControlName = "dockPanelBom";
            this.document1.FloatLocation = new System.Drawing.Point(722, 457);
            this.document1.FloatSize = new System.Drawing.Size(200, 200);
            this.document1.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // dockManagerItemBom
            // 
            this.dockManagerItemBom.Form = this;
            this.dockManagerItemBom.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelBom,
            this.dockPanelItems});
            this.dockManagerItemBom.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanelBom
            // 
            this.dockPanelBom.Controls.Add(this.controlContainer1);
            this.dockPanelBom.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanelBom.DockedAsTabbedDocument = true;
            this.dockPanelBom.FloatLocation = new System.Drawing.Point(722, 457);
            this.dockPanelBom.ID = new System.Guid("63a1ee9e-1e98-4944-ab37-e52c272cf405");
            this.dockPanelBom.Location = new System.Drawing.Point(0, 0);
            this.dockPanelBom.Name = "dockPanelBom";
            this.dockPanelBom.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanelBom.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelBom.SavedIndex = 0;
            this.dockPanelBom.Size = new System.Drawing.Size(950, 628);
            this.dockPanelBom.Text = "BOM";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.lblCopyBomFromSku);
            this.controlContainer1.Controls.Add(this.btnCopyFromSku);
            this.controlContainer1.Controls.Add(this.lblLegendF4);
            this.controlContainer1.Controls.Add(this.btnCancel);
            this.controlContainer1.Controls.Add(this.btnAcciones);
            this.controlContainer1.Controls.Add(this.lblCopyBom);
            this.controlContainer1.Controls.Add(this.btnCopyBom);
            this.controlContainer1.Controls.Add(this.btnAddBomFactory);
            this.controlContainer1.Controls.Add(this.lblFactory);
            this.controlContainer1.Controls.Add(this.slueFactory);
            this.controlContainer1.Controls.Add(this.grdItemBom);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(950, 628);
            this.controlContainer1.TabIndex = 0;
            // 
            // lblCopyBomFromSku
            // 
            this.lblCopyBomFromSku.Location = new System.Drawing.Point(538, 15);
            this.lblCopyBomFromSku.Name = "lblCopyBomFromSku";
            this.lblCopyBomFromSku.Size = new System.Drawing.Size(97, 13);
            this.lblCopyBomFromSku.TabIndex = 16;
            this.lblCopyBomFromSku.Text = "Copy BOM from SKU";
            // 
            // btnCopyFromSku
            // 
            this.btnCopyFromSku.ImageOptions.Image = global::BOM.Properties.Resources.cloning16x16;
            this.btnCopyFromSku.Location = new System.Drawing.Point(507, 9);
            this.btnCopyFromSku.Name = "btnCopyFromSku";
            this.btnCopyFromSku.Size = new System.Drawing.Size(25, 25);
            this.btnCopyFromSku.TabIndex = 15;
            this.btnCopyFromSku.Text = "simpleButton1";
            // 
            // lblLegendF4
            // 
            this.lblLegendF4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLegendF4.Location = new System.Drawing.Point(823, 606);
            this.lblLegendF4.Name = "lblLegendF4";
            this.lblLegendF4.Size = new System.Drawing.Size(82, 13);
            this.lblLegendF4.TabIndex = 14;
            this.lblLegendF4.Text = "F4 to delete Row";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(96, 586);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 33);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            // 
            // btnAcciones
            // 
            this.btnAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAcciones.Location = new System.Drawing.Point(3, 586);
            this.btnAcciones.Name = "btnAcciones";
            this.btnAcciones.Size = new System.Drawing.Size(75, 33);
            this.btnAcciones.TabIndex = 12;
            this.btnAcciones.Text = "Edit";
            // 
            // lblCopyBom
            // 
            this.lblCopyBom.Location = new System.Drawing.Point(409, 15);
            this.lblCopyBom.Name = "lblCopyBom";
            this.lblCopyBom.Size = new System.Drawing.Size(63, 13);
            this.lblCopyBom.TabIndex = 11;
            this.lblCopyBom.Text = "Copy BOM to";
            // 
            // btnCopyBom
            // 
            this.btnCopyBom.ImageOptions.ImageUri.Uri = "Replace;Size16x16";
            this.btnCopyBom.Location = new System.Drawing.Point(378, 9);
            this.btnCopyBom.Name = "btnCopyBom";
            this.btnCopyBom.Size = new System.Drawing.Size(25, 25);
            this.btnCopyBom.TabIndex = 10;
            this.btnCopyBom.Text = "simpleButton1";
            // 
            // btnAddBomFactory
            // 
            this.btnAddBomFactory.ImageOptions.ImageUri.Uri = "Add;Size16x16";
            this.btnAddBomFactory.Location = new System.Drawing.Point(289, 9);
            this.btnAddBomFactory.Name = "btnAddBomFactory";
            this.btnAddBomFactory.Size = new System.Drawing.Size(25, 25);
            this.btnAddBomFactory.TabIndex = 9;
            // 
            // lblFactory
            // 
            this.lblFactory.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFactory.Location = new System.Drawing.Point(3, 11);
            this.lblFactory.Name = "lblFactory";
            this.lblFactory.Size = new System.Drawing.Size(104, 19);
            this.lblFactory.TabIndex = 8;
            this.lblFactory.Text = "Factory";
            // 
            // slueFactory
            // 
            this.slueFactory.Location = new System.Drawing.Point(133, 11);
            this.slueFactory.Name = "slueFactory";
            this.slueFactory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueFactory.Properties.PopupView = this.searchLookUpEdit1View;
            this.slueFactory.Size = new System.Drawing.Size(150, 20);
            this.slueFactory.TabIndex = 7;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // grdItemBom
            // 
            this.grdItemBom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdItemBom.Location = new System.Drawing.Point(3, 54);
            this.grdItemBom.MainView = this.gridViewItemBom;
            this.grdItemBom.Name = "grdItemBom";
            this.grdItemBom.Size = new System.Drawing.Size(944, 516);
            this.grdItemBom.TabIndex = 0;
            this.grdItemBom.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItemBom});
            // 
            // gridViewItemBom
            // 
            this.gridViewItemBom.GridControl = this.grdItemBom;
            this.gridViewItemBom.Name = "gridViewItemBom";
            // 
            // dockPanelItems
            // 
            this.dockPanelItems.Controls.Add(this.dockPanel1_Container);
            this.dockPanelItems.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelItems.ID = new System.Guid("4389c2a9-78b6-425d-b27a-e50d4cf1a7c6");
            this.dockPanelItems.Location = new System.Drawing.Point(0, 0);
            this.dockPanelItems.Name = "dockPanelItems";
            this.dockPanelItems.OriginalSize = new System.Drawing.Size(305, 200);
            this.dockPanelItems.Size = new System.Drawing.Size(305, 656);
            this.dockPanelItems.Text = "Items";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.grdItems);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(296, 629);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // grdItems
            // 
            this.grdItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdItems.Location = new System.Drawing.Point(0, 0);
            this.grdItems.MainView = this.gridViewItems;
            this.grdItems.Name = "grdItems";
            this.grdItems.Size = new System.Drawing.Size(296, 629);
            this.grdItems.TabIndex = 0;
            this.grdItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItems});
            // 
            // gridViewItems
            // 
            this.gridViewItems.GridControl = this.grdItems;
            this.gridViewItems.Name = "gridViewItems";
            // 
            // documentManagerBom
            // 
            this.documentManagerBom.ContainerControl = this;
            this.documentManagerBom.View = this.tabbedView1;
            this.documentManagerBom.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // tabbedView1
            // 
            this.tabbedView1.DocumentGroups.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup[] {
            this.documentGroup1});
            this.tabbedView1.Documents.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseDocument[] {
            this.document1});
            dockingContainer1.Element = this.documentGroup1;
            this.tabbedView1.RootContainer.Nodes.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer[] {
            dockingContainer1});
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(45, 72);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(102, 309);
            this.gridControl2.TabIndex = 0;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            // 
            // gridControl3
            // 
            this.gridControl3.Location = new System.Drawing.Point(45, 72);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(102, 309);
            this.gridControl3.TabIndex = 0;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(409, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Copy BOM to";
            // 
            // BomManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 656);
            this.Controls.Add(this.dockPanelItems);
            this.Name = "BomManagement";
            this.Text = "BomManagement";
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManagerItemBom)).EndInit();
            this.dockPanelBom.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            this.controlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueFactory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItemBom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemBom)).EndInit();
            this.dockPanelItems.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManagerBom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManagerItemBom;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelItems;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelBom;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManagerBom;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraGrid.GridControl grdItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItems;
        private DevExpress.XtraGrid.GridControl grdItemBom;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItemBom;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document1;
        private DevExpress.XtraEditors.LabelControl lblCopyBom;
        private DevExpress.XtraEditors.SimpleButton btnCopyBom;
        private DevExpress.XtraEditors.SimpleButton btnAddBomFactory;
        private DevExpress.XtraEditors.LabelControl lblFactory;
        private DevExpress.XtraEditors.SearchLookUpEdit slueFactory;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton btnAcciones;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblLegendF4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblCopyBomFromSku;
        private DevExpress.XtraEditors.SimpleButton btnCopyFromSku;
    }
}