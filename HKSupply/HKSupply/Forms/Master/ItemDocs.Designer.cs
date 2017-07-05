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
            this.lblModel = new DevExpress.XtraEditors.LabelControl();
            this.slueModel = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.checkedListBoxControlItems = new DevExpress.XtraEditors.CheckedListBoxControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlItems)).BeginInit();
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
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(1338, 79);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Size = new System.Drawing.Size(1338, 31);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 79);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpMain;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 489);
            this.xtcGeneral.TabIndex = 2;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpMain});
            // 
            // xtpMain
            // 
            this.xtpMain.Controls.Add(this.gbNewDoc);
            this.xtpMain.Controls.Add(this.checkedListBoxControlItems);
            this.xtpMain.Controls.Add(this.lblModel);
            this.xtpMain.Controls.Add(this.slueModel);
            this.xtpMain.Name = "xtpMain";
            this.xtpMain.Size = new System.Drawing.Size(1332, 461);
            this.xtpMain.Text = "Docs";
            // 
            // lblModel
            // 
            this.lblModel.Location = new System.Drawing.Point(21, 18);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(28, 13);
            this.lblModel.TabIndex = 15;
            this.lblModel.Text = "Model";
            // 
            // slueModel
            // 
            this.slueModel.Location = new System.Drawing.Point(63, 15);
            this.slueModel.MenuManager = this.ribbonControl;
            this.slueModel.Name = "slueModel";
            this.slueModel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueModel.Properties.View = this.gridView2;
            this.slueModel.Size = new System.Drawing.Size(172, 20);
            this.slueModel.TabIndex = 14;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // checkedListBoxControlItems
            // 
            this.checkedListBoxControlItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkedListBoxControlItems.Location = new System.Drawing.Point(21, 53);
            this.checkedListBoxControlItems.Name = "checkedListBoxControlItems";
            this.checkedListBoxControlItems.Size = new System.Drawing.Size(370, 180);
            this.checkedListBoxControlItems.TabIndex = 17;
            // 
            // gbNewDoc
            // 
            this.gbNewDoc.Controls.Add(this.layoutControlNewDoc);
            this.gbNewDoc.Location = new System.Drawing.Point(21, 239);
            this.gbNewDoc.Name = "gbNewDoc";
            this.gbNewDoc.Size = new System.Drawing.Size(611, 49);
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
            this.layoutControlNewDoc.Location = new System.Drawing.Point(-1, 13);
            this.layoutControlNewDoc.Name = "layoutControlNewDoc";
            this.layoutControlNewDoc.Root = this.layoutControlGroup3;
            this.layoutControlNewDoc.Size = new System.Drawing.Size(611, 55);
            this.layoutControlNewDoc.TabIndex = 0;
            this.layoutControlNewDoc.Text = "layoutControl1";
            // 
            // lueDocType
            // 
            this.lueDocType.Location = new System.Drawing.Point(7, 7);
            this.lueDocType.MenuManager = this.ribbonControl;
            this.lueDocType.Name = "lueDocType";
            this.lueDocType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDocType.Size = new System.Drawing.Size(95, 20);
            this.lueDocType.StyleController = this.layoutControlNewDoc;
            this.lueDocType.TabIndex = 56;
            // 
            // sbViewNewDoc
            // 
            this.sbViewNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("sbViewNewDoc.Image")));
            this.sbViewNewDoc.Location = new System.Drawing.Point(106, 7);
            this.sbViewNewDoc.MaximumSize = new System.Drawing.Size(25, 25);
            this.sbViewNewDoc.MinimumSize = new System.Drawing.Size(25, 25);
            this.sbViewNewDoc.Name = "sbViewNewDoc";
            this.sbViewNewDoc.Size = new System.Drawing.Size(25, 25);
            this.sbViewNewDoc.StyleController = this.layoutControlNewDoc;
            this.sbViewNewDoc.TabIndex = 54;
            this.sbViewNewDoc.Click += new System.EventHandler(this.sbViewNewDoc_Click);
            // 
            // sbOpenFileNewDoc
            // 
            this.sbOpenFileNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("sbOpenFileNewDoc.Image")));
            this.sbOpenFileNewDoc.Location = new System.Drawing.Point(135, 7);
            this.sbOpenFileNewDoc.MaximumSize = new System.Drawing.Size(25, 25);
            this.sbOpenFileNewDoc.MinimumSize = new System.Drawing.Size(25, 25);
            this.sbOpenFileNewDoc.Name = "sbOpenFileNewDoc";
            this.sbOpenFileNewDoc.Size = new System.Drawing.Size(25, 25);
            this.sbOpenFileNewDoc.StyleController = this.layoutControlNewDoc;
            this.sbOpenFileNewDoc.TabIndex = 55;
            this.sbOpenFileNewDoc.Click += new System.EventHandler(this.sbOpenFileNewDoc_Click);
            // 
            // txtPathNewDoc
            // 
            this.txtPathNewDoc.Location = new System.Drawing.Point(189, 7);
            this.txtPathNewDoc.Name = "txtPathNewDoc";
            this.txtPathNewDoc.Size = new System.Drawing.Size(415, 20);
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
            this.layoutControlGroup3.Size = new System.Drawing.Size(611, 55);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.sbViewNewDoc;
            this.layoutControlItem34.CustomizationFormText = "PDF";
            this.layoutControlItem34.Location = new System.Drawing.Point(99, 0);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(29, 45);
            this.layoutControlItem34.Text = "PDF";
            this.layoutControlItem34.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem34.TextVisible = false;
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.Control = this.sbOpenFileNewDoc;
            this.layoutControlItem35.CustomizationFormText = "layoutControlItem33";
            this.layoutControlItem35.Location = new System.Drawing.Point(128, 0);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Size = new System.Drawing.Size(29, 45);
            this.layoutControlItem35.Text = "layoutControlItem33";
            this.layoutControlItem35.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem35.TextVisible = false;
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.txtPathNewDoc;
            this.layoutControlItem36.CustomizationFormText = "PDF Path";
            this.layoutControlItem36.Location = new System.Drawing.Point(157, 0);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Size = new System.Drawing.Size(444, 45);
            this.layoutControlItem36.Text = "Path";
            this.layoutControlItem36.TextSize = new System.Drawing.Size(22, 13);
            // 
            // lcilueDocType
            // 
            this.lcilueDocType.Control = this.lueDocType;
            this.lcilueDocType.Location = new System.Drawing.Point(0, 0);
            this.lcilueDocType.Name = "lcilueDocType";
            this.lcilueDocType.Size = new System.Drawing.Size(99, 45);
            this.lcilueDocType.TextSize = new System.Drawing.Size(0, 0);
            this.lcilueDocType.TextVisible = false;
            // 
            // ItemDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 599);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "ItemDocs";
            this.Text = "ItemDocs";
            this.Load += new System.EventHandler(this.ItemDocs_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpMain.ResumeLayout(false);
            this.xtpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlItems)).EndInit();
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
    }
}