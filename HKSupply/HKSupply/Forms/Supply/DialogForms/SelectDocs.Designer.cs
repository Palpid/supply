namespace HKSupply.Forms.Supply.DialogForms
{
    partial class SelectDocs
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
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.checkedListBoxDocs = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // sbOk
            // 
            this.sbOk.Location = new System.Drawing.Point(199, 262);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(78, 23);
            this.sbOk.TabIndex = 13;
            this.sbOk.Text = "OK";
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(118, 262);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(78, 23);
            this.sbCancel.TabIndex = 12;
            this.sbCancel.Text = "Cancel";
            // 
            // checkedListBoxDocs
            // 
            this.checkedListBoxDocs.FormattingEnabled = true;
            this.checkedListBoxDocs.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxDocs.Name = "checkedListBoxDocs";
            this.checkedListBoxDocs.Size = new System.Drawing.Size(264, 244);
            this.checkedListBoxDocs.TabIndex = 10;
            // 
            // SelectDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 302);
            this.Controls.Add(this.sbOk);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.checkedListBoxDocs);
            this.Name = "SelectDocs";
            this.Text = "SelectDocs";
            this.Load += new System.EventHandler(this.SelectDocs_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private System.Windows.Forms.CheckedListBox checkedListBoxDocs;
    }
}