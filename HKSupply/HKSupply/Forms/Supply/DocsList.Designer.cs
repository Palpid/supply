namespace HKSupply.Forms.Supply
{
    partial class DocsList
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
            this.xtpDocsList = new System.Windows.Forms.TabPage();
            this.xgrdLines = new DevExpress.XtraGrid.GridControl();
            this.gridViewLines = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.slueCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.slueDocType = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblDocType = new DevExpress.XtraEditors.LabelControl();
            this.sbFilter = new DevExpress.XtraEditors.SimpleButton();
            this.lblAnd = new DevExpress.XtraEditors.LabelControl();
            this.lblDocDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditPODateEnd = new DevExpress.XtraEditors.DateEdit();
            this.dateEditPODateIni = new DevExpress.XtraEditors.DateEdit();
            this.slueStatus = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.txtDocNumber = new DevExpress.XtraEditors.TextEdit();
            this.lblDocNumber = new DevExpress.XtraEditors.LabelControl();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpDocsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).BeginInit();
            this.gbFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueDocType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
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
            this.xtcGeneral.Controls.Add(this.xtpDocsList);
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 125);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedIndex = 0;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 559);
            this.xtcGeneral.TabIndex = 3;
            // 
            // xtpDocsList
            // 
            this.xtpDocsList.Controls.Add(this.xgrdLines);
            this.xtpDocsList.Controls.Add(this.gbFilters);
            this.xtpDocsList.Location = new System.Drawing.Point(4, 22);
            this.xtpDocsList.Name = "xtpDocsList";
            this.xtpDocsList.Padding = new System.Windows.Forms.Padding(3);
            this.xtpDocsList.Size = new System.Drawing.Size(1330, 533);
            this.xtpDocsList.TabIndex = 0;
            this.xtpDocsList.Text = "DOCS LIST";
            this.xtpDocsList.UseVisualStyleBackColor = true;
            this.xtpDocsList.Click += new System.EventHandler(this.xtpDocsList_Click);
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
            this.xgrdLines.Size = new System.Drawing.Size(1316, 414);
            this.xgrdLines.TabIndex = 1;
            this.xgrdLines.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLines});
            // 
            // gridViewLines
            // 
            this.gridViewLines.GridControl = this.xgrdLines;
            this.gridViewLines.Name = "gridViewLines";
            // 
            // gbFilters
            // 
            this.gbFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFilters.Controls.Add(this.slueCustomer);
            this.gbFilters.Controls.Add(this.lblCustomer);
            this.gbFilters.Controls.Add(this.slueDocType);
            this.gbFilters.Controls.Add(this.lblDocType);
            this.gbFilters.Controls.Add(this.sbFilter);
            this.gbFilters.Controls.Add(this.lblAnd);
            this.gbFilters.Controls.Add(this.lblDocDate);
            this.gbFilters.Controls.Add(this.dateEditPODateEnd);
            this.gbFilters.Controls.Add(this.dateEditPODateIni);
            this.gbFilters.Controls.Add(this.slueStatus);
            this.gbFilters.Controls.Add(this.lblStatus);
            this.gbFilters.Controls.Add(this.txtDocNumber);
            this.gbFilters.Controls.Add(this.lblDocNumber);
            this.gbFilters.Controls.Add(this.slueSupplier);
            this.gbFilters.Controls.Add(this.lblSupplier);
            this.gbFilters.Location = new System.Drawing.Point(6, 6);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(1316, 101);
            this.gbFilters.TabIndex = 0;
            this.gbFilters.TabStop = false;
            // 
            // slueCustomer
            // 
            this.slueCustomer.Location = new System.Drawing.Point(370, 43);
            this.slueCustomer.MenuManager = this.ribbonControl;
            this.slueCustomer.Name = "slueCustomer";
            this.slueCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueCustomer.Properties.View = this.gridView3;
            this.slueCustomer.Size = new System.Drawing.Size(142, 20);
            this.slueCustomer.TabIndex = 28;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCustomer.Location = new System.Drawing.Point(263, 46);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(84, 13);
            this.lblCustomer.TabIndex = 27;
            this.lblCustomer.Text = "Customer";
            // 
            // slueDocType
            // 
            this.slueDocType.Location = new System.Drawing.Point(115, 13);
            this.slueDocType.MenuManager = this.ribbonControl;
            this.slueDocType.Name = "slueDocType";
            this.slueDocType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueDocType.Properties.View = this.gridView2;
            this.slueDocType.Size = new System.Drawing.Size(142, 20);
            this.slueDocType.TabIndex = 26;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // lblDocType
            // 
            this.lblDocType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocType.Location = new System.Drawing.Point(8, 16);
            this.lblDocType.Name = "lblDocType";
            this.lblDocType.Size = new System.Drawing.Size(84, 13);
            this.lblDocType.TabIndex = 25;
            this.lblDocType.Text = "Type";
            // 
            // sbFilter
            // 
            this.sbFilter.ImageOptions.ImageUri.Uri = "Find;Size32x32";
            this.sbFilter.Location = new System.Drawing.Point(842, 47);
            this.sbFilter.Name = "sbFilter";
            this.sbFilter.Size = new System.Drawing.Size(82, 38);
            this.sbFilter.TabIndex = 24;
            this.sbFilter.Text = "Search";
            // 
            // lblAnd
            // 
            this.lblAnd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAnd.Location = new System.Drawing.Point(773, 16);
            this.lblAnd.Name = "lblAnd";
            this.lblAnd.Size = new System.Drawing.Size(39, 13);
            this.lblAnd.TabIndex = 23;
            this.lblAnd.Text = "and";
            // 
            // lblDocDate
            // 
            this.lblDocDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocDate.Location = new System.Drawing.Point(527, 16);
            this.lblDocDate.Name = "lblDocDate";
            this.lblDocDate.Size = new System.Drawing.Size(115, 13);
            this.lblDocDate.TabIndex = 22;
            this.lblDocDate.Text = "Doc. Date        between";
            // 
            // dateEditPODateEnd
            // 
            this.dateEditPODateEnd.EditValue = null;
            this.dateEditPODateEnd.Location = new System.Drawing.Point(818, 13);
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
            this.dateEditPODateIni.Location = new System.Drawing.Point(648, 13);
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
            // txtDocNumber
            // 
            this.txtDocNumber.Location = new System.Drawing.Point(370, 13);
            this.txtDocNumber.MenuManager = this.ribbonControl;
            this.txtDocNumber.Name = "txtDocNumber";
            this.txtDocNumber.Size = new System.Drawing.Size(142, 20);
            this.txtDocNumber.TabIndex = 17;
            // 
            // lblDocNumber
            // 
            this.lblDocNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocNumber.Location = new System.Drawing.Point(263, 16);
            this.lblDocNumber.Name = "lblDocNumber";
            this.lblDocNumber.Size = new System.Drawing.Size(84, 13);
            this.lblDocNumber.TabIndex = 16;
            this.lblDocNumber.Text = "Doc. Number";
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
            // DocsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 715);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "DocsList";
            this.Text = "Docs List";
            this.Load += new System.EventHandler(this.DocsList_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpDocsList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).EndInit();
            this.gbFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueDocType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl xtcGeneral;
        private System.Windows.Forms.TabPage xtpDocsList;
        private DevExpress.XtraGrid.GridControl xgrdLines;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLines;
        private System.Windows.Forms.GroupBox gbFilters;
        private DevExpress.XtraEditors.SimpleButton sbFilter;
        private DevExpress.XtraEditors.LabelControl lblAnd;
        private DevExpress.XtraEditors.LabelControl lblDocDate;
        private DevExpress.XtraEditors.DateEdit dateEditPODateEnd;
        private DevExpress.XtraEditors.DateEdit dateEditPODateIni;
        private DevExpress.XtraEditors.SearchLookUpEdit slueStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.TextEdit txtDocNumber;
        private DevExpress.XtraEditors.LabelControl lblDocNumber;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraEditors.SearchLookUpEdit slueDocType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl lblDocType;
        private DevExpress.XtraEditors.SearchLookUpEdit slueCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
    }
}