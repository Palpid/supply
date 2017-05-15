namespace HKSupply.Forms.Master
{
    partial class PrototypeManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrototypeManagement));
            this.xtcGeneral = new DevExpress.XtraTab.XtraTabControl();
            this.xtpList = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdProtos = new DevExpress.XtraGrid.GridControl();
            this.rootGridViewProtos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtpDocs = new DevExpress.XtraTab.XtraTabPage();
            this.gbProtoInfo = new System.Windows.Forms.GroupBox();
            this.lblProtoName = new DevExpress.XtraEditors.LabelControl();
            this.lblIdProto = new DevExpress.XtraEditors.LabelControl();
            this.gbDocsHistory = new System.Windows.Forms.GroupBox();
            this.xgrdDocsHistory = new DevExpress.XtraGrid.GridControl();
            this.gridViewDocsHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            this.gbLastDocs = new System.Windows.Forms.GroupBox();
            this.xgrdLastDocs = new DevExpress.XtraGrid.GridControl();
            this.gridViewLastDocs = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdProtos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewProtos)).BeginInit();
            this.xtpDocs.SuspendLayout();
            this.gbProtoInfo.SuspendLayout();
            this.gbDocsHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdDocsHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDocsHistory)).BeginInit();
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
            this.gbLastDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLastDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLastDocs)).BeginInit();
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
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 684);
            this.ribbonStatusBar.Size = new System.Drawing.Size(1338, 31);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 79);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpList;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 605);
            this.xtcGeneral.TabIndex = 2;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpList,
            this.xtpDocs});
            // 
            // xtpList
            // 
            this.xtpList.Controls.Add(this.xgrdProtos);
            this.xtpList.Name = "xtpList";
            this.xtpList.Size = new System.Drawing.Size(1332, 577);
            this.xtpList.Text = "List";
            // 
            // xgrdProtos
            // 
            this.xgrdProtos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdProtos.Location = new System.Drawing.Point(0, 0);
            this.xgrdProtos.MainView = this.rootGridViewProtos;
            this.xgrdProtos.MenuManager = this.ribbonControl;
            this.xgrdProtos.Name = "xgrdProtos";
            this.xgrdProtos.Size = new System.Drawing.Size(1332, 577);
            this.xgrdProtos.TabIndex = 0;
            this.xgrdProtos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootGridViewProtos});
            // 
            // rootGridViewProtos
            // 
            this.rootGridViewProtos.GridControl = this.xgrdProtos;
            this.rootGridViewProtos.Name = "rootGridViewProtos";
            // 
            // xtpDocs
            // 
            this.xtpDocs.AutoScroll = true;
            this.xtpDocs.Controls.Add(this.gbProtoInfo);
            this.xtpDocs.Controls.Add(this.gbDocsHistory);
            this.xtpDocs.Controls.Add(this.gbNewDoc);
            this.xtpDocs.Controls.Add(this.gbLastDocs);
            this.xtpDocs.Name = "xtpDocs";
            this.xtpDocs.Size = new System.Drawing.Size(1332, 577);
            this.xtpDocs.Text = "Docs";
            // 
            // gbProtoInfo
            // 
            this.gbProtoInfo.Controls.Add(this.lblProtoName);
            this.gbProtoInfo.Controls.Add(this.lblIdProto);
            this.gbProtoInfo.Location = new System.Drawing.Point(17, 3);
            this.gbProtoInfo.Name = "gbProtoInfo";
            this.gbProtoInfo.Size = new System.Drawing.Size(611, 49);
            this.gbProtoInfo.TabIndex = 7;
            this.gbProtoInfo.TabStop = false;
            this.gbProtoInfo.Text = "Prototype";
            // 
            // lblProtoName
            // 
            this.lblProtoName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblProtoName.Location = new System.Drawing.Point(182, 20);
            this.lblProtoName.Name = "lblProtoName";
            this.lblProtoName.Size = new System.Drawing.Size(423, 13);
            this.lblProtoName.TabIndex = 1;
            this.lblProtoName.Text = "Proto Name";
            // 
            // lblIdProto
            // 
            this.lblIdProto.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblIdProto.Location = new System.Drawing.Point(6, 20);
            this.lblIdProto.Name = "lblIdProto";
            this.lblIdProto.Size = new System.Drawing.Size(147, 13);
            this.lblIdProto.TabIndex = 0;
            this.lblIdProto.Text = "Id Proto";
            // 
            // gbDocsHistory
            // 
            this.gbDocsHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbDocsHistory.Controls.Add(this.xgrdDocsHistory);
            this.gbDocsHistory.Location = new System.Drawing.Point(678, 3);
            this.gbDocsHistory.Name = "gbDocsHistory";
            this.gbDocsHistory.Size = new System.Drawing.Size(611, 491);
            this.gbDocsHistory.TabIndex = 6;
            this.gbDocsHistory.TabStop = false;
            this.gbDocsHistory.Text = "Docs History";
            // 
            // xgrdDocsHistory
            // 
            this.xgrdDocsHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdDocsHistory.Location = new System.Drawing.Point(3, 17);
            this.xgrdDocsHistory.MainView = this.gridViewDocsHistory;
            this.xgrdDocsHistory.MenuManager = this.ribbonControl;
            this.xgrdDocsHistory.Name = "xgrdDocsHistory";
            this.xgrdDocsHistory.Size = new System.Drawing.Size(605, 471);
            this.xgrdDocsHistory.TabIndex = 1;
            this.xgrdDocsHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDocsHistory});
            // 
            // gridViewDocsHistory
            // 
            this.gridViewDocsHistory.GridControl = this.xgrdDocsHistory;
            this.gridViewDocsHistory.Name = "gridViewDocsHistory";
            // 
            // gbNewDoc
            // 
            this.gbNewDoc.Controls.Add(this.layoutControlNewDoc);
            this.gbNewDoc.Location = new System.Drawing.Point(11, 308);
            this.gbNewDoc.Name = "gbNewDoc";
            this.gbNewDoc.Size = new System.Drawing.Size(611, 49);
            this.gbNewDoc.TabIndex = 5;
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
            // gbLastDocs
            // 
            this.gbLastDocs.Controls.Add(this.xgrdLastDocs);
            this.gbLastDocs.Location = new System.Drawing.Point(11, 58);
            this.gbLastDocs.Name = "gbLastDocs";
            this.gbLastDocs.Size = new System.Drawing.Size(611, 244);
            this.gbLastDocs.TabIndex = 3;
            this.gbLastDocs.TabStop = false;
            this.gbLastDocs.Text = "Last Docs";
            // 
            // xgrdLastDocs
            // 
            this.xgrdLastDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdLastDocs.Location = new System.Drawing.Point(3, 17);
            this.xgrdLastDocs.MainView = this.gridViewLastDocs;
            this.xgrdLastDocs.MenuManager = this.ribbonControl;
            this.xgrdLastDocs.Name = "xgrdLastDocs";
            this.xgrdLastDocs.Size = new System.Drawing.Size(605, 224);
            this.xgrdLastDocs.TabIndex = 0;
            this.xgrdLastDocs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLastDocs});
            // 
            // gridViewLastDocs
            // 
            this.gridViewLastDocs.GridControl = this.xgrdLastDocs;
            this.gridViewLastDocs.Name = "gridViewLastDocs";
            // 
            // PrototypeManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 715);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "PrototypeManagement";
            this.Text = "Prototype Management";
            this.Load += new System.EventHandler(this.PrototypesManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdProtos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewProtos)).EndInit();
            this.xtpDocs.ResumeLayout(false);
            this.gbProtoInfo.ResumeLayout(false);
            this.gbDocsHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdDocsHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDocsHistory)).EndInit();
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
            this.gbLastDocs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLastDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLastDocs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpList;
        private DevExpress.XtraGrid.GridControl xgrdProtos;
        private DevExpress.XtraGrid.Views.Grid.GridView rootGridViewProtos;
        private DevExpress.XtraTab.XtraTabPage xtpDocs;
        private System.Windows.Forms.GroupBox gbLastDocs;
        private DevExpress.XtraGrid.GridControl xgrdLastDocs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLastDocs;
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
        private System.Windows.Forms.GroupBox gbDocsHistory;
        private DevExpress.XtraGrid.GridControl xgrdDocsHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDocsHistory;
        private System.Windows.Forms.GroupBox gbProtoInfo;
        private DevExpress.XtraEditors.LabelControl lblIdProto;
        private DevExpress.XtraEditors.LabelControl lblProtoName;
    }
}