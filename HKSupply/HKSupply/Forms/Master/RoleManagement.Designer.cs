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
            this.xgrdRoles = new DevExpress.XtraGrid.GridControl();
            this.rootGridViewRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xgrdRoles
            // 
            this.xgrdRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdRoles.Location = new System.Drawing.Point(12, 186);
            this.xgrdRoles.MainView = this.rootGridViewRoles;
            this.xgrdRoles.MenuManager = this.ribbonControl;
            this.xgrdRoles.Name = "xgrdRoles";
            this.xgrdRoles.Size = new System.Drawing.Size(898, 504);
            this.xgrdRoles.TabIndex = 2;
            this.xgrdRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootGridViewRoles});
            // 
            // rootGridViewRoles
            // 
            this.rootGridViewRoles.GridControl = this.xgrdRoles;
            this.rootGridViewRoles.Name = "rootGridViewRoles";
            // 
            // RoleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 737);
            this.Controls.Add(this.xgrdRoles);
            this.Name = "RoleManagement";
            this.Text = "Role Management";
            this.Load += new System.EventHandler(this.RoleManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdRoles, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewRoles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView rootGridViewRoles;
    }
}