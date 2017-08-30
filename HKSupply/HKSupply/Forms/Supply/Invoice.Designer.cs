namespace HKSupply.Forms.Supply
{
    partial class Invoice
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
            this.xtpINV = new DevExpress.XtraTab.XtraTabPage();
            this.xtcPO = new DevExpress.XtraTab.XtraTabControl();
            this.xtpGoods = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdLines = new DevExpress.XtraGrid.GridControl();
            this.gridViewLines = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtpTerms = new DevExpress.XtraTab.XtraTabPage();
            this.sluePaymentTerm = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblTxtInvoiceTo = new DevExpress.XtraEditors.LabelControl();
            this.lblTxtShipTo = new DevExpress.XtraEditors.LabelControl();
            this.lblTxtContact = new DevExpress.XtraEditors.LabelControl();
            this.lblTxtAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblTxtCompany = new DevExpress.XtraEditors.LabelControl();
            this.lblInvoiceTo = new DevExpress.XtraEditors.LabelControl();
            this.lblTermPayment = new DevExpress.XtraEditors.LabelControl();
            this.lblShipTo = new DevExpress.XtraEditors.LabelControl();
            this.lblContact = new DevExpress.XtraEditors.LabelControl();
            this.lblAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblCompany = new DevExpress.XtraEditors.LabelControl();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.slueCurrency = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.slueDeliveryTerms = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblCurrency = new DevExpress.XtraEditors.LabelControl();
            this.lblTermsOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.slueCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.lblINVNumber = new DevExpress.XtraEditors.LabelControl();
            this.txtINVNumber = new DevExpress.XtraEditors.TextEdit();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.lblWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblINVDateWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditINVDate = new DevExpress.XtraEditors.DateEdit();
            this.lblINVDate = new DevExpress.XtraEditors.LabelControl();
            this.lblDNNumber = new DevExpress.XtraEditors.LabelControl();
            this.txtDNNumber = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpINV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcPO)).BeginInit();
            this.xtcPO.SuspendLayout();
            this.xtpGoods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).BeginInit();
            this.xtpTerms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sluePaymentTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.gbHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueDeliveryTerms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtINVNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditINVDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditINVDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDNNumber.Properties)).BeginInit();
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
            this.xtcGeneral.SelectedTabPage = this.xtpINV;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 605);
            this.xtcGeneral.TabIndex = 6;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpINV});
            // 
            // xtpINV
            // 
            this.xtpINV.Controls.Add(this.xtcPO);
            this.xtpINV.Controls.Add(this.gbHeader);
            this.xtpINV.Name = "xtpINV";
            this.xtpINV.Size = new System.Drawing.Size(1332, 577);
            this.xtpINV.Text = "INVOICE";
            // 
            // xtcPO
            // 
            this.xtcPO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtcPO.Location = new System.Drawing.Point(11, 118);
            this.xtcPO.Name = "xtcPO";
            this.xtcPO.SelectedTabPage = this.xtpGoods;
            this.xtcPO.Size = new System.Drawing.Size(1314, 456);
            this.xtcPO.TabIndex = 3;
            this.xtcPO.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpGoods,
            this.xtpTerms});
            // 
            // xtpGoods
            // 
            this.xtpGoods.Controls.Add(this.xgrdLines);
            this.xtpGoods.Name = "xtpGoods";
            this.xtpGoods.Size = new System.Drawing.Size(1308, 428);
            this.xtpGoods.Text = "GOODS";
            // 
            // xgrdLines
            // 
            this.xgrdLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdLines.Location = new System.Drawing.Point(0, 0);
            this.xgrdLines.MainView = this.gridViewLines;
            this.xgrdLines.MenuManager = this.ribbonControl;
            this.xgrdLines.Name = "xgrdLines";
            this.xgrdLines.Size = new System.Drawing.Size(1308, 428);
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
            this.xtpTerms.Controls.Add(this.sluePaymentTerm);
            this.xtpTerms.Controls.Add(this.lblTxtInvoiceTo);
            this.xtpTerms.Controls.Add(this.lblTxtShipTo);
            this.xtpTerms.Controls.Add(this.lblTxtContact);
            this.xtpTerms.Controls.Add(this.lblTxtAddress);
            this.xtpTerms.Controls.Add(this.lblTxtCompany);
            this.xtpTerms.Controls.Add(this.lblInvoiceTo);
            this.xtpTerms.Controls.Add(this.lblTermPayment);
            this.xtpTerms.Controls.Add(this.lblShipTo);
            this.xtpTerms.Controls.Add(this.lblContact);
            this.xtpTerms.Controls.Add(this.lblAddress);
            this.xtpTerms.Controls.Add(this.lblCompany);
            this.xtpTerms.Name = "xtpTerms";
            this.xtpTerms.Size = new System.Drawing.Size(1308, 428);
            this.xtpTerms.Text = "TERMS";
            // 
            // sluePaymentTerm
            // 
            this.sluePaymentTerm.Location = new System.Drawing.Point(405, 234);
            this.sluePaymentTerm.MenuManager = this.ribbonControl;
            this.sluePaymentTerm.Name = "sluePaymentTerm";
            this.sluePaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sluePaymentTerm.Properties.View = this.gridView3;
            this.sluePaymentTerm.Size = new System.Drawing.Size(389, 20);
            this.sluePaymentTerm.TabIndex = 39;
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // lblTxtInvoiceTo
            // 
            this.lblTxtInvoiceTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtInvoiceTo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtInvoiceTo.Location = new System.Drawing.Point(405, 193);
            this.lblTxtInvoiceTo.Name = "lblTxtInvoiceTo";
            this.lblTxtInvoiceTo.Size = new System.Drawing.Size(693, 20);
            this.lblTxtInvoiceTo.TabIndex = 38;
            this.lblTxtInvoiceTo.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtShipTo
            // 
            this.lblTxtShipTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtShipTo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtShipTo.Location = new System.Drawing.Point(405, 174);
            this.lblTxtShipTo.Name = "lblTxtShipTo";
            this.lblTxtShipTo.Size = new System.Drawing.Size(693, 20);
            this.lblTxtShipTo.TabIndex = 37;
            this.lblTxtShipTo.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtContact
            // 
            this.lblTxtContact.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtContact.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtContact.Location = new System.Drawing.Point(405, 130);
            this.lblTxtContact.Name = "lblTxtContact";
            this.lblTxtContact.Size = new System.Drawing.Size(693, 20);
            this.lblTxtContact.TabIndex = 36;
            this.lblTxtContact.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtAddress
            // 
            this.lblTxtAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtAddress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtAddress.Location = new System.Drawing.Point(405, 111);
            this.lblTxtAddress.Name = "lblTxtAddress";
            this.lblTxtAddress.Size = new System.Drawing.Size(693, 20);
            this.lblTxtAddress.TabIndex = 35;
            this.lblTxtAddress.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtCompany
            // 
            this.lblTxtCompany.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtCompany.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtCompany.Location = new System.Drawing.Point(405, 92);
            this.lblTxtCompany.Name = "lblTxtCompany";
            this.lblTxtCompany.Size = new System.Drawing.Size(693, 20);
            this.lblTxtCompany.TabIndex = 34;
            this.lblTxtCompany.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblInvoiceTo
            // 
            this.lblInvoiceTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInvoiceTo.Location = new System.Drawing.Point(302, 197);
            this.lblInvoiceTo.Name = "lblInvoiceTo";
            this.lblInvoiceTo.Size = new System.Drawing.Size(97, 13);
            this.lblInvoiceTo.TabIndex = 33;
            this.lblInvoiceTo.Text = "Invoice To:";
            // 
            // lblTermPayment
            // 
            this.lblTermPayment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTermPayment.Location = new System.Drawing.Point(302, 237);
            this.lblTermPayment.Name = "lblTermPayment";
            this.lblTermPayment.Size = new System.Drawing.Size(97, 13);
            this.lblTermPayment.TabIndex = 32;
            this.lblTermPayment.Text = "Term of Payment:";
            // 
            // lblShipTo
            // 
            this.lblShipTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblShipTo.Location = new System.Drawing.Point(302, 178);
            this.lblShipTo.Name = "lblShipTo";
            this.lblShipTo.Size = new System.Drawing.Size(97, 13);
            this.lblShipTo.TabIndex = 31;
            this.lblShipTo.Text = "Ship To:";
            // 
            // lblContact
            // 
            this.lblContact.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblContact.Location = new System.Drawing.Point(302, 134);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(97, 13);
            this.lblContact.TabIndex = 30;
            this.lblContact.Text = "Contact:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAddress.Location = new System.Drawing.Point(302, 115);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(97, 13);
            this.lblAddress.TabIndex = 29;
            this.lblAddress.Text = "Address:";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCompany.Location = new System.Drawing.Point(302, 96);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(97, 13);
            this.lblCompany.TabIndex = 28;
            this.lblCompany.Text = "Company:";
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.slueCurrency);
            this.gbHeader.Controls.Add(this.slueDeliveryTerms);
            this.gbHeader.Controls.Add(this.lblCurrency);
            this.gbHeader.Controls.Add(this.lblTermsOfDelivery);
            this.gbHeader.Controls.Add(this.slueCustomer);
            this.gbHeader.Controls.Add(this.lblCustomer);
            this.gbHeader.Controls.Add(this.lblINVNumber);
            this.gbHeader.Controls.Add(this.txtINVNumber);
            this.gbHeader.Controls.Add(this.sbSearch);
            this.gbHeader.Controls.Add(this.lblWeek);
            this.gbHeader.Controls.Add(this.lblINVDateWeek);
            this.gbHeader.Controls.Add(this.lblDate);
            this.gbHeader.Controls.Add(this.dateEditINVDate);
            this.gbHeader.Controls.Add(this.lblINVDate);
            this.gbHeader.Controls.Add(this.lblDNNumber);
            this.gbHeader.Controls.Add(this.txtDNNumber);
            this.gbHeader.Location = new System.Drawing.Point(11, 3);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(1314, 99);
            this.gbHeader.TabIndex = 2;
            this.gbHeader.TabStop = false;
            // 
            // slueCurrency
            // 
            this.slueCurrency.Location = new System.Drawing.Point(772, 57);
            this.slueCurrency.MenuManager = this.ribbonControl;
            this.slueCurrency.Name = "slueCurrency";
            this.slueCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueCurrency.Properties.View = this.gridView2;
            this.slueCurrency.Size = new System.Drawing.Size(194, 20);
            this.slueCurrency.TabIndex = 26;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // slueDeliveryTerms
            // 
            this.slueDeliveryTerms.Location = new System.Drawing.Point(772, 38);
            this.slueDeliveryTerms.MenuManager = this.ribbonControl;
            this.slueDeliveryTerms.Name = "slueDeliveryTerms";
            this.slueDeliveryTerms.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueDeliveryTerms.Properties.View = this.gridView1;
            this.slueDeliveryTerms.Size = new System.Drawing.Size(389, 20);
            this.slueDeliveryTerms.TabIndex = 25;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCurrency.Location = new System.Drawing.Point(665, 58);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(84, 13);
            this.lblCurrency.TabIndex = 24;
            this.lblCurrency.Text = "Currency";
            // 
            // lblTermsOfDelivery
            // 
            this.lblTermsOfDelivery.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTermsOfDelivery.Location = new System.Drawing.Point(665, 39);
            this.lblTermsOfDelivery.Name = "lblTermsOfDelivery";
            this.lblTermsOfDelivery.Size = new System.Drawing.Size(84, 13);
            this.lblTermsOfDelivery.TabIndex = 23;
            this.lblTermsOfDelivery.Text = "Terms of Delivery";
            // 
            // slueCustomer
            // 
            this.slueCustomer.Location = new System.Drawing.Point(772, 17);
            this.slueCustomer.MenuManager = this.ribbonControl;
            this.slueCustomer.Name = "slueCustomer";
            this.slueCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueCustomer.Properties.View = this.searchLookUpEdit1View;
            this.slueCustomer.Size = new System.Drawing.Size(219, 20);
            this.slueCustomer.TabIndex = 22;
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
            this.lblCustomer.Location = new System.Drawing.Point(665, 20);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(84, 13);
            this.lblCustomer.TabIndex = 21;
            this.lblCustomer.Text = "CUSTOMER";
            // 
            // lblINVNumber
            // 
            this.lblINVNumber.Location = new System.Drawing.Point(43, 59);
            this.lblINVNumber.Name = "lblINVNumber";
            this.lblINVNumber.Size = new System.Drawing.Size(75, 13);
            this.lblINVNumber.TabIndex = 19;
            this.lblINVNumber.Text = "Invoice Number";
            // 
            // txtINVNumber
            // 
            this.txtINVNumber.Location = new System.Drawing.Point(124, 53);
            this.txtINVNumber.MenuManager = this.ribbonControl;
            this.txtINVNumber.Name = "txtINVNumber";
            this.txtINVNumber.Size = new System.Drawing.Size(120, 20);
            this.txtINVNumber.TabIndex = 20;
            // 
            // sbSearch
            // 
            this.sbSearch.ImageOptions.ImageUri.Uri = "Find;Size32x32";
            this.sbSearch.Location = new System.Drawing.Point(1213, 14);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(43, 35);
            this.sbSearch.TabIndex = 16;
            // 
            // lblWeek
            // 
            this.lblWeek.Location = new System.Drawing.Point(560, 14);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(27, 13);
            this.lblWeek.TabIndex = 9;
            this.lblWeek.Text = "Week";
            // 
            // lblINVDateWeek
            // 
            this.lblINVDateWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblINVDateWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblINVDateWeek.Location = new System.Drawing.Point(544, 33);
            this.lblINVDateWeek.Name = "lblINVDateWeek";
            this.lblINVDateWeek.Size = new System.Drawing.Size(65, 20);
            this.lblINVDateWeek.TabIndex = 7;
            this.lblINVDateWeek.Text = "XX";
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(472, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(23, 13);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date";
            // 
            // dateEditINVDate
            // 
            this.dateEditINVDate.EditValue = null;
            this.dateEditINVDate.Location = new System.Drawing.Point(432, 33);
            this.dateEditINVDate.MenuManager = this.ribbonControl;
            this.dateEditINVDate.Name = "dateEditINVDate";
            this.dateEditINVDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditINVDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditINVDate.Size = new System.Drawing.Size(106, 20);
            this.dateEditINVDate.TabIndex = 4;
            // 
            // lblINVDate
            // 
            this.lblINVDate.Location = new System.Drawing.Point(357, 34);
            this.lblINVDate.Name = "lblINVDate";
            this.lblINVDate.Size = new System.Drawing.Size(52, 13);
            this.lblINVDate.TabIndex = 2;
            this.lblINVDate.Text = "CREATION";
            // 
            // lblDNNumber
            // 
            this.lblDNNumber.Location = new System.Drawing.Point(64, 36);
            this.lblDNNumber.Name = "lblDNNumber";
            this.lblDNNumber.Size = new System.Drawing.Size(51, 13);
            this.lblDNNumber.TabIndex = 0;
            this.lblDNNumber.Text = "DNNumber";
            // 
            // txtDNNumber
            // 
            this.txtDNNumber.Location = new System.Drawing.Point(124, 33);
            this.txtDNNumber.MenuManager = this.ribbonControl;
            this.txtDNNumber.Name = "txtDNNumber";
            this.txtDNNumber.Size = new System.Drawing.Size(120, 20);
            this.txtDNNumber.TabIndex = 1;
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 715);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "Invoice";
            this.Text = "Invoice";
            this.Load += new System.EventHandler(this.Invoice_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpINV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtcPO)).EndInit();
            this.xtcPO.ResumeLayout(false);
            this.xtpGoods.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLines)).EndInit();
            this.xtpTerms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sluePaymentTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slueCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueDeliveryTerms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtINVNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditINVDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditINVDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDNNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpINV;
        private DevExpress.XtraTab.XtraTabControl xtcPO;
        private DevExpress.XtraTab.XtraTabPage xtpGoods;
        private DevExpress.XtraGrid.GridControl xgrdLines;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLines;
        private DevExpress.XtraTab.XtraTabPage xtpTerms;
        private DevExpress.XtraEditors.SearchLookUpEdit sluePaymentTerm;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl lblTxtInvoiceTo;
        private DevExpress.XtraEditors.LabelControl lblTxtShipTo;
        private DevExpress.XtraEditors.LabelControl lblTxtContact;
        private DevExpress.XtraEditors.LabelControl lblTxtAddress;
        private DevExpress.XtraEditors.LabelControl lblTxtCompany;
        private DevExpress.XtraEditors.LabelControl lblInvoiceTo;
        private DevExpress.XtraEditors.LabelControl lblTermPayment;
        private DevExpress.XtraEditors.LabelControl lblShipTo;
        private DevExpress.XtraEditors.LabelControl lblContact;
        private DevExpress.XtraEditors.LabelControl lblAddress;
        private DevExpress.XtraEditors.LabelControl lblCompany;
        private System.Windows.Forms.GroupBox gbHeader;
        private DevExpress.XtraEditors.SearchLookUpEdit slueCurrency;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SearchLookUpEdit slueDeliveryTerms;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lblCurrency;
        private DevExpress.XtraEditors.LabelControl lblTermsOfDelivery;
        private DevExpress.XtraEditors.SearchLookUpEdit slueCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.LabelControl lblINVNumber;
        private DevExpress.XtraEditors.TextEdit txtINVNumber;
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraEditors.LabelControl lblWeek;
        private DevExpress.XtraEditors.LabelControl lblINVDateWeek;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.DateEdit dateEditINVDate;
        private DevExpress.XtraEditors.LabelControl lblINVDate;
        private DevExpress.XtraEditors.LabelControl lblDNNumber;
        private DevExpress.XtraEditors.TextEdit txtDNNumber;
    }
}