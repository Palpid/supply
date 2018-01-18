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
            this.sbOk.Location = new System.Drawing.Point(298, 403);
            this.sbOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(117, 35);
            this.sbOk.TabIndex = 13;
            this.sbOk.Text = "OK";
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(177, 403);
            this.sbCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(117, 35);
            this.sbCancel.TabIndex = 12;
            this.sbCancel.Text = "Cancel";
            // 
            // checkedListBoxDocs
            // 
            this.checkedListBoxDocs.FormattingEnabled = true;
            this.checkedListBoxDocs.Location = new System.Drawing.Point(18, 18);
            this.checkedListBoxDocs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkedListBoxDocs.Name = "checkedListBoxDocs";
            this.checkedListBoxDocs.Size = new System.Drawing.Size(394, 361);
            this.checkedListBoxDocs.TabIndex = 10;
            // 
            // SelectDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 465);
            this.Controls.Add(this.sbOk);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.checkedListBoxDocs);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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