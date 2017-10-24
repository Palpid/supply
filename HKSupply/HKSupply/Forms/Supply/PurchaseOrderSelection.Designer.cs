namespace HKSupply.Forms.Supply
{
    partial class PurchaseOrderSelection
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
            this.xtpPOSection = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdLines = new DevExpress.XtraGrid.GridControl();
            this.gridViewLines = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbFilter = new DevExpress.XtraEditors.SimpleButton();
            this.lblAnd = new DevExpress.XtraEditors.LabelControl();
            this.lblDocDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditPODateEnd = new DevExpress.XtraEditors.DateEdit();
            this.dateEditPODateIni = new DevExpress.XtraEditors.DateEdit();
            this.slueStatus = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.txtPONumber = new DevExpress.XtraEditors.TextEdit();
            this.lblPONumber = new DevExpress.XtraEditors.LabelControl();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.pcFilter = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpPOSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).BeginInit();
            this.pcFilter.SuspendLayout();
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
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 684);
            this.ribbonStatusBar.Size = new System.Drawing.Size(1338, 31);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 125);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpPOSection;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 559);
            this.xtcGeneral.TabIndex = 3;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpPOSection});
            // 
            // xtpPOSection
            // 
            this.xtpPOSection.Controls.Add(this.pcFilter);
            this.xtpPOSection.Controls.Add(this.xgrdLines);
            this.xtpPOSection.Name = "xtpPOSection";
            this.xtpPOSection.Size = new System.Drawing.Size(1332, 531);
            this.xtpPOSection.Text = "PURCHASE ORDER SELECTION";
            // 
            // xgrdLines
            // 
            this.xgrdLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdLines.Location = new System.Drawing.Point(6, 110);
            this.xgrdLines.MainView = this.gridViewLines;
            this.xgrdLines.MenuManager = this.ribbonControl;
            this.xgrdLines.Name = "xgrdLines";
            this.xgrdLines.Size = new System.Drawing.Size(1320, 418);
            this.xgrdLines.TabIndex = 2;
            this.xgrdLines.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLines});
            // 
            // gridViewLines
            // 
            this.gridViewLines.GridControl = this.xgrdLines;
            this.gridViewLines.Name = "gridViewLines";
            // 
            // sbFilter
            // 
            this.sbFilter.Location = new System.Drawing.Point(647, 44);
            this.sbFilter.Name = "sbFilter";
            this.sbFilter.Size = new System.Drawing.Size(82, 38);
            this.sbFilter.TabIndex = 24;
            this.sbFilter.Text = "Search";
            // 
            // lblAnd
            // 
            this.lblAnd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAnd.Location = new System.Drawing.Point(578, 13);
            this.lblAnd.Name = "lblAnd";
            this.lblAnd.Size = new System.Drawing.Size(39, 13);
            this.lblAnd.TabIndex = 23;
            this.lblAnd.Text = "and";
            // 
            // lblDocDate
            // 
            this.lblDocDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocDate.Location = new System.Drawing.Point(332, 13);
            this.lblDocDate.Name = "lblDocDate";
            this.lblDocDate.Size = new System.Drawing.Size(115, 13);
            this.lblDocDate.TabIndex = 22;
            this.lblDocDate.Text = "PO Date          between";
            // 
            // dateEditPODateEnd
            // 
            this.dateEditPODateEnd.EditValue = null;
            this.dateEditPODateEnd.Location = new System.Drawing.Point(623, 10);
            this.dateEditPODateEnd.MenuManager = this.ribbonControl;
            this.dateEditPODateEnd.Name = "dateEditPODateEnd";
            this.dateEditPODateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateEnd.Size = new System.Drawing.Size(106, 20);
            this.dateEditPODateEnd.TabIndex = 21;
            // 
            // dateEditPODateIni
            // 
            this.dateEditPODateIni.EditValue = null;
            this.dateEditPODateIni.Location = new System.Drawing.Point(453, 10);
            this.dateEditPODateIni.MenuManager = this.ribbonControl;
            this.dateEditPODateIni.Name = "dateEditPODateIni";
            this.dateEditPODateIni.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateIni.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateIni.Size = new System.Drawing.Size(106, 20);
            this.dateEditPODateIni.TabIndex = 20;
            // 
            // slueStatus
            // 
            this.slueStatus.Location = new System.Drawing.Point(117, 62);
            this.slueStatus.MenuManager = this.ribbonControl;
            this.slueStatus.Name = "slueStatus";
            this.slueStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueStatus.Properties.View = this.gridView1;
            this.slueStatus.Size = new System.Drawing.Size(142, 20);
            this.slueStatus.TabIndex = 19;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatus.Location = new System.Drawing.Point(10, 65);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(84, 13);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status";
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(117, 10);
            this.txtPONumber.MenuManager = this.ribbonControl;
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.Size = new System.Drawing.Size(142, 20);
            this.txtPONumber.TabIndex = 17;
            // 
            // lblPONumber
            // 
            this.lblPONumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPONumber.Location = new System.Drawing.Point(10, 13);
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Size = new System.Drawing.Size(84, 13);
            this.lblPONumber.TabIndex = 16;
            this.lblPONumber.Text = "PO Number";
            // 
            // slueSupplier
            // 
            this.slueSupplier.Location = new System.Drawing.Point(117, 36);
            this.slueSupplier.MenuManager = this.ribbonControl;
            this.slueSupplier.Name = "slueSupplier";
            this.slueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueSupplier.Properties.View = this.searchLookUpEdit1View;
            this.slueSupplier.Size = new System.Drawing.Size(142, 20);
            this.slueSupplier.TabIndex = 15;
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
            this.lblSupplier.Location = new System.Drawing.Point(10, 39);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(84, 13);
            this.lblSupplier.TabIndex = 14;
            this.lblSupplier.Text = "Supplier";
            // 
            // pcFilter
            // 
            this.pcFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcFilter.Controls.Add(this.sbFilter);
            this.pcFilter.Controls.Add(this.lblAnd);
            this.pcFilter.Controls.Add(this.lblSupplier);
            this.pcFilter.Controls.Add(this.lblDocDate);
            this.pcFilter.Controls.Add(this.slueSupplier);
            this.pcFilter.Controls.Add(this.dateEditPODateEnd);
            this.pcFilter.Controls.Add(this.lblPONumber);
            this.pcFilter.Controls.Add(this.dateEditPODateIni);
            this.pcFilter.Controls.Add(this.txtPONumber);
            this.pcFilter.Controls.Add(this.slueStatus);
            this.pcFilter.Controls.Add(this.lblStatus);
            this.pcFilter.Location = new System.Drawing.Point(6, 13);
            this.pcFilter.Name = "pcFilter";
            this.pcFilter.Size = new System.Drawing.Size(1319, 91);
            this.pcFilter.TabIndex = 3;
            // 
            // PurchaseOrderSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 715);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "PurchaseOrderSelection";
            this.Text = "Purchase Order Selection";
            this.Load += new System.EventHandler(this.PurchaseOrderSelection_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpPOSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).EndInit();
            this.pcFilter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpPOSection;
        private DevExpress.XtraGrid.GridControl xgrdLines;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLines;
        private DevExpress.XtraEditors.SimpleButton sbFilter;
        private DevExpress.XtraEditors.LabelControl lblAnd;
        private DevExpress.XtraEditors.LabelControl lblDocDate;
        private DevExpress.XtraEditors.DateEdit dateEditPODateEnd;
        private DevExpress.XtraEditors.DateEdit dateEditPODateIni;
        private DevExpress.XtraEditors.SearchLookUpEdit slueStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.TextEdit txtPONumber;
        private DevExpress.XtraEditors.LabelControl lblPONumber;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraEditors.PanelControl pcFilter;
    }
}