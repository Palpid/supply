namespace HKSupply.Forms.Supply.SupplyMaterials
{
    partial class QualityControlPending
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
            this.xtpQualityControlPending = new DevExpress.XtraTab.XtraTabPage();
            this.pcFilter = new DevExpress.XtraEditors.PanelControl();
            this.memoEditRemarks = new DevExpress.XtraEditors.MemoEdit();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lblRemarks = new DevExpress.XtraEditors.LabelControl();
            this.txtQCPNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtSupplierReference = new DevExpress.XtraEditors.TextEdit();
            this.lblQCPNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblSupplierReference = new DevExpress.XtraEditors.LabelControl();
            this.lblPKDocDate = new DevExpress.XtraEditors.LabelControl();
            this.lbltxtStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblPKDelivery = new DevExpress.XtraEditors.LabelControl();
            this.sbFinishQCP = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditQCPDocDate = new DevExpress.XtraEditors.DateEdit();
            this.dateEditQCPDelivery = new DevExpress.XtraEditors.DateEdit();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.lblQCPDocDateWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblQCPDeliveryWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblWeek = new DevExpress.XtraEditors.LabelControl();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.xtcQCP = new DevExpress.XtraTab.XtraTabControl();
            this.xtpQCPDetail = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdItemsBatch = new DevExpress.XtraGrid.GridControl();
            this.gridViewItemsBatch = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xgrdLinesQCP = new DevExpress.XtraGrid.GridControl();
            this.gridViewLinesQCP = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpQualityControlPending.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).BeginInit();
            this.pcFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCPNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplierReference.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQCPDocDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQCPDocDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQCPDelivery.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQCPDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcQCP)).BeginInit();
            this.xtcQCP.SuspendLayout();
            this.xtpQCPDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdItemsBatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemsBatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLinesQCP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLinesQCP)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(1338, 125);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 688);
            this.ribbonStatusBar.Size = new System.Drawing.Size(1338, 27);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 125);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpQualityControlPending;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 563);
            this.xtcGeneral.TabIndex = 8;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpQualityControlPending});
            // 
            // xtpQualityControlPending
            // 
            this.xtpQualityControlPending.Controls.Add(this.pcFilter);
            this.xtpQualityControlPending.Controls.Add(this.xtcQCP);
            this.xtpQualityControlPending.Name = "xtpQualityControlPending";
            this.xtpQualityControlPending.Size = new System.Drawing.Size(1332, 535);
            this.xtpQualityControlPending.Text = "Quality Control Pending";
            // 
            // pcFilter
            // 
            this.pcFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcFilter.Controls.Add(this.memoEditRemarks);
            this.pcFilter.Controls.Add(this.sbSearch);
            this.pcFilter.Controls.Add(this.lblRemarks);
            this.pcFilter.Controls.Add(this.txtQCPNumber);
            this.pcFilter.Controls.Add(this.txtSupplierReference);
            this.pcFilter.Controls.Add(this.lblQCPNumber);
            this.pcFilter.Controls.Add(this.lblSupplierReference);
            this.pcFilter.Controls.Add(this.lblPKDocDate);
            this.pcFilter.Controls.Add(this.lbltxtStatus);
            this.pcFilter.Controls.Add(this.lblPKDelivery);
            this.pcFilter.Controls.Add(this.sbFinishQCP);
            this.pcFilter.Controls.Add(this.dateEditQCPDocDate);
            this.pcFilter.Controls.Add(this.dateEditQCPDelivery);
            this.pcFilter.Controls.Add(this.lblDate);
            this.pcFilter.Controls.Add(this.lblQCPDocDateWeek);
            this.pcFilter.Controls.Add(this.lblQCPDeliveryWeek);
            this.pcFilter.Controls.Add(this.lblWeek);
            this.pcFilter.Controls.Add(this.slueSupplier);
            this.pcFilter.Controls.Add(this.lblSupplier);
            this.pcFilter.Location = new System.Drawing.Point(11, 13);
            this.pcFilter.Name = "pcFilter";
            this.pcFilter.Size = new System.Drawing.Size(1314, 142);
            this.pcFilter.TabIndex = 5;
            // 
            // memoEditRemarks
            // 
            this.memoEditRemarks.Location = new System.Drawing.Point(642, 61);
            this.memoEditRemarks.MenuManager = this.ribbonControl;
            this.memoEditRemarks.Name = "memoEditRemarks";
            this.memoEditRemarks.Size = new System.Drawing.Size(389, 32);
            this.memoEditRemarks.TabIndex = 32;
            // 
            // sbSearch
            // 
            this.sbSearch.Location = new System.Drawing.Point(949, 14);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(82, 38);
            this.sbSearch.TabIndex = 16;
            this.sbSearch.Text = "Search";
            // 
            // lblRemarks
            // 
            this.lblRemarks.Location = new System.Drawing.Point(535, 62);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(41, 13);
            this.lblRemarks.TabIndex = 31;
            this.lblRemarks.Text = "Remarks";
            // 
            // txtQCPNumber
            // 
            this.txtQCPNumber.Location = new System.Drawing.Point(7, 33);
            this.txtQCPNumber.MenuManager = this.ribbonControl;
            this.txtQCPNumber.Name = "txtQCPNumber";
            this.txtQCPNumber.Size = new System.Drawing.Size(120, 20);
            this.txtQCPNumber.TabIndex = 1;
            // 
            // txtSupplierReference
            // 
            this.txtSupplierReference.Location = new System.Drawing.Point(7, 73);
            this.txtSupplierReference.MenuManager = this.ribbonControl;
            this.txtSupplierReference.Name = "txtSupplierReference";
            this.txtSupplierReference.Size = new System.Drawing.Size(120, 20);
            this.txtSupplierReference.TabIndex = 30;
            // 
            // lblQCPNumber
            // 
            this.lblQCPNumber.Location = new System.Drawing.Point(40, 14);
            this.lblQCPNumber.Name = "lblQCPNumber";
            this.lblQCPNumber.Size = new System.Drawing.Size(61, 13);
            this.lblQCPNumber.TabIndex = 0;
            this.lblQCPNumber.Text = "QCP Number";
            // 
            // lblSupplierReference
            // 
            this.lblSupplierReference.Location = new System.Drawing.Point(20, 56);
            this.lblSupplierReference.Name = "lblSupplierReference";
            this.lblSupplierReference.Size = new System.Drawing.Size(91, 13);
            this.lblSupplierReference.TabIndex = 29;
            this.lblSupplierReference.Text = "Supplier Reference";
            // 
            // lblPKDocDate
            // 
            this.lblPKDocDate.Location = new System.Drawing.Point(249, 34);
            this.lblPKDocDate.Name = "lblPKDocDate";
            this.lblPKDocDate.Size = new System.Drawing.Size(52, 13);
            this.lblPKDocDate.TabIndex = 2;
            this.lblPKDocDate.Text = "CREATION";
            // 
            // lbltxtStatus
            // 
            this.lbltxtStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbltxtStatus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lbltxtStatus.Location = new System.Drawing.Point(131, 33);
            this.lbltxtStatus.Name = "lbltxtStatus";
            this.lbltxtStatus.Size = new System.Drawing.Size(65, 20);
            this.lbltxtStatus.TabIndex = 22;
            this.lbltxtStatus.Text = "XX";
            // 
            // lblPKDelivery
            // 
            this.lblPKDelivery.Location = new System.Drawing.Point(249, 53);
            this.lblPKDelivery.Name = "lblPKDelivery";
            this.lblPKDelivery.Size = new System.Drawing.Size(47, 13);
            this.lblPKDelivery.TabIndex = 3;
            this.lblPKDelivery.Text = "DELIVERY";
            // 
            // sbFinishQCP
            // 
            this.sbFinishQCP.ImageOptions.ImageUri.Uri = "Apply";
            this.sbFinishQCP.Location = new System.Drawing.Point(8, 99);
            this.sbFinishQCP.Name = "sbFinishQCP";
            this.sbFinishQCP.Size = new System.Drawing.Size(188, 32);
            this.sbFinishQCP.TabIndex = 21;
            this.sbFinishQCP.Text = "Close QCP";
            // 
            // dateEditQCPDocDate
            // 
            this.dateEditQCPDocDate.EditValue = null;
            this.dateEditQCPDocDate.Location = new System.Drawing.Point(324, 33);
            this.dateEditQCPDocDate.MenuManager = this.ribbonControl;
            this.dateEditQCPDocDate.Name = "dateEditQCPDocDate";
            this.dateEditQCPDocDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditQCPDocDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditQCPDocDate.Size = new System.Drawing.Size(106, 20);
            this.dateEditQCPDocDate.TabIndex = 4;
            // 
            // dateEditQCPDelivery
            // 
            this.dateEditQCPDelivery.EditValue = null;
            this.dateEditQCPDelivery.Location = new System.Drawing.Point(324, 52);
            this.dateEditQCPDelivery.MenuManager = this.ribbonControl;
            this.dateEditQCPDelivery.Name = "dateEditQCPDelivery";
            this.dateEditQCPDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditQCPDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditQCPDelivery.Size = new System.Drawing.Size(106, 20);
            this.dateEditQCPDelivery.TabIndex = 5;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(364, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(23, 13);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date";
            // 
            // lblQCPDocDateWeek
            // 
            this.lblQCPDocDateWeek.AccessibleRole = System.Windows.Forms.AccessibleRole.Animation;
            this.lblQCPDocDateWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblQCPDocDateWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblQCPDocDateWeek.Location = new System.Drawing.Point(436, 33);
            this.lblQCPDocDateWeek.Name = "lblQCPDocDateWeek";
            this.lblQCPDocDateWeek.Size = new System.Drawing.Size(65, 20);
            this.lblQCPDocDateWeek.TabIndex = 7;
            this.lblQCPDocDateWeek.Text = "XX";
            // 
            // lblQCPDeliveryWeek
            // 
            this.lblQCPDeliveryWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblQCPDeliveryWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblQCPDeliveryWeek.Location = new System.Drawing.Point(436, 53);
            this.lblQCPDeliveryWeek.Name = "lblQCPDeliveryWeek";
            this.lblQCPDeliveryWeek.Size = new System.Drawing.Size(65, 19);
            this.lblQCPDeliveryWeek.TabIndex = 8;
            this.lblQCPDeliveryWeek.Text = "XX";
            // 
            // lblWeek
            // 
            this.lblWeek.Location = new System.Drawing.Point(452, 14);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(27, 13);
            this.lblWeek.TabIndex = 9;
            this.lblWeek.Text = "Week";
            // 
            // slueSupplier
            // 
            this.slueSupplier.Location = new System.Drawing.Point(642, 33);
            this.slueSupplier.MenuManager = this.ribbonControl;
            this.slueSupplier.Name = "slueSupplier";
            this.slueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueSupplier.Properties.View = this.searchLookUpEdit1View;
            this.slueSupplier.Size = new System.Drawing.Size(219, 20);
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
            this.lblSupplier.Location = new System.Drawing.Point(535, 36);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(84, 13);
            this.lblSupplier.TabIndex = 10;
            this.lblSupplier.Text = "SUPPLIER";
            // 
            // xtcQCP
            // 
            this.xtcQCP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtcQCP.Location = new System.Drawing.Point(11, 161);
            this.xtcQCP.Name = "xtcQCP";
            this.xtcQCP.SelectedTabPage = this.xtpQCPDetail;
            this.xtcQCP.Size = new System.Drawing.Size(1314, 371);
            this.xtcQCP.TabIndex = 4;
            this.xtcQCP.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpQCPDetail});
            // 
            // xtpQCPDetail
            // 
            this.xtpQCPDetail.Controls.Add(this.xgrdItemsBatch);
            this.xtpQCPDetail.Controls.Add(this.xgrdLinesQCP);
            this.xtpQCPDetail.Name = "xtpQCPDetail";
            this.xtpQCPDetail.Size = new System.Drawing.Size(1308, 343);
            this.xtpQCPDetail.Text = "Quality Control Pending Detail";
            // 
            // xgrdItemsBatch
            // 
            this.xgrdItemsBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xgrdItemsBatch.Location = new System.Drawing.Point(3, 227);
            this.xgrdItemsBatch.MainView = this.gridViewItemsBatch;
            this.xgrdItemsBatch.MenuManager = this.ribbonControl;
            this.xgrdItemsBatch.Name = "xgrdItemsBatch";
            this.xgrdItemsBatch.Size = new System.Drawing.Size(500, 113);
            this.xgrdItemsBatch.TabIndex = 4;
            this.xgrdItemsBatch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItemsBatch});
            // 
            // gridViewItemsBatch
            // 
            this.gridViewItemsBatch.GridControl = this.xgrdItemsBatch;
            this.gridViewItemsBatch.Name = "gridViewItemsBatch";
            // 
            // xgrdLinesQCP
            // 
            this.xgrdLinesQCP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdLinesQCP.Location = new System.Drawing.Point(3, 3);
            this.xgrdLinesQCP.MainView = this.gridViewLinesQCP;
            this.xgrdLinesQCP.MenuManager = this.ribbonControl;
            this.xgrdLinesQCP.Name = "xgrdLinesQCP";
            this.xgrdLinesQCP.Size = new System.Drawing.Size(1302, 218);
            this.xgrdLinesQCP.TabIndex = 0;
            this.xgrdLinesQCP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLinesQCP});
            // 
            // gridViewLinesQCP
            // 
            this.gridViewLinesQCP.GridControl = this.xgrdLinesQCP;
            this.gridViewLinesQCP.Name = "gridViewLinesQCP";
            // 
            // QualityControlPending
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 715);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "QualityControlPending";
            this.Text = "QualityControlPending";
            this.Load += new System.EventHandler(this.QualityControlPending_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpQualityControlPending.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).EndInit();
            this.pcFilter.ResumeLayout(false);
            this.pcFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQCPNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSupplierReference.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQCPDocDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQCPDocDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQCPDelivery.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQCPDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcQCP)).EndInit();
            this.xtcQCP.ResumeLayout(false);
            this.xtpQCPDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdItemsBatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemsBatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLinesQCP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLinesQCP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpQualityControlPending;
        private DevExpress.XtraEditors.PanelControl pcFilter;
        private DevExpress.XtraEditors.MemoEdit memoEditRemarks;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraEditors.LabelControl lblRemarks;
        private DevExpress.XtraEditors.TextEdit txtQCPNumber;
        private DevExpress.XtraEditors.TextEdit txtSupplierReference;
        private DevExpress.XtraEditors.LabelControl lblQCPNumber;
        private DevExpress.XtraEditors.LabelControl lblSupplierReference;
        private DevExpress.XtraEditors.LabelControl lblPKDocDate;
        private DevExpress.XtraEditors.LabelControl lbltxtStatus;
        private DevExpress.XtraEditors.LabelControl lblPKDelivery;
        private DevExpress.XtraEditors.SimpleButton sbFinishQCP;
        private DevExpress.XtraEditors.DateEdit dateEditQCPDocDate;
        private DevExpress.XtraEditors.DateEdit dateEditQCPDelivery;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.LabelControl lblQCPDocDateWeek;
        private DevExpress.XtraEditors.LabelControl lblQCPDeliveryWeek;
        private DevExpress.XtraEditors.LabelControl lblWeek;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraTab.XtraTabControl xtcQCP;
        private DevExpress.XtraTab.XtraTabPage xtpQCPDetail;
        private DevExpress.XtraGrid.GridControl xgrdItemsBatch;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItemsBatch;
        private DevExpress.XtraGrid.GridControl xgrdLinesQCP;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLinesQCP;
    }
}