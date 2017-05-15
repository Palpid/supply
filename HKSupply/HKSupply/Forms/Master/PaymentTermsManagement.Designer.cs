namespace HKSupply.Forms.Master
{
    partial class PaymentTermsManagement
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
            this.xgrdPaymentTerms = new DevExpress.XtraGrid.GridControl();
            this.rootgridViewPaymentTerms = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdPaymentTerms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootgridViewPaymentTerms)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xgrdPaymentTerms
            // 
            this.xgrdPaymentTerms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdPaymentTerms.Location = new System.Drawing.Point(0, 143);
            this.xgrdPaymentTerms.MainView = this.rootgridViewPaymentTerms;
            this.xgrdPaymentTerms.MenuManager = this.ribbonControl;
            this.xgrdPaymentTerms.Name = "xgrdPaymentTerms";
            this.xgrdPaymentTerms.Size = new System.Drawing.Size(790, 425);
            this.xgrdPaymentTerms.TabIndex = 2;
            this.xgrdPaymentTerms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootgridViewPaymentTerms});
            // 
            // rootgridViewPaymentTerms
            // 
            this.rootgridViewPaymentTerms.GridControl = this.xgrdPaymentTerms;
            this.rootgridViewPaymentTerms.Name = "rootgridViewPaymentTerms";
            // 
            // PaymentTermsManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xgrdPaymentTerms);
            this.Name = "PaymentTermsManagement";
            this.Text = "Payment Terms Management";
            this.Load += new System.EventHandler(this.PaymentTermsManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdPaymentTerms, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdPaymentTerms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootgridViewPaymentTerms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdPaymentTerms;
        private DevExpress.XtraGrid.Views.Grid.GridView rootgridViewPaymentTerms;
    }
}