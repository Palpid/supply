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
            this.xgrdStores = new DevExpress.XtraGrid.GridControl();
            this.rootGridViewStores = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdStores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewStores)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xgrdStores
            // 
            this.xgrdStores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdStores.Location = new System.Drawing.Point(12, 186);
            this.xgrdStores.MainView = this.rootGridViewStores;
            this.xgrdStores.MenuManager = this.ribbonControl;
            this.xgrdStores.Name = "xgrdStores";
            this.xgrdStores.Size = new System.Drawing.Size(898, 504);
            this.xgrdStores.TabIndex = 2;
            this.xgrdStores.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootGridViewStores});
            // 
            // rootGridViewStores
            // 
            this.rootGridViewStores.GridControl = this.xgrdStores;
            this.rootGridViewStores.Name = "rootGridViewStores";
            // 
            // StoreManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 737);
            this.Controls.Add(this.xgrdStores);
            this.Name = "StoreManagement";
            this.Text = "StoreManagement";
            this.Load += new System.EventHandler(this.StoreManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdStores, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdStores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewStores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdStores;
        private DevExpress.XtraGrid.Views.Grid.GridView rootGridViewStores;
    }
}