namespace HKSupply.Forms.Master.DialogForms
{
    partial class SelectSuppliers
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
            this.checkedListBoxSuppliers = new System.Windows.Forms.CheckedListBox();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // checkedListBoxSuppliers
            // 
            this.checkedListBoxSuppliers.FormattingEnabled = true;
            this.checkedListBoxSuppliers.Location = new System.Drawing.Point(3, 3);
            this.checkedListBoxSuppliers.Name = "checkedListBoxSuppliers";
            this.checkedListBoxSuppliers.Size = new System.Drawing.Size(269, 214);
            this.checkedListBoxSuppliers.TabIndex = 0;
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(116, 226);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(75, 23);
            this.sbCancel.TabIndex = 1;
            this.sbCancel.Text = "Cancel";
            // 
            // sbOk
            // 
            this.sbOk.Location = new System.Drawing.Point(197, 226);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(75, 23);
            this.sbOk.TabIndex = 2;
            this.sbOk.Text = "OK";
            // 
            // SelectSuppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.sbOk);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.checkedListBoxSuppliers);
            this.Name = "SelectSuppliers";
            this.Text = "SelectSuppliers";
            this.Load += new System.EventHandler(this.SelectSupplirs_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxSuppliers;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOk;
    }
}