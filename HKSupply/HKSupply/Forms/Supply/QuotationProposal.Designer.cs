namespace HKSupply.Forms.Supply
{
    partial class QuotationProposal
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
            this.xtpQP = new DevExpress.XtraTab.XtraTabPage();
            this.xtcPO = new DevExpress.XtraTab.XtraTabControl();
            this.xtpProposal = new DevExpress.XtraTab.XtraTabPage();
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
            this.memoEditRemarks = new DevExpress.XtraEditors.MemoEdit();
            this.lblRemarks = new DevExpress.XtraEditors.LabelControl();
            this.lblQPNumber = new DevExpress.XtraEditors.LabelControl();
            this.txtQPNumber = new DevExpress.XtraEditors.TextEdit();
            this.sbFinishQP = new DevExpress.XtraEditors.SimpleButton();
            this.sbOrder = new DevExpress.XtraEditors.SimpleButton();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.slueCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.lblWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblQPCreationDateWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblPODateWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditQPCreationDate = new DevExpress.XtraEditors.DateEdit();
            this.dateEditPODate = new DevExpress.XtraEditors.DateEdit();
            this.lblQPCreationDate = new DevExpress.XtraEditors.LabelControl();
            this.lblPODate = new DevExpress.XtraEditors.LabelControl();
            this.lblPONumber = new DevExpress.XtraEditors.LabelControl();
            this.txtPONumber = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpQP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcPO)).BeginInit();
            this.xtcPO.SuspendLayout();
            this.xtpProposal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).BeginInit();
            this.xtpTerms.SuspendLayout();
            this.gbHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQPNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQPCreationDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQPCreationDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Margin = new System.Windows.Forms.Padding(4);
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
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(4);
            this.ribbonStatusBar.Size = new System.Drawing.Size(1338, 31);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 125);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpQP;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 559);
            this.xtcGeneral.TabIndex = 3;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpQP});
            // 
            // xtpQP
            // 
            this.xtpQP.Controls.Add(this.xtcPO);
            this.xtpQP.Controls.Add(this.gbHeader);
            this.xtpQP.Name = "xtpQP";
            this.xtpQP.Size = new System.Drawing.Size(1332, 531);
            this.xtpQP.Text = "PURCHASE ORDER";
            // 
            // xtcPO
            // 
            this.xtcPO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtcPO.Location = new System.Drawing.Point(11, 107);
            this.xtcPO.Name = "xtcPO";
            this.xtcPO.SelectedTabPage = this.xtpProposal;
            this.xtcPO.Size = new System.Drawing.Size(1314, 420);
            this.xtcPO.TabIndex = 3;
            this.xtcPO.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpProposal,
            this.xtpTerms});
            // 
            // xtpProposal
            // 
            this.xtpProposal.Controls.Add(this.xgrdLines);
            this.xtpProposal.Name = "xtpProposal";
            this.xtpProposal.Size = new System.Drawing.Size(1308, 392);
            this.xtpProposal.Text = "PROPOSAL";
            // 
            // xgrdLines
            // 
            this.xgrdLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdLines.Location = new System.Drawing.Point(0, 0);
            this.xgrdLines.MainView = this.gridViewLines;
            this.xgrdLines.MenuManager = this.ribbonControl;
            this.xgrdLines.Name = "xgrdLines";
            this.xgrdLines.Size = new System.Drawing.Size(1308, 392);
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
            this.xtpTerms.Name = "xtpTerms";
            this.xtpTerms.Size = new System.Drawing.Size(1308, 392);
            this.xtpTerms.Text = "TERMS";
            // 
            // lblTxtContact
            // 
            this.lblTxtContact.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtContact.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtContact.Location = new System.Drawing.Point(225, 97);
            this.lblTxtContact.Name = "lblTxtContact";
            this.lblTxtContact.Size = new System.Drawing.Size(693, 20);
            this.lblTxtContact.TabIndex = 11;
            this.lblTxtContact.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtAddress
            // 
            this.lblTxtAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtAddress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtAddress.Location = new System.Drawing.Point(225, 78);
            this.lblTxtAddress.Name = "lblTxtAddress";
            this.lblTxtAddress.Size = new System.Drawing.Size(693, 20);
            this.lblTxtAddress.TabIndex = 10;
            this.lblTxtAddress.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtCompany
            // 
            this.lblTxtCompany.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtCompany.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtCompany.Location = new System.Drawing.Point(225, 59);
            this.lblTxtCompany.Name = "lblTxtCompany";
            this.lblTxtCompany.Size = new System.Drawing.Size(693, 20);
            this.lblTxtCompany.TabIndex = 9;
            this.lblTxtCompany.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblContact
            // 
            this.lblContact.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblContact.Location = new System.Drawing.Point(122, 101);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(97, 13);
            this.lblContact.TabIndex = 5;
            this.lblContact.Text = "Contact:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAddress.Location = new System.Drawing.Point(122, 82);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(97, 13);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Address:";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCompany.Location = new System.Drawing.Point(122, 63);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(97, 13);
            this.lblCompany.TabIndex = 3;
            this.lblCompany.Text = "Company:";
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.memoEditRemarks);
            this.gbHeader.Controls.Add(this.lblRemarks);
            this.gbHeader.Controls.Add(this.lblQPNumber);
            this.gbHeader.Controls.Add(this.txtQPNumber);
            this.gbHeader.Controls.Add(this.sbFinishQP);
            this.gbHeader.Controls.Add(this.sbOrder);
            this.gbHeader.Controls.Add(this.sbSearch);
            this.gbHeader.Controls.Add(this.slueCustomer);
            this.gbHeader.Controls.Add(this.lblCustomer);
            this.gbHeader.Controls.Add(this.lblWeek);
            this.gbHeader.Controls.Add(this.lblQPCreationDateWeek);
            this.gbHeader.Controls.Add(this.lblPODateWeek);
            this.gbHeader.Controls.Add(this.lblDate);
            this.gbHeader.Controls.Add(this.dateEditQPCreationDate);
            this.gbHeader.Controls.Add(this.dateEditPODate);
            this.gbHeader.Controls.Add(this.lblQPCreationDate);
            this.gbHeader.Controls.Add(this.lblPODate);
            this.gbHeader.Controls.Add(this.lblPONumber);
            this.gbHeader.Controls.Add(this.txtPONumber);
            this.gbHeader.Location = new System.Drawing.Point(11, 3);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(1314, 99);
            this.gbHeader.TabIndex = 2;
            this.gbHeader.TabStop = false;
            // 
            // memoEditRemarks
            // 
            this.memoEditRemarks.Location = new System.Drawing.Point(733, 52);
            this.memoEditRemarks.MenuManager = this.ribbonControl;
            this.memoEditRemarks.Name = "memoEditRemarks";
            this.memoEditRemarks.Size = new System.Drawing.Size(389, 32);
            this.memoEditRemarks.TabIndex = 34;
            // 
            // lblRemarks
            // 
            this.lblRemarks.Location = new System.Drawing.Point(643, 55);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(41, 13);
            this.lblRemarks.TabIndex = 33;
            this.lblRemarks.Text = "Remarks";
            // 
            // lblQPNumber
            // 
            this.lblQPNumber.Location = new System.Drawing.Point(145, 14);
            this.lblQPNumber.Name = "lblQPNumber";
            this.lblQPNumber.Size = new System.Drawing.Size(96, 13);
            this.lblQPNumber.TabIndex = 19;
            this.lblQPNumber.Text = "Q. Proposal Number";
            // 
            // txtQPNumber
            // 
            this.txtQPNumber.Location = new System.Drawing.Point(132, 33);
            this.txtQPNumber.MenuManager = this.ribbonControl;
            this.txtQPNumber.Name = "txtQPNumber";
            this.txtQPNumber.Size = new System.Drawing.Size(120, 20);
            this.txtQPNumber.TabIndex = 20;
            // 
            // sbFinishQP
            // 
            this.sbFinishQP.ImageOptions.ImageUri.Uri = "Apply";
            this.sbFinishQP.Location = new System.Drawing.Point(7, 59);
            this.sbFinishQP.Name = "sbFinishQP";
            this.sbFinishQP.Size = new System.Drawing.Size(119, 32);
            this.sbFinishQP.TabIndex = 18;
            this.sbFinishQP.Text = "Finish QP";
            // 
            // sbOrder
            // 
            this.sbOrder.Location = new System.Drawing.Point(1080, 14);
            this.sbOrder.Name = "sbOrder";
            this.sbOrder.Size = new System.Drawing.Size(43, 35);
            this.sbOrder.TabIndex = 17;
            this.sbOrder.Text = "Order";
            // 
            // sbSearch
            // 
            this.sbSearch.Location = new System.Drawing.Point(1025, 14);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(43, 35);
            this.sbSearch.TabIndex = 16;
            this.sbSearch.Text = "Search";
            // 
            // slueCustomer
            // 
            this.slueCustomer.Location = new System.Drawing.Point(733, 33);
            this.slueCustomer.MenuManager = this.ribbonControl;
            this.slueCustomer.Name = "slueCustomer";
            this.slueCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueCustomer.Properties.View = this.searchLookUpEdit1View;
            this.slueCustomer.Size = new System.Drawing.Size(219, 20);
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
            this.lblCustomer.Location = new System.Drawing.Point(643, 36);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(84, 13);
            this.lblCustomer.TabIndex = 10;
            this.lblCustomer.Text = "SUPPLIER";
            // 
            // lblWeek
            // 
            this.lblWeek.Location = new System.Drawing.Point(560, 14);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(27, 13);
            this.lblWeek.TabIndex = 9;
            this.lblWeek.Text = "Week";
            // 
            // lblQPCreationDateWeek
            // 
            this.lblQPCreationDateWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblQPCreationDateWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblQPCreationDateWeek.Location = new System.Drawing.Point(544, 53);
            this.lblQPCreationDateWeek.Name = "lblQPCreationDateWeek";
            this.lblQPCreationDateWeek.Size = new System.Drawing.Size(65, 19);
            this.lblQPCreationDateWeek.TabIndex = 8;
            this.lblQPCreationDateWeek.Text = "XX";
            // 
            // lblPODateWeek
            // 
            this.lblPODateWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblPODateWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblPODateWeek.Location = new System.Drawing.Point(544, 33);
            this.lblPODateWeek.Name = "lblPODateWeek";
            this.lblPODateWeek.Size = new System.Drawing.Size(65, 20);
            this.lblPODateWeek.TabIndex = 7;
            this.lblPODateWeek.Text = "XX";
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(472, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(23, 13);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date";
            // 
            // dateEditQPCreationDate
            // 
            this.dateEditQPCreationDate.EditValue = null;
            this.dateEditQPCreationDate.Location = new System.Drawing.Point(432, 52);
            this.dateEditQPCreationDate.MenuManager = this.ribbonControl;
            this.dateEditQPCreationDate.Name = "dateEditQPCreationDate";
            this.dateEditQPCreationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditQPCreationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditQPCreationDate.Size = new System.Drawing.Size(106, 20);
            this.dateEditQPCreationDate.TabIndex = 5;
            // 
            // dateEditPODate
            // 
            this.dateEditPODate.EditValue = null;
            this.dateEditPODate.Location = new System.Drawing.Point(432, 33);
            this.dateEditPODate.MenuManager = this.ribbonControl;
            this.dateEditPODate.Name = "dateEditPODate";
            this.dateEditPODate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditPODate.Size = new System.Drawing.Size(106, 20);
            this.dateEditPODate.TabIndex = 4;
            // 
            // lblQPCreationDate
            // 
            this.lblQPCreationDate.Location = new System.Drawing.Point(357, 53);
            this.lblQPCreationDate.Name = "lblQPCreationDate";
            this.lblQPCreationDate.Size = new System.Drawing.Size(69, 13);
            this.lblQPCreationDate.TabIndex = 3;
            this.lblQPCreationDate.Text = "QP CREATION";
            // 
            // lblPODate
            // 
            this.lblPODate.Location = new System.Drawing.Point(357, 34);
            this.lblPODate.Name = "lblPODate";
            this.lblPODate.Size = new System.Drawing.Size(43, 13);
            this.lblPODate.TabIndex = 2;
            this.lblPODate.Text = "PO DATE";
            // 
            // lblPONumber
            // 
            this.lblPONumber.Location = new System.Drawing.Point(39, 14);
            this.lblPONumber.Name = "lblPONumber";
            this.lblPONumber.Size = new System.Drawing.Size(54, 13);
            this.lblPONumber.TabIndex = 0;
            this.lblPONumber.Text = "PO Number";
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(6, 33);
            this.txtPONumber.MenuManager = this.ribbonControl;
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.Size = new System.Drawing.Size(120, 20);
            this.txtPONumber.TabIndex = 1;
            // 
            // QuotationProposal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 715);
            this.Controls.Add(this.xtcGeneral);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "QuotationProposal";
            this.Text = "Quotation Proposal";
            this.Load += new System.EventHandler(this.QuotationProposal_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpQP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtcPO)).EndInit();
            this.xtcPO.ResumeLayout(false);
            this.xtpProposal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).EndInit();
            this.xtpTerms.ResumeLayout(false);
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditRemarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQPNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQPCreationDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditQPCreationDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditPODate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpQP;
        private DevExpress.XtraTab.XtraTabControl xtcPO;
        private DevExpress.XtraTab.XtraTabPage xtpProposal;
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
        private DevExpress.XtraEditors.SimpleButton sbFinishQP;
        private DevExpress.XtraEditors.SimpleButton sbOrder;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraEditors.SearchLookUpEdit slueCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.LabelControl lblWeek;
        private DevExpress.XtraEditors.LabelControl lblQPCreationDateWeek;
        private DevExpress.XtraEditors.LabelControl lblPODateWeek;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.DateEdit dateEditQPCreationDate;
        private DevExpress.XtraEditors.DateEdit dateEditPODate;
        private DevExpress.XtraEditors.LabelControl lblQPCreationDate;
        private DevExpress.XtraEditors.LabelControl lblPODate;
        private DevExpress.XtraEditors.LabelControl lblPONumber;
        private DevExpress.XtraEditors.TextEdit txtPONumber;
        private DevExpress.XtraEditors.LabelControl lblQPNumber;
        private DevExpress.XtraEditors.TextEdit txtQPNumber;
        private DevExpress.XtraEditors.MemoEdit memoEditRemarks;
        private DevExpress.XtraEditors.LabelControl lblRemarks;
    }
}