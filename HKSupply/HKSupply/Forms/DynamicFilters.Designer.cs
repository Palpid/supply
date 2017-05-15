namespace HKSupply.Forms
{
    partial class DynamicFilters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DynamicFilters));
            this.tlpFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblColumn1Header = new System.Windows.Forms.Label();
            this.lblColumn2Header = new System.Windows.Forms.Label();
            this.lblColumn3Header = new System.Windows.Forms.Label();
            this.lblColumn4Header = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.tlpFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpFilters
            // 
            this.tlpFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpFilters.AutoScroll = true;
            this.tlpFilters.ColumnCount = 4;
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpFilters.Controls.Add(this.lblColumn1Header, 0, 0);
            this.tlpFilters.Controls.Add(this.lblColumn2Header, 1, 0);
            this.tlpFilters.Controls.Add(this.lblColumn3Header, 2, 0);
            this.tlpFilters.Controls.Add(this.lblColumn4Header, 3, 0);
            this.tlpFilters.Location = new System.Drawing.Point(12, 19);
            this.tlpFilters.Name = "tlpFilters";
            this.tlpFilters.RowCount = 1;
            this.tlpFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFilters.Size = new System.Drawing.Size(981, 495);
            this.tlpFilters.TabIndex = 0;
            // 
            // lblColumn1Header
            // 
            this.lblColumn1Header.AutoSize = true;
            this.lblColumn1Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumn1Header.Location = new System.Drawing.Point(3, 0);
            this.lblColumn1Header.Name = "lblColumn1Header";
            this.lblColumn1Header.Size = new System.Drawing.Size(107, 17);
            this.lblColumn1Header.TabIndex = 0;
            this.lblColumn1Header.Text = "Column Name";
            // 
            // lblColumn2Header
            // 
            this.lblColumn2Header.AutoSize = true;
            this.lblColumn2Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumn2Header.Location = new System.Drawing.Point(297, 0);
            this.lblColumn2Header.Name = "lblColumn2Header";
            this.lblColumn2Header.Size = new System.Drawing.Size(76, 17);
            this.lblColumn2Header.TabIndex = 1;
            this.lblColumn2Header.Text = "Condition";
            // 
            // lblColumn3Header
            // 
            this.lblColumn3Header.AutoSize = true;
            this.lblColumn3Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumn3Header.Location = new System.Drawing.Point(493, 0);
            this.lblColumn3Header.Name = "lblColumn3Header";
            this.lblColumn3Header.Size = new System.Drawing.Size(45, 17);
            this.lblColumn3Header.TabIndex = 2;
            this.lblColumn3Header.Text = "Filter";
            // 
            // lblColumn4Header
            // 
            this.lblColumn4Header.AutoSize = true;
            this.lblColumn4Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumn4Header.Location = new System.Drawing.Point(836, 0);
            this.lblColumn4Header.Name = "lblColumn4Header";
            this.lblColumn4Header.Size = new System.Drawing.Size(50, 17);
            this.lblColumn4Header.TabIndex = 3;
            this.lblColumn4Header.Text = "Union";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(841, 520);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(151, 29);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // DynamicFilters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1005, 560);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.tlpFilters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DynamicFilters";
            this.Load += new System.EventHandler(this.DynamicFilters_Load);
            this.tlpFilters.ResumeLayout(false);
            this.tlpFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpFilters;
        private System.Windows.Forms.Label lblColumn1Header;
        private System.Windows.Forms.Label lblColumn2Header;
        private System.Windows.Forms.Label lblColumn3Header;
        private System.Windows.Forms.Label lblColumn4Header;
        private System.Windows.Forms.Button btnFilter;
    }
}