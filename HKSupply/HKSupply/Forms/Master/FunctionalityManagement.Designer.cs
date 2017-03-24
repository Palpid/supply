namespace HKSupply.Forms.Master
{
    partial class FunctionalityManagement
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
            this.grdFunctionalities = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdFunctionalities)).BeginInit();
            this.SuspendLayout();
            // 
            // grdFunctionalities
            // 
            this.grdFunctionalities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFunctionalities.BackgroundColor = System.Drawing.Color.White;
            this.grdFunctionalities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFunctionalities.Location = new System.Drawing.Point(16, 62);
            this.grdFunctionalities.Margin = new System.Windows.Forms.Padding(4);
            this.grdFunctionalities.Name = "grdFunctionalities";
            this.grdFunctionalities.ReadOnly = true;
            this.grdFunctionalities.Size = new System.Drawing.Size(1183, 522);
            this.grdFunctionalities.TabIndex = 1;
            // 
            // FunctionalityManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1215, 598);
            this.Controls.Add(this.grdFunctionalities);
            this.Name = "FunctionalityManagement";
            this.Text = "Functionality Management";
            this.Load += new System.EventHandler(this.FunctionalityManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdFunctionalities)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdFunctionalities;
    }
}