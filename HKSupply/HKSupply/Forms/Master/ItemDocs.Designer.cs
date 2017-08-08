namespace HKSupply.Forms.Master
{
    partial class ItemDocs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemDocs));
            this.xtcGeneral = new DevExpress.XtraTab.XtraTabControl();
            this.xtpMain = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdLastDocs = new DevExpress.XtraGrid.GridControl();
            this.gridViewLastDocs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbSupplier = new System.Windows.Forms.GroupBox();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbNewDoc = new System.Windows.Forms.GroupBox();
            this.layoutControlNewDoc = new DevExpress.XtraLayout.LayoutControl();
            this.lueDocType = new DevExpress.XtraEditors.LookUpEdit();
            this.sbViewNewDoc = new DevExpress.XtraEditors.SimpleButton();
            this.sbOpenFileNewDoc = new DevExpress.XtraEditors.SimpleButton();
            this.txtPathNewDoc = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcilueDocType = new DevExpress.XtraLayout.LayoutControlItem();
            this.checkedListBoxControlItems = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.lblModel = new DevExpress.XtraEditors.LabelControl();
            this.slueModel = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLastDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLastDocs)).BeginInit();
            this.gbSupplier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.gbNewDoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNewDoc)).BeginInit();
            this.layoutControlNewDoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDocType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPathNewDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcilueDocType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ribbonControl.Size = new System.Drawing.Size(2007, 122);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ribbonStatusBar.Size = new System.Drawing.Size(2007, 48);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 122);
            this.xtcGeneral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpMain;
            this.xtcGeneral.Size = new System.Drawing.Size(2007, 753);
            this.xtcGeneral.TabIndex = 2;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpMain});
            // 
            // xtpMain
            // 
            this.xtpMain.Controls.Add(this.xgrdLastDocs);
            this.xtpMain.Controls.Add(this.gbSupplier);
            this.xtpMain.Controls.Add(this.gbNewDoc);
            this.xtpMain.Controls.Add(this.checkedListBoxControlItems);
            this.xtpMain.Controls.Add(this.lblModel);
            this.xtpMain.Controls.Add(this.slueModel);
            this.xtpMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtpMain.Name = "xtpMain";
            this.xtpMain.Size = new System.Drawing.Size(1997, 711);
            this.xtpMain.Text = "Docs";
            // 
            // xgrdLastDocs
            // 
            this.xgrdLastDocs.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xgrdLastDocs.Location = new System.Drawing.Point(656, 13);
            this.xgrdLastDocs.MainView = this.gridViewLastDocs;
            this.xgrdLastDocs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xgrdLastDocs.MenuManager = this.ribbonControl;
            this.xgrdLastDocs.Name = "xgrdLastDocs";
            this.xgrdLastDocs.Size = new System.Drawing.Size(1003, 327);
            this.xgrdLastDocs.TabIndex = 20;
            this.xgrdLastDocs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLastDocs});
            // 
            // gridViewLastDocs
            // 
            this.gridViewLastDocs.GridControl = this.xgrdLastDocs;
            this.gridViewLastDocs.Name = "gridViewLastDocs";
            // 
            // gbSupplier
            // 
            this.gbSupplier.Controls.Add(this.lblSupplier);
            this.gbSupplier.Controls.Add(this.slueSupplier);
            this.gbSupplier.Location = new System.Drawing.Point(32, 444);
            this.gbSupplier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbSupplier.Name = "gbSupplier";
            this.gbSupplier.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbSupplier.Size = new System.Drawing.Size(555, 57);
            this.gbSupplier.TabIndex = 19;
            this.gbSupplier.TabStop = false;
            // 
            // lblSupplier
            // 
            this.lblSupplier.Location = new System.Drawing.Point(0, 23);
            this.lblSupplier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(58, 19);
            this.lblSupplier.TabIndex = 17;
            this.lblSupplier.Text = "Supplier";
            // 
            // slueSupplier
            // 
            this.slueSupplier.Location = new System.Drawing.Point(63, 19);
            this.slueSupplier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.slueSupplier.MenuManager = this.ribbonControl;
            this.slueSupplier.Name = "slueSupplier";
            this.slueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueSupplier.Properties.View = this.gridView1;
            this.slueSupplier.Size = new System.Drawing.Size(258, 26);
            this.slueSupplier.TabIndex = 16;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gbNewDoc
            // 
            this.gbNewDoc.Controls.Add(this.layoutControlNewDoc);
            this.gbNewDoc.Location = new System.Drawing.Point(32, 349);
            this.gbNewDoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbNewDoc.Name = "gbNewDoc";
            this.gbNewDoc.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbNewDoc.Size = new System.Drawing.Size(916, 72);
            this.gbNewDoc.TabIndex = 18;
            this.gbNewDoc.TabStop = false;
            this.gbNewDoc.Text = "New Doc";
            // 
            // layoutControlNewDoc
            // 
            this.layoutControlNewDoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutControlNewDoc.Controls.Add(this.lueDocType);
            this.layoutControlNewDoc.Controls.Add(this.sbViewNewDoc);
            this.layoutControlNewDoc.Controls.Add(this.sbOpenFileNewDoc);
            this.layoutControlNewDoc.Controls.Add(this.txtPathNewDoc);
            this.layoutControlNewDoc.Location = new System.Drawing.Point(-2, 19);
            this.layoutControlNewDoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControlNewDoc.Name = "layoutControlNewDoc";
            this.layoutControlNewDoc.Root = this.layoutControlGroup3;
            this.layoutControlNewDoc.Size = new System.Drawing.Size(916, 80);
            this.layoutControlNewDoc.TabIndex = 0;
            this.layoutControlNewDoc.Text = "layoutControl1";
            // 
            // lueDocType
            // 
            this.lueDocType.Location = new System.Drawing.Point(8, 8);
            this.lueDocType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lueDocType.MenuManager = this.ribbonControl;
            this.lueDocType.Name = "lueDocType";
            this.lueDocType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDocType.Size = new System.Drawing.Size(143, 26);
            this.lueDocType.StyleController = this.layoutControlNewDoc;
            this.lueDocType.TabIndex = 56;
            // 
            // sbViewNewDoc
            // 
            this.sbViewNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("sbViewNewDoc.Image")));
            this.sbViewNewDoc.Location = new System.Drawing.Point(157, 8);
            this.sbViewNewDoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sbViewNewDoc.MaximumSize = new System.Drawing.Size(38, 37);
            this.sbViewNewDoc.MinimumSize = new System.Drawing.Size(38, 37);
            this.sbViewNewDoc.Name = "sbViewNewDoc";
            this.sbViewNewDoc.Size = new System.Drawing.Size(38, 37);
            this.sbViewNewDoc.StyleController = this.layoutControlNewDoc;
            this.sbViewNewDoc.TabIndex = 54;
            this.sbViewNewDoc.Click += new System.EventHandler(this.sbViewNewDoc_Click);
            // 
            // sbOpenFileNewDoc
            // 
            this.sbOpenFileNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("sbOpenFileNewDoc.Image")));
            this.sbOpenFileNewDoc.Location = new System.Drawing.Point(201, 8);
            this.sbOpenFileNewDoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sbOpenFileNewDoc.MaximumSize = new System.Drawing.Size(38, 37);
            this.sbOpenFileNewDoc.MinimumSize = new System.Drawing.Size(38, 37);
            this.sbOpenFileNewDoc.Name = "sbOpenFileNewDoc";
            this.sbOpenFileNewDoc.Size = new System.Drawing.Size(38, 37);
            this.sbOpenFileNewDoc.StyleController = this.layoutControlNewDoc;
            this.sbOpenFileNewDoc.TabIndex = 55;
            this.sbOpenFileNewDoc.Click += new System.EventHandler(this.sbOpenFileNewDoc_Click);
            // 
            // txtPathNewDoc
            // 
            this.txtPathNewDoc.Location = new System.Drawing.Point(279, 8);
            this.txtPathNewDoc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPathNewDoc.Name = "txtPathNewDoc";
            this.txtPathNewDoc.Size = new System.Drawing.Size(629, 26);
            this.txtPathNewDoc.StyleController = this.layoutControlNewDoc;
            this.txtPathNewDoc.TabIndex = 53;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem34,
            this.layoutControlItem35,
            this.layoutControlItem36,
            this.lcilueDocType});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup3.Size = new System.Drawing.Size(916, 80);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.sbViewNewDoc;
            this.layoutControlItem34.CustomizationFormText = "PDF";
            this.layoutControlItem34.Location = new System.Drawing.Point(149, 0);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(44, 70);
            this.layoutControlItem34.Text = "PDF";
            this.layoutControlItem34.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem34.TextVisible = false;
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.Control = this.sbOpenFileNewDoc;
            this.layoutControlItem35.CustomizationFormText = "layoutControlItem33";
            this.layoutControlItem35.Location = new System.Drawing.Point(193, 0);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Size = new System.Drawing.Size(44, 70);
            this.layoutControlItem35.Text = "layoutControlItem33";
            this.layoutControlItem35.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem35.TextVisible = false;
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.txtPathNewDoc;
            this.layoutControlItem36.CustomizationFormText = "PDF Path";
            this.layoutControlItem36.Location = new System.Drawing.Point(237, 0);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Size = new System.Drawing.Size(669, 70);
            this.layoutControlItem36.Text = "Path";
            this.layoutControlItem36.TextSize = new System.Drawing.Size(31, 19);
            // 
            // lcilueDocType
            // 
            this.lcilueDocType.Control = this.lueDocType;
            this.lcilueDocType.Location = new System.Drawing.Point(0, 0);
            this.lcilueDocType.Name = "lcilueDocType";
            this.lcilueDocType.Size = new System.Drawing.Size(149, 70);
            this.lcilueDocType.TextSize = new System.Drawing.Size(0, 0);
            this.lcilueDocType.TextVisible = false;
            // 
            // checkedListBoxControlItems
            // 
            this.checkedListBoxControlItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxControlItems.Location = new System.Drawing.Point(32, 77);
            this.checkedListBoxControlItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkedListBoxControlItems.Name = "checkedListBoxControlItems";
            this.checkedListBoxControlItems.Size = new System.Drawing.Size(555, 263);
            this.checkedListBoxControlItems.TabIndex = 17;
            // 
            // lblModel
            // 
            this.lblModel.Location = new System.Drawing.Point(32, 26);
            this.lblModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(42, 19);
            this.lblModel.TabIndex = 15;
            this.lblModel.Text = "Model";
            // 
            // slueModel
            // 
            this.slueModel.Location = new System.Drawing.Point(94, 22);
            this.slueModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.slueModel.MenuManager = this.ribbonControl;
            this.slueModel.Name = "slueModel";
            this.slueModel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueModel.Properties.View = this.gridView2;
            this.slueModel.Size = new System.Drawing.Size(258, 26);
            this.slueModel.TabIndex = 14;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // ItemDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2007, 875);
            this.Controls.Add(this.xtcGeneral);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ItemDocs";
            this.Text = "ItemDocs";
            this.Load += new System.EventHandler(this.ItemDocs_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpMain.ResumeLayout(false);
            this.xtpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLastDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLastDocs)).EndInit();
            this.gbSupplier.ResumeLayout(false);
            this.gbSupplier.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.gbNewDoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNewDoc)).EndInit();
            this.layoutControlNewDoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueDocType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPathNewDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcilueDocType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpMain;
        private DevExpress.XtraEditors.LabelControl lblModel;
        private DevExpress.XtraEditors.SearchLookUpEdit slueModel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlItems;
        private System.Windows.Forms.GroupBox gbNewDoc;
        private DevExpress.XtraLayout.LayoutControl layoutControlNewDoc;
        private DevExpress.XtraEditors.LookUpEdit lueDocType;
        private DevExpress.XtraEditors.SimpleButton sbViewNewDoc;
        private DevExpress.XtraEditors.SimpleButton sbOpenFileNewDoc;
        private DevExpress.XtraEditors.TextEdit txtPathNewDoc;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlItem lcilueDocType;
        private System.Windows.Forms.GroupBox gbSupplier;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl xgrdLastDocs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLastDocs;
    }
}