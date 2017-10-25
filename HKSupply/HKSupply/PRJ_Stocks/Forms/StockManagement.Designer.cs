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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CB_Action = new System.Windows.Forms.ComboBox();
            this.CB_NewOwner = new System.Windows.Forms.ComboBox();
            this.CB_WareTypeDEST = new System.Windows.Forms.ComboBox();
            this.CB_WareDEST = new System.Windows.Forms.ComboBox();
            this.TxT_NewQTT = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GC_Stocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GC_Stocks
            // 
            this.GC_Stocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GC_Stocks.Location = new System.Drawing.Point(5, 4);
            this.GC_Stocks.MainView = this.gridView1;
            this.GC_Stocks.Name = "GC_Stocks";
            this.GC_Stocks.Size = new System.Drawing.Size(962, 528);
            this.GC_Stocks.TabIndex = 1;
            this.GC_Stocks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.GC_Stocks;
            this.gridView1.Name = "gridView1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.CB_Action);
            this.groupBox1.Controls.Add(this.CB_NewOwner);
            this.groupBox1.Controls.Add(this.CB_WareTypeDEST);
            this.groupBox1.Controls.Add(this.CB_WareDEST);
            this.groupBox1.Controls.Add(this.TxT_NewQTT);
            this.groupBox1.Location = new System.Drawing.Point(12, 538);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(947, 73);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // CB_Action
            // 
            this.CB_Action.FormattingEnabled = true;
            this.CB_Action.Location = new System.Drawing.Point(803, 19);
            this.CB_Action.Name = "CB_Action";
            this.CB_Action.Size = new System.Drawing.Size(121, 21);
            this.CB_Action.TabIndex = 6;
            // 
            // CB_NewOwner
            // 
            this.CB_NewOwner.FormattingEnabled = true;
            this.CB_NewOwner.Location = new System.Drawing.Point(485, 37);
            this.CB_NewOwner.Name = "CB_NewOwner";
            this.CB_NewOwner.Size = new System.Drawing.Size(121, 21);
            this.CB_NewOwner.TabIndex = 5;
            // 
            // CB_WareTypeDEST
            // 
            this.CB_WareTypeDEST.FormattingEnabled = true;
            this.CB_WareTypeDEST.Location = new System.Drawing.Point(193, 37);
            this.CB_WareTypeDEST.Name = "CB_WareTypeDEST";
            this.CB_WareTypeDEST.Size = new System.Drawing.Size(121, 21);
            this.CB_WareTypeDEST.TabIndex = 4;
            // 
            // CB_WareDEST
            // 
            this.CB_WareDEST.FormattingEnabled = true;
            this.CB_WareDEST.Location = new System.Drawing.Point(66, 37);
            this.CB_WareDEST.Name = "CB_WareDEST";
            this.CB_WareDEST.Size = new System.Drawing.Size(121, 21);
            this.CB_WareDEST.TabIndex = 3;
            // 
            // TxT_NewQTT
            // 
            this.TxT_NewQTT.Location = new System.Drawing.Point(612, 37);
            this.TxT_NewQTT.Name = "TxT_NewQTT";
            this.TxT_NewQTT.Size = new System.Drawing.Size(116, 20);
            this.TxT_NewQTT.TabIndex = 2;
            // 
            // StockManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 636);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GC_Stocks);
            this.Name = "StockManagement";
            this.Text = "StockManagment";
            this.Load += new System.EventHandler(this.StockManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GC_Stocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl GC_Stocks;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CB_Action;
        private System.Windows.Forms.ComboBox CB_NewOwner;
        private System.Windows.Forms.ComboBox CB_WareTypeDEST;
        private System.Windows.Forms.ComboBox CB_WareDEST;
        private System.Windows.Forms.TextBox TxT_NewQTT;
    }
}