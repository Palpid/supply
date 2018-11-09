namespace BOM.Forms.DialogForms
{
    partial class SelectBomClone
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
            this.lblFactory = new DevExpress.XtraEditors.LabelControl();
            this.checkedListBoxFactories = new System.Windows.Forms.CheckedListBox();
            this.lblSKU = new DevExpress.XtraEditors.LabelControl();
            this.slueItemSource = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdBomFactory = new DevExpress.XtraGrid.GridControl();
            this.gridViewBomFactory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sbOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.slueItemSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBomFactory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBomFactory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFactory
            // 
            this.lblFactory.Location = new System.Drawing.Point(12, 12);
            this.lblFactory.Name = "lblFactory";
            this.lblFactory.Size = new System.Drawing.Size(37, 13);
            this.lblFactory.TabIndex = 12;
            this.lblFactory.Text = "Factory";
            // 
            // checkedListBoxFactories
            // 
            this.checkedListBoxFactories.FormattingEnabled = true;
            this.checkedListBoxFactories.Location = new System.Drawing.Point(12, 37);
            this.checkedListBoxFactories.Name = "checkedListBoxFactories";
            this.checkedListBoxFactories.Size = new System.Drawing.Size(188, 214);
            this.checkedListBoxFactories.TabIndex = 11;
            // 
            // lblSKU
            // 
            this.lblSKU.Location = new System.Drawing.Point(225, 12);
            this.lblSKU.Name = "lblSKU";
            this.lblSKU.Size = new System.Drawing.Size(19, 13);
            this.lblSKU.TabIndex = 13;
            this.lblSKU.Text = "SKU";
            // 
            // slueItemSource
            // 
            this.slueItemSource.Location = new System.Drawing.Point(288, 9);
            this.slueItemSource.Name = "slueItemSource";
            this.slueItemSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueItemSource.Properties.PopupView = this.searchLookUpEdit1View;
            this.slueItemSource.Size = new System.Drawing.Size(235, 20);
            this.slueItemSource.TabIndex = 14;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // grdBomFactory
            // 
            this.grdBomFactory.Location = new System.Drawing.Point(225, 37);
            this.grdBomFactory.MainView = this.gridViewBomFactory;
            this.grdBomFactory.Name = "grdBomFactory";
            this.grdBomFactory.Size = new System.Drawing.Size(298, 214);
            this.grdBomFactory.TabIndex = 15;
            this.grdBomFactory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBomFactory});
            // 
            // gridViewBomFactory
            // 
            this.gridViewBomFactory.GridControl = this.grdBomFactory;
            this.gridViewBomFactory.Name = "gridViewBomFactory";
            // 
            // sbOk
            // 
            this.sbOk.Location = new System.Drawing.Point(448, 267);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new System.Drawing.Size(75, 23);
            this.sbOk.TabIndex = 17;
            this.sbOk.Text = "OK";
            // 
            // sbCancel
            // 
            this.sbCancel.Location = new System.Drawing.Point(367, 267);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(75, 23);
            this.sbCancel.TabIndex = 16;
            this.sbCancel.Text = "Cancel";
            // 
            // SelectBomClone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 296);
            this.Controls.Add(this.sbOk);
            this.Controls.Add(this.sbCancel);
            this.Controls.Add(this.grdBomFactory);
            this.Controls.Add(this.slueItemSource);
            this.Controls.Add(this.lblSKU);
            this.Controls.Add(this.lblFactory);
            this.Controls.Add(this.checkedListBoxFactories);
            this.Name = "SelectBomClone";
            this.Text = "SelectBomClone";
            ((System.ComponentModel.ISupportInitialize)(this.slueItemSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBomFactory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBomFactory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblFactory;
        private System.Windows.Forms.CheckedListBox checkedListBoxFactories;
        private DevExpress.XtraEditors.LabelControl lblSKU;
        private DevExpress.XtraEditors.SearchLookUpEdit slueItemSource;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.GridControl grdBomFactory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBomFactory;
        private DevExpress.XtraEditors.SimpleButton sbOk;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
    }
}