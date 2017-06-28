namespace HKSupply.Forms.Reports
{
    partial class BOMReport
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
            this.sbShowReport = new DevExpress.XtraEditors.SimpleButton();
            this.sbFilter = new DevExpress.XtraEditors.SimpleButton();
            this.xgrdList = new DevExpress.XtraGrid.GridControl();
            this.gridViewList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblModel = new DevExpress.XtraEditors.LabelControl();
            this.slueModel = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblItemBcn = new DevExpress.XtraEditors.LabelControl();
            this.lblSupplier = new DevExpress.XtraEditors.LabelControl();
            this.slueItem = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.slueSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(1338, 79);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Size = new System.Drawing.Size(1338, 31);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 79);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpList;
            this.xtcGeneral.Size = new System.Drawing.Size(1338, 489);
            this.xtcGeneral.TabIndex = 2;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpList});
            // 
            // xtpList
            // 
            this.xtpList.Controls.Add(this.sbShowReport);
            this.xtpList.Controls.Add(this.sbFilter);
            this.xtpList.Controls.Add(this.xgrdList);
            this.xtpList.Controls.Add(this.lblModel);
            this.xtpList.Controls.Add(this.slueModel);
            this.xtpList.Controls.Add(this.lblItemBcn);
            this.xtpList.Controls.Add(this.lblSupplier);
            this.xtpList.Controls.Add(this.slueItem);
            this.xtpList.Controls.Add(this.slueSupplier);
            this.xtpList.Name = "xtpList";
            this.xtpList.Size = new System.Drawing.Size(1332, 461);
            this.xtpList.Text = "List";
            // 
            // sbShowReport
            // 
            this.sbShowReport.Location = new System.Drawing.Point(1051, 7);
            this.sbShowReport.Name = "sbShowReport";
            this.sbShowReport.Size = new System.Drawing.Size(73, 26);
            this.sbShowReport.TabIndex = 16;
            this.sbShowReport.Text = "Show Report";
            this.sbShowReport.Click += new System.EventHandler(this.sbShowReport_Click);
            // 
            // sbFilter
            // 
            this.sbFilter.Location = new System.Drawing.Point(987, 7);
            this.sbFilter.Name = "sbFilter";
            this.sbFilter.Size = new System.Drawing.Size(58, 26);
            this.sbFilter.TabIndex = 15;
            this.sbFilter.Text = "Filter";
            this.sbFilter.Click += new System.EventHandler(this.sbFilter_Click);
            // 
            // xgrdList
            // 
            this.xgrdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgrdList.Location = new System.Drawing.Point(11, 47);
            this.xgrdList.MainView = this.gridViewList;
            this.xgrdList.MenuManager = this.ribbonControl;
            this.xgrdList.Name = "xgrdList";
            this.xgrdList.Size = new System.Drawing.Size(1314, 411);
            this.xgrdList.TabIndex = 14;
            this.xgrdList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
            // 
            // gridViewList
            // 
            this.gridViewList.GridControl = this.xgrdList;
            this.gridViewList.Name = "gridViewList";
            // 
            // lblModel
            // 
            this.lblModel.Location = new System.Drawing.Point(344, 13);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(28, 13);
            this.lblModel.TabIndex = 13;
            this.lblModel.Text = "Model";
            // 
            // slueModel
            // 
            this.slueModel.Location = new System.Drawing.Point(386, 10);
            this.slueModel.MenuManager = this.ribbonControl;
            this.slueModel.Name = "slueModel";
            this.slueModel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueModel.Properties.View = this.gridView2;
            this.slueModel.Size = new System.Drawing.Size(262, 20);
            this.slueModel.TabIndex = 11;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // lblItemBcn
            // 
            this.lblItemBcn.Location = new System.Drawing.Point(677, 13);
            this.lblItemBcn.Name = "lblItemBcn";
            this.lblItemBcn.Size = new System.Drawing.Size(22, 13);
            this.lblItemBcn.TabIndex = 9;
            this.lblItemBcn.Text = "Item";
            // 
            // lblSupplier
            // 
            this.lblSupplier.Location = new System.Drawing.Point(11, 13);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(38, 13);
            this.lblSupplier.TabIndex = 8;
            this.lblSupplier.Text = "Supplier";
            // 
            // slueItem
            // 
            this.slueItem.Location = new System.Drawing.Point(719, 10);
            this.slueItem.MenuManager = this.ribbonControl;
            this.slueItem.Name = "slueItem";
            this.slueItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueItem.Properties.View = this.gridView1;
            this.slueItem.Size = new System.Drawing.Size(262, 20);
            this.slueItem.TabIndex = 7;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // slueSupplier
            // 
            this.slueSupplier.Location = new System.Drawing.Point(65, 10);
            this.slueSupplier.MenuManager = this.ribbonControl;
            this.slueSupplier.Name = "slueSupplier";
            this.slueSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueSupplier.Properties.View = this.searchLookUpEdit1View;
            this.slueSupplier.Size = new System.Drawing.Size(262, 20);
            this.slueSupplier.TabIndex = 6;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // BOMReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 599);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "BOMReport";
            this.Text = "BOM Report";
            this.Load += new System.EventHandler(this.BOMReport_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpList.ResumeLayout(false);
            this.xtpList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpList;
        private DevExpress.XtraEditors.LabelControl lblModel;
        private DevExpress.XtraEditors.SearchLookUpEdit slueModel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl lblItemBcn;
        private DevExpress.XtraEditors.LabelControl lblSupplier;
        private DevExpress.XtraEditors.SearchLookUpEdit slueItem;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchLookUpEdit slueSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.GridControl xgrdList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewList;
        private DevExpress.XtraEditors.SimpleButton sbFilter;
        private DevExpress.XtraEditors.SimpleButton sbShowReport;
    }
}