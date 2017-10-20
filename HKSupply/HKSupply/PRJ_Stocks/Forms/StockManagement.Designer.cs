namespace HKSupply.PRJ_Stocks.Forms
{
    partial class StockManagement
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
            this.GC_Stocks = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GC_Stocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // GC_Stocks
            // 
            this.GC_Stocks.Location = new System.Drawing.Point(4, 124);
            this.GC_Stocks.MainView = this.gridView1;
            this.GC_Stocks.Name = "GC_Stocks";
            this.GC_Stocks.Size = new System.Drawing.Size(964, 509);
            this.GC_Stocks.TabIndex = 1;
            this.GC_Stocks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.GC_Stocks;
            this.gridView1.Name = "gridView1";
            // 
            // StockManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 636);
            this.Controls.Add(this.GC_Stocks);
            this.Name = "StockManagement";
            this.Text = "StockManagment";
            this.Load += new System.EventHandler(this.StockManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GC_Stocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl GC_Stocks;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}