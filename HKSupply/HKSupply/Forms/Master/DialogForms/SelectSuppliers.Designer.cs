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
            this.checkedListBoxSuppliersSource = new System.Windows.Forms.CheckedListBox();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.checkedListBoxSuppliersDestination = new System.Windows.Forms.CheckedListBox();
            this.lblSource = new DevExpress.XtraEditors.LabelControl();
            this.lblDestination = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // checkedListBoxSuppliersSource
            // 
            this.checkedListBoxSuppliersSource.FormattingEnabled = true;
            this.checkedListBoxSuppliersSource.Location = new System.Drawing.Point(3, 37);
            this.checkedListBoxSuppliersSource.Name = "checkedListBoxSuppliersSource";
            this.checkedListBoxSuppliersSource.Size = new System.Drawing.Size(188, 214);
            this.checkedListBoxSuppliersSource.TabIndex = 0;
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(232, 257);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(75, 23);
            this.sbCancel.TabIndex = 1;
            this.sbCancel.Text = "Cancel";
            // 
            // sbOk
            // 
            this.sbOk.Location = new System.Drawing.Point(313, 257);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(75, 23);
            this.sbOk.TabIndex = 2;
            this.sbOk.Text = "OK";
            // 
            // checkedListBoxSuppliersDestination
            // 
            this.checkedListBoxSuppliersDestination.FormattingEnabled = true;
            this.checkedListBoxSuppliersDestination.Location = new System.Drawing.Point(200, 37);
            this.checkedListBoxSuppliersDestination.Name = "checkedListBoxSuppliersDestination";
            this.checkedListBoxSuppliersDestination.Size = new System.Drawing.Size(188, 214);
            this.checkedListBoxSuppliersDestination.TabIndex = 3;
            // 
            // lblSource
            // 
            this.lblSource.Location = new System.Drawing.Point(3, 12);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(33, 13);
            this.lblSource.TabIndex = 4;
            this.lblSource.Text = "Source";
            // 
            // lblDestination
            // 
            this.lblDestination.Location = new System.Drawing.Point(200, 12);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(54, 13);
            this.lblDestination.TabIndex = 5;
            this.lblDestination.Text = "Destination";
            // 
            // SelectSuppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 295);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.checkedListBoxSuppliersDestination);
            this.Controls.Add(this.sbOk);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.checkedListBoxSuppliersSource);
            this.Name = "SelectSuppliers";
            this.Text = "SelectSuppliers";
            this.Load += new System.EventHandler(this.SelectSupplirs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxSuppliersSource;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private System.Windows.Forms.CheckedListBox checkedListBoxSuppliersDestination;
        private DevExpress.XtraEditors.LabelControl lblSource;
        private DevExpress.XtraEditors.LabelControl lblDestination;
    }
}