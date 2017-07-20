namespace HKSupply.Forms.Master.DialogForms
{
    partial class SelectItem2CopyBom
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
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.checkedListBoxItems = new System.Windows.Forms.CheckedListBox();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lbltxtSupplier = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // lblSupplier
            // 
            this.lblSupplier.Location = new System.Drawing.Point(12, 11);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(42, 13);
            this.lblSupplier.TabIndex = 6;
            this.lblSupplier.Text = "Supplier:";
            // 
            // checkedListBoxItems
            // 
            this.checkedListBoxItems.FormattingEnabled = true;
            this.checkedListBoxItems.Location = new System.Drawing.Point(12, 36);
            this.checkedListBoxItems.Name = "checkedListBoxItems";
            this.checkedListBoxItems.Size = new System.Drawing.Size(264, 214);
            this.checkedListBoxItems.TabIndex = 5;
            // 
            // sbOk
            // 
            this.sbOk.Location = new System.Drawing.Point(199, 256);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(78, 23);
            this.sbOk.TabIndex = 8;
            this.sbOk.Text = "OK";
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(118, 256);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(78, 23);
            this.sbCancel.TabIndex = 7;
            this.sbCancel.Text = "Cancel";
            // 
            // lbltxtSupplier
            // 
            this.lbltxtSupplier.Location = new System.Drawing.Point(71, 11);
            this.lbltxtSupplier.Name = "lbltxtSupplier";
            this.lbltxtSupplier.Size = new System.Drawing.Size(24, 13);
            this.lbltxtSupplier.TabIndex = 9;
            this.lbltxtSupplier.Text = "XXXX";
            // 
            // SelectItem2CopyBom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 302);
            this.Controls.Add(this.lbltxtSupplier);
            this.Controls.Add(this.sbOk);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.checkedListBoxItems);
            this.Name = "SelectItem2CopyBom";
            this.Text = "SelectItem2CopyBom";
            this.Load += new System.EventHandler(this.SelectItem2CopyBom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private System.Windows.Forms.CheckedListBox checkedListBoxItems;
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.LabelControl lbltxtSupplier;
    }
}