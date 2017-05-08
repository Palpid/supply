namespace HKSupply.Forms.Master
{
    partial class FunctionalityManagement
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
            this.xgrdFunctionalities = new DevExpress.XtraGrid.GridControl();
            this.rootGridViewFunctionalities = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdFunctionalities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewFunctionalities)).BeginInit();
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
            // xgrdFunctionalities
            // 
            this.xgrdFunctionalities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdFunctionalities.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xgrdFunctionalities.Location = new System.Drawing.Point(0, 79);
            this.xgrdFunctionalities.MainView = this.rootGridViewFunctionalities;
            this.xgrdFunctionalities.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xgrdFunctionalities.MenuManager = this.ribbonControl;
            this.xgrdFunctionalities.Name = "xgrdFunctionalities";
            this.xgrdFunctionalities.Size = new System.Drawing.Size(790, 489);
            this.xgrdFunctionalities.TabIndex = 2;
            this.xgrdFunctionalities.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootGridViewFunctionalities});
            // 
            // rootGridViewFunctionalities
            // 
            this.rootGridViewFunctionalities.GridControl = this.xgrdFunctionalities;
            this.rootGridViewFunctionalities.Name = "rootGridViewFunctionalities";
            // 
            // FunctionalityManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xgrdFunctionalities);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FunctionalityManagement";
            this.Text = "FunctionalityManagement";
            this.Load += new System.EventHandler(this.FunctionalityManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdFunctionalities, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdFunctionalities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewFunctionalities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdFunctionalities;
        private DevExpress.XtraGrid.Views.Grid.GridView rootGridViewFunctionalities;
    }
}