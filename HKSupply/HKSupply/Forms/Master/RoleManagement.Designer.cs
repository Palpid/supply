namespace HKSupply.Forms.Master
{
    partial class RoleManagement
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
            this.grdRoles = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // grdRoles
            // 
            this.grdRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRoles.Location = new System.Drawing.Point(12, 50);
            this.grdRoles.Name = "grdRoles";
            this.grdRoles.ReadOnly = true;
            this.grdRoles.Size = new System.Drawing.Size(887, 424);
            this.grdRoles.TabIndex = 0;
            // 
            // RoleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(911, 486);
            this.Controls.Add(this.grdRoles);
            this.Name = "RoleManagement";
            this.Text = "RoleManagement";
            this.Load += new System.EventHandler(this.RoleManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdRoles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdRoles;
    }
}