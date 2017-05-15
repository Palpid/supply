namespace HKSupply.Forms.Master
{
    partial class FunctionalityRoleManagement_v1
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
            this.grdFuncRoles = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdFuncRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // grdFuncRoles
            // 
            this.grdFuncRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFuncRoles.BackgroundColor = System.Drawing.Color.White;
            this.grdFuncRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFuncRoles.Location = new System.Drawing.Point(16, 62);
            this.grdFuncRoles.Name = "grdFuncRoles";
            this.grdFuncRoles.RowTemplate.Height = 24;
            this.grdFuncRoles.Size = new System.Drawing.Size(1183, 522);
            this.grdFuncRoles.TabIndex = 1;
            // 
            // FunctionalityRoleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1215, 598);
            this.Controls.Add(this.grdFuncRoles);
            this.Name = "FunctionalityRoleManagement_v1";
            this.Text = "Functionality Role Management v1";
            this.Load += new System.EventHandler(this.FunctionalityRoleManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdFuncRoles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdFuncRoles;
    }
}