namespace HKSupply.Forms.Master
{
    partial class BomBreakdownManagement
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
            this.xgrdBomBreakdown = new DevExpress.XtraGrid.GridControl();
            this.gridViewBomBreakdown = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdBomBreakdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBomBreakdown)).BeginInit();
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
            // xgrdBomBreakdown
            // 
            this.xgrdBomBreakdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdBomBreakdown.Location = new System.Drawing.Point(0, 79);
            this.xgrdBomBreakdown.MainView = this.gridViewBomBreakdown;
            this.xgrdBomBreakdown.MenuManager = this.ribbonControl;
            this.xgrdBomBreakdown.Name = "xgrdBomBreakdown";
            this.xgrdBomBreakdown.Size = new System.Drawing.Size(790, 489);
            this.xgrdBomBreakdown.TabIndex = 2;
            this.xgrdBomBreakdown.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBomBreakdown});
            // 
            // gridViewBomBreakdown
            // 
            this.gridViewBomBreakdown.GridControl = this.xgrdBomBreakdown;
            this.gridViewBomBreakdown.Name = "gridViewBomBreakdown";
            // 
            // BomBreakdownManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xgrdBomBreakdown);
            this.Name = "BomBreakdownManagement";
            this.Text = "BomBreakdownManagement";
            this.Load += new System.EventHandler(this.BomBreakdownManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdBomBreakdown, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdBomBreakdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBomBreakdown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdBomBreakdown;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBomBreakdown;
    }
}