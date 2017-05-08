namespace HKSupply.Forms.Master
{
    partial class PrototypeManagement
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
            this.xtcGeneral = new DevExpress.XtraTab.XtraTabControl();
            this.xtpList = new DevExpress.XtraTab.XtraTabPage();
            this.xgrdProtos = new DevExpress.XtraGrid.GridControl();
            this.rootGridViewProtos = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdProtos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewProtos)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 143);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpList;
            this.xtcGeneral.Size = new System.Drawing.Size(790, 425);
            this.xtcGeneral.TabIndex = 2;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpList});
            // 
            // xtpList
            // 
            this.xtpList.Controls.Add(this.xgrdProtos);
            this.xtpList.Name = "xtpList";
            this.xtpList.Size = new System.Drawing.Size(784, 397);
            this.xtpList.Text = "List";
            // 
            // xgrdProtos
            // 
            this.xgrdProtos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgrdProtos.Location = new System.Drawing.Point(0, 0);
            this.xgrdProtos.MainView = this.rootGridViewProtos;
            this.xgrdProtos.MenuManager = this.ribbonControl;
            this.xgrdProtos.Name = "xgrdProtos";
            this.xgrdProtos.Size = new System.Drawing.Size(784, 397);
            this.xgrdProtos.TabIndex = 0;
            this.xgrdProtos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.rootGridViewProtos});
            // 
            // rootGridViewProtos
            // 
            this.rootGridViewProtos.GridControl = this.xgrdProtos;
            this.rootGridViewProtos.Name = "rootGridViewProtos";
            // 
            // PrototypeManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 599);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "PrototypeManagement";
            this.Text = "Prototype Management";
            this.Load += new System.EventHandler(this.PrototypesManagement_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdProtos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGridViewProtos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpList;
        private DevExpress.XtraGrid.GridControl xgrdProtos;
        private DevExpress.XtraGrid.Views.Grid.GridView rootGridViewProtos;
    }
}