namespace CustomControls
{
    partial class StackView
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StackView));
            this.stackStrip = new System.Windows.Forms.ToolStrip();
            this.editStackButton = new System.Windows.Forms.ToolStripButton();
            this.newStackButton = new System.Windows.Forms.ToolStripButton();
            this.saveStackButton = new System.Windows.Forms.ToolStripButton();
            this.cancelStackButton = new System.Windows.Forms.ToolStripButton();
            this.stackStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // stackStrip
            // 
            this.stackStrip.CanOverflow = false;
            this.stackStrip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stackStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.stackStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editStackButton,
            this.newStackButton,
            this.saveStackButton,
            this.cancelStackButton});
            this.stackStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.stackStrip.Location = new System.Drawing.Point(0, 0);
            this.stackStrip.Name = "stackStrip";
            this.stackStrip.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.stackStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.stackStrip.Size = new System.Drawing.Size(366, 33);
            this.stackStrip.TabIndex = 0;
            this.stackStrip.Text = "toolStrip1";
            // 
            // editStackButton
            // 
            this.editStackButton.CheckOnClick = true;
            this.editStackButton.ForeColor = System.Drawing.Color.White;
            this.editStackButton.Image = ((System.Drawing.Image)(resources.GetObject("editStackButton.Image")));
            this.editStackButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editStackButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editStackButton.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.editStackButton.Margin = new System.Windows.Forms.Padding(0);
            this.editStackButton.Name = "editStackButton";
            this.editStackButton.Padding = new System.Windows.Forms.Padding(3);
            this.editStackButton.Size = new System.Drawing.Size(58, 26);
            this.editStackButton.Text = "Edit";
            this.editStackButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editStackButton.Click += new System.EventHandler(this.stackButton_Click);
            // 
            // newStackButton
            // 
            this.newStackButton.CheckOnClick = true;
            this.newStackButton.ForeColor = System.Drawing.Color.White;
            this.newStackButton.Image = ((System.Drawing.Image)(resources.GetObject("newStackButton.Image")));
            this.newStackButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newStackButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newStackButton.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.newStackButton.Margin = new System.Windows.Forms.Padding(0);
            this.newStackButton.Name = "newStackButton";
            this.newStackButton.Padding = new System.Windows.Forms.Padding(3);
            this.newStackButton.Size = new System.Drawing.Size(61, 26);
            this.newStackButton.Text = "New";
            this.newStackButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newStackButton.Click += new System.EventHandler(this.stackButton_Click);
            // 
            // saveStackButton
            // 
            this.saveStackButton.CheckOnClick = true;
            this.saveStackButton.ForeColor = System.Drawing.Color.White;
            this.saveStackButton.Image = ((System.Drawing.Image)(resources.GetObject("saveStackButton.Image")));
            this.saveStackButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveStackButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveStackButton.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.saveStackButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveStackButton.Name = "saveStackButton";
            this.saveStackButton.Padding = new System.Windows.Forms.Padding(3);
            this.saveStackButton.Size = new System.Drawing.Size(66, 26);
            this.saveStackButton.Text = "Save";
            this.saveStackButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveStackButton.Click += new System.EventHandler(this.stackButton_Click);
            // 
            // cancelStackButton
            // 
            this.cancelStackButton.CheckOnClick = true;
            this.cancelStackButton.ForeColor = System.Drawing.Color.White;
            this.cancelStackButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelStackButton.Image")));
            this.cancelStackButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelStackButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cancelStackButton.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.cancelStackButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelStackButton.Name = "cancelStackButton";
            this.cancelStackButton.Padding = new System.Windows.Forms.Padding(3);
            this.cancelStackButton.Size = new System.Drawing.Size(76, 26);
            this.cancelStackButton.Text = "Cancel";
            this.cancelStackButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelStackButton.Click += new System.EventHandler(this.stackButton_Click);
            // 
            // StackView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.stackStrip);
            this.Name = "StackView";
            this.Size = new System.Drawing.Size(366, 41);
            this.Load += new System.EventHandler(this.StackView_Load);
            this.stackStrip.ResumeLayout(false);
            this.stackStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip stackStrip;
        private System.Windows.Forms.ToolStripButton editStackButton;
        private System.Windows.Forms.ToolStripButton newStackButton;
        private System.Windows.Forms.ToolStripButton saveStackButton;
        private System.Windows.Forms.ToolStripButton cancelStackButton;
    }
}
