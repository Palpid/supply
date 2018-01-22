namespace HKSupply.Forms.Supply.DialogForms
{
    partial class AddReceiptItem
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
            this.xtcPK = new DevExpress.XtraTab.XtraTabControl();
            this.xtpPOSelection = new DevExpress.XtraTab.XtraTabPage();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.xgrdPoSelection = new DevExpress.XtraGrid.GridControl();
            this.gridViewPoSelection = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xgrdLinesPoSelection = new DevExpress.XtraGrid.GridControl();
            this.gridViewLinesPoSelection = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.xtcPK)).BeginInit();
            this.xtcPK.SuspendLayout();
            this.xtpPOSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdPoSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPoSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLinesPoSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLinesPoSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // xtcPK
            // 
            this.xtcPK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcPK.Location = new System.Drawing.Point(0, 0);
            this.xtcPK.Name = "xtcPK";
            this.xtcPK.SelectedTabPage = this.xtpPOSelection;
            this.xtcPK.Size = new System.Drawing.Size(1017, 541);
            this.xtcPK.TabIndex = 5;
            this.xtcPK.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpPOSelection});
            // 
            // xtpPOSelection
            // 
            this.xtpPOSelection.Controls.Add(this.sbOk);
            this.xtpPOSelection.Controls.Add(this.sbCancel);
            this.xtpPOSelection.Controls.Add(this.xgrdPoSelection);
            this.xtpPOSelection.Controls.Add(this.xgrdLinesPoSelection);
            this.xtpPOSelection.Name = "xtpPOSelection";
            this.xtpPOSelection.Size = new System.Drawing.Size(1011, 513);
            this.xtpPOSelection.Text = "PO Selection";
            // 
            // sbOk
            // 
            this.sbOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbOk.Location = new System.Drawing.Point(701, 446);
            this.sbOk.Margin = new System.Windows.Forms.Padding(2);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(124, 21);
            this.sbOk.TabIndex = 3;
            this.sbOk.Text = "OK";
            // 
            // sbCancel
            // 
            this.sbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbCancel.Location = new System.Drawing.Point(829, 446);
            this.sbCancel.Margin = new System.Windows.Forms.Padding(2);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(124, 21);
            this.sbCancel.TabIndex = 2;
            this.sbCancel.Text = "Cancel";
            // 
            // xgrdPoSelection
            // 
            this.xgrdPoSelection.Location = new System.Drawing.Point(3, 3);
            this.xgrdPoSelection.MainView = this.gridViewPoSelection;
            this.xgrdPoSelection.Name = "xgrdPoSelection";
            this.xgrdPoSelection.Size = new System.Drawing.Size(886, 114);
            this.xgrdPoSelection.TabIndex = 1;
            this.xgrdPoSelection.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPoSelection});
            // 
            // gridViewPoSelection
            // 
            this.gridViewPoSelection.GridControl = this.xgrdPoSelection;
            this.gridViewPoSelection.Name = "gridViewPoSelection";
            // 
            // xgrdLinesPoSelection
            // 
            this.xgrdLinesPoSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdLinesPoSelection.Location = new System.Drawing.Point(3, 123);
            this.xgrdLinesPoSelection.MainView = this.gridViewLinesPoSelection;
            this.xgrdLinesPoSelection.Name = "xgrdLinesPoSelection";
            this.xgrdLinesPoSelection.Size = new System.Drawing.Size(1005, 318);
            this.xgrdLinesPoSelection.TabIndex = 0;
            this.xgrdLinesPoSelection.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLinesPoSelection});
            // 
            // gridViewLinesPoSelection
            // 
            this.gridViewLinesPoSelection.GridControl = this.xgrdLinesPoSelection;
            this.gridViewLinesPoSelection.Name = "gridViewLinesPoSelection";
            // 
            // AddReceiptItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 541);
            this.Controls.Add(this.xtcPK);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AddReceiptItem";
            this.Text = "AddReceiptItem";
            this.Load += new System.EventHandler(this.AddReceiptItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtcPK)).EndInit();
            this.xtcPK.ResumeLayout(false);
            this.xtpPOSelection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdPoSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPoSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdLinesPoSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLinesPoSelection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcPK;
        private DevExpress.XtraTab.XtraTabPage xtpPOSelection;
        private DevExpress.XtraGrid.GridControl xgrdPoSelection;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPoSelection;
        private DevExpress.XtraGrid.GridControl xgrdLinesPoSelection;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLinesPoSelection;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraEditors.SimpleButton sbOk;
    }
}