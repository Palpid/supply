namespace HKSupply.Forms.Master
{
    partial class BoxManagement
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
            this.xtraTabPageBox = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdBoxes = new DevExpress.XtraGrid.GridControl();
            this.gridViewBoxes = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGeneral)).BeginInit();
            this.xtraTabControlGeneral.SuspendLayout();
            this.xtraTabPageBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdBoxes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBoxes)).BeginInit();
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
            // xtraTabControlGeneral
            // 
            this.xtraTabControlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlGeneral.Location = new System.Drawing.Point(0, 125);
            this.xtraTabControlGeneral.Name = "xtraTabControlGeneral";
            this.xtraTabControlGeneral.SelectedTabPage = this.xtraTabPageBox;
            this.xtraTabControlGeneral.Size = new System.Drawing.Size(1063, 447);
            this.xtraTabControlGeneral.TabIndex = 3;
            this.xtraTabControlGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageBox});
            // 
            // xtraTabPageBox
            // 
            this.xtraTabPageBox.Controls.Add(this.xgrdBoxes);
            this.xtraTabPageBox.Name = "xtraTabPageBox";
            this.xtraTabPageBox.Size = new System.Drawing.Size(1057, 419);
            this.xtraTabPageBox.Text = "Supply Boxes";
            // 
            // xgrdBoxes
            // 
            this.xgrdBoxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdBoxes.Location = new System.Drawing.Point(0, 0);
            this.xgrdBoxes.MainView = this.gridViewBoxes;
            this.xgrdBoxes.MenuManager = this.ribbonControl;
            this.xgrdBoxes.Name = "xgrdBoxes";
            this.xgrdBoxes.Size = new System.Drawing.Size(1057, 419);
            this.xgrdBoxes.TabIndex = 0;
            this.xgrdBoxes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBoxes});
            // 
            // gridViewBoxes
            // 
            this.gridViewBoxes.GridControl = this.xgrdBoxes;
            this.gridViewBoxes.Name = "gridViewBoxes";
            // 
            // BoxManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 599);
            this.Controls.Add(this.xtraTabControlGeneral);
            this.Name = "BoxManagement";
            this.Text = "BoxManagement";
            this.Load += new System.EventHandler(this.BoxManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtraTabControlGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlGeneral)).EndInit();
            this.xtraTabControlGeneral.ResumeLayout(false);
            this.xtraTabPageBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdBoxes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBoxes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControlGeneral;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageBox;
        private DevExpress.XtraGrid.GridControl xgrdBoxes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBoxes;
    }
}