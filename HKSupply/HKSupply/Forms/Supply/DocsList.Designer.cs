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
            this.xtcGeneral = new DevExpress.XtraTab.XtraTabControl();
            this.xtpDocsList = new DevExpress.XtraTab.XtraTabPage();
            this.pcFilter = new DevExpress.XtraEditors.PanelControl();
            this.sbFilter = new DevExpress.XtraEditors.SimpleButton();
            this.slueCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblAnd = new DevExpress.XtraEditors.LabelControl();
            this.lblDocType = new DevExpress.XtraEditors.LabelControl();
            this.lblDocDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditPODateEnd = new DevExpress.XtraEditors.DateEdit();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.dateEditPODateIni = new DevExpress.XtraEditors.DateEdit();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.slueDocType = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.slueStatus = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblDocNumber = new DevExpress.XtraEditors.LabelControl();
            this.txtDocNumber = new DevExpress.XtraEditors.TextEdit();
            this.xgrdLines = new DevExpress.XtraGrid.GridControl();
            this.gridViewLines = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpDocsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).BeginInit();
            this.pcFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueDocType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).BeginInit();
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
            this.xtcGeneral.SelectedTabPage = this.xtpDocsList;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 559);
            this.xtcGeneral.TabIndex = 4;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpDocsList});
            // 
            // xtpDocsList
            // 
            this.xtpDocsList.Controls.Add(this.pcFilter);
            this.xtpDocsList.Controls.Add(this.xgrdLines);
            this.xtpDocsList.Name = "xtpDocsList";
            this.xtpDocsList.Size = new System.Drawing.Size(1332, 531);
            this.xtpDocsList.Text = "DOCS LIST";
            // 
            // pcFilter
            // 
            this.pcFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcFilter.Controls.Add(this.sbFilter);
            this.pcFilter.Controls.Add(this.slueCustomer);
            this.pcFilter.Controls.Add(this.lblAnd);
            this.pcFilter.Controls.Add(this.lblDocType);
            this.pcFilter.Controls.Add(this.lblDocDate);
            this.pcFilter.Controls.Add(this.dateEditPODateEnd);
            this.pcFilter.Controls.Add(this.lblCustomer);
            this.pcFilter.Controls.Add(this.dateEditPODateIni);
            this.pcFilter.Controls.Add(this.lblSupplier);
            this.pcFilter.Controls.Add(this.slueDocType);
            this.pcFilter.Controls.Add(this.slueSupplier);
            this.pcFilter.Controls.Add(this.lblStatus);
            this.pcFilter.Controls.Add(this.slueStatus);
            this.pcFilter.Controls.Add(this.lblDocNumber);
            this.pcFilter.Controls.Add(this.txtDocNumber);
            this.pcFilter.Location = new System.Drawing.Point(11, 10);
            this.pcFilter.Name = "pcFilter";
            this.pcFilter.Size = new System.Drawing.Size(1314, 94);
            this.pcFilter.TabIndex = 3;
            // 
            // sbFilter
            // 
            this.sbFilter.Location = new System.Drawing.Point(868, 44);
            this.sbFilter.Name = "sbFilter";
            this.sbFilter.Size = new System.Drawing.Size(82, 38);
            this.sbFilter.TabIndex = 24;
            this.sbFilter.Text = "Search";
            // 
            // slueCustomer
            // 
            this.slueCustomer.Location = new System.Drawing.Point(386, 40);
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
            // lblAnd
            // 
            this.lblAnd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAnd.Location = new System.Drawing.Point(799, 13);
            this.lblAnd.Name = "lblAnd";
            this.lblAnd.Size = new System.Drawing.Size(39, 13);
            this.lblAnd.TabIndex = 23;
            this.lblAnd.Text = "and";
            // 
            // lblDocType
            // 
            this.lblDocType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocType.Location = new System.Drawing.Point(5, 13);
            this.lblDocType.Name = "lblDocType";
            this.lblDocType.Size = new System.Drawing.Size(84, 13);
            this.lblDocType.TabIndex = 25;
            this.lblDocType.Text = "Type";
            // 
            // lblDocDate
            // 
            this.lblDocDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocDate.Location = new System.Drawing.Point(553, 13);
            this.lblDocDate.Name = "lblDocDate";
            this.lblDocDate.Size = new System.Drawing.Size(115, 13);
            this.lblDocDate.TabIndex = 22;
            this.lblDocDate.Text = "Doc. Date        between";
            // 
            // dateEditPODateEnd
            // 
            this.dateEditPODateEnd.EditValue = null;
            this.dateEditPODateEnd.Location = new System.Drawing.Point(844, 10);
            this.dateEditPODateEnd.MenuManager = this.ribbonControl;
            this.dateEditPODateEnd.Name = "dateEditPODateEnd";
            this.dateEditPODateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateEnd.Size = new System.Drawing.Size(106, 20);
            this.dateEditPODateEnd.TabIndex = 21;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCustomer.Location = new System.Drawing.Point(279, 43);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(84, 13);
            this.lblCustomer.TabIndex = 27;
            this.lblCustomer.Text = "Customer";
            // 
            // dateEditPODateIni
            // 
            this.dateEditPODateIni.EditValue = null;
            this.dateEditPODateIni.Location = new System.Drawing.Point(674, 10);
            this.dateEditPODateIni.MenuManager = this.ribbonControl;
            this.dateEditPODateIni.Name = "dateEditPODateIni";
            this.dateEditPODateIni.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateIni.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODateIni.Size = new System.Drawing.Size(106, 20);
            this.dateEditPODateIni.TabIndex = 20;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSupplier.Location = new System.Drawing.Point(5, 43);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(84, 13);
            this.lblSupplier.TabIndex = 14;
            this.lblSupplier.Text = "Supplier";
            // 
            // slueDocType
            // 
            this.slueDocType.Location = new System.Drawing.Point(112, 10);
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
            // slueSupplier
            // 
            this.slueSupplier.Location = new System.Drawing.Point(112, 40);
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
            // lblStatus
            // 
            this.lblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatus.Location = new System.Drawing.Point(5, 69);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(84, 13);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status";
            // 
            // slueStatus
            // 
            this.slueStatus.Location = new System.Drawing.Point(112, 66);
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
            // lblDocNumber
            // 
            this.lblDocNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocNumber.Location = new System.Drawing.Point(279, 13);
            this.lblDocNumber.Name = "lblDocNumber";
            this.lblDocNumber.Size = new System.Drawing.Size(84, 13);
            this.lblDocNumber.TabIndex = 16;
            this.lblDocNumber.Text = "Doc. Number";
            // 
            // txtDocNumber
            // 
            this.txtDocNumber.Location = new System.Drawing.Point(386, 10);
            this.txtDocNumber.MenuManager = this.ribbonControl;
            this.txtDocNumber.Name = "txtDocNumber";
            this.txtDocNumber.Size = new System.Drawing.Size(142, 20);
            this.txtDocNumber.TabIndex = 17;
            // 
            // xgrdLines
            // 
            this.xgrdLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdLines.Location = new System.Drawing.Point(11, 110);
            this.xgrdLines.MainView = this.gridViewLines;
            this.xgrdLines.MenuManager = this.ribbonControl;
            this.xgrdLines.Name = "xgrdLines";
            this.xgrdLines.Size = new System.Drawing.Size(1314, 418);
            this.xgrdLines.TabIndex = 2;
            this.xgrdLines.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLines});
            // 
            // gridViewLines
            // 
            this.gridViewLines.GridControl = this.xgrdLines;
            this.gridViewLines.Name = "gridViewLines";
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
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpDocsList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).EndInit();
            this.pcFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODateIni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueDocType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpDocsList;
        private DevExpress.XtraGrid.GridControl xgrdLines;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLines;
        private DevExpress.XtraEditors.SearchLookUpEdit slueCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.SearchLookUpEdit slueDocType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl lblDocType;
        private DevExpress.XtraEditors.SearchLookUpEdit slueStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.TextEdit txtDocNumber;
        private DevExpress.XtraEditors.LabelControl lblDocNumber;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraEditors.PanelControl pcFilter;
        private DevExpress.XtraEditors.SimpleButton sbFilter;
        private DevExpress.XtraEditors.LabelControl lblAnd;
        private DevExpress.XtraEditors.LabelControl lblDocDate;
        private DevExpress.XtraEditors.DateEdit dateEditPODateEnd;
        private DevExpress.XtraEditors.DateEdit dateEditPODateIni;
    }
}