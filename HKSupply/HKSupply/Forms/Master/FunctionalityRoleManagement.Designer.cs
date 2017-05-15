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
            this.ribbonControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // xgrdFuncRoles
            // 
            this.xgrdFuncRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdFuncRoles.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xgrdFuncRoles.Location = new System.Drawing.Point(0, 79);
            this.xgrdFuncRoles.MainView = this.rootGridViewFuncRoles;
            this.xgrdFuncRoles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xgrdFuncRoles.MenuManager = this.ribbonControl;
            this.xgrdFuncRoles.Name = "xgrdFuncRoles";
            this.xgrdFuncRoles.Size = new System.Drawing.Size(790, 489);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xgrdFuncRoles);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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