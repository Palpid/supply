namespace HKSupply.Forms.Master
{
    partial class StoreManagement
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
            this.grdStores = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).BeginInit();
            this.SuspendLayout();
            // 
            // grdStores
            // 
            this.grdStores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdStores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdStores.Location = new System.Drawing.Point(16, 62);
            this.grdStores.Margin = new System.Windows.Forms.Padding(4);
            this.grdStores.Name = "grdStores";
            this.grdStores.ReadOnly = true;
            this.grdStores.Size = new System.Drawing.Size(1183, 522);
            this.grdStores.TabIndex = 1;
            // 
            // StoreManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 598);
            this.Controls.Add(this.grdStores);
            this.Name = "StoreManagement";
            this.Text = "Store Management";
            this.Load += new System.EventHandler(this.StoreManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdStores;
    }
}