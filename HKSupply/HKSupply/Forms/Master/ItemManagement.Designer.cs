namespace HKSupply.Forms.Master
{
    partial class ItemManagement
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
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cmbColFilter = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.grdItems = new System.Windows.Forms.DataGridView();
            this.tpForm = new System.Windows.Forms.TabPage();
            this.btnNewVersion = new System.Windows.Forms.Button();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtMmFront = new System.Windows.Forms.TextBox();
            this.txtRetired = new System.Windows.Forms.TextBox();
            this.txtLanched = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtTimestamp = new System.Windows.Forms.TextBox();
            this.txtIdSubversion = new System.Windows.Forms.TextBox();
            this.txtCaliber = new System.Windows.Forms.TextBox();
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
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.tcGeneral.SuspendLayout();
            this.tpGrid.SuspendLayout();
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            this.tpForm.SuspendLayout();
            this.tlpForm.SuspendLayout();
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
            this.tpGrid.Controls.Add(this.grdItems);
            this.tpGrid.Location = new System.Drawing.Point(4, 25);
            this.tpGrid.Name = "tpGrid";
            this.tpGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tpGrid.Size = new System.Drawing.Size(1175, 493);
            this.tpGrid.TabIndex = 0;
            this.tpGrid.Text = "List";
            this.tpGrid.UseVisualStyleBackColor = true;
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
            this.gbFilter.TabIndex = 2;
            this.gbFilter.TabStop = false;
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(486, 21);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(18, 17);
            this.chkFilter.TabIndex = 3;
            this.chkFilter.UseVisualStyleBackColor = true;
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
            // grdItems
            // 
            this.grdItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdItems.Location = new System.Drawing.Point(6, 104);
            this.grdItems.Name = "grdItems";
            this.grdItems.RowTemplate.Height = 24;
            this.grdItems.Size = new System.Drawing.Size(1163, 383);
            this.grdItems.TabIndex = 0;
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
            this.tlpForm.Controls.Add(this.txtCategoryName, 1, 12);
            this.tlpForm.Controls.Add(this.txtSize, 1, 11);
            this.tlpForm.Controls.Add(this.txtMmFront, 1, 10);
            this.tlpForm.Controls.Add(this.txtRetired, 1, 9);
            this.tlpForm.Controls.Add(this.txtLanched, 1, 8);
            this.tlpForm.Controls.Add(this.txtStatus, 1, 7);
            this.tlpForm.Controls.Add(this.txtModel, 1, 6);
            this.tlpForm.Controls.Add(this.txtItemName, 1, 4);
            this.tlpForm.Controls.Add(this.txtTimestamp, 1, 3);
            this.tlpForm.Controls.Add(this.txtIdSubversion, 1, 2);
            this.tlpForm.Controls.Add(this.txtCaliber, 1, 13);
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
            this.tlpForm.Controls.Add(this.txtItemCode, 1, 0);
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
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(224, 347);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(549, 22);
            this.txtCategoryName.TabIndex = 7;
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(224, 319);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(549, 22);
            this.txtSize.TabIndex = 7;
            // 
            // txtMmFront
            // 
            this.txtMmFront.Location = new System.Drawing.Point(224, 291);
            this.txtMmFront.Name = "txtMmFront";
            this.txtMmFront.Size = new System.Drawing.Size(549, 22);
            this.txtMmFront.TabIndex = 7;
            // 
            // txtRetired
            // 
            this.txtRetired.Location = new System.Drawing.Point(224, 263);
            this.txtRetired.Name = "txtRetired";
            this.txtRetired.Size = new System.Drawing.Size(549, 22);
            this.txtRetired.TabIndex = 7;
            // 
            // txtLanched
            // 
            this.txtLanched.Location = new System.Drawing.Point(224, 234);
            this.txtLanched.Name = "txtLanched";
            this.txtLanched.Size = new System.Drawing.Size(549, 22);
            this.txtLanched.TabIndex = 7;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(224, 205);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(549, 22);
            this.txtStatus.TabIndex = 6;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(224, 176);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(549, 22);
            this.txtModel.TabIndex = 6;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(224, 118);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(549, 22);
            this.txtItemName.TabIndex = 5;
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
            // txtCaliber
            // 
            this.txtCaliber.Location = new System.Drawing.Point(224, 375);
            this.txtCaliber.Name = "txtCaliber";
            this.txtCaliber.Size = new System.Drawing.Size(549, 22);
            this.txtCaliber.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(3, 372);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(193, 20);
            this.label14.TabIndex = 3;
            this.label14.Text = "Caliber";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(3, 344);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(193, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "Category Name";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 316);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(193, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Size";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 288);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(193, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "Front (mm)";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 231);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(193, 29);
            this.label9.TabIndex = 1;
            this.label9.Text = "Launched";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 29);
            this.label8.TabIndex = 1;
            this.label8.Text = "Status";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 29);
            this.label7.TabIndex = 1;
            this.label7.Text = "Model";
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
            this.label5.Text = "Item Name";
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
            this.label1.Text = "Item Code";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Id Version";
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(224, 3);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(549, 22);
            this.txtItemCode.TabIndex = 2;
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
            this.label10.Text = "Retired";
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
            // ItemManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 598);
            this.Controls.Add(this.tcGeneral);
            this.Name = "ItemManagement";
            this.Text = "Item Management";
            this.Load += new System.EventHandler(this.ItemManagement_Load);
            this.tcGeneral.ResumeLayout(false);
            this.tpGrid.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.tpForm.ResumeLayout(false);
            this.tlpForm.ResumeLayout(false);
            this.tlpForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcGeneral;
        private System.Windows.Forms.TabPage tpGrid;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cmbColFilter;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView grdItems;
        private System.Windows.Forms.TabPage tpForm;
        private System.Windows.Forms.Button btnNewVersion;
        private System.Windows.Forms.TableLayoutPanel tlpForm;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtMmFront;
        private System.Windows.Forms.TextBox txtRetired;
        private System.Windows.Forms.TextBox txtLanched;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtTimestamp;
        private System.Windows.Forms.TextBox txtIdSubversion;
        private System.Windows.Forms.TextBox txtCaliber;
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
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkActive;
    }
}