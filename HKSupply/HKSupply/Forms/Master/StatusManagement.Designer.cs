namespace HKSupply.Forms.Master
{
    partial class StatusManagement
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
            this.xtraTabPageStatusHK = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdStatusHK = new DevExpress.XtraGrid.GridControl();
            this.gridViewStatusHK = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPageStatusCial = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdStatusCial = new DevExpress.XtraGrid.GridControl();
            this.gridViewStatusCial = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGeneral)).BeginInit();
            this.xtraTabControlGeneral.SuspendLayout();
            this.xtraTabPageStatusHK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdStatusHK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStatusHK)).BeginInit();
            this.xtraTabPageStatusCial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdStatusCial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStatusCial)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xtraTabControlGeneral
            // 
            this.xtraTabControlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlGeneral.Location = new System.Drawing.Point(0, 143);
            this.xtraTabControlGeneral.Name = "xtraTabControlGeneral";
            this.xtraTabControlGeneral.SelectedTabPage = this.xtraTabPageStatusHK;
            this.xtraTabControlGeneral.Size = new System.Drawing.Size(790, 425);
            this.xtraTabControlGeneral.TabIndex = 3;
            this.xtraTabControlGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageStatusHK,
            this.xtraTabPageStatusCial});
            // 
            // xtraTabPageStatusHK
            // 
            this.xtraTabPageStatusHK.Controls.Add(this.xgrdStatusHK);
            this.xtraTabPageStatusHK.Name = "xtraTabPageStatusHK";
            this.xtraTabPageStatusHK.Size = new System.Drawing.Size(784, 397);
            this.xtraTabPageStatusHK.Text = "Status HK";
            // 
            // xgrdStatusHK
            // 
            this.xgrdStatusHK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdStatusHK.Location = new System.Drawing.Point(0, 0);
            this.xgrdStatusHK.MainView = this.gridViewStatusHK;
            this.xgrdStatusHK.MenuManager = this.ribbonControl;
            this.xgrdStatusHK.Name = "xgrdStatusHK";
            this.xgrdStatusHK.Size = new System.Drawing.Size(784, 397);
            this.xgrdStatusHK.TabIndex = 0;
            this.xgrdStatusHK.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStatusHK});
            // 
            // gridViewStatusHK
            // 
            this.gridViewStatusHK.GridControl = this.xgrdStatusHK;
            this.gridViewStatusHK.Name = "gridViewStatusHK";
            // 
            // xtraTabPageStatusCial
            // 
            this.xtraTabPageStatusCial.Controls.Add(this.xgrdStatusCial);
            this.xtraTabPageStatusCial.Name = "xtraTabPageStatusCial";
            this.xtraTabPageStatusCial.Size = new System.Drawing.Size(784, 397);
            this.xtraTabPageStatusCial.Text = "Status Cial.";
            // 
            // xgrdStatusCial
            // 
            this.xgrdStatusCial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdStatusCial.Location = new System.Drawing.Point(0, 0);
            this.xgrdStatusCial.MainView = this.gridViewStatusCial;
            this.xgrdStatusCial.MenuManager = this.ribbonControl;
            this.xgrdStatusCial.Name = "xgrdStatusCial";
            this.xgrdStatusCial.Size = new System.Drawing.Size(784, 397);
            this.xgrdStatusCial.TabIndex = 1;
            this.xgrdStatusCial.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStatusCial});
            // 
            // gridViewStatusCial
            // 
            this.gridViewStatusCial.GridControl = this.xgrdStatusCial;
            this.gridViewStatusCial.Name = "gridViewStatusCial";
            // 
            // StatusManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xtraTabControlGeneral);
            this.Name = "StatusManagement";
            this.Text = "StatusManagement";
            this.Load += new System.EventHandler(this.StatusManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtraTabControlGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGeneral)).EndInit();
            this.xtraTabControlGeneral.ResumeLayout(false);
            this.xtraTabPageStatusHK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdStatusHK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStatusHK)).EndInit();
            this.xtraTabPageStatusCial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdStatusCial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStatusCial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControlGeneral;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageStatusHK;
        private DevExpress.XtraGrid.GridControl xgrdStatusHK;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewStatusHK;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageStatusCial;
        private DevExpress.XtraGrid.GridControl xgrdStatusCial;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewStatusCial;
    }
}