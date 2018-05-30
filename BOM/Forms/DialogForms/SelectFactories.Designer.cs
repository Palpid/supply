namespace BOM.Forms.DialogForms
{
    partial class SelectFactories
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
            this.lblDestination = new DevExpress.XtraEditors.LabelControl();
            this.lblSource = new DevExpress.XtraEditors.LabelControl();
            this.checkedListBoxFactoriesDestination = new System.Windows.Forms.CheckedListBox();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.checkedListBoxFactoriesSource = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // lblDestination
            // 
            this.lblDestination.Location = new System.Drawing.Point(209, 12);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(54, 13);
            this.lblDestination.TabIndex = 11;
            this.lblDestination.Text = "Destination";
            // 
            // lblSource
            // 
            this.lblSource.Location = new System.Drawing.Point(12, 12);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(33, 13);
            this.lblSource.TabIndex = 10;
            this.lblSource.Text = "Source";
            // 
            // checkedListBoxFactoriesDestination
            // 
            this.checkedListBoxFactoriesDestination.FormattingEnabled = true;
            this.checkedListBoxFactoriesDestination.Location = new System.Drawing.Point(209, 37);
            this.checkedListBoxFactoriesDestination.Name = "checkedListBoxFactoriesDestination";
            this.checkedListBoxFactoriesDestination.Size = new System.Drawing.Size(188, 214);
            this.checkedListBoxFactoriesDestination.TabIndex = 9;
            // 
            // sbOk
            // 
            this.sbOk.Location = new System.Drawing.Point(322, 257);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(75, 23);
            this.sbOk.TabIndex = 8;
            this.sbOk.Text = "OK";
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(241, 257);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(75, 23);
            this.sbCancel.TabIndex = 7;
            this.sbCancel.Text = "Cancel";
            // 
            // checkedListBoxFactoriesSource
            // 
            this.checkedListBoxFactoriesSource.FormattingEnabled = true;
            this.checkedListBoxFactoriesSource.Location = new System.Drawing.Point(12, 37);
            this.checkedListBoxFactoriesSource.Name = "checkedListBoxFactoriesSource";
            this.checkedListBoxFactoriesSource.Size = new System.Drawing.Size(188, 214);
            this.checkedListBoxFactoriesSource.TabIndex = 6;
            // 
            // SelectFactories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 286);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.checkedListBoxFactoriesDestination);
            this.Controls.Add(this.sbOk);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.checkedListBoxFactoriesSource);
            this.Name = "SelectFactories";
            this.Text = "Select Factories";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblDestination;
        private DevExpress.XtraEditors.LabelControl lblSource;
        private System.Windows.Forms.CheckedListBox checkedListBoxFactoriesDestination;
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private System.Windows.Forms.CheckedListBox checkedListBoxFactoriesSource;
    }
}