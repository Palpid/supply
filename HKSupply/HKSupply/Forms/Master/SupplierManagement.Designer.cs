namespace HKSupply.Forms.Master
{
    partial class SupplierManagement
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
            this.tcGeneral = new System.Windows.Forms.TabControl();
            this.tpGrid = new System.Windows.Forms.TabPage();
            this.btnLoad = new System.Windows.Forms.Button();
            this.grdSuppliers = new System.Windows.Forms.DataGridView();
            this.tpForm = new System.Windows.Forms.TabPage();
            this.btnNewVersion = new System.Windows.Forms.Button();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.txtPaymentTerms = new System.Windows.Forms.TextBox();
            this.txtIntercom = new System.Windows.Forms.TextBox();
            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this.txtBillingAddress = new System.Windows.Forms.TextBox();
            this.txtShippingAddress = new System.Windows.Forms.TextBox();
            this.txtVatNumber = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtTimestamp = new System.Windows.Forms.TextBox();
            this.txtIdSubversion = new System.Windows.Forms.TextBox();
            this.txtCurreny = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdSupplier = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cmbColFilter = new System.Windows.Forms.ComboBox();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.tcGeneral.SuspendLayout();
            this.tpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSuppliers)).BeginInit();
            this.tpForm.SuspendLayout();
            this.tlpForm.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcGeneral
            // 
            this.tcGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcGeneral.Controls.Add(this.tpGrid);
            this.tcGeneral.Controls.Add(this.tpForm);
            this.tcGeneral.Location = new System.Drawing.Point(16, 62);
            this.tcGeneral.Name = "tcGeneral";
            this.tcGeneral.SelectedIndex = 0;
            this.tcGeneral.Size = new System.Drawing.Size(1183, 522);
            this.tcGeneral.TabIndex = 1;
            // 
            // tpGrid
            // 
            this.tpGrid.Controls.Add(this.gbFilter);
            this.tpGrid.Controls.Add(this.btnLoad);
            this.tpGrid.Controls.Add(this.grdSuppliers);
            this.tpGrid.Location = new System.Drawing.Point(4, 25);
            this.tpGrid.Name = "tpGrid";
            this.tpGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tpGrid.Size = new System.Drawing.Size(1175, 493);
            this.tpGrid.TabIndex = 0;
            this.tpGrid.Text = "List";
            this.tpGrid.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(1066, 69);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(102, 29);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // grdSuppliers
            // 
            this.grdSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSuppliers.Location = new System.Drawing.Point(6, 104);
            this.grdSuppliers.Name = "grdSuppliers";
            this.grdSuppliers.RowTemplate.Height = 24;
            this.grdSuppliers.Size = new System.Drawing.Size(1163, 383);
            this.grdSuppliers.TabIndex = 0;
            // 
            // tpForm
            // 
            this.tpForm.Controls.Add(this.btnNewVersion);
            this.tpForm.Controls.Add(this.tlpForm);
            this.tpForm.Location = new System.Drawing.Point(4, 25);
            this.tpForm.Name = "tpForm";
            this.tpForm.Padding = new System.Windows.Forms.Padding(3);
            this.tpForm.Size = new System.Drawing.Size(1175, 493);
            this.tpForm.TabIndex = 1;
            this.tpForm.Text = "Form";
            this.tpForm.UseVisualStyleBackColor = true;
            // 
            // btnNewVersion
            // 
            this.btnNewVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewVersion.Location = new System.Drawing.Point(1094, 5);
            this.btnNewVersion.Name = "btnNewVersion";
            this.btnNewVersion.Size = new System.Drawing.Size(75, 50);
            this.btnNewVersion.TabIndex = 1;
            this.btnNewVersion.Text = "New Version";
            this.btnNewVersion.UseVisualStyleBackColor = true;
            this.btnNewVersion.Click += new System.EventHandler(this.btnNewVersion_Click);
            // 
            // tlpForm
            // 
            this.tlpForm.AutoScroll = true;
            this.tlpForm.ColumnCount = 2;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tlpForm.Controls.Add(this.txtPaymentTerms, 1, 12);
            this.tlpForm.Controls.Add(this.txtIntercom, 1, 11);
            this.tlpForm.Controls.Add(this.txtContactPhone, 1, 10);
            this.tlpForm.Controls.Add(this.txtContactName, 1, 9);
            this.tlpForm.Controls.Add(this.txtBillingAddress, 1, 8);
            this.tlpForm.Controls.Add(this.txtShippingAddress, 1, 7);
            this.tlpForm.Controls.Add(this.txtVatNumber, 1, 6);
            this.tlpForm.Controls.Add(this.txtName, 1, 4);
            this.tlpForm.Controls.Add(this.txtTimestamp, 1, 3);
            this.tlpForm.Controls.Add(this.txtIdSubversion, 1, 2);
            this.tlpForm.Controls.Add(this.txtCurreny, 1, 13);
            this.tlpForm.Controls.Add(this.label14, 0, 13);
            this.tlpForm.Controls.Add(this.label13, 0, 12);
            this.tlpForm.Controls.Add(this.label12, 0, 11);
            this.tlpForm.Controls.Add(this.label11, 0, 10);
            this.tlpForm.Controls.Add(this.label9, 0, 8);
            this.tlpForm.Controls.Add(this.label8, 0, 7);
            this.tlpForm.Controls.Add(this.label7, 0, 6);
            this.tlpForm.Controls.Add(this.label6, 0, 5);
            this.tlpForm.Controls.Add(this.label5, 0, 4);
            this.tlpForm.Controls.Add(this.label4, 0, 3);
            this.tlpForm.Controls.Add(this.txtIdVersion, 1, 1);
            this.tlpForm.Controls.Add(this.label1, 0, 0);
            this.tlpForm.Controls.Add(this.label2, 0, 1);
            this.tlpForm.Controls.Add(this.txtIdSupplier, 1, 0);
            this.tlpForm.Controls.Add(this.label3, 0, 2);
            this.tlpForm.Controls.Add(this.label10, 0, 9);
            this.tlpForm.Controls.Add(this.chkActive, 1, 5);
            this.tlpForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlpForm.Location = new System.Drawing.Point(3, 3);
            this.tlpForm.Name = "tlpForm";
            this.tlpForm.RowCount = 14;
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpForm.Size = new System.Drawing.Size(776, 487);
            this.tlpForm.TabIndex = 0;
            // 
            // txtPaymentTerms
            // 
            this.txtPaymentTerms.Location = new System.Drawing.Point(224, 347);
            this.txtPaymentTerms.Name = "txtPaymentTerms";
            this.txtPaymentTerms.Size = new System.Drawing.Size(549, 22);
            this.txtPaymentTerms.TabIndex = 7;
            // 
            // txtIntercom
            // 
            this.txtIntercom.Location = new System.Drawing.Point(224, 319);
            this.txtIntercom.Name = "txtIntercom";
            this.txtIntercom.Size = new System.Drawing.Size(549, 22);
            this.txtIntercom.TabIndex = 7;
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(224, 291);
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(549, 22);
            this.txtContactPhone.TabIndex = 7;
            // 
            // txtContactName
            // 
            this.txtContactName.Location = new System.Drawing.Point(224, 263);
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(549, 22);
            this.txtContactName.TabIndex = 7;
            // 
            // txtBillingAddress
            // 
            this.txtBillingAddress.Location = new System.Drawing.Point(224, 234);
            this.txtBillingAddress.Name = "txtBillingAddress";
            this.txtBillingAddress.Size = new System.Drawing.Size(549, 22);
            this.txtBillingAddress.TabIndex = 7;
            // 
            // txtShippingAddress
            // 
            this.txtShippingAddress.Location = new System.Drawing.Point(224, 205);
            this.txtShippingAddress.Name = "txtShippingAddress";
            this.txtShippingAddress.Size = new System.Drawing.Size(549, 22);
            this.txtShippingAddress.TabIndex = 6;
            // 
            // txtVatNumber
            // 
            this.txtVatNumber.Location = new System.Drawing.Point(224, 176);
            this.txtVatNumber.Name = "txtVatNumber";
            this.txtVatNumber.Size = new System.Drawing.Size(549, 22);
            this.txtVatNumber.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(224, 118);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(549, 22);
            this.txtName.TabIndex = 5;
            // 
            // txtTimestamp
            // 
            this.txtTimestamp.Location = new System.Drawing.Point(224, 89);
            this.txtTimestamp.Name = "txtTimestamp";
            this.txtTimestamp.Size = new System.Drawing.Size(549, 22);
            this.txtTimestamp.TabIndex = 5;
            // 
            // txtIdSubversion
            // 
            this.txtIdSubversion.Location = new System.Drawing.Point(224, 60);
            this.txtIdSubversion.Name = "txtIdSubversion";
            this.txtIdSubversion.Size = new System.Drawing.Size(549, 22);
            this.txtIdSubversion.TabIndex = 4;
            // 
            // txtCurreny
            // 
            this.txtCurreny.Location = new System.Drawing.Point(224, 375);
            this.txtCurreny.Name = "txtCurreny";
            this.txtCurreny.Size = new System.Drawing.Size(549, 22);
            this.txtCurreny.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(3, 372);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(193, 20);
            this.label14.TabIndex = 3;
            this.label14.Text = "Currency";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 344);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(193, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "Payment Terms";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 316);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(193, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Intercom";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 288);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(193, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "Contact Phone";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 231);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(193, 29);
            this.label9.TabIndex = 1;
            this.label9.Text = "Billing Address";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 29);
            this.label8.TabIndex = 1;
            this.label8.Text = "Shipping Address";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 29);
            this.label7.TabIndex = 1;
            this.label7.Text = "VAT Number";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 29);
            this.label6.TabIndex = 1;
            this.label6.Text = "Active";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 29);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 29);
            this.label4.TabIndex = 1;
            this.label4.Text = "Timestamp";
            // 
            // txtIdVersion
            // 
            this.txtIdVersion.Location = new System.Drawing.Point(224, 32);
            this.txtIdVersion.Name = "txtIdVersion";
            this.txtIdVersion.Size = new System.Drawing.Size(549, 22);
            this.txtIdVersion.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id Supplier";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Id Version";
            // 
            // txtIdSupplier
            // 
            this.txtIdSupplier.Location = new System.Drawing.Point(224, 3);
            this.txtIdSupplier.Name = "txtIdSupplier";
            this.txtIdSupplier.Size = new System.Drawing.Size(549, 22);
            this.txtIdSupplier.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Id Subversion";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 260);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 20);
            this.label10.TabIndex = 5;
            this.label10.Text = "Contact name";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkActive.Location = new System.Drawing.Point(224, 147);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(549, 23);
            this.chkActive.TabIndex = 8;
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // gbFilter
            // 
            this.gbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFilter.Controls.Add(this.chkFilter);
            this.gbFilter.Controls.Add(this.txtFilter);
            this.gbFilter.Controls.Add(this.cmbColFilter);
            this.gbFilter.Location = new System.Drawing.Point(6, 6);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(1162, 52);
            this.gbFilter.TabIndex = 3;
            this.gbFilter.TabStop = false;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(223, 18);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(257, 22);
            this.txtFilter.TabIndex = 1;
            // 
            // cmbColFilter
            // 
            this.cmbColFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColFilter.FormattingEnabled = true;
            this.cmbColFilter.Location = new System.Drawing.Point(6, 17);
            this.cmbColFilter.Name = "cmbColFilter";
            this.cmbColFilter.Size = new System.Drawing.Size(205, 24);
            this.cmbColFilter.TabIndex = 0;
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(486, 21);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(18, 17);
            this.chkFilter.TabIndex = 2;
            this.chkFilter.UseVisualStyleBackColor = true;
            // 
            // SupplierManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 598);
            this.Controls.Add(this.tcGeneral);
            this.Name = "SupplierManagement";
            this.Text = "Supplier Management";
            this.Load += new System.EventHandler(this.SupplierManagement_Load);
            this.tcGeneral.ResumeLayout(false);
            this.tpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSuppliers)).EndInit();
            this.tpForm.ResumeLayout(false);
            this.tlpForm.ResumeLayout(false);
            this.tlpForm.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcGeneral;
        private System.Windows.Forms.TabPage tpGrid;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView grdSuppliers;
        private System.Windows.Forms.TabPage tpForm;
        private System.Windows.Forms.Button btnNewVersion;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.TextBox txtPaymentTerms;
        private System.Windows.Forms.TextBox txtIntercom;
        private System.Windows.Forms.TextBox txtContactPhone;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.TextBox txtBillingAddress;
        private System.Windows.Forms.TextBox txtShippingAddress;
        private System.Windows.Forms.TextBox txtVatNumber;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtTimestamp;
        private System.Windows.Forms.TextBox txtIdSubversion;
        private System.Windows.Forms.TextBox txtCurreny;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIdVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdSupplier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cmbColFilter;
        private System.Windows.Forms.CheckBox chkFilter;
    }
}