namespace HKSupply.Forms.Master
{
    partial class IncotermsManagement
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
            this.xgrdIncoterms = new DevExpress.XtraGrid.GridControl();
            this.rootgridViewIncoterms = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdIncoterms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootgridViewIncoterms)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xgrdIncoterms
            // 
            this.xgrdIncoterms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdIncoterms.Location = new System.Drawing.Point(0, 143);
            this.xgrdIncoterms.MainView = this.rootgridViewIncoterms;
            this.xgrdIncoterms.MenuManager = this.ribbonControl;
            this.xgrdIncoterms.Name = "xgrdIncoterms";
            this.xgrdIncoterms.Size = new System.Drawing.Size(790, 425);
            this.xgrdIncoterms.TabIndex = 3;
            this.xgrdIncoterms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootgridViewIncoterms});
            // 
            // rootgridViewIncoterms
            // 
            this.rootgridViewIncoterms.GridControl = this.xgrdIncoterms;
            this.rootgridViewIncoterms.Name = "rootgridViewIncoterms";
            // 
            // IncotermsManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xgrdIncoterms);
            this.Name = "IncotermsManagement";
            this.Text = "IncotermsManagement";
            this.Load += new System.EventHandler(this.IncotermsManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xgrdIncoterms, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdIncoterms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootgridViewIncoterms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl xgrdIncoterms;
        private DevExpress.XtraGrid.Views.Grid.GridView rootgridViewIncoterms;
    }
}