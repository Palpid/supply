namespace HKSupply.Forms.Master
{
    partial class FunctionalityRoleManagement
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
            this.xgrdFuncRoles = new DevExpress.XtraGrid.GridControl();
            this.rootGridViewFuncRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdFuncRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewFuncRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xgrdFuncRoles
            // 
            this.xgrdFuncRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdFuncRoles.Location = new System.Drawing.Point(12, 186);
            this.xgrdFuncRoles.MainView = this.rootGridViewFuncRoles;
            this.xgrdFuncRoles.MenuManager = this.ribbonControl;
            this.xgrdFuncRoles.Name = "xgrdFuncRoles";
            this.xgrdFuncRoles.Size = new System.Drawing.Size(898, 504);
            this.xgrdFuncRoles.TabIndex = 2;
            this.xgrdFuncRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootGridViewFuncRoles});
            // 
            // rootGridViewFuncRoles
            // 
            this.rootGridViewFuncRoles.GridControl = this.xgrdFuncRoles;
            this.rootGridViewFuncRoles.Name = "rootGridViewFuncRoles";
            // 
            // FunctionalityRoleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 737);
            this.Controls.Add(this.xgrdFuncRoles);
            this.Name = "FunctionalityRoleManagement";
            this.Text = "FunctionalityRoleManagement";
            this.Load += new System.EventHandler(this.FunctionalityRoleManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdFuncRoles, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdFuncRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewFuncRoles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdFuncRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView rootGridViewFuncRoles;
    }
}