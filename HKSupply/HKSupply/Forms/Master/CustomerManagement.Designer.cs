﻿namespace HKSupply.Forms.Master
{
    partial class CustomerManagement
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
            this.grdCustomers = new System.Windows.Forms.DataGridView();
            this.tpForm = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtIdVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdCustomer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCurreny = new System.Windows.Forms.TextBox();
            this.txtIdSubversion = new System.Windows.Forms.TextBox();
            this.txtTimestamp = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtVatNumber = new System.Windows.Forms.TextBox();
            this.txtShippingAddress = new System.Windows.Forms.TextBox();
            this.txtBillingAddress = new System.Windows.Forms.TextBox();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.txtIntercom = new System.Windows.Forms.TextBox();
            this.txtPaymentTerms = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.tcGeneral.SuspendLayout();
            this.tpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomers)).BeginInit();
            this.tpForm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.tcGeneral.TabIndex = 0;
            // 
            // tpGrid
            // 
            this.tpGrid.Controls.Add(this.grdCustomers);
            this.tpGrid.Location = new System.Drawing.Point(4, 25);
            this.tpGrid.Name = "tpGrid";
            this.tpGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tpGrid.Size = new System.Drawing.Size(1175, 493);
            this.tpGrid.TabIndex = 0;
            this.tpGrid.Text = "List";
            this.tpGrid.UseVisualStyleBackColor = true;
            // 
            // grdCustomers
            // 
            this.grdCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCustomers.Location = new System.Drawing.Point(6, 6);
            this.grdCustomers.Name = "grdCustomers";
            this.grdCustomers.RowTemplate.Height = 24;
            this.grdCustomers.Size = new System.Drawing.Size(1163, 481);
            this.grdCustomers.TabIndex = 0;
            // 
            // tpForm
            // 
            this.tpForm.Controls.Add(this.tableLayoutPanel1);
            this.tpForm.Location = new System.Drawing.Point(4, 25);
            this.tpForm.Name = "tpForm";
            this.tpForm.Padding = new System.Windows.Forms.Padding(3);
            this.tpForm.Size = new System.Drawing.Size(1175, 493);
            this.tpForm.TabIndex = 1;
            this.tpForm.Text = "Form";
            this.tpForm.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanel1.Controls.Add(this.txtPaymentTerms, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtIntercom, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtContactPhone, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtContactName, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtBillingAddress, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtShippingAddress, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtVatNumber, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtTimestamp, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtIdSubversion, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCurreny, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtIdVersion, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtIdCustomer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.chkActive, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(776, 487);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.label1.Text = "Id Custumer";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Id Version";
            // 
            // txtIdCustomer
            // 
            this.txtIdCustomer.Location = new System.Drawing.Point(224, 3);
            this.txtIdCustomer.Name = "txtIdCustomer";
            this.txtIdCustomer.Size = new System.Drawing.Size(549, 22);
            this.txtIdCustomer.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Id Subversion";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 29);
            this.label4.TabIndex = 1;
            this.label4.Text = "Timestamp";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 29);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 29);
            this.label6.TabIndex = 1;
            this.label6.Text = "Active";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 29);
            this.label7.TabIndex = 1;
            this.label7.Text = "VAT Number";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 29);
            this.label8.TabIndex = 1;
            this.label8.Text = "Shipping Address";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 231);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(193, 29);
            this.label9.TabIndex = 1;
            this.label9.Text = "Billing Address";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 260);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(193, 20);
            this.label10.TabIndex = 5;
            this.label10.Text = "Contact name";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 288);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(193, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "Contact Phone";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 316);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(193, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Intercom";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 344);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(193, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "Payment Terms";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(3, 372);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(193, 20);
            this.label14.TabIndex = 3;
            this.label14.Text = "Currency";
            // 
            // txtCurreny
            // 
            this.txtCurreny.Location = new System.Drawing.Point(224, 375);
            this.txtCurreny.Name = "txtCurreny";
            this.txtCurreny.Size = new System.Drawing.Size(549, 22);
            this.txtCurreny.TabIndex = 4;
            // 
            // txtIdSubversion
            // 
            this.txtIdSubversion.Location = new System.Drawing.Point(224, 60);
            this.txtIdSubversion.Name = "txtIdSubversion";
            this.txtIdSubversion.Size = new System.Drawing.Size(549, 22);
            this.txtIdSubversion.TabIndex = 4;
            // 
            // txtTimestamp
            // 
            this.txtTimestamp.Location = new System.Drawing.Point(224, 89);
            this.txtTimestamp.Name = "txtTimestamp";
            this.txtTimestamp.Size = new System.Drawing.Size(549, 22);
            this.txtTimestamp.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(224, 118);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(549, 22);
            this.txtName.TabIndex = 5;
            // 
            // txtVatNumber
            // 
            this.txtVatNumber.Location = new System.Drawing.Point(224, 176);
            this.txtVatNumber.Name = "txtVatNumber";
            this.txtVatNumber.Size = new System.Drawing.Size(549, 22);
            this.txtVatNumber.TabIndex = 6;
            // 
            // txtShippingAddress
            // 
            this.txtShippingAddress.Location = new System.Drawing.Point(224, 205);
            this.txtShippingAddress.Name = "txtShippingAddress";
            this.txtShippingAddress.Size = new System.Drawing.Size(549, 22);
            this.txtShippingAddress.TabIndex = 6;
            // 
            // txtBillingAddress
            // 
            this.txtBillingAddress.Location = new System.Drawing.Point(224, 234);
            this.txtBillingAddress.Name = "txtBillingAddress";
            this.txtBillingAddress.Size = new System.Drawing.Size(549, 22);
            this.txtBillingAddress.TabIndex = 7;
            // 
            // txtContactName
            // 
            this.txtContactName.Location = new System.Drawing.Point(224, 263);
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(549, 22);
            this.txtContactName.TabIndex = 7;
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(224, 291);
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(549, 22);
            this.txtContactPhone.TabIndex = 7;
            // 
            // txtIntercom
            // 
            this.txtIntercom.Location = new System.Drawing.Point(224, 319);
            this.txtIntercom.Name = "txtIntercom";
            this.txtIntercom.Size = new System.Drawing.Size(549, 22);
            this.txtIntercom.TabIndex = 7;
            // 
            // txtPaymentTerms
            // 
            this.txtPaymentTerms.Location = new System.Drawing.Point(224, 347);
            this.txtPaymentTerms.Name = "txtPaymentTerms";
            this.txtPaymentTerms.Size = new System.Drawing.Size(549, 22);
            this.txtPaymentTerms.TabIndex = 7;
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
            // CustomerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1215, 598);
            this.Controls.Add(this.tcGeneral);
            this.Name = "CustomerManagement";
            this.Text = "Customer Management";
            this.Load += new System.EventHandler(this.CustomerManagement_Load);
            this.tcGeneral.ResumeLayout(false);
            this.tpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomers)).EndInit();
            this.tpForm.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcGeneral;
        private System.Windows.Forms.TabPage tpGrid;
        private System.Windows.Forms.TabPage tpForm;
        private System.Windows.Forms.DataGridView grdCustomers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtIdVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdCustomer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
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
        private System.Windows.Forms.CheckBox chkActive;
    }
}