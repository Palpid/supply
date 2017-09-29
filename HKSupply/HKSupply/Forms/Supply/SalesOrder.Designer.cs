namespace HKSupply.Forms.Supply
{
    partial class SalesOrder
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
            this.xtpSO = new DevExpress.XtraTab.XtraTabPage();
            this.xtcPO = new DevExpress.XtraTab.XtraTabControl();
            this.xtpGoods = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdLines = new DevExpress.XtraGrid.GridControl();
            this.gridViewLines = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtpTerms = new DevExpress.XtraTab.XtraTabPage();
            this.lblTxtContact = new DevExpress.XtraEditors.LabelControl();
            this.lblTxtAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblTxtCompany = new DevExpress.XtraEditors.LabelControl();
            this.lblContact = new DevExpress.XtraEditors.LabelControl();
            this.lblAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblCompany = new DevExpress.XtraEditors.LabelControl();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.lblSONumber = new DevExpress.XtraEditors.LabelControl();
            this.txtSONumber = new DevExpress.XtraEditors.TextEdit();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.slueCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.lblWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblSODateWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblPODateWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditSODate = new DevExpress.XtraEditors.DateEdit();
            this.dateEditPODate = new DevExpress.XtraEditors.DateEdit();
            this.lblSOCreationDate = new DevExpress.XtraEditors.LabelControl();
            this.lblPODate = new DevExpress.XtraEditors.LabelControl();
            this.lblPONumber = new DevExpress.XtraEditors.LabelControl();
            this.txtPONumber = new DevExpress.XtraEditors.TextEdit();
            this.memoEditRemarks = new DevExpress.XtraEditors.MemoEdit();
            this.lblRemarks = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpSO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcPO)).BeginInit();
            this.xtcPO.SuspendLayout();
            this.xtpGoods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).BeginInit();
            this.xtpTerms.SuspendLayout();
            this.gbHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSONumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSODate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSODate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemarks.Properties)).BeginInit();
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
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 997);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ribbonStatusBar.Size = new System.Drawing.Size(2007, 48);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 122);
            this.xtcGeneral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpSO;
            this.xtcGeneral.Size = new System.Drawing.Size(2007, 875);
            this.xtcGeneral.TabIndex = 4;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpSO});
            // 
            // xtpSO
            // 
            this.xtpSO.Controls.Add(this.xtcPO);
            this.xtpSO.Controls.Add(this.gbHeader);
            this.xtpSO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtpSO.Name = "xtpSO";
            this.xtpSO.Size = new System.Drawing.Size(1997, 833);
            this.xtpSO.Text = "SALES ORDER";
            // 
            // xtcPO
            // 
            this.xtcPO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtcPO.Location = new System.Drawing.Point(16, 150);
            this.xtcPO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtcPO.Name = "xtcPO";
            this.xtcPO.SelectedTabPage = this.xtpGoods;
            this.xtcPO.Size = new System.Drawing.Size(1971, 679);
            this.xtcPO.TabIndex = 3;
            this.xtcPO.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpGoods,
            this.xtpTerms});
            // 
            // xtpGoods
            // 
            this.xtpGoods.Controls.Add(this.xgrdLines);
            this.xtpGoods.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtpGoods.Name = "xtpGoods";
            this.xtpGoods.Size = new System.Drawing.Size(1961, 637);
            this.xtpGoods.Text = "GOODS";
            // 
            // xgrdLines
            // 
            this.xgrdLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdLines.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xgrdLines.Location = new System.Drawing.Point(0, 0);
            this.xgrdLines.MainView = this.gridViewLines;
            this.xgrdLines.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xgrdLines.MenuManager = this.ribbonControl;
            this.xgrdLines.Name = "xgrdLines";
            this.xgrdLines.Size = new System.Drawing.Size(1961, 637);
            this.xgrdLines.TabIndex = 0;
            this.xgrdLines.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLines});
            // 
            // gridViewLines
            // 
            this.gridViewLines.GridControl = this.xgrdLines;
            this.gridViewLines.Name = "gridViewLines";
            // 
            // xtpTerms
            // 
            this.xtpTerms.Controls.Add(this.lblTxtContact);
            this.xtpTerms.Controls.Add(this.lblTxtAddress);
            this.xtpTerms.Controls.Add(this.lblTxtCompany);
            this.xtpTerms.Controls.Add(this.lblContact);
            this.xtpTerms.Controls.Add(this.lblAddress);
            this.xtpTerms.Controls.Add(this.lblCompany);
            this.xtpTerms.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.xtpTerms.Name = "xtpTerms";
            this.xtpTerms.Size = new System.Drawing.Size(1961, 624);
            this.xtpTerms.Text = "TERMS";
            // 
            // lblTxtContact
            // 
            this.lblTxtContact.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtContact.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtContact.Location = new System.Drawing.Point(338, 142);
            this.lblTxtContact.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblTxtContact.Name = "lblTxtContact";
            this.lblTxtContact.Size = new System.Drawing.Size(1040, 29);
            this.lblTxtContact.TabIndex = 11;
            this.lblTxtContact.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtAddress
            // 
            this.lblTxtAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtAddress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtAddress.Location = new System.Drawing.Point(338, 114);
            this.lblTxtAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblTxtAddress.Name = "lblTxtAddress";
            this.lblTxtAddress.Size = new System.Drawing.Size(1040, 29);
            this.lblTxtAddress.TabIndex = 10;
            this.lblTxtAddress.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtCompany
            // 
            this.lblTxtCompany.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtCompany.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtCompany.Location = new System.Drawing.Point(338, 86);
            this.lblTxtCompany.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblTxtCompany.Name = "lblTxtCompany";
            this.lblTxtCompany.Size = new System.Drawing.Size(1040, 29);
            this.lblTxtCompany.TabIndex = 9;
            this.lblTxtCompany.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblContact
            // 
            this.lblContact.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblContact.Location = new System.Drawing.Point(183, 148);
            this.lblContact.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(146, 19);
            this.lblContact.TabIndex = 5;
            this.lblContact.Text = "Contact:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAddress.Location = new System.Drawing.Point(183, 120);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(146, 19);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Address:";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCompany.Location = new System.Drawing.Point(183, 92);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(146, 19);
            this.lblCompany.TabIndex = 3;
            this.lblCompany.Text = "Company:";
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.memoEditRemarks);
            this.gbHeader.Controls.Add(this.lblRemarks);
            this.gbHeader.Controls.Add(this.lblSONumber);
            this.gbHeader.Controls.Add(this.txtSONumber);
            this.gbHeader.Controls.Add(this.sbSearch);
            this.gbHeader.Controls.Add(this.slueCustomer);
            this.gbHeader.Controls.Add(this.lblCustomer);
            this.gbHeader.Controls.Add(this.lblWeek);
            this.gbHeader.Controls.Add(this.lblSODateWeek);
            this.gbHeader.Controls.Add(this.lblPODateWeek);
            this.gbHeader.Controls.Add(this.lblDate);
            this.gbHeader.Controls.Add(this.dateEditSODate);
            this.gbHeader.Controls.Add(this.dateEditPODate);
            this.gbHeader.Controls.Add(this.lblSOCreationDate);
            this.gbHeader.Controls.Add(this.lblPODate);
            this.gbHeader.Controls.Add(this.lblPONumber);
            this.gbHeader.Controls.Add(this.txtPONumber);
            this.gbHeader.Location = new System.Drawing.Point(16, 4);
            this.gbHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbHeader.Size = new System.Drawing.Size(1971, 138);
            this.gbHeader.TabIndex = 2;
            this.gbHeader.TabStop = false;
            // 
            // lblSONumber
            // 
            this.lblSONumber.Location = new System.Drawing.Point(96, 86);
            this.lblSONumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblSONumber.Name = "lblSONumber";
            this.lblSONumber.Size = new System.Drawing.Size(78, 19);
            this.lblSONumber.TabIndex = 19;
            this.lblSONumber.Text = "SONumber";
            // 
            // txtSONumber
            // 
            this.txtSONumber.Location = new System.Drawing.Point(186, 77);
            this.txtSONumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSONumber.MenuManager = this.ribbonControl;
            this.txtSONumber.Name = "txtSONumber";
            this.txtSONumber.Size = new System.Drawing.Size(180, 26);
            this.txtSONumber.TabIndex = 20;
            // 
            // sbSearch
            // 
            this.sbSearch.ImageOptions.ImageUri.Uri = "Find;Size32x32";
            this.sbSearch.Location = new System.Drawing.Point(1479, 20);
            this.sbSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(64, 51);
            this.sbSearch.TabIndex = 16;
            // 
            // slueCustomer
            // 
            this.slueCustomer.Location = new System.Drawing.Point(1100, 48);
            this.slueCustomer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.slueCustomer.MenuManager = this.ribbonControl;
            this.slueCustomer.Name = "slueCustomer";
            this.slueCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueCustomer.Properties.View = this.searchLookUpEdit1View;
            this.slueCustomer.Size = new System.Drawing.Size(328, 26);
            this.slueCustomer.TabIndex = 13;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCustomer.Location = new System.Drawing.Point(964, 53);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(126, 19);
            this.lblCustomer.TabIndex = 10;
            this.lblCustomer.Text = "CUSTOMER";
            // 
            // lblWeek
            // 
            this.lblWeek.Location = new System.Drawing.Point(840, 20);
            this.lblWeek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 19);
            this.lblWeek.TabIndex = 9;
            this.lblWeek.Text = "Week";
            // 
            // lblSODateWeek
            // 
            this.lblSODateWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSODateWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblSODateWeek.Location = new System.Drawing.Point(816, 77);
            this.lblSODateWeek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblSODateWeek.Name = "lblSODateWeek";
            this.lblSODateWeek.Size = new System.Drawing.Size(98, 28);
            this.lblSODateWeek.TabIndex = 8;
            this.lblSODateWeek.Text = "XX";
            // 
            // lblPODateWeek
            // 
            this.lblPODateWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPODateWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblPODateWeek.Location = new System.Drawing.Point(816, 48);
            this.lblPODateWeek.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblPODateWeek.Name = "lblPODateWeek";
            this.lblPODateWeek.Size = new System.Drawing.Size(98, 29);
            this.lblPODateWeek.TabIndex = 7;
            this.lblPODateWeek.Text = "XX";
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(708, 20);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(32, 19);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date";
            // 
            // dateEditSODate
            // 
            this.dateEditSODate.EditValue = null;
            this.dateEditSODate.Location = new System.Drawing.Point(648, 76);
            this.dateEditSODate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateEditSODate.MenuManager = this.ribbonControl;
            this.dateEditSODate.Name = "dateEditSODate";
            this.dateEditSODate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditSODate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditSODate.Size = new System.Drawing.Size(159, 26);
            this.dateEditSODate.TabIndex = 5;
            // 
            // dateEditPODate
            // 
            this.dateEditPODate.EditValue = null;
            this.dateEditPODate.Location = new System.Drawing.Point(648, 48);
            this.dateEditPODate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateEditPODate.MenuManager = this.ribbonControl;
            this.dateEditPODate.Name = "dateEditPODate";
            this.dateEditPODate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODate.Size = new System.Drawing.Size(159, 26);
            this.dateEditPODate.TabIndex = 4;
            // 
            // lblSOCreationDate
            // 
            this.lblSOCreationDate.Location = new System.Drawing.Point(536, 77);
            this.lblSOCreationDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblSOCreationDate.Name = "lblSOCreationDate";
            this.lblSOCreationDate.Size = new System.Drawing.Size(105, 19);
            this.lblSOCreationDate.TabIndex = 3;
            this.lblSOCreationDate.Text = "SO CREATION";
            // 
            // lblPODate
            // 
            this.lblPODate.Location = new System.Drawing.Point(536, 50);
            this.lblPODate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblPODate.Name = "lblPODate";
            this.lblPODate.Size = new System.Drawing.Size(67, 19);
            this.lblPODate.TabIndex = 2;
            this.lblPODate.Text = "PO DATE";
            // 
            // lblPONumber
            // 
            this.lblPONumber.Location = new System.Drawing.Point(96, 53);
            this.lblPONumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Size = new System.Drawing.Size(83, 19);
            this.lblPONumber.TabIndex = 0;
            this.lblPONumber.Text = "PO Number";
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(186, 48);
            this.txtPONumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPONumber.MenuManager = this.ribbonControl;
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.Size = new System.Drawing.Size(180, 26);
            this.txtPONumber.TabIndex = 1;
            // 
            // memoEditRemarks
            // 
            this.memoEditRemarks.Location = new System.Drawing.Point(1100, 76);
            this.memoEditRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.memoEditRemarks.MenuManager = this.ribbonControl;
            this.memoEditRemarks.Name = "memoEditRemarks";
            this.memoEditRemarks.Size = new System.Drawing.Size(584, 47);
            this.memoEditRemarks.TabIndex = 34;
            // 
            // lblRemarks
            // 
            this.lblRemarks.Location = new System.Drawing.Point(964, 80);
            this.lblRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(61, 19);
            this.lblRemarks.TabIndex = 33;
            this.lblRemarks.Text = "Remarks";
            // 
            // SalesOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2007, 1045);
            this.Controls.Add(this.xtcGeneral);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SalesOrder";
            this.Text = "Sales Order";
            this.Load += new System.EventHandler(this.SalesOrder_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpSO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtcPO)).EndInit();
            this.xtcPO.ResumeLayout(false);
            this.xtpGoods.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).EndInit();
            this.xtpTerms.ResumeLayout(false);
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSONumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSODate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSODate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemarks.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpSO;
        private DevExpress.XtraTab.XtraTabControl xtcPO;
        private DevExpress.XtraTab.XtraTabPage xtpGoods;
        private DevExpress.XtraGrid.GridControl xgrdLines;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLines;
        private DevExpress.XtraTab.XtraTabPage xtpTerms;
        private DevExpress.XtraEditors.LabelControl lblTxtContact;
        private DevExpress.XtraEditors.LabelControl lblTxtAddress;
        private DevExpress.XtraEditors.LabelControl lblTxtCompany;
        private DevExpress.XtraEditors.LabelControl lblContact;
        private DevExpress.XtraEditors.LabelControl lblAddress;
        private DevExpress.XtraEditors.LabelControl lblCompany;
        private System.Windows.Forms.GroupBox gbHeader;
        private DevExpress.XtraEditors.LabelControl lblSONumber;
        private DevExpress.XtraEditors.TextEdit txtSONumber;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraEditors.SearchLookUpEdit slueCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.LabelControl lblWeek;
        private DevExpress.XtraEditors.LabelControl lblSODateWeek;
        private DevExpress.XtraEditors.LabelControl lblPODateWeek;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.DateEdit dateEditSODate;
        private DevExpress.XtraEditors.DateEdit dateEditPODate;
        private DevExpress.XtraEditors.LabelControl lblSOCreationDate;
        private DevExpress.XtraEditors.LabelControl lblPODate;
        private DevExpress.XtraEditors.LabelControl lblPONumber;
        private DevExpress.XtraEditors.TextEdit txtPONumber;
        private DevExpress.XtraEditors.MemoEdit memoEditRemarks;
        private DevExpress.XtraEditors.LabelControl lblRemarks;
    }
}