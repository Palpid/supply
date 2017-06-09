namespace HKSupply.Forms.Master.DialogForms
{
    partial class EditHfBom
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
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.xgrdItemBom = new DevExpress.XtraGrid.GridControl();
            this.gridViewItemBom = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.xgrdItemBom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemBom)).BeginInit();
            this.SuspendLayout();
            // 
            // sbSave
            // 
            this.sbSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbSave.Location = new System.Drawing.Point(1307, 502);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(75, 23);
            this.sbSave.TabIndex = 4;
            this.sbSave.Text = "Save";
            // 
            // sbCancel
            // 
            this.sbCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbCancel.Location = new System.Drawing.Point(1226, 502);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(75, 23);
            this.sbCancel.TabIndex = 3;
            this.sbCancel.Text = "Cancel";
            // 
            // xgrdItemBom
            // 
            this.xgrdItemBom.Location = new System.Drawing.Point(12, 12);
            this.xgrdItemBom.MainView = this.gridViewItemBom;
            this.xgrdItemBom.Name = "xgrdItemBom";
            this.xgrdItemBom.Size = new System.Drawing.Size(1370, 484);
            this.xgrdItemBom.TabIndex = 5;
            this.xgrdItemBom.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItemBom});
            // 
            // gridViewItemBom
            // 
            this.gridViewItemBom.GridControl = this.xgrdItemBom;
            this.gridViewItemBom.Name = "gridViewItemBom";
            // 
            // EditHfBom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 537);
            this.Controls.Add(this.xgrdItemBom);
            this.Controls.Add(this.sbSave);
            this.Controls.Add(this.sbCancel);
            this.Name = "EditHfBom";
            this.Text = "Edit";
            this.Load += new System.EventHandler(this.EditHfBom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xgrdItemBom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItemBom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraGrid.GridControl xgrdItemBom;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItemBom;
    }
}