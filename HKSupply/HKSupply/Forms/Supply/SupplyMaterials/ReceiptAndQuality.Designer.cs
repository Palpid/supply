namespace HKSupply.Forms.Supply.SupplyMaterials
{
    partial class ReceiptAndQuality
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
            this.xtcGeneral = new DevExpress.XtraTab.XtraTabControl();
            this.xtpReceiptAndQuality = new DevExpress.XtraTab.XtraTabPage();
            this.pcFilter = new DevExpress.XtraEditors.PanelControl();
            this.sbAddItem = new DevExpress.XtraEditors.SimpleButton();
            this.memoEditRemarks = new DevExpress.XtraEditors.MemoEdit();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lblRemarks = new DevExpress.XtraEditors.LabelControl();
            this.txtPKNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplierReference = new DevExpress.XtraEditors.TextEdit();
            this.lblPKNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblSupplierReference = new DevExpress.XtraEditors.LabelControl();
            this.lblPKDocDate = new DevExpress.XtraEditors.LabelControl();
            this.lbltxtStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblPKDelivery = new DevExpress.XtraEditors.LabelControl();
            this.sbFinishQC = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditPKDocDate = new DevExpress.XtraEditors.DateEdit();
            this.dateEditPKDelivery = new DevExpress.XtraEditors.DateEdit();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.lblPKDocDateWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblPKDeliveryWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblWeek = new DevExpress.XtraEditors.LabelControl();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.xtcPK = new DevExpress.XtraTab.XtraTabControl();
            this.xtpPLDetail = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdItemsBatch = new DevExpress.XtraGrid.GridControl();
            this.gridViewItemsBatch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xgrdLinesPL = new DevExpress.XtraGrid.GridControl();
            this.gridViewLinesPL = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpReceiptAndQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).BeginInit();
            this.pcFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPKNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplierReference.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPKDocDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPKDocDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPKDelivery.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPKDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcPK)).BeginInit();
            this.xtcPK.SuspendLayout();
            this.xtpPLDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdItemsBatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemsBatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLinesPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLinesPL)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonControl.Size = new System.Drawing.Size(2007, 192);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 997);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonStatusBar.Size = new System.Drawing.Size(2007, 48);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 192);
            this.xtcGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpReceiptAndQuality;
            this.xtcGeneral.Size = new System.Drawing.Size(2007, 805);
            this.xtcGeneral.TabIndex = 7;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpReceiptAndQuality});
            // 
            // xtpReceiptAndQuality
            // 
            this.xtpReceiptAndQuality.Controls.Add(this.pcFilter);
            this.xtpReceiptAndQuality.Controls.Add(this.xtcPK);
            this.xtpReceiptAndQuality.Margin = new System.Windows.Forms.Padding(4);
            this.xtpReceiptAndQuality.Name = "xtpReceiptAndQuality";
            this.xtpReceiptAndQuality.Size = new System.Drawing.Size(1997, 763);
            this.xtpReceiptAndQuality.Text = "Counting, Receipt of Goods & Quality";
            // 
            // pcFilter
            // 
            this.pcFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcFilter.Controls.Add(this.sbAddItem);
            this.pcFilter.Controls.Add(this.memoEditRemarks);
            this.pcFilter.Controls.Add(this.sbSearch);
            this.pcFilter.Controls.Add(this.lblRemarks);
            this.pcFilter.Controls.Add(this.txtPKNumber);
            this.pcFilter.Controls.Add(this.txtSupplierReference);
            this.pcFilter.Controls.Add(this.lblPKNumber);
            this.pcFilter.Controls.Add(this.lblSupplierReference);
            this.pcFilter.Controls.Add(this.lblPKDocDate);
            this.pcFilter.Controls.Add(this.lbltxtStatus);
            this.pcFilter.Controls.Add(this.lblPKDelivery);
            this.pcFilter.Controls.Add(this.sbFinishQC);
            this.pcFilter.Controls.Add(this.dateEditPKDocDate);
            this.pcFilter.Controls.Add(this.dateEditPKDelivery);
            this.pcFilter.Controls.Add(this.lblDate);
            this.pcFilter.Controls.Add(this.lblPKDocDateWeek);
            this.pcFilter.Controls.Add(this.lblPKDeliveryWeek);
            this.pcFilter.Controls.Add(this.lblWeek);
            this.pcFilter.Controls.Add(this.slueSupplier);
            this.pcFilter.Controls.Add(this.lblSupplier);
            this.pcFilter.Location = new System.Drawing.Point(16, 19);
            this.pcFilter.Margin = new System.Windows.Forms.Padding(4);
            this.pcFilter.Name = "pcFilter";
            this.pcFilter.Size = new System.Drawing.Size(1971, 208);
            this.pcFilter.TabIndex = 5;
            // 
            // sbAddItem
            // 
            this.sbAddItem.Location = new System.Drawing.Point(314, 145);
            this.sbAddItem.Margin = new System.Windows.Forms.Padding(4);
            this.sbAddItem.Name = "sbAddItem";
            this.sbAddItem.Size = new System.Drawing.Size(282, 47);
            this.sbAddItem.TabIndex = 33;
            this.sbAddItem.Text = "Add Item";
            // 
            // memoEditRemarks
            // 
            this.memoEditRemarks.Location = new System.Drawing.Point(963, 89);
            this.memoEditRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.memoEditRemarks.MenuManager = this.ribbonControl;
            this.memoEditRemarks.Name = "memoEditRemarks";
            this.memoEditRemarks.Size = new System.Drawing.Size(584, 47);
            this.memoEditRemarks.TabIndex = 32;
            // 
            // sbSearch
            // 
            this.sbSearch.Location = new System.Drawing.Point(1424, 20);
            this.sbSearch.Margin = new System.Windows.Forms.Padding(4);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(123, 56);
            this.sbSearch.TabIndex = 16;
            this.sbSearch.Text = "Search";
            // 
            // lblRemarks
            // 
            this.lblRemarks.Location = new System.Drawing.Point(802, 91);
            this.lblRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(61, 19);
            this.lblRemarks.TabIndex = 31;
            this.lblRemarks.Text = "Remarks";
            // 
            // txtPKNumber
            // 
            this.txtPKNumber.Location = new System.Drawing.Point(10, 48);
            this.txtPKNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtPKNumber.MenuManager = this.ribbonControl;
            this.txtPKNumber.Name = "txtPKNumber";
            this.txtPKNumber.Size = new System.Drawing.Size(180, 26);
            this.txtPKNumber.TabIndex = 1;
            // 
            // txtSupplierReference
            // 
            this.txtSupplierReference.Location = new System.Drawing.Point(10, 107);
            this.txtSupplierReference.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupplierReference.MenuManager = this.ribbonControl;
            this.txtSupplierReference.Name = "txtSupplierReference";
            this.txtSupplierReference.Size = new System.Drawing.Size(180, 26);
            this.txtSupplierReference.TabIndex = 30;
            // 
            // lblPKNumber
            // 
            this.lblPKNumber.Location = new System.Drawing.Point(60, 20);
            this.lblPKNumber.Margin = new System.Windows.Forms.Padding(4);
            this.lblPKNumber.Name = "lblPKNumber";
            this.lblPKNumber.Size = new System.Drawing.Size(80, 19);
            this.lblPKNumber.TabIndex = 0;
            this.lblPKNumber.Text = "PK Number";
            // 
            // lblSupplierReference
            // 
            this.lblSupplierReference.Location = new System.Drawing.Point(30, 82);
            this.lblSupplierReference.Margin = new System.Windows.Forms.Padding(4);
            this.lblSupplierReference.Name = "lblSupplierReference";
            this.lblSupplierReference.Size = new System.Drawing.Size(132, 19);
            this.lblSupplierReference.TabIndex = 29;
            this.lblSupplierReference.Text = "Supplier Reference";
            // 
            // lblPKDocDate
            // 
            this.lblPKDocDate.Location = new System.Drawing.Point(374, 50);
            this.lblPKDocDate.Margin = new System.Windows.Forms.Padding(4);
            this.lblPKDocDate.Name = "lblPKDocDate";
            this.lblPKDocDate.Size = new System.Drawing.Size(79, 19);
            this.lblPKDocDate.TabIndex = 2;
            this.lblPKDocDate.Text = "CREATION";
            // 
            // lbltxtStatus
            // 
            this.lbltxtStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbltxtStatus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lbltxtStatus.Location = new System.Drawing.Point(196, 48);
            this.lbltxtStatus.Margin = new System.Windows.Forms.Padding(4);
            this.lbltxtStatus.Name = "lbltxtStatus";
            this.lbltxtStatus.Size = new System.Drawing.Size(98, 29);
            this.lbltxtStatus.TabIndex = 22;
            this.lbltxtStatus.Text = "XX";
            // 
            // lblPKDelivery
            // 
            this.lblPKDelivery.Location = new System.Drawing.Point(374, 77);
            this.lblPKDelivery.Margin = new System.Windows.Forms.Padding(4);
            this.lblPKDelivery.Name = "lblPKDelivery";
            this.lblPKDelivery.Size = new System.Drawing.Size(73, 19);
            this.lblPKDelivery.TabIndex = 3;
            this.lblPKDelivery.Text = "DELIVERY";
            // 
            // sbFinishQC
            // 
            this.sbFinishQC.ImageOptions.ImageUri.Uri = "Apply";
            this.sbFinishQC.Location = new System.Drawing.Point(12, 145);
            this.sbFinishQC.Margin = new System.Windows.Forms.Padding(4);
            this.sbFinishQC.Name = "sbFinishQC";
            this.sbFinishQC.Size = new System.Drawing.Size(282, 47);
            this.sbFinishQC.TabIndex = 21;
            this.sbFinishQC.Text = "Close QC";
            // 
            // dateEditPKDocDate
            // 
            this.dateEditPKDocDate.EditValue = null;
            this.dateEditPKDocDate.Location = new System.Drawing.Point(486, 48);
            this.dateEditPKDocDate.Margin = new System.Windows.Forms.Padding(4);
            this.dateEditPKDocDate.MenuManager = this.ribbonControl;
            this.dateEditPKDocDate.Name = "dateEditPKDocDate";
            this.dateEditPKDocDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPKDocDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPKDocDate.Size = new System.Drawing.Size(159, 26);
            this.dateEditPKDocDate.TabIndex = 4;
            // 
            // dateEditPKDelivery
            // 
            this.dateEditPKDelivery.EditValue = null;
            this.dateEditPKDelivery.Location = new System.Drawing.Point(486, 76);
            this.dateEditPKDelivery.Margin = new System.Windows.Forms.Padding(4);
            this.dateEditPKDelivery.MenuManager = this.ribbonControl;
            this.dateEditPKDelivery.Name = "dateEditPKDelivery";
            this.dateEditPKDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPKDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPKDelivery.Size = new System.Drawing.Size(159, 26);
            this.dateEditPKDelivery.TabIndex = 5;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(546, 20);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 19);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date";
            // 
            // lblPKDocDateWeek
            // 
            this.lblPKDocDateWeek.AccessibleRole = System.Windows.Forms.AccessibleRole.Animation;
            this.lblPKDocDateWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPKDocDateWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblPKDocDateWeek.Location = new System.Drawing.Point(654, 48);
            this.lblPKDocDateWeek.Margin = new System.Windows.Forms.Padding(4);
            this.lblPKDocDateWeek.Name = "lblPKDocDateWeek";
            this.lblPKDocDateWeek.Size = new System.Drawing.Size(98, 29);
            this.lblPKDocDateWeek.TabIndex = 7;
            this.lblPKDocDateWeek.Text = "XX";
            // 
            // lblPKDeliveryWeek
            // 
            this.lblPKDeliveryWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPKDeliveryWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblPKDeliveryWeek.Location = new System.Drawing.Point(654, 77);
            this.lblPKDeliveryWeek.Margin = new System.Windows.Forms.Padding(4);
            this.lblPKDeliveryWeek.Name = "lblPKDeliveryWeek";
            this.lblPKDeliveryWeek.Size = new System.Drawing.Size(98, 28);
            this.lblPKDeliveryWeek.TabIndex = 8;
            this.lblPKDeliveryWeek.Text = "XX";
            // 
            // lblWeek
            // 
            this.lblWeek.Location = new System.Drawing.Point(678, 20);
            this.lblWeek.Margin = new System.Windows.Forms.Padding(4);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 19);
            this.lblWeek.TabIndex = 9;
            this.lblWeek.Text = "Week";
            // 
            // slueSupplier
            // 
            this.slueSupplier.Location = new System.Drawing.Point(963, 48);
            this.slueSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.slueSupplier.MenuManager = this.ribbonControl;
            this.slueSupplier.Name = "slueSupplier";
            this.slueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueSupplier.Properties.View = this.searchLookUpEdit1View;
            this.slueSupplier.Size = new System.Drawing.Size(328, 26);
            this.slueSupplier.TabIndex = 13;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSupplier.Location = new System.Drawing.Point(802, 53);
            this.lblSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(126, 19);
            this.lblSupplier.TabIndex = 10;
            this.lblSupplier.Text = "SUPPLIER";
            // 
            // xtcPK
            // 
            this.xtcPK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtcPK.Location = new System.Drawing.Point(16, 235);
            this.xtcPK.Margin = new System.Windows.Forms.Padding(4);
            this.xtcPK.Name = "xtcPK";
            this.xtcPK.SelectedTabPage = this.xtpPLDetail;
            this.xtcPK.Size = new System.Drawing.Size(1971, 524);
            this.xtcPK.TabIndex = 4;
            this.xtcPK.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpPLDetail});
            // 
            // xtpPLDetail
            // 
            this.xtpPLDetail.Controls.Add(this.xgrdItemsBatch);
            this.xtpPLDetail.Controls.Add(this.xgrdLinesPL);
            this.xtpPLDetail.Margin = new System.Windows.Forms.Padding(4);
            this.xtpPLDetail.Name = "xtpPLDetail";
            this.xtpPLDetail.Size = new System.Drawing.Size(1961, 482);
            this.xtpPLDetail.Text = "Packing Detail";
            // 
            // xgrdItemsBatch
            // 
            this.xgrdItemsBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xgrdItemsBatch.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.xgrdItemsBatch.Location = new System.Drawing.Point(4, 314);
            this.xgrdItemsBatch.MainView = this.gridViewItemsBatch;
            this.xgrdItemsBatch.Margin = new System.Windows.Forms.Padding(4);
            this.xgrdItemsBatch.MenuManager = this.ribbonControl;
            this.xgrdItemsBatch.Name = "xgrdItemsBatch";
            this.xgrdItemsBatch.Size = new System.Drawing.Size(750, 165);
            this.xgrdItemsBatch.TabIndex = 4;
            this.xgrdItemsBatch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItemsBatch});
            // 
            // gridViewItemsBatch
            // 
            this.gridViewItemsBatch.GridControl = this.xgrdItemsBatch;
            this.gridViewItemsBatch.Name = "gridViewItemsBatch";
            // 
            // xgrdLinesPL
            // 
            this.xgrdLinesPL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdLinesPL.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.xgrdLinesPL.Location = new System.Drawing.Point(4, 4);
            this.xgrdLinesPL.MainView = this.gridViewLinesPL;
            this.xgrdLinesPL.Margin = new System.Windows.Forms.Padding(4);
            this.xgrdLinesPL.MenuManager = this.ribbonControl;
            this.xgrdLinesPL.Name = "xgrdLinesPL";
            this.xgrdLinesPL.Size = new System.Drawing.Size(1953, 301);
            this.xgrdLinesPL.TabIndex = 0;
            this.xgrdLinesPL.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLinesPL});
            // 
            // gridViewLinesPL
            // 
            this.gridViewLinesPL.GridControl = this.xgrdLinesPL;
            this.gridViewLinesPL.Name = "gridViewLinesPL";
            // 
            // ReceiptAndQuality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2007, 1045);
            this.Controls.Add(this.xtcGeneral);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ReceiptAndQuality";
            this.Text = "ReceiptAndQuality";
            this.Load += new System.EventHandler(this.ReceiptAndQuality_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpReceiptAndQuality.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).EndInit();
            this.pcFilter.ResumeLayout(false);
            this.pcFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPKNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplierReference.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPKDocDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPKDocDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPKDelivery.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPKDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcPK)).EndInit();
            this.xtcPK.ResumeLayout(false);
            this.xtpPLDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdItemsBatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemsBatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLinesPL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLinesPL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpReceiptAndQuality;
        private DevExpress.XtraEditors.PanelControl pcFilter;
        private DevExpress.XtraEditors.MemoEdit memoEditRemarks;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraEditors.LabelControl lblRemarks;
        private DevExpress.XtraEditors.TextEdit txtPKNumber;
        private DevExpress.XtraEditors.TextEdit txtSupplierReference;
        private DevExpress.XtraEditors.LabelControl lblPKNumber;
        private DevExpress.XtraEditors.LabelControl lblSupplierReference;
        private DevExpress.XtraEditors.LabelControl lblPKDocDate;
        private DevExpress.XtraEditors.LabelControl lbltxtStatus;
        private DevExpress.XtraEditors.LabelControl lblPKDelivery;
        private DevExpress.XtraEditors.SimpleButton sbFinishQC;
        private DevExpress.XtraEditors.DateEdit dateEditPKDocDate;
        private DevExpress.XtraEditors.DateEdit dateEditPKDelivery;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.LabelControl lblPKDocDateWeek;
        private DevExpress.XtraEditors.LabelControl lblPKDeliveryWeek;
        private DevExpress.XtraEditors.LabelControl lblWeek;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraTab.XtraTabControl xtcPK;
        private DevExpress.XtraTab.XtraTabPage xtpPLDetail;
        private DevExpress.XtraGrid.GridControl xgrdLinesPL;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLinesPL;
        private DevExpress.XtraGrid.GridControl xgrdItemsBatch;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItemsBatch;
        private DevExpress.XtraEditors.SimpleButton sbAddItem;
    }
}