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
            this.xtcGeneral = new System.Windows.Forms.TabControl();
            this.xtpPOSection = new System.Windows.Forms.TabPage();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.xgrdLines = new DevExpress.XtraGrid.GridControl();
            this.gridViewLines = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.lblPONumber = new DevExpress.XtraEditors.LabelControl();
            this.txtPONumber = new DevExpress.XtraEditors.TextEdit();
            this.slueStatus = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblDocDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditPODateEnd = new DevExpress.XtraEditors.DateEdit();
            this.dateEditPODateIni = new DevExpress.XtraEditors.DateEdit();
            this.lblAnd = new DevExpress.XtraEditors.LabelControl();
            this.sbFilter = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpPOSection.SuspendLayout();
            this.gbFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties)).BeginInit();
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
            this.xtcGeneral.Controls.Add(this.xtpPOSection);
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 79);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedIndex = 0;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 605);
            this.xtcGeneral.TabIndex = 2;
            // 
            // xtpPOSection
            // 
            this.xtpPOSection.Controls.Add(this.xgrdLines);
            this.xtpPOSection.Controls.Add(this.gbFilters);
            this.xtpPOSection.Location = new System.Drawing.Point(4, 22);
            this.xtpPOSection.Name = "xtpPOSection";
            this.xtpPOSection.Padding = new System.Windows.Forms.Padding(3);
            this.xtpPOSection.Size = new System.Drawing.Size(1330, 579);
            this.xtpPOSection.TabIndex = 0;
            this.xtpPOSection.Text = "PURCHASE ORDER SELECTION";
            this.xtpPOSection.UseVisualStyleBackColor = true;
            // 
            // gbFilters
            // 
            this.gbFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFilters.Controls.Add(this.sbFilter);
            this.gbFilters.Controls.Add(this.lblAnd);
            this.gbFilters.Controls.Add(this.lblDocDate);
            this.gbFilters.Controls.Add(this.dateEditPODateEnd);
            this.gbFilters.Controls.Add(this.dateEditPODateIni);
            this.gbFilters.Controls.Add(this.slueStatus);
            this.gbFilters.Controls.Add(this.lblStatus);
            this.gbFilters.Controls.Add(this.txtPONumber);
            this.gbFilters.Controls.Add(this.lblPONumber);
            this.gbFilters.Controls.Add(this.slueSupplier);
            this.gbFilters.Controls.Add(this.lblSupplier);
            this.gbFilters.Location = new System.Drawing.Point(6, 6);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(1316, 101);
            this.gbFilters.TabIndex = 0;
            this.gbFilters.TabStop = false;
            // 
            // xgrdLines
            // 
            this.xgrdLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdLines.Location = new System.Drawing.Point(6, 113);
            this.xgrdLines.MainView = this.gridViewLines;
            this.xgrdLines.MenuManager = this.ribbonControl;
            this.xgrdLines.Name = "xgrdLines";
            this.xgrdLines.Size = new System.Drawing.Size(1316, 460);
            this.xgrdLines.TabIndex = 1;
            this.xgrdLines.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLines});
            // 
            // gridViewLines
            // 
            this.gridViewLines.GridControl = this.xgrdLines;
            this.gridViewLines.Name = "gridViewLines";
            // 
            // slueSupplier
            // 
            this.slueSupplier.Location = new System.Drawing.Point(115, 43);
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
            this.lblSupplier.Location = new System.Drawing.Point(8, 46);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(84, 13);
            this.lblSupplier.TabIndex = 14;
            this.lblSupplier.Text = "Supplier";
            // 
            // lblPONumber
            // 
            this.lblPONumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPONumber.Location = new System.Drawing.Point(8, 20);
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Size = new System.Drawing.Size(84, 13);
            this.lblPONumber.TabIndex = 16;
            this.lblPONumber.Text = "PO Number";
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(115, 17);
            this.txtPONumber.MenuManager = this.ribbonControl;
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.Size = new System.Drawing.Size(142, 20);
            this.txtPONumber.TabIndex = 17;
            // 
            // slueStatus
            // 
            this.slueStatus.Location = new System.Drawing.Point(115, 69);
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
            this.lblStatus.Location = new System.Drawing.Point(8, 72);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(84, 13);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status";
            // 
            // lblDocDate
            // 
            this.lblDocDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocDate.Location = new System.Drawing.Point(330, 20);
            this.lblDocDate.Name = "lblDocDate";
            this.lblDocDate.Size = new System.Drawing.Size(115, 13);
            this.lblDocDate.TabIndex = 22;
            this.lblDocDate.Text = "PO Date          between";
            // 
            // dateEditPODateEnd
            // 
            this.dateEditPODateEnd.EditValue = null;
            this.dateEditPODateEnd.Location = new System.Drawing.Point(621, 17);
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
            this.dateEditPODateIni.Location = new System.Drawing.Point(451, 17);
            this.dateEditPODateIni.MenuManager = this.ribbonControl;
            this.dateEditPODateIni.Name = "dateEditPODateIni";
            this.dateEditPODateIni.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateIni.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateIni.Size = new System.Drawing.Size(106, 20);
            this.dateEditPODateIni.TabIndex = 20;
            // 
            // lblAnd
            // 
            this.lblAnd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAnd.Location = new System.Drawing.Point(576, 20);
            this.lblAnd.Name = "lblAnd";
            this.lblAnd.Size = new System.Drawing.Size(39, 13);
            this.lblAnd.TabIndex = 23;
            this.lblAnd.Text = "and";
            // 
            // sbFilter
            // 
            this.sbFilter.ImageUri.Uri = "Find;Size32x32";
            this.sbFilter.Location = new System.Drawing.Point(687, 51);
            this.sbFilter.Name = "sbFilter";
            this.sbFilter.Size = new System.Drawing.Size(40, 38);
            this.sbFilter.TabIndex = 24;
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
            this.xtcGeneral.ResumeLayout(false);
            this.xtpPOSection.ResumeLayout(false);
            this.gbFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl xtcGeneral;
        private System.Windows.Forms.TabPage xtpPOSection;
        private System.Windows.Forms.GroupBox gbFilters;
        private DevExpress.XtraGrid.GridControl xgrdLines;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLines;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraEditors.LabelControl lblPONumber;
        private DevExpress.XtraEditors.TextEdit txtPONumber;
        private DevExpress.XtraEditors.SearchLookUpEdit slueStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.LabelControl lblAnd;
        private DevExpress.XtraEditors.LabelControl lblDocDate;
        private DevExpress.XtraEditors.DateEdit dateEditPODateEnd;
        private DevExpress.XtraEditors.DateEdit dateEditPODateIni;
        private DevExpress.XtraEditors.SimpleButton sbFilter;
    }
}