namespace HKSupply.Forms.Master
{
    partial class UserManagement
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
            this.xgrdUsers = new DevExpress.XtraGrid.GridControl();
            this.rootGridViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xgrdUsers
            // 
            this.xgrdUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdUsers.Location = new System.Drawing.Point(12, 186);
            this.xgrdUsers.MainView = this.rootGridViewUsers;
            this.xgrdUsers.MenuManager = this.ribbonControl;
            this.xgrdUsers.Name = "xgrdUsers";
            this.xgrdUsers.Size = new System.Drawing.Size(898, 504);
            this.xgrdUsers.TabIndex = 2;
            this.xgrdUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootGridViewUsers});
            // 
            // rootGridViewUsers
            // 
            this.rootGridViewUsers.GridControl = this.xgrdUsers;
            this.rootGridViewUsers.Name = "rootGridViewUsers";
            // 
            // UserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 737);
            this.Controls.Add(this.xgrdUsers);
            this.Name = "UserManagement";
            this.Text = "User Management";
            this.Load += new System.EventHandler(this.UserManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdUsers, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView rootGridViewUsers;
    }
}