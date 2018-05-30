namespace BOM.Forms
{
    partial class FrmSelector
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
            this.components = new System.ComponentModel.Container();
            this.peBomManagement = new DevExpress.XtraEditors.PictureEdit();
            this.peImportExcel = new DevExpress.XtraEditors.PictureEdit();
            this.peMassiveUpdate = new DevExpress.XtraEditors.PictureEdit();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.peBomManagement.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peImportExcel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peMassiveUpdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // peBomManagement
            // 
            this.peBomManagement.Cursor = System.Windows.Forms.Cursors.Default;
            this.peBomManagement.Location = new System.Drawing.Point(53, 123);
            this.peBomManagement.Name = "peBomManagement";
            this.peBomManagement.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peBomManagement.Size = new System.Drawing.Size(209, 209);
            this.peBomManagement.TabIndex = 0;
            // 
            // peImportExcel
            // 
            this.peImportExcel.Cursor = System.Windows.Forms.Cursors.Default;
            this.peImportExcel.Location = new System.Drawing.Point(295, 123);
            this.peImportExcel.Name = "peImportExcel";
            this.peImportExcel.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peImportExcel.Size = new System.Drawing.Size(209, 209);
            this.peImportExcel.TabIndex = 1;
            // 
            // peMassiveUpdate
            // 
            this.peMassiveUpdate.Cursor = System.Windows.Forms.Cursors.Default;
            this.peMassiveUpdate.Location = new System.Drawing.Point(537, 123);
            this.peMassiveUpdate.Name = "peMassiveUpdate";
            this.peMassiveUpdate.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peMassiveUpdate.Size = new System.Drawing.Size(209, 209);
            this.peMassiveUpdate.TabIndex = 2;
            // 
            // FrmSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 469);
            this.Controls.Add(this.peMassiveUpdate);
            this.Controls.Add(this.peImportExcel);
            this.Controls.Add(this.peBomManagement);
            this.Name = "FrmSelector";
            this.Text = "Selector";
            ((System.ComponentModel.ISupportInitialize)(this.peBomManagement.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peImportExcel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peMassiveUpdate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit peBomManagement;
        private DevExpress.XtraEditors.PictureEdit peImportExcel;
        private DevExpress.XtraEditors.PictureEdit peMassiveUpdate;
        private DevExpress.Utils.ToolTipController toolTipController1;
    }
}