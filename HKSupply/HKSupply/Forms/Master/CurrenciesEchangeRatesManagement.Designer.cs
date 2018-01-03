namespace HKSupply.Forms.Master
{
    partial class CurrenciesEchangeRatesManagement
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
            this.xtraTabControlGeneral = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageCurrency = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdCurrencies = new DevExpress.XtraGrid.GridControl();
            this.gridViewCurrencies = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPageEchangeRates = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdEchangeRates = new DevExpress.XtraGrid.GridControl();
            this.gridViewEchangeRates = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGeneral)).BeginInit();
            this.xtraTabControlGeneral.SuspendLayout();
            this.xtraTabPageCurrency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdCurrencies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCurrencies)).BeginInit();
            this.xtraTabPageEchangeRates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdEchangeRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEchangeRates)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(790, 125);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Size = new System.Drawing.Size(790, 27);
            // 
            // xtraTabControlGeneral
            // 
            this.xtraTabControlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlGeneral.Location = new System.Drawing.Point(0, 125);
            this.xtraTabControlGeneral.Name = "xtraTabControlGeneral";
            this.xtraTabControlGeneral.SelectedTabPage = this.xtraTabPageCurrency;
            this.xtraTabControlGeneral.Size = new System.Drawing.Size(790, 447);
            this.xtraTabControlGeneral.TabIndex = 2;
            this.xtraTabControlGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageCurrency,
            this.xtraTabPageEchangeRates});
            // 
            // xtraTabPageCurrency
            // 
            this.xtraTabPageCurrency.Controls.Add(this.xgrdCurrencies);
            this.xtraTabPageCurrency.Name = "xtraTabPageCurrency";
            this.xtraTabPageCurrency.Size = new System.Drawing.Size(784, 419);
            this.xtraTabPageCurrency.Text = "Currency";
            // 
            // xgrdCurrencies
            // 
            this.xgrdCurrencies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdCurrencies.Location = new System.Drawing.Point(0, 0);
            this.xgrdCurrencies.MainView = this.gridViewCurrencies;
            this.xgrdCurrencies.MenuManager = this.ribbonControl;
            this.xgrdCurrencies.Name = "xgrdCurrencies";
            this.xgrdCurrencies.Size = new System.Drawing.Size(784, 419);
            this.xgrdCurrencies.TabIndex = 0;
            this.xgrdCurrencies.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCurrencies});
            // 
            // gridViewCurrencies
            // 
            this.gridViewCurrencies.GridControl = this.xgrdCurrencies;
            this.gridViewCurrencies.Name = "gridViewCurrencies";
            // 
            // xtraTabPageEchangeRates
            // 
            this.xtraTabPageEchangeRates.Controls.Add(this.xgrdEchangeRates);
            this.xtraTabPageEchangeRates.Name = "xtraTabPageEchangeRates";
            this.xtraTabPageEchangeRates.Size = new System.Drawing.Size(784, 397);
            this.xtraTabPageEchangeRates.Text = "Echange Rates";
            // 
            // xgrdEchangeRates
            // 
            this.xgrdEchangeRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdEchangeRates.Location = new System.Drawing.Point(0, 0);
            this.xgrdEchangeRates.MainView = this.gridViewEchangeRates;
            this.xgrdEchangeRates.MenuManager = this.ribbonControl;
            this.xgrdEchangeRates.Name = "xgrdEchangeRates";
            this.xgrdEchangeRates.Size = new System.Drawing.Size(784, 397);
            this.xgrdEchangeRates.TabIndex = 1;
            this.xgrdEchangeRates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEchangeRates});
            // 
            // gridViewEchangeRates
            // 
            this.gridViewEchangeRates.GridControl = this.xgrdEchangeRates;
            this.gridViewEchangeRates.Name = "gridViewEchangeRates";
            // 
            // CurrenciesEchangeRatesManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xtraTabControlGeneral);
            this.Name = "CurrenciesEchangeRatesManagement";
            this.Text = "CurrenciesEchangeRatesManagement";
            this.Load += new System.EventHandler(this.CurrenciesEchangeRatesManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtraTabControlGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGeneral)).EndInit();
            this.xtraTabControlGeneral.ResumeLayout(false);
            this.xtraTabPageCurrency.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdCurrencies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCurrencies)).EndInit();
            this.xtraTabPageEchangeRates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdEchangeRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEchangeRates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControlGeneral;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageCurrency;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageEchangeRates;
        private DevExpress.XtraGrid.GridControl xgrdCurrencies;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCurrencies;
        private DevExpress.XtraGrid.GridControl xgrdEchangeRates;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewEchangeRates;
    }
}