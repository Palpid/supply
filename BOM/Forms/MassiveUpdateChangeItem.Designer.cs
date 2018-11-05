namespace BOM.Forms
{
    partial class MassiveUpdateChangeItem
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
            this.slueOriginalItem = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblOriginalItem = new DevExpress.XtraEditors.LabelControl();
            this.lblChangeTo = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.slueChangeItem = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtOriginalItemName = new DevExpress.XtraEditors.TextEdit();
            this.txtChangeItemName = new DevExpress.XtraEditors.TextEdit();
            this.grdBom = new DevExpress.XtraGrid.GridControl();
            this.gridViewBom = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.slueOriginalItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueChangeItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOriginalItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChangeItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBom)).BeginInit();
            this.SuspendLayout();
            // 
            // slueOriginalItem
            // 
            this.slueOriginalItem.Location = new System.Drawing.Point(111, 9);
            this.slueOriginalItem.Name = "slueOriginalItem";
            this.slueOriginalItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueOriginalItem.Properties.PopupView = this.searchLookUpEdit1View;
            this.slueOriginalItem.Size = new System.Drawing.Size(207, 20);
            this.slueOriginalItem.TabIndex = 0;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // lblOriginalItem
            // 
            this.lblOriginalItem.Location = new System.Drawing.Point(12, 12);
            this.lblOriginalItem.Name = "lblOriginalItem";
            this.lblOriginalItem.Size = new System.Drawing.Size(61, 13);
            this.lblOriginalItem.TabIndex = 2;
            this.lblOriginalItem.Text = "Original Item";
            // 
            // lblChangeTo
            // 
            this.lblChangeTo.Location = new System.Drawing.Point(12, 54);
            this.lblChangeTo.Name = "lblChangeTo";
            this.lblChangeTo.Size = new System.Drawing.Size(52, 13);
            this.lblChangeTo.TabIndex = 3;
            this.lblChangeTo.Text = "Change To";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(629, 77);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            // 
            // slueChangeItem
            // 
            this.slueChangeItem.Location = new System.Drawing.Point(111, 51);
            this.slueChangeItem.Name = "slueChangeItem";
            this.slueChangeItem.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueChangeItem.Properties.PopupView = this.gridView1;
            this.slueChangeItem.Size = new System.Drawing.Size(207, 20);
            this.slueChangeItem.TabIndex = 5;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // txtOriginalItemName
            // 
            this.txtOriginalItemName.Location = new System.Drawing.Point(324, 9);
            this.txtOriginalItemName.Name = "txtOriginalItemName";
            this.txtOriginalItemName.Properties.ReadOnly = true;
            this.txtOriginalItemName.Properties.UseReadOnlyAppearance = false;
            this.txtOriginalItemName.Size = new System.Drawing.Size(425, 20);
            this.txtOriginalItemName.TabIndex = 6;
            // 
            // txtChangeItemName
            // 
            this.txtChangeItemName.Location = new System.Drawing.Point(324, 51);
            this.txtChangeItemName.Name = "txtChangeItemName";
            this.txtChangeItemName.Properties.ReadOnly = true;
            this.txtChangeItemName.Properties.UseReadOnlyAppearance = false;
            this.txtChangeItemName.Size = new System.Drawing.Size(425, 20);
            this.txtChangeItemName.TabIndex = 7;
            // 
            // grdBom
            // 
            this.grdBom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdBom.Location = new System.Drawing.Point(12, 107);
            this.grdBom.MainView = this.gridViewBom;
            this.grdBom.Name = "grdBom";
            this.grdBom.Size = new System.Drawing.Size(738, 322);
            this.grdBom.TabIndex = 8;
            this.grdBom.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBom});
            // 
            // gridViewBom
            // 
            this.gridViewBom.GridControl = this.grdBom;
            this.gridViewBom.Name = "gridViewBom";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(502, 77);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(121, 24);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            // 
            // MassiveUpdateChangeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 454);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.grdBom);
            this.Controls.Add(this.txtChangeItemName);
            this.Controls.Add(this.txtOriginalItemName);
            this.Controls.Add(this.slueChangeItem);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblChangeTo);
            this.Controls.Add(this.lblOriginalItem);
            this.Controls.Add(this.slueOriginalItem);
            this.Name = "MassiveUpdateChangeItem";
            this.Text = "Massive Update";
            ((System.ComponentModel.ISupportInitialize)(this.slueOriginalItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueChangeItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOriginalItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChangeItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit slueOriginalItem;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl lblOriginalItem;
        private DevExpress.XtraEditors.LabelControl lblChangeTo;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SearchLookUpEdit slueChangeItem;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtOriginalItemName;
        private DevExpress.XtraEditors.TextEdit txtChangeItemName;
        private DevExpress.XtraGrid.GridControl grdBom;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBom;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}