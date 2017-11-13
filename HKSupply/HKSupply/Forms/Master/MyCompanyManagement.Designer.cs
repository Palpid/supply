namespace HKSupply.Forms.Master
{
    partial class MyCompanyManagement
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
            this.xtpMyCompany = new DevExpress.XtraTab.XtraTabPage();
            this.gbContactInfo = new System.Windows.Forms.GroupBox();
            this.gbMainInfo = new System.Windows.Forms.GroupBox();
            this.txtShippingAddressZh = new DevExpress.XtraEditors.TextEdit();
            this.lblShippingAddressZh = new DevExpress.XtraEditors.LabelControl();
            this.txtShippingAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblShippingAddress = new DevExpress.XtraEditors.LabelControl();
            this.txtVatNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtId = new DevExpress.XtraEditors.TextEdit();
            this.lblVatNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblId = new DevExpress.XtraEditors.LabelControl();
            this.txtBillingAddressZh = new DevExpress.XtraEditors.TextEdit();
            this.lblBillingAddressZh = new DevExpress.XtraEditors.LabelControl();
            this.txtBillingAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblBillingAddress = new DevExpress.XtraEditors.LabelControl();
            this.lueCurrency = new DevExpress.XtraEditors.LookUpEdit();
            this.lblIdCurrency = new DevExpress.XtraEditors.LabelControl();
            this.txtContactName = new DevExpress.XtraEditors.TextEdit();
            this.lblContactName = new DevExpress.XtraEditors.LabelControl();
            this.txtContactNameZh = new DevExpress.XtraEditors.TextEdit();
            this.lblContactNameZh = new DevExpress.XtraEditors.LabelControl();
            this.txtContactPhone = new DevExpress.XtraEditors.TextEdit();
            this.lblContactPhone = new DevExpress.XtraEditors.LabelControl();
            this.gbComments = new System.Windows.Forms.GroupBox();
            this.memoEditComments = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpMyCompany.SuspendLayout();
            this.gbContactInfo.SuspendLayout();
            this.gbMainInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtShippingAddressZh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShippingAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVatNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillingAddressZh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillingAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNameZh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactPhone.Properties)).BeginInit();
            this.gbComments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComments.Properties)).BeginInit();
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
            this.xtcGeneral.SelectedTabPage = this.xtpMyCompany;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 605);
            this.xtcGeneral.TabIndex = 2;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpMyCompany});
            // 
            // xtpMyCompany
            // 
            this.xtpMyCompany.Controls.Add(this.gbComments);
            this.xtpMyCompany.Controls.Add(this.gbContactInfo);
            this.xtpMyCompany.Controls.Add(this.gbMainInfo);
            this.xtpMyCompany.Name = "xtpMyCompany";
            this.xtpMyCompany.Size = new System.Drawing.Size(1332, 577);
            this.xtpMyCompany.Text = "Company";
            // 
            // gbContactInfo
            // 
            this.gbContactInfo.Controls.Add(this.txtContactPhone);
            this.gbContactInfo.Controls.Add(this.lblContactPhone);
            this.gbContactInfo.Controls.Add(this.txtContactNameZh);
            this.gbContactInfo.Controls.Add(this.lblContactNameZh);
            this.gbContactInfo.Controls.Add(this.txtContactName);
            this.gbContactInfo.Controls.Add(this.lblContactName);
            this.gbContactInfo.Location = new System.Drawing.Point(682, 21);
            this.gbContactInfo.Name = "gbContactInfo";
            this.gbContactInfo.Size = new System.Drawing.Size(475, 103);
            this.gbContactInfo.TabIndex = 1;
            this.gbContactInfo.TabStop = false;
            this.gbContactInfo.Text = "Contact";
            // 
            // gbMainInfo
            // 
            this.gbMainInfo.Controls.Add(this.lblIdCurrency);
            this.gbMainInfo.Controls.Add(this.lueCurrency);
            this.gbMainInfo.Controls.Add(this.txtBillingAddressZh);
            this.gbMainInfo.Controls.Add(this.lblBillingAddressZh);
            this.gbMainInfo.Controls.Add(this.txtBillingAddress);
            this.gbMainInfo.Controls.Add(this.lblBillingAddress);
            this.gbMainInfo.Controls.Add(this.txtShippingAddressZh);
            this.gbMainInfo.Controls.Add(this.lblShippingAddressZh);
            this.gbMainInfo.Controls.Add(this.txtShippingAddress);
            this.gbMainInfo.Controls.Add(this.lblShippingAddress);
            this.gbMainInfo.Controls.Add(this.txtVatNumber);
            this.gbMainInfo.Controls.Add(this.txtName);
            this.gbMainInfo.Controls.Add(this.txtId);
            this.gbMainInfo.Controls.Add(this.lblVatNumber);
            this.gbMainInfo.Controls.Add(this.lblName);
            this.gbMainInfo.Controls.Add(this.lblId);
            this.gbMainInfo.Location = new System.Drawing.Point(11, 21);
            this.gbMainInfo.Name = "gbMainInfo";
            this.gbMainInfo.Size = new System.Drawing.Size(651, 233);
            this.gbMainInfo.TabIndex = 0;
            this.gbMainInfo.TabStop = false;
            this.gbMainInfo.Text = "Main";
            // 
            // txtShippingAddressZh
            // 
            this.txtShippingAddressZh.Location = new System.Drawing.Point(162, 123);
            this.txtShippingAddressZh.MenuManager = this.ribbonControl;
            this.txtShippingAddressZh.Name = "txtShippingAddressZh";
            this.txtShippingAddressZh.Size = new System.Drawing.Size(483, 20);
            this.txtShippingAddressZh.TabIndex = 9;
            // 
            // lblShippingAddressZh
            // 
            this.lblShippingAddressZh.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblShippingAddressZh.Location = new System.Drawing.Point(6, 126);
            this.lblShippingAddressZh.Name = "lblShippingAddressZh";
            this.lblShippingAddressZh.Size = new System.Drawing.Size(150, 13);
            this.lblShippingAddressZh.TabIndex = 8;
            this.lblShippingAddressZh.Text = "Shipping Address (Chinese)";
            // 
            // txtShippingAddress
            // 
            this.txtShippingAddress.Location = new System.Drawing.Point(162, 97);
            this.txtShippingAddress.MenuManager = this.ribbonControl;
            this.txtShippingAddress.Name = "txtShippingAddress";
            this.txtShippingAddress.Size = new System.Drawing.Size(483, 20);
            this.txtShippingAddress.TabIndex = 7;
            // 
            // lblShippingAddress
            // 
            this.lblShippingAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblShippingAddress.Location = new System.Drawing.Point(6, 100);
            this.lblShippingAddress.Name = "lblShippingAddress";
            this.lblShippingAddress.Size = new System.Drawing.Size(150, 13);
            this.lblShippingAddress.TabIndex = 6;
            this.lblShippingAddress.Text = "Shipping Address";
            // 
            // txtVatNumber
            // 
            this.txtVatNumber.Location = new System.Drawing.Point(162, 71);
            this.txtVatNumber.MenuManager = this.ribbonControl;
            this.txtVatNumber.Name = "txtVatNumber";
            this.txtVatNumber.Size = new System.Drawing.Size(483, 20);
            this.txtVatNumber.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(162, 45);
            this.txtName.MenuManager = this.ribbonControl;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(483, 20);
            this.txtName.TabIndex = 4;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(162, 17);
            this.txtId.MenuManager = this.ribbonControl;
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(483, 20);
            this.txtId.TabIndex = 3;
            // 
            // lblVatNumber
            // 
            this.lblVatNumber.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVatNumber.Location = new System.Drawing.Point(6, 74);
            this.lblVatNumber.Name = "lblVatNumber";
            this.lblVatNumber.Size = new System.Drawing.Size(150, 13);
            this.lblVatNumber.TabIndex = 2;
            this.lblVatNumber.Text = "VAT Number";
            // 
            // lblName
            // 
            this.lblName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblName.Location = new System.Drawing.Point(6, 48);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(150, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // lblId
            // 
            this.lblId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblId.Location = new System.Drawing.Point(6, 20);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(150, 13);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Id";
            // 
            // txtBillingAddressZh
            // 
            this.txtBillingAddressZh.Location = new System.Drawing.Point(162, 175);
            this.txtBillingAddressZh.MenuManager = this.ribbonControl;
            this.txtBillingAddressZh.Name = "txtBillingAddressZh";
            this.txtBillingAddressZh.Size = new System.Drawing.Size(483, 20);
            this.txtBillingAddressZh.TabIndex = 13;
            // 
            // lblBillingAddressZh
            // 
            this.lblBillingAddressZh.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblBillingAddressZh.Location = new System.Drawing.Point(6, 178);
            this.lblBillingAddressZh.Name = "lblBillingAddressZh";
            this.lblBillingAddressZh.Size = new System.Drawing.Size(150, 13);
            this.lblBillingAddressZh.TabIndex = 12;
            this.lblBillingAddressZh.Text = "Billing Address (Chinese)";
            // 
            // txtBillingAddress
            // 
            this.txtBillingAddress.Location = new System.Drawing.Point(162, 149);
            this.txtBillingAddress.MenuManager = this.ribbonControl;
            this.txtBillingAddress.Name = "txtBillingAddress";
            this.txtBillingAddress.Size = new System.Drawing.Size(483, 20);
            this.txtBillingAddress.TabIndex = 11;
            // 
            // lblBillingAddress
            // 
            this.lblBillingAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblBillingAddress.Location = new System.Drawing.Point(6, 152);
            this.lblBillingAddress.Name = "lblBillingAddress";
            this.lblBillingAddress.Size = new System.Drawing.Size(150, 13);
            this.lblBillingAddress.TabIndex = 10;
            this.lblBillingAddress.Text = "Billing Address";
            // 
            // lueCurrency
            // 
            this.lueCurrency.Location = new System.Drawing.Point(162, 201);
            this.lueCurrency.MenuManager = this.ribbonControl;
            this.lueCurrency.Name = "lueCurrency";
            this.lueCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCurrency.Size = new System.Drawing.Size(120, 20);
            this.lueCurrency.TabIndex = 14;
            // 
            // lblIdCurrency
            // 
            this.lblIdCurrency.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblIdCurrency.Location = new System.Drawing.Point(6, 204);
            this.lblIdCurrency.Name = "lblIdCurrency";
            this.lblIdCurrency.Size = new System.Drawing.Size(150, 13);
            this.lblIdCurrency.TabIndex = 15;
            this.lblIdCurrency.Text = "Billing Address (Chinese)";
            // 
            // txtContactName
            // 
            this.txtContactName.Location = new System.Drawing.Point(160, 20);
            this.txtContactName.MenuManager = this.ribbonControl;
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(309, 20);
            this.txtContactName.TabIndex = 6;
            // 
            // lblContactName
            // 
            this.lblContactName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblContactName.Location = new System.Drawing.Point(4, 23);
            this.lblContactName.Name = "lblContactName";
            this.lblContactName.Size = new System.Drawing.Size(150, 13);
            this.lblContactName.TabIndex = 5;
            this.lblContactName.Text = "Contact Name";
            // 
            // txtContactNameZh
            // 
            this.txtContactNameZh.Location = new System.Drawing.Point(160, 46);
            this.txtContactNameZh.MenuManager = this.ribbonControl;
            this.txtContactNameZh.Name = "txtContactNameZh";
            this.txtContactNameZh.Size = new System.Drawing.Size(309, 20);
            this.txtContactNameZh.TabIndex = 8;
            // 
            // lblContactNameZh
            // 
            this.lblContactNameZh.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblContactNameZh.Location = new System.Drawing.Point(4, 49);
            this.lblContactNameZh.Name = "lblContactNameZh";
            this.lblContactNameZh.Size = new System.Drawing.Size(150, 13);
            this.lblContactNameZh.TabIndex = 7;
            this.lblContactNameZh.Text = "Contact Name (Chinese)";
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(160, 72);
            this.txtContactPhone.MenuManager = this.ribbonControl;
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(309, 20);
            this.txtContactPhone.TabIndex = 10;
            // 
            // lblContactPhone
            // 
            this.lblContactPhone.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblContactPhone.Location = new System.Drawing.Point(4, 75);
            this.lblContactPhone.Name = "lblContactPhone";
            this.lblContactPhone.Size = new System.Drawing.Size(150, 13);
            this.lblContactPhone.TabIndex = 9;
            this.lblContactPhone.Text = "Contact Phone";
            // 
            // gbComments
            // 
            this.gbComments.Controls.Add(this.memoEditComments);
            this.gbComments.Location = new System.Drawing.Point(679, 130);
            this.gbComments.Name = "gbComments";
            this.gbComments.Size = new System.Drawing.Size(477, 123);
            this.gbComments.TabIndex = 2;
            this.gbComments.TabStop = false;
            this.gbComments.Text = "Comments";
            // 
            // memoEditComments
            // 
            this.memoEditComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEditComments.Location = new System.Drawing.Point(3, 17);
            this.memoEditComments.MenuManager = this.ribbonControl;
            this.memoEditComments.Name = "memoEditComments";
            this.memoEditComments.Size = new System.Drawing.Size(471, 103);
            this.memoEditComments.TabIndex = 0;
            // 
            // MyCompanyManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 715);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "MyCompanyManagement";
            this.Text = "MyCompanyManagement";
            this.Load += new System.EventHandler(this.MyCompanyManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpMyCompany.ResumeLayout(false);
            this.gbContactInfo.ResumeLayout(false);
            this.gbMainInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtShippingAddressZh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShippingAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVatNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillingAddressZh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillingAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNameZh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactPhone.Properties)).EndInit();
            this.gbComments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditComments.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpMyCompany;
        private System.Windows.Forms.GroupBox gbMainInfo;
        private System.Windows.Forms.GroupBox gbContactInfo;
        private DevExpress.XtraEditors.TextEdit txtVatNumber;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtId;
        private DevExpress.XtraEditors.LabelControl lblVatNumber;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblId;
        private DevExpress.XtraEditors.TextEdit txtShippingAddressZh;
        private DevExpress.XtraEditors.LabelControl lblShippingAddressZh;
        private DevExpress.XtraEditors.TextEdit txtShippingAddress;
        private DevExpress.XtraEditors.LabelControl lblShippingAddress;
        private DevExpress.XtraEditors.TextEdit txtBillingAddressZh;
        private DevExpress.XtraEditors.LabelControl lblBillingAddressZh;
        private DevExpress.XtraEditors.TextEdit txtBillingAddress;
        private DevExpress.XtraEditors.LabelControl lblBillingAddress;
        private System.Windows.Forms.GroupBox gbComments;
        private DevExpress.XtraEditors.TextEdit txtContactPhone;
        private DevExpress.XtraEditors.LabelControl lblContactPhone;
        private DevExpress.XtraEditors.TextEdit txtContactNameZh;
        private DevExpress.XtraEditors.LabelControl lblContactNameZh;
        private DevExpress.XtraEditors.TextEdit txtContactName;
        private DevExpress.XtraEditors.LabelControl lblContactName;
        private DevExpress.XtraEditors.LabelControl lblIdCurrency;
        private DevExpress.XtraEditors.LookUpEdit lueCurrency;
        private DevExpress.XtraEditors.MemoEdit memoEditComments;
    }
}