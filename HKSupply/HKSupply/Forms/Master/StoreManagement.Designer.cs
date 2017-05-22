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
            this.ribbonControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // xgrdStores
            // 
            this.xgrdStores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdStores.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xgrdStores.Location = new System.Drawing.Point(0, 79);
            this.xgrdStores.MainView = this.rootGridViewStores;
            this.xgrdStores.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xgrdStores.MenuManager = this.ribbonControl;
            this.xgrdStores.Name = "xgrdStores";
            this.xgrdStores.Size = new System.Drawing.Size(790, 489);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xgrdStores);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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