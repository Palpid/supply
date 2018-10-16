namespace BOM.Forms
{
    partial class BomImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BomImport));
            this.grdImport = new DevExpress.XtraGrid.GridControl();
            this.gridViewImport = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOpenTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.btnProcesar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPathExcelFile = new DevExpress.XtraEditors.TextEdit();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.lblFichero = new DevExpress.XtraEditors.LabelControl();
            this.btnCargar = new DevExpress.XtraEditors.SimpleButton();
            this.btnViewFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadXls = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPathExcelFile.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdImport
            // 
            this.grdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdImport.Location = new System.Drawing.Point(12, 73);
            this.grdImport.MainView = this.gridViewImport;
            this.grdImport.Name = "grdImport";
            this.grdImport.Size = new System.Drawing.Size(982, 461);
            this.grdImport.TabIndex = 4;
            this.grdImport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewImport});
            // 
            // gridViewImport
            // 
            this.gridViewImport.GridControl = this.grdImport;
            this.gridViewImport.Name = "gridViewImport";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnOpenTemplate);
            this.panelControl1.Controls.Add(this.btnProcesar);
            this.panelControl1.Controls.Add(this.txtPathExcelFile);
            this.panelControl1.Controls.Add(this.btnOpenFile);
            this.panelControl1.Controls.Add(this.lblFichero);
            this.panelControl1.Controls.Add(this.btnCargar);
            this.panelControl1.Controls.Add(this.btnViewFile);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(982, 55);
            this.panelControl1.TabIndex = 3;
            // 
            // btnOpenTemplate
            // 
            this.btnOpenTemplate.Location = new System.Drawing.Point(781, 16);
            this.btnOpenTemplate.Name = "btnOpenTemplate";
            this.btnOpenTemplate.Size = new System.Drawing.Size(145, 26);
            this.btnOpenTemplate.TabIndex = 69;
            this.btnOpenTemplate.Text = "Download Template";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(578, 16);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(112, 26);
            this.btnProcesar.TabIndex = 68;
            this.btnProcesar.Text = "Procesar";
            // 
            // txtPathExcelFile
            // 
            this.txtPathExcelFile.Location = new System.Drawing.Point(66, 19);
            this.txtPathExcelFile.Name = "txtPathExcelFile";
            this.txtPathExcelFile.Size = new System.Drawing.Size(326, 20);
            this.txtPathExcelFile.TabIndex = 65;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.ImageOptions.Image")));
            this.btnOpenFile.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOpenFile.Location = new System.Drawing.Point(398, 16);
            this.btnOpenFile.MaximumSize = new System.Drawing.Size(25, 25);
            this.btnOpenFile.MinimumSize = new System.Drawing.Size(25, 25);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(25, 25);
            this.btnOpenFile.TabIndex = 66;
            // 
            // lblFichero
            // 
            this.lblFichero.Location = new System.Drawing.Point(14, 22);
            this.lblFichero.Name = "lblFichero";
            this.lblFichero.Size = new System.Drawing.Size(35, 13);
            this.lblFichero.TabIndex = 63;
            this.lblFichero.Text = "Fichero";
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(460, 16);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(112, 26);
            this.btnCargar.TabIndex = 64;
            this.btnCargar.Text = "Cargar";
            // 
            // btnViewFile
            // 
            this.btnViewFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnViewFile.ImageOptions.Image")));
            this.btnViewFile.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnViewFile.Location = new System.Drawing.Point(429, 16);
            this.btnViewFile.MaximumSize = new System.Drawing.Size(25, 25);
            this.btnViewFile.MinimumSize = new System.Drawing.Size(25, 25);
            this.btnViewFile.Name = "btnViewFile";
            this.btnViewFile.Size = new System.Drawing.Size(25, 25);
            this.btnViewFile.TabIndex = 67;
            // 
            // btnDownloadXls
            // 
            this.btnDownloadXls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadXls.ImageOptions.Image = global::BOM.Properties.Resources.excel_icon;
            this.btnDownloadXls.Location = new System.Drawing.Point(950, 540);
            this.btnDownloadXls.Name = "btnDownloadXls";
            this.btnDownloadXls.Size = new System.Drawing.Size(44, 44);
            this.btnDownloadXls.TabIndex = 5;
            // 
            // BomImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 596);
            this.Controls.Add(this.btnDownloadXls);
            this.Controls.Add(this.grdImport);
            this.Controls.Add(this.panelControl1);
            this.Name = "BomImport";
            this.Text = "BomImport";
            ((System.ComponentModel.ISupportInitialize)(this.grdImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPathExcelFile.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdImport;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewImport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtPathExcelFile;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.LabelControl lblFichero;
        private DevExpress.XtraEditors.SimpleButton btnCargar;
        private DevExpress.XtraEditors.SimpleButton btnViewFile;
        private DevExpress.XtraEditors.SimpleButton btnProcesar;
        private DevExpress.XtraEditors.SimpleButton btnOpenTemplate;
        private DevExpress.XtraEditors.SimpleButton btnDownloadXls;
    }
}