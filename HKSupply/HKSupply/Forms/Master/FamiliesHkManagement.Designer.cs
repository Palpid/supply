namespace HKSupply.Forms.Master
{
    partial class FamiliesHkManagement
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
            this.xgrdFamiliesHk = new DevExpress.XtraGrid.GridControl();
            this.rootgridViewFamiliesHk = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdFamiliesHk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootgridViewFamiliesHk)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xgrdFamiliesHk
            // 
            this.xgrdFamiliesHk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdFamiliesHk.Location = new System.Drawing.Point(0, 143);
            this.xgrdFamiliesHk.MainView = this.rootgridViewFamiliesHk;
            this.xgrdFamiliesHk.MenuManager = this.ribbonControl;
            this.xgrdFamiliesHk.Name = "xgrdFamiliesHk";
            this.xgrdFamiliesHk.Size = new System.Drawing.Size(790, 425);
            this.xgrdFamiliesHk.TabIndex = 4;
            this.xgrdFamiliesHk.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootgridViewFamiliesHk});
            // 
            // rootgridViewFamiliesHk
            // 
            this.rootgridViewFamiliesHk.GridControl = this.xgrdFamiliesHk;
            this.rootgridViewFamiliesHk.Name = "rootgridViewFamiliesHk";
            // 
            // FamiliesHkManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xgrdFamiliesHk);
            this.Name = "FamiliesHkManagement";
            this.Text = "FamiliesHkManagement";
            this.Load += new System.EventHandler(this.FamiliesHkManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdFamiliesHk, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdFamiliesHk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootgridViewFamiliesHk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdFamiliesHk;
        private DevExpress.XtraGrid.Views.Grid.GridView rootgridViewFamiliesHk;
    }
}