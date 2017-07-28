namespace HKSupply.Forms.Supply
{
    partial class PurchaseOrder
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
            this.xtpPO = new DevExpress.XtraTab.XtraTabPage();
            this.xtcPO = new DevExpress.XtraTab.XtraTabControl();
            this.xtpOrderedGoods = new DevExpress.XtraTab.XtraTabPage();
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
            this.sbImportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.sbFinishPO = new DevExpress.XtraEditors.SimpleButton();
            this.sbOrder = new DevExpress.XtraEditors.SimpleButton();
            this.sbSearch = new DevExpress.XtraEditors.SimpleButton();
            this.slueCurrency = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.slueDeliveryTerms = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblCurrency = new DevExpress.XtraEditors.LabelControl();
            this.lblTermsOfDelivery = new DevExpress.XtraEditors.LabelControl();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.lblWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblDeliveryWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblDocDateWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.dateEditDelivery = new DevExpress.XtraEditors.DateEdit();
            this.dateEditDocDate = new DevExpress.XtraEditors.DateEdit();
            this.lblDelivery = new DevExpress.XtraEditors.LabelControl();
            this.lblDocDate = new DevExpress.XtraEditors.LabelControl();
            this.lblPONumber = new DevExpress.XtraEditors.LabelControl();
            this.txtPONumber = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpPO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcPO)).BeginInit();
            this.xtcPO.SuspendLayout();
            this.xtpOrderedGoods.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDelivery.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDocDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDocDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).BeginInit();
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
            this.xtcGeneral.SelectedTabPage = this.xtpPO;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 605);
            this.xtcGeneral.TabIndex = 2;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpPO});
            // 
            // xtpPO
            // 
            this.xtpPO.Controls.Add(this.xtcPO);
            this.xtpPO.Controls.Add(this.gbHeader);
            this.xtpPO.Name = "xtpPO";
            this.xtpPO.Size = new System.Drawing.Size(1332, 577);
            this.xtpPO.Text = "PURCHASE ORDER";
            // 
            // xtcPO
            // 
            this.xtcPO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtcPO.Location = new System.Drawing.Point(11, 118);
            this.xtcPO.Name = "xtcPO";
            this.xtcPO.SelectedTabPage = this.xtpOrderedGoods;
            this.xtcPO.Size = new System.Drawing.Size(1314, 456);
            this.xtcPO.TabIndex = 3;
            this.xtcPO.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpOrderedGoods,
            this.xtpTerms});
            // 
            // xtpOrderedGoods
            // 
            this.xtpOrderedGoods.Controls.Add(this.xgrdLines);
            this.xtpOrderedGoods.Name = "xtpOrderedGoods";
            this.xtpOrderedGoods.Size = new System.Drawing.Size(1308, 428);
            this.xtpOrderedGoods.Text = "ORDERED GOODS";
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
            this.sluePaymentTerm.Location = new System.Drawing.Point(225, 201);
            this.sluePaymentTerm.MenuManager = this.ribbonControl;
            this.sluePaymentTerm.Name = "sluePaymentTerm";
            this.sluePaymentTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sluePaymentTerm.Properties.View = this.gridView3;
            this.sluePaymentTerm.Size = new System.Drawing.Size(389, 20);
            this.sluePaymentTerm.TabIndex = 15;
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
            this.lblTxtInvoiceTo.Location = new System.Drawing.Point(225, 160);
            this.lblTxtInvoiceTo.Name = "lblTxtInvoiceTo";
            this.lblTxtInvoiceTo.Size = new System.Drawing.Size(693, 20);
            this.lblTxtInvoiceTo.TabIndex = 13;
            this.lblTxtInvoiceTo.Text = "XXXXXXXXXXXXXXXXXXXXX";
            // 
            // lblTxtShipTo
            // 
            this.lblTxtShipTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTxtShipTo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTxtShipTo.Location = new System.Drawing.Point(225, 141);
            this.lblTxtShipTo.Name = "lblTxtShipTo";
            this.lblTxtShipTo.Size = new System.Drawing.Size(693, 20);
            this.lblTxtShipTo.TabIndex = 12;
            this.lblTxtShipTo.Text = "XXXXXXXXXXXXXXXXXXXXX";
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
            // lblInvoiceTo
            // 
            this.lblInvoiceTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInvoiceTo.Location = new System.Drawing.Point(122, 164);
            this.lblInvoiceTo.Name = "lblInvoiceTo";
            this.lblInvoiceTo.Size = new System.Drawing.Size(97, 13);
            this.lblInvoiceTo.TabIndex = 8;
            this.lblInvoiceTo.Text = "Invoice To:";
            // 
            // lblTermPayment
            // 
            this.lblTermPayment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTermPayment.Location = new System.Drawing.Point(122, 204);
            this.lblTermPayment.Name = "lblTermPayment";
            this.lblTermPayment.Size = new System.Drawing.Size(97, 13);
            this.lblTermPayment.TabIndex = 7;
            this.lblTermPayment.Text = "Term of Payment:";
            // 
            // lblShipTo
            // 
            this.lblShipTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblShipTo.Location = new System.Drawing.Point(122, 145);
            this.lblShipTo.Name = "lblShipTo";
            this.lblShipTo.Size = new System.Drawing.Size(97, 13);
            this.lblShipTo.TabIndex = 6;
            this.lblShipTo.Text = "Ship To:";
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
            this.gbHeader.Controls.Add(this.sbImportExcel);
            this.gbHeader.Controls.Add(this.sbFinishPO);
            this.gbHeader.Controls.Add(this.sbOrder);
            this.gbHeader.Controls.Add(this.sbSearch);
            this.gbHeader.Controls.Add(this.slueCurrency);
            this.gbHeader.Controls.Add(this.slueDeliveryTerms);
            this.gbHeader.Controls.Add(this.slueSupplier);
            this.gbHeader.Controls.Add(this.lblCurrency);
            this.gbHeader.Controls.Add(this.lblTermsOfDelivery);
            this.gbHeader.Controls.Add(this.lblSupplier);
            this.gbHeader.Controls.Add(this.lblWeek);
            this.gbHeader.Controls.Add(this.lblDeliveryWeek);
            this.gbHeader.Controls.Add(this.lblDocDateWeek);
            this.gbHeader.Controls.Add(this.lblDate);
            this.gbHeader.Controls.Add(this.dateEditDelivery);
            this.gbHeader.Controls.Add(this.dateEditDocDate);
            this.gbHeader.Controls.Add(this.lblDelivery);
            this.gbHeader.Controls.Add(this.lblDocDate);
            this.gbHeader.Controls.Add(this.lblPONumber);
            this.gbHeader.Controls.Add(this.txtPONumber);
            this.gbHeader.Location = new System.Drawing.Point(11, 3);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(1314, 99);
            this.gbHeader.TabIndex = 2;
            this.gbHeader.TabStop = false;
            // 
            // sbImportExcel
            // 
            this.sbImportExcel.Location = new System.Drawing.Point(1035, 57);
            this.sbImportExcel.Name = "sbImportExcel";
            this.sbImportExcel.Size = new System.Drawing.Size(43, 35);
            this.sbImportExcel.TabIndex = 19;
            this.sbImportExcel.Text = "Excel";
            // 
            // sbFinishPO
            // 
            this.sbFinishPO.ImageUri.Uri = "Apply";
            this.sbFinishPO.Location = new System.Drawing.Point(7, 59);
            this.sbFinishPO.Name = "sbFinishPO";
            this.sbFinishPO.Size = new System.Drawing.Size(119, 32);
            this.sbFinishPO.TabIndex = 18;
            this.sbFinishPO.Text = "Finish PO";
            // 
            // sbOrder
            // 
            this.sbOrder.ImageUri.Uri = "SortAsc;Size32x32";
            this.sbOrder.Location = new System.Drawing.Point(986, 56);
            this.sbOrder.Name = "sbOrder";
            this.sbOrder.Size = new System.Drawing.Size(43, 35);
            this.sbOrder.TabIndex = 17;
            // 
            // sbSearch
            // 
            this.sbSearch.ImageUri.Uri = "Find;Size32x32";
            this.sbSearch.Location = new System.Drawing.Point(986, 14);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(43, 35);
            this.sbSearch.TabIndex = 16;
            // 
            // slueCurrency
            // 
            this.slueCurrency.Location = new System.Drawing.Point(563, 71);
            this.slueCurrency.MenuManager = this.ribbonControl;
            this.slueCurrency.Name = "slueCurrency";
            this.slueCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueCurrency.Properties.View = this.gridView2;
            this.slueCurrency.Size = new System.Drawing.Size(194, 20);
            this.slueCurrency.TabIndex = 15;
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
            this.slueDeliveryTerms.Location = new System.Drawing.Point(563, 52);
            this.slueDeliveryTerms.MenuManager = this.ribbonControl;
            this.slueDeliveryTerms.Name = "slueDeliveryTerms";
            this.slueDeliveryTerms.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueDeliveryTerms.Properties.View = this.gridView1;
            this.slueDeliveryTerms.Size = new System.Drawing.Size(389, 20);
            this.slueDeliveryTerms.TabIndex = 14;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // slueSupplier
            // 
            this.slueSupplier.Location = new System.Drawing.Point(563, 33);
            this.slueSupplier.MenuManager = this.ribbonControl;
            this.slueSupplier.Name = "slueSupplier";
            this.slueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueSupplier.Properties.View = this.searchLookUpEdit1View;
            this.slueSupplier.Size = new System.Drawing.Size(389, 20);
            this.slueSupplier.TabIndex = 13;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCurrency.Location = new System.Drawing.Point(456, 72);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(84, 13);
            this.lblCurrency.TabIndex = 12;
            this.lblCurrency.Text = "Currency";
            // 
            // lblTermsOfDelivery
            // 
            this.lblTermsOfDelivery.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTermsOfDelivery.Location = new System.Drawing.Point(456, 53);
            this.lblTermsOfDelivery.Name = "lblTermsOfDelivery";
            this.lblTermsOfDelivery.Size = new System.Drawing.Size(84, 13);
            this.lblTermsOfDelivery.TabIndex = 11;
            this.lblTermsOfDelivery.Text = "Terms df Delivery";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSupplier.Location = new System.Drawing.Point(456, 36);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(84, 13);
            this.lblSupplier.TabIndex = 10;
            this.lblSupplier.Text = "SUPPLIER";
            // 
            // lblWeek
            // 
            this.lblWeek.Location = new System.Drawing.Point(317, 14);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(27, 13);
            this.lblWeek.TabIndex = 9;
            this.lblWeek.Text = "Week";
            // 
            // lblDeliveryWeek
            // 
            this.lblDeliveryWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDeliveryWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblDeliveryWeek.Location = new System.Drawing.Point(301, 53);
            this.lblDeliveryWeek.Name = "lblDeliveryWeek";
            this.lblDeliveryWeek.Size = new System.Drawing.Size(65, 19);
            this.lblDeliveryWeek.TabIndex = 8;
            this.lblDeliveryWeek.Text = "XX";
            // 
            // lblDocDateWeek
            // 
            this.lblDocDateWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDocDateWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblDocDateWeek.Location = new System.Drawing.Point(301, 33);
            this.lblDocDateWeek.Name = "lblDocDateWeek";
            this.lblDocDateWeek.Size = new System.Drawing.Size(65, 20);
            this.lblDocDateWeek.TabIndex = 7;
            this.lblDocDateWeek.Text = "XX";
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(229, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(23, 13);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date";
            // 
            // dateEditDelivery
            // 
            this.dateEditDelivery.EditValue = null;
            this.dateEditDelivery.Location = new System.Drawing.Point(189, 52);
            this.dateEditDelivery.MenuManager = this.ribbonControl;
            this.dateEditDelivery.Name = "dateEditDelivery";
            this.dateEditDelivery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDelivery.Size = new System.Drawing.Size(106, 20);
            this.dateEditDelivery.TabIndex = 5;
            // 
            // dateEditDocDate
            // 
            this.dateEditDocDate.EditValue = null;
            this.dateEditDocDate.Location = new System.Drawing.Point(189, 33);
            this.dateEditDocDate.MenuManager = this.ribbonControl;
            this.dateEditDocDate.Name = "dateEditDocDate";
            this.dateEditDocDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDocDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDocDate.Size = new System.Drawing.Size(106, 20);
            this.dateEditDocDate.TabIndex = 4;
            // 
            // lblDelivery
            // 
            this.lblDelivery.Location = new System.Drawing.Point(136, 55);
            this.lblDelivery.Name = "lblDelivery";
            this.lblDelivery.Size = new System.Drawing.Size(47, 13);
            this.lblDelivery.TabIndex = 3;
            this.lblDelivery.Text = "DELIVERY";
            // 
            // lblDocDate
            // 
            this.lblDocDate.Location = new System.Drawing.Point(132, 36);
            this.lblDocDate.Name = "lblDocDate";
            this.lblDocDate.Size = new System.Drawing.Size(51, 13);
            this.lblDocDate.TabIndex = 2;
            this.lblDocDate.Text = "DOC DATE";
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
            // PurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 715);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "PurchaseOrder";
            this.Text = "PurchaseOrder";
            this.Load += new System.EventHandler(this.PurchaseOrder_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpPO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtcPO)).EndInit();
            this.xtcPO.ResumeLayout(false);
            this.xtpOrderedGoods.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDelivery.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDocDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDocDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPONumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpPO;
        private System.Windows.Forms.GroupBox gbHeader;
        private DevExpress.XtraEditors.LabelControl lblWeek;
        private DevExpress.XtraEditors.LabelControl lblDeliveryWeek;
        private DevExpress.XtraEditors.LabelControl lblDocDateWeek;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.DateEdit dateEditDelivery;
        private DevExpress.XtraEditors.DateEdit dateEditDocDate;
        private DevExpress.XtraEditors.LabelControl lblDelivery;
        private DevExpress.XtraEditors.LabelControl lblDocDate;
        private DevExpress.XtraEditors.LabelControl lblPONumber;
        private DevExpress.XtraEditors.TextEdit txtPONumber;
        private DevExpress.XtraEditors.LabelControl lblCurrency;
        private DevExpress.XtraEditors.LabelControl lblTermsOfDelivery;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraEditors.SearchLookUpEdit slueCurrency;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SearchLookUpEdit slueDeliveryTerms;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraTab.XtraTabControl xtcPO;
        private DevExpress.XtraTab.XtraTabPage xtpOrderedGoods;
        private DevExpress.XtraTab.XtraTabPage xtpTerms;
        private DevExpress.XtraGrid.GridControl xgrdLines;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLines;
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
        private DevExpress.XtraEditors.SimpleButton sbSearch;
        private DevExpress.XtraEditors.SimpleButton sbOrder;
        private DevExpress.XtraEditors.SimpleButton sbFinishPO;
        private DevExpress.XtraEditors.SimpleButton sbImportExcel;
    }
}