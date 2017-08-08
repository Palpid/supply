namespace HKSupply.Forms.Master
{
    partial class SupplierFactoryCoeffManagement
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
            this.xgrdSupplierFactoryCoeff = new DevExpress.XtraGrid.GridControl();
            this.gridViewSupplierFactoryCoeff = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdSupplierFactoryCoeff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSupplierFactoryCoeff)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // xgrdSupplierFactoryCoeff
            // 
            this.xgrdSupplierFactoryCoeff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdSupplierFactoryCoeff.Location = new System.Drawing.Point(0, 79);
            this.xgrdSupplierFactoryCoeff.MainView = this.gridViewSupplierFactoryCoeff;
            this.xgrdSupplierFactoryCoeff.MenuManager = this.ribbonControl;
            this.xgrdSupplierFactoryCoeff.Name = "xgrdSupplierFactoryCoeff";
            this.xgrdSupplierFactoryCoeff.Size = new System.Drawing.Size(790, 489);
            this.xgrdSupplierFactoryCoeff.TabIndex = 2;
            this.xgrdSupplierFactoryCoeff.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSupplierFactoryCoeff});
            // 
            // gridViewSupplierFactoryCoeff
            // 
            this.gridViewSupplierFactoryCoeff.GridControl = this.xgrdSupplierFactoryCoeff;
            this.gridViewSupplierFactoryCoeff.Name = "gridViewSupplierFactoryCoeff";
            // 
            // SupplierFactoryCoeffManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xgrdSupplierFactoryCoeff);
            this.Name = "SupplierFactoryCoeffManagement";
            this.Text = "SupplierFactoryCoeffManagement";
            this.Load += new System.EventHandler(this.SupplierFactoryCoeffManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdSupplierFactoryCoeff, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdSupplierFactoryCoeff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSupplierFactoryCoeff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdSupplierFactoryCoeff;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSupplierFactoryCoeff;
    }
}