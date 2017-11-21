namespace HKSupply.Forms.Supply.Dashboard
{
    partial class DashboardQP
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
            this.xtpDashboard = new DevExpress.XtraTab.XtraTabPage();
            this.pcFilter = new DevExpress.XtraEditors.PanelControl();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.lueType = new DevExpress.XtraEditors.LookUpEdit();
            this.ccbeItemGroup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ccbeCustomer = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblItemGroup = new DevExpress.XtraEditors.LabelControl();
            this.sbGenerate = new DevExpress.XtraEditors.SimpleButton();
            this.lblDateIni = new DevExpress.XtraEditors.LabelControl();
            this.lblDateEnd = new DevExpress.XtraEditors.LabelControl();
            this.dateEditDateIni = new DevExpress.XtraEditors.DateEdit();
            this.dateEditDateEnd = new DevExpress.XtraEditors.DateEdit();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.lblDateIniWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblDateEndWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblWeek = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.xtcDashboard = new DevExpress.XtraTab.XtraTabControl();
            this.xtpGrid = new DevExpress.XtraTab.XtraTabPage();
            this.pivotGridDashboardQP = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.xtpLineChart = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControlChart = new DevExpress.XtraEditors.SplitContainerControl();
            this.chartControlMT = new DevExpress.XtraCharts.ChartControl();
            this.chartControlHW = new DevExpress.XtraCharts.ChartControl();
            this.xtpBarChart = new DevExpress.XtraTab.XtraTabPage();
            this.chartControlPercDesv = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).BeginInit();
            this.xtcGeneral.SuspendLayout();
            this.xtpDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).BeginInit();
            this.pcFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccbeItemGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccbeCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateIni.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateIni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcDashboard)).BeginInit();
            this.xtcDashboard.SuspendLayout();
            this.xtpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridDashboardQP)).BeginInit();
            this.xtpLineChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlChart)).BeginInit();
            this.splitContainerControlChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlMT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlHW)).BeginInit();
            this.xtpBarChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlPercDesv)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 618);
            this.ribbonStatusBar.Size = new System.Drawing.Size(1063, 31);
            // 
            // xtcGeneral
            // 
            this.xtcGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGeneral.Location = new System.Drawing.Point(0, 125);
            this.xtcGeneral.Name = "xtcGeneral";
            this.xtcGeneral.SelectedTabPage = this.xtpDashboard;
            this.xtcGeneral.Size = new System.Drawing.Size(1063, 493);
            this.xtcGeneral.TabIndex = 6;
            this.xtcGeneral.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpDashboard});
            // 
            // xtpDashboard
            // 
            this.xtpDashboard.Controls.Add(this.pcFilter);
            this.xtpDashboard.Controls.Add(this.xtcDashboard);
            this.xtpDashboard.Name = "xtpDashboard";
            this.xtpDashboard.Size = new System.Drawing.Size(1057, 465);
            this.xtpDashboard.Text = "Dashboard Quotation Proposal";
            // 
            // pcFilter
            // 
            this.pcFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcFilter.Controls.Add(this.lblType);
            this.pcFilter.Controls.Add(this.lueType);
            this.pcFilter.Controls.Add(this.ccbeItemGroup);
            this.pcFilter.Controls.Add(this.ccbeCustomer);
            this.pcFilter.Controls.Add(this.lblItemGroup);
            this.pcFilter.Controls.Add(this.sbGenerate);
            this.pcFilter.Controls.Add(this.lblDateIni);
            this.pcFilter.Controls.Add(this.lblDateEnd);
            this.pcFilter.Controls.Add(this.dateEditDateIni);
            this.pcFilter.Controls.Add(this.dateEditDateEnd);
            this.pcFilter.Controls.Add(this.lblDate);
            this.pcFilter.Controls.Add(this.lblDateIniWeek);
            this.pcFilter.Controls.Add(this.lblDateEndWeek);
            this.pcFilter.Controls.Add(this.lblWeek);
            this.pcFilter.Controls.Add(this.lblCustomer);
            this.pcFilter.Location = new System.Drawing.Point(11, 13);
            this.pcFilter.Name = "pcFilter";
            this.pcFilter.Size = new System.Drawing.Size(1039, 142);
            this.pcFilter.TabIndex = 5;
            // 
            // lblType
            // 
            this.lblType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblType.Location = new System.Drawing.Point(19, 89);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(84, 13);
            this.lblType.TabIndex = 24;
            this.lblType.Text = "TYPE";
            // 
            // lueType
            // 
            this.lueType.Location = new System.Drawing.Point(126, 86);
            this.lueType.MenuManager = this.ribbonControl;
            this.lueType.Name = "lueType";
            this.lueType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueType.Size = new System.Drawing.Size(219, 20);
            this.lueType.TabIndex = 23;
            // 
            // ccbeItemGroup
            // 
            this.ccbeItemGroup.Location = new System.Drawing.Point(126, 60);
            this.ccbeItemGroup.MenuManager = this.ribbonControl;
            this.ccbeItemGroup.Name = "ccbeItemGroup";
            this.ccbeItemGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccbeItemGroup.Size = new System.Drawing.Size(219, 20);
            this.ccbeItemGroup.TabIndex = 22;
            // 
            // ccbeCustomer
            // 
            this.ccbeCustomer.Location = new System.Drawing.Point(126, 34);
            this.ccbeCustomer.MenuManager = this.ribbonControl;
            this.ccbeCustomer.Name = "ccbeCustomer";
            this.ccbeCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccbeCustomer.Size = new System.Drawing.Size(219, 20);
            this.ccbeCustomer.TabIndex = 21;
            // 
            // lblItemGroup
            // 
            this.lblItemGroup.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblItemGroup.Location = new System.Drawing.Point(19, 63);
            this.lblItemGroup.Name = "lblItemGroup";
            this.lblItemGroup.Size = new System.Drawing.Size(84, 13);
            this.lblItemGroup.TabIndex = 17;
            this.lblItemGroup.Text = "ITEM GROUP";
            // 
            // sbGenerate
            // 
            this.sbGenerate.Location = new System.Drawing.Point(639, 34);
            this.sbGenerate.Name = "sbGenerate";
            this.sbGenerate.Size = new System.Drawing.Size(82, 38);
            this.sbGenerate.TabIndex = 16;
            this.sbGenerate.Text = "Generate";
            // 
            // lblDateIni
            // 
            this.lblDateIni.Location = new System.Drawing.Point(367, 34);
            this.lblDateIni.Name = "lblDateIni";
            this.lblDateIni.Size = new System.Drawing.Size(32, 13);
            this.lblDateIni.TabIndex = 2;
            this.lblDateIni.Text = "START";
            // 
            // lblDateEnd
            // 
            this.lblDateEnd.Location = new System.Drawing.Point(367, 53);
            this.lblDateEnd.Name = "lblDateEnd";
            this.lblDateEnd.Size = new System.Drawing.Size(20, 13);
            this.lblDateEnd.TabIndex = 3;
            this.lblDateEnd.Text = "END";
            // 
            // dateEditDateIni
            // 
            this.dateEditDateIni.EditValue = null;
            this.dateEditDateIni.Location = new System.Drawing.Point(442, 33);
            this.dateEditDateIni.MenuManager = this.ribbonControl;
            this.dateEditDateIni.Name = "dateEditDateIni";
            this.dateEditDateIni.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDateIni.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDateIni.Size = new System.Drawing.Size(106, 20);
            this.dateEditDateIni.TabIndex = 4;
            // 
            // dateEditDateEnd
            // 
            this.dateEditDateEnd.EditValue = null;
            this.dateEditDateEnd.Location = new System.Drawing.Point(442, 52);
            this.dateEditDateEnd.MenuManager = this.ribbonControl;
            this.dateEditDateEnd.Name = "dateEditDateEnd";
            this.dateEditDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditDateEnd.Size = new System.Drawing.Size(106, 20);
            this.dateEditDateEnd.TabIndex = 5;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(482, 14);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(23, 13);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date";
            // 
            // lblDateIniWeek
            // 
            this.lblDateIniWeek.AccessibleRole = System.Windows.Forms.AccessibleRole.Animation;
            this.lblDateIniWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDateIniWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblDateIniWeek.Location = new System.Drawing.Point(554, 33);
            this.lblDateIniWeek.Name = "lblDateIniWeek";
            this.lblDateIniWeek.Size = new System.Drawing.Size(65, 20);
            this.lblDateIniWeek.TabIndex = 7;
            this.lblDateIniWeek.Text = "XX";
            // 
            // lblDateEndWeek
            // 
            this.lblDateEndWeek.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDateEndWeek.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblDateEndWeek.Location = new System.Drawing.Point(554, 53);
            this.lblDateEndWeek.Name = "lblDateEndWeek";
            this.lblDateEndWeek.Size = new System.Drawing.Size(65, 19);
            this.lblDateEndWeek.TabIndex = 8;
            this.lblDateEndWeek.Text = "XX";
            // 
            // lblWeek
            // 
            this.lblWeek.Location = new System.Drawing.Point(570, 14);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(27, 13);
            this.lblWeek.TabIndex = 9;
            this.lblWeek.Text = "Week";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCustomer.Location = new System.Drawing.Point(19, 36);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(84, 13);
            this.lblCustomer.TabIndex = 10;
            this.lblCustomer.Text = "CUSTOMER";
            // 
            // xtcDashboard
            // 
            this.xtcDashboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtcDashboard.Location = new System.Drawing.Point(11, 161);
            this.xtcDashboard.Name = "xtcDashboard";
            this.xtcDashboard.SelectedTabPage = this.xtpGrid;
            this.xtcDashboard.Size = new System.Drawing.Size(1039, 301);
            this.xtcDashboard.TabIndex = 4;
            this.xtcDashboard.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpGrid,
            this.xtpLineChart,
            this.xtpBarChart});
            // 
            // xtpGrid
            // 
            this.xtpGrid.Controls.Add(this.pivotGridDashboardQP);
            this.xtpGrid.Name = "xtpGrid";
            this.xtpGrid.Size = new System.Drawing.Size(1033, 273);
            this.xtpGrid.Text = "DATA";
            // 
            // pivotGridDashboardQP
            // 
            this.pivotGridDashboardQP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridDashboardQP.Location = new System.Drawing.Point(0, 0);
            this.pivotGridDashboardQP.Name = "pivotGridDashboardQP";
            this.pivotGridDashboardQP.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized;
            this.pivotGridDashboardQP.Size = new System.Drawing.Size(1033, 273);
            this.pivotGridDashboardQP.TabIndex = 3;
            // 
            // xtpLineChart
            // 
            this.xtpLineChart.Controls.Add(this.splitContainerControlChart);
            this.xtpLineChart.Name = "xtpLineChart";
            this.xtpLineChart.Size = new System.Drawing.Size(1033, 273);
            this.xtpLineChart.Text = "LINE CHART";
            // 
            // splitContainerControlChart
            // 
            this.splitContainerControlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControlChart.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControlChart.Horizontal = false;
            this.splitContainerControlChart.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControlChart.Name = "splitContainerControlChart";
            this.splitContainerControlChart.Panel1.Controls.Add(this.chartControlMT);
            this.splitContainerControlChart.Panel1.Text = "Panel1";
            this.splitContainerControlChart.Panel2.Controls.Add(this.chartControlHW);
            this.splitContainerControlChart.Panel2.Text = "Panel2";
            this.splitContainerControlChart.Size = new System.Drawing.Size(1033, 273);
            this.splitContainerControlChart.SplitterPosition = 135;
            this.splitContainerControlChart.TabIndex = 0;
            this.splitContainerControlChart.Text = "splitContainerControl1";
            // 
            // chartControlMT
            // 
            this.chartControlMT.DataBindings = null;
            this.chartControlMT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlMT.Legend.Name = "Default Legend";
            this.chartControlMT.Location = new System.Drawing.Point(0, 0);
            this.chartControlMT.Name = "chartControlMT";
            this.chartControlMT.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControlMT.Size = new System.Drawing.Size(1033, 135);
            this.chartControlMT.TabIndex = 0;
            // 
            // chartControlHW
            // 
            this.chartControlHW.DataBindings = null;
            this.chartControlHW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlHW.Legend.Name = "Default Legend";
            this.chartControlHW.Location = new System.Drawing.Point(0, 0);
            this.chartControlHW.Name = "chartControlHW";
            this.chartControlHW.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControlHW.Size = new System.Drawing.Size(1033, 133);
            this.chartControlHW.TabIndex = 1;
            // 
            // xtpBarChart
            // 
            this.xtpBarChart.Controls.Add(this.chartControlPercDesv);
            this.xtpBarChart.Name = "xtpBarChart";
            this.xtpBarChart.Size = new System.Drawing.Size(1033, 273);
            this.xtpBarChart.Text = "BAR CHART";
            // 
            // chartControlPercDesv
            // 
            this.chartControlPercDesv.DataBindings = null;
            this.chartControlPercDesv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlPercDesv.Legend.Name = "Default Legend";
            this.chartControlPercDesv.Location = new System.Drawing.Point(0, 0);
            this.chartControlPercDesv.Name = "chartControlPercDesv";
            this.chartControlPercDesv.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControlPercDesv.Size = new System.Drawing.Size(1033, 273);
            this.chartControlPercDesv.TabIndex = 1;
            // 
            // DashboardQP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 649);
            this.Controls.Add(this.xtcGeneral);
            this.Name = "DashboardQP";
            this.Text = "DashboardQP";
            this.Load += new System.EventHandler(this.DashboardQP_Load);
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar, 0);
            this.Controls.SetChildIndex(this.xtcGeneral, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGeneral)).EndInit();
            this.xtcGeneral.ResumeLayout(false);
            this.xtpDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcFilter)).EndInit();
            this.pcFilter.ResumeLayout(false);
            this.pcFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccbeItemGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccbeCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateIni.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateIni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditDateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcDashboard)).EndInit();
            this.xtcDashboard.ResumeLayout(false);
            this.xtpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridDashboardQP)).EndInit();
            this.xtpLineChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlChart)).EndInit();
            this.splitContainerControlChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControlMT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlHW)).EndInit();
            this.xtpBarChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControlPercDesv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcGeneral;
        private DevExpress.XtraTab.XtraTabPage xtpDashboard;
        private DevExpress.XtraEditors.PanelControl pcFilter;
        private DevExpress.XtraEditors.SimpleButton sbGenerate;
        private DevExpress.XtraEditors.LabelControl lblDateIni;
        private DevExpress.XtraEditors.LabelControl lblDateEnd;
        private DevExpress.XtraEditors.DateEdit dateEditDateIni;
        private DevExpress.XtraEditors.DateEdit dateEditDateEnd;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.LabelControl lblDateIniWeek;
        private DevExpress.XtraEditors.LabelControl lblDateEndWeek;
        private DevExpress.XtraEditors.LabelControl lblWeek;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraTab.XtraTabControl xtcDashboard;
        private DevExpress.XtraTab.XtraTabPage xtpGrid;
        private DevExpress.XtraTab.XtraTabPage xtpLineChart;
        private DevExpress.XtraEditors.LabelControl lblItemGroup;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccbeCustomer;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccbeItemGroup;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridDashboardQP;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlChart;
        private DevExpress.XtraCharts.ChartControl chartControlMT;
        private DevExpress.XtraCharts.ChartControl chartControlHW;
        private DevExpress.XtraEditors.LabelControl lblType;
        private DevExpress.XtraEditors.LookUpEdit lueType;
        private DevExpress.XtraTab.XtraTabPage xtpBarChart;
        private DevExpress.XtraCharts.ChartControl chartControlPercDesv;
    }
}