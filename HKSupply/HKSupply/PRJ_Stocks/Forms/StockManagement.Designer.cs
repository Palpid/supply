namespace HKSupply.PRJ_Stocks.Forms
{
    partial class StockManagement
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
            this.GC_Stocks = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWare = new DevExpress.XtraEditors.TextEdit();
            this.txtWareType = new DevExpress.XtraEditors.TextEdit();
            this.txtItem = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.CB_Owner = new System.Windows.Forms.ComboBox();
            this.txtFreeStk = new DevExpress.XtraEditors.TextEdit();
            this.txtNewQTT = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalStk = new DevExpress.XtraEditors.TextEdit();
            this.txtAsgStk = new DevExpress.XtraEditors.TextEdit();
            this.txtOnwStk = new DevExpress.XtraEditors.TextEdit();
            this.BtnAsingToOwner = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.BtnStkAdjust = new System.Windows.Forms.Button();
            this.CB_OwnDEST = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.BtnMoveStk = new System.Windows.Forms.Button();
            this.CB_WareDEST = new System.Windows.Forms.ComboBox();
            this.BtnFreeOwner = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCerateXLTemplate = new System.Windows.Forms.Button();
            this.BtnImportExcel = new System.Windows.Forms.Button();
            this.BtnClearOwner = new System.Windows.Forms.Button();
            this.BtnAdjustOwner = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.tabStk = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.GC_Movs = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BtnExecuteImport = new System.Windows.Forms.Button();
            this.GC_ImportExcel = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.GC_Lots = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GC_LotsMovements = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.GC_Stocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWare.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWareType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreeStk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewQTT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalStk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAsgStk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOnwStk.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabStk.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GC_Movs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GC_ImportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GC_Lots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GC_LotsMovements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            this.SuspendLayout();
            // 
            // GC_Stocks
            // 
            this.GC_Stocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GC_Stocks.Location = new System.Drawing.Point(3, 3);
            this.GC_Stocks.MainView = this.gridView1;
            this.GC_Stocks.Name = "GC_Stocks";
            this.GC_Stocks.Size = new System.Drawing.Size(984, 401);
            this.GC_Stocks.TabIndex = 1;
            this.GC_Stocks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.GC_Stocks.Click += new System.EventHandler(this.GC_Stocks_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.GC_Stocks;
            this.gridView1.Name = "gridView1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Warehouse";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Item";
            // 
            // txtWare
            // 
            this.txtWare.Enabled = false;
            this.txtWare.Location = new System.Drawing.Point(10, 41);
            this.txtWare.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtWare.Name = "txtWare";
            this.txtWare.Size = new System.Drawing.Size(226, 20);
            this.txtWare.TabIndex = 35;
            // 
            // txtWareType
            // 
            this.txtWareType.Enabled = false;
            this.txtWareType.Location = new System.Drawing.Point(10, 65);
            this.txtWareType.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtWareType.Name = "txtWareType";
            this.txtWareType.Size = new System.Drawing.Size(226, 20);
            this.txtWareType.TabIndex = 36;
            // 
            // txtItem
            // 
            this.txtItem.Enabled = false;
            this.txtItem.Location = new System.Drawing.Point(297, 42);
            this.txtItem.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(211, 20);
            this.txtItem.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Owner";
            // 
            // CB_Owner
            // 
            this.CB_Owner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Owner.FormattingEnabled = true;
            this.CB_Owner.Location = new System.Drawing.Point(297, 65);
            this.CB_Owner.Name = "CB_Owner";
            this.CB_Owner.Size = new System.Drawing.Size(211, 21);
            this.CB_Owner.TabIndex = 40;
            this.CB_Owner.SelectedIndexChanged += new System.EventHandler(this.CB_Owner_SelectedIndexChanged);
            // 
            // txtFreeStk
            // 
            this.txtFreeStk.EditValue = "0";
            this.txtFreeStk.Enabled = false;
            this.txtFreeStk.Location = new System.Drawing.Point(514, 41);
            this.txtFreeStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtFreeStk.Name = "txtFreeStk";
            this.txtFreeStk.Size = new System.Drawing.Size(83, 20);
            this.txtFreeStk.TabIndex = 43;
            // 
            // txtNewQTT
            // 
            this.txtNewQTT.EditValue = "0";
            this.txtNewQTT.Location = new System.Drawing.Point(507, 124);
            this.txtNewQTT.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtNewQTT.Name = "txtNewQTT";
            this.txtNewQTT.Size = new System.Drawing.Size(82, 20);
            this.txtNewQTT.TabIndex = 44;
            this.txtNewQTT.EditValueChanged += new System.EventHandler(this.txtNewQTT_EditValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(512, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "New Quantity";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(703, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "Total";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(611, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Assigned";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(533, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Free";
            // 
            // txtTotalStk
            // 
            this.txtTotalStk.EditValue = "0";
            this.txtTotalStk.Enabled = false;
            this.txtTotalStk.Location = new System.Drawing.Point(684, 41);
            this.txtTotalStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtTotalStk.Name = "txtTotalStk";
            this.txtTotalStk.Size = new System.Drawing.Size(83, 20);
            this.txtTotalStk.TabIndex = 50;
            // 
            // txtAsgStk
            // 
            this.txtAsgStk.EditValue = "0";
            this.txtAsgStk.Enabled = false;
            this.txtAsgStk.Location = new System.Drawing.Point(599, 41);
            this.txtAsgStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtAsgStk.Name = "txtAsgStk";
            this.txtAsgStk.Size = new System.Drawing.Size(83, 20);
            this.txtAsgStk.TabIndex = 51;
            // 
            // txtOnwStk
            // 
            this.txtOnwStk.EditValue = "0";
            this.txtOnwStk.Enabled = false;
            this.txtOnwStk.Location = new System.Drawing.Point(599, 64);
            this.txtOnwStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtOnwStk.Name = "txtOnwStk";
            this.txtOnwStk.Size = new System.Drawing.Size(83, 20);
            this.txtOnwStk.TabIndex = 56;
            // 
            // BtnAsingToOwner
            // 
            this.BtnAsingToOwner.Location = new System.Drawing.Point(297, 151);
            this.BtnAsingToOwner.Name = "BtnAsingToOwner";
            this.BtnAsingToOwner.Size = new System.Drawing.Size(64, 30);
            this.BtnAsingToOwner.TabIndex = 57;
            this.BtnAsingToOwner.Text = "Assign ";
            this.BtnAsingToOwner.UseVisualStyleBackColor = true;
            this.BtnAsingToOwner.Click += new System.EventHandler(this.BtnAsingToOwner_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(275, 293);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 30);
            this.button2.TabIndex = 58;
            this.button2.Text = "Free";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // BtnStkAdjust
            // 
            this.BtnStkAdjust.Location = new System.Drawing.Point(506, 151);
            this.BtnStkAdjust.Name = "BtnStkAdjust";
            this.BtnStkAdjust.Size = new System.Drawing.Size(83, 30);
            this.BtnStkAdjust.TabIndex = 60;
            this.BtnStkAdjust.Text = "Adjust";
            this.BtnStkAdjust.UseVisualStyleBackColor = true;
            this.BtnStkAdjust.Click += new System.EventHandler(this.BtnStkAdjust_Click);
            // 
            // CB_OwnDEST
            // 
            this.CB_OwnDEST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_OwnDEST.FormattingEnabled = true;
            this.CB_OwnDEST.Location = new System.Drawing.Point(259, 124);
            this.CB_OwnDEST.Name = "CB_OwnDEST";
            this.CB_OwnDEST.Size = new System.Drawing.Size(198, 21);
            this.CB_OwnDEST.TabIndex = 65;
            this.CB_OwnDEST.SelectedIndexChanged += new System.EventHandler(this.CB_OwnDEST_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(263, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 66;
            this.label11.Text = "Owner";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "Warehouse";
            // 
            // BtnMoveStk
            // 
            this.BtnMoveStk.Location = new System.Drawing.Point(10, 151);
            this.BtnMoveStk.Name = "BtnMoveStk";
            this.BtnMoveStk.Size = new System.Drawing.Size(129, 30);
            this.BtnMoveStk.TabIndex = 59;
            this.BtnMoveStk.Text = "Move";
            this.BtnMoveStk.UseVisualStyleBackColor = true;
            this.BtnMoveStk.Click += new System.EventHandler(this.BtnMoveStk_Click);
            // 
            // CB_WareDEST
            // 
            this.CB_WareDEST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_WareDEST.FormattingEnabled = true;
            this.CB_WareDEST.Location = new System.Drawing.Point(6, 124);
            this.CB_WareDEST.Name = "CB_WareDEST";
            this.CB_WareDEST.Size = new System.Drawing.Size(226, 21);
            this.CB_WareDEST.TabIndex = 72;
            this.CB_WareDEST.SelectedIndexChanged += new System.EventHandler(this.CB_WareDEST_SelectedIndexChanged);
            // 
            // BtnFreeOwner
            // 
            this.BtnFreeOwner.Location = new System.Drawing.Point(684, 64);
            this.BtnFreeOwner.Name = "BtnFreeOwner";
            this.BtnFreeOwner.Size = new System.Drawing.Size(83, 22);
            this.BtnFreeOwner.TabIndex = 74;
            this.BtnFreeOwner.Text = "Free Owner";
            this.BtnFreeOwner.UseVisualStyleBackColor = true;
            this.BtnFreeOwner.Click += new System.EventHandler(this.BtnFreeOwner_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCerateXLTemplate);
            this.groupBox1.Controls.Add(this.BtnImportExcel);
            this.groupBox1.Controls.Add(this.BtnClearOwner);
            this.groupBox1.Controls.Add(this.BtnAdjustOwner);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.BtnFreeOwner);
            this.groupBox1.Controls.Add(this.CB_WareDEST);
            this.groupBox1.Controls.Add(this.BtnMoveStk);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.CB_OwnDEST);
            this.groupBox1.Controls.Add(this.BtnStkAdjust);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.BtnAsingToOwner);
            this.groupBox1.Controls.Add(this.txtOnwStk);
            this.groupBox1.Controls.Add(this.txtAsgStk);
            this.groupBox1.Controls.Add(this.txtTotalStk);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNewQTT);
            this.groupBox1.Controls.Add(this.txtFreeStk);
            this.groupBox1.Controls.Add(this.CB_Owner);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtItem);
            this.groupBox1.Controls.Add(this.txtWareType);
            this.groupBox1.Controls.Add(this.txtWare);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(999, 192);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stock Actions";
            // 
            // btnCerateXLTemplate
            // 
            this.btnCerateXLTemplate.Location = new System.Drawing.Point(874, 102);
            this.btnCerateXLTemplate.Name = "btnCerateXLTemplate";
            this.btnCerateXLTemplate.Size = new System.Drawing.Size(111, 28);
            this.btnCerateXLTemplate.TabIndex = 79;
            this.btnCerateXLTemplate.Text = "Create XL Template";
            this.btnCerateXLTemplate.UseVisualStyleBackColor = true;
            this.btnCerateXLTemplate.Click += new System.EventHandler(this.btnCerateXLTemplate_Click);
            // 
            // BtnImportExcel
            // 
            this.BtnImportExcel.Location = new System.Drawing.Point(874, 135);
            this.BtnImportExcel.Name = "BtnImportExcel";
            this.BtnImportExcel.Size = new System.Drawing.Size(111, 28);
            this.BtnImportExcel.TabIndex = 78;
            this.BtnImportExcel.Text = "Import XL";
            this.BtnImportExcel.UseVisualStyleBackColor = true;
            this.BtnImportExcel.Click += new System.EventHandler(this.BtnImportExcel_Click);
            // 
            // BtnClearOwner
            // 
            this.BtnClearOwner.Location = new System.Drawing.Point(462, 124);
            this.BtnClearOwner.Name = "BtnClearOwner";
            this.BtnClearOwner.Size = new System.Drawing.Size(24, 21);
            this.BtnClearOwner.TabIndex = 77;
            this.BtnClearOwner.Text = "C";
            this.BtnClearOwner.UseVisualStyleBackColor = true;
            this.BtnClearOwner.Click += new System.EventHandler(this.BtnClearOwner_Click);
            // 
            // BtnAdjustOwner
            // 
            this.BtnAdjustOwner.Location = new System.Drawing.Point(367, 151);
            this.BtnAdjustOwner.Name = "BtnAdjustOwner";
            this.BtnAdjustOwner.Size = new System.Drawing.Size(64, 30);
            this.BtnAdjustOwner.TabIndex = 76;
            this.BtnAdjustOwner.Text = "Adjust";
            this.BtnAdjustOwner.UseVisualStyleBackColor = true;
            this.BtnAdjustOwner.Click += new System.EventHandler(this.BtnAdjustOwner_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BtnSave.Location = new System.Drawing.Point(872, 19);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(111, 30);
            this.BtnSave.TabIndex = 75;
            this.BtnSave.Text = "Save Changes";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tabStk
            // 
            this.tabStk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabStk.Controls.Add(this.tabPage1);
            this.tabStk.Controls.Add(this.tabPage2);
            this.tabStk.Controls.Add(this.tabPage4);
            this.tabStk.Controls.Add(this.tabPage5);
            this.tabStk.Controls.Add(this.tabPage3);
            this.tabStk.Location = new System.Drawing.Point(5, 200);
            this.tabStk.Name = "tabStk";
            this.tabStk.SelectedIndex = 0;
            this.tabStk.Size = new System.Drawing.Size(998, 433);
            this.tabStk.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GC_Stocks);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(990, 407);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Stock";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.GC_Movs);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(990, 407);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Movements";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // GC_Movs
            // 
            this.GC_Movs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GC_Movs.Location = new System.Drawing.Point(3, 3);
            this.GC_Movs.MainView = this.gridView2;
            this.GC_Movs.Name = "GC_Movs";
            this.GC_Movs.Size = new System.Drawing.Size(984, 401);
            this.GC_Movs.TabIndex = 2;
            this.GC_Movs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.GC_Movs;
            this.gridView2.Name = "gridView2";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.BtnExecuteImport);
            this.tabPage3.Controls.Add(this.GC_ImportExcel);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(990, 407);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ImportData";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // BtnExecuteImport
            // 
            this.BtnExecuteImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExecuteImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BtnExecuteImport.Location = new System.Drawing.Point(877, 374);
            this.BtnExecuteImport.Name = "BtnExecuteImport";
            this.BtnExecuteImport.Size = new System.Drawing.Size(109, 29);
            this.BtnExecuteImport.TabIndex = 79;
            this.BtnExecuteImport.Text = "Import";
            this.BtnExecuteImport.UseVisualStyleBackColor = false;
            this.BtnExecuteImport.Click += new System.EventHandler(this.BtnExecuteImport_Click);
            // 
            // GC_ImportExcel
            // 
            this.GC_ImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GC_ImportExcel.Location = new System.Drawing.Point(3, 4);
            this.GC_ImportExcel.MainView = this.gridView3;
            this.GC_ImportExcel.Name = "GC_ImportExcel";
            this.GC_ImportExcel.Size = new System.Drawing.Size(984, 367);
            this.GC_ImportExcel.TabIndex = 2;
            this.GC_ImportExcel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.GC_ImportExcel;
            this.gridView3.Name = "gridView3";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.GC_Lots);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(990, 407);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Lots";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.GC_LotsMovements);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(990, 407);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Lot Movements";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // GC_Lots
            // 
            this.GC_Lots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GC_Lots.Location = new System.Drawing.Point(0, 0);
            this.GC_Lots.MainView = this.gridView4;
            this.GC_Lots.Name = "GC_Lots";
            this.GC_Lots.Size = new System.Drawing.Size(990, 407);
            this.GC_Lots.TabIndex = 3;
            this.GC_Lots.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.GC_Lots;
            this.gridView4.Name = "gridView4";
            // 
            // GC_LotsMovements
            // 
            this.GC_LotsMovements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GC_LotsMovements.Location = new System.Drawing.Point(0, 0);
            this.GC_LotsMovements.MainView = this.gridView5;
            this.GC_LotsMovements.Name = "GC_LotsMovements";
            this.GC_LotsMovements.Size = new System.Drawing.Size(990, 407);
            this.GC_LotsMovements.TabIndex = 3;
            this.GC_LotsMovements.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.GridControl = this.GC_LotsMovements;
            this.gridView5.Name = "gridView5";
            // 
            // StockManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 636);
            this.Controls.Add(this.tabStk);
            this.Controls.Add(this.groupBox1);
            this.Name = "StockManagement";
            this.Text = "StockManagment";
            this.Load += new System.EventHandler(this.StockManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GC_Stocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWare.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWareType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreeStk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewQTT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalStk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAsgStk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOnwStk.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabStk.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GC_Movs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GC_ImportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GC_Lots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GC_LotsMovements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl GC_Stocks;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtWare;
        private DevExpress.XtraEditors.TextEdit txtWareType;
        private DevExpress.XtraEditors.TextEdit txtItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CB_Owner;
        private DevExpress.XtraEditors.TextEdit txtFreeStk;
        private DevExpress.XtraEditors.TextEdit txtNewQTT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit txtTotalStk;
        private DevExpress.XtraEditors.TextEdit txtAsgStk;
        private DevExpress.XtraEditors.TextEdit txtOnwStk;
        private System.Windows.Forms.Button BtnAsingToOwner;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button BtnStkAdjust;
        private System.Windows.Forms.ComboBox CB_OwnDEST;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button BtnMoveStk;
        private System.Windows.Forms.ComboBox CB_WareDEST;
        private System.Windows.Forms.Button BtnFreeOwner;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabStk;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private DevExpress.XtraGrid.GridControl GC_Movs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnClearOwner;
        private System.Windows.Forms.Button BtnAdjustOwner;
        private System.Windows.Forms.Button BtnImportExcel;
        private System.Windows.Forms.TabPage tabPage3;
        private DevExpress.XtraGrid.GridControl GC_ImportExcel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.Button BtnExecuteImport;
        private System.Windows.Forms.Button btnCerateXLTemplate;
        private System.Windows.Forms.TabPage tabPage4;
        private DevExpress.XtraGrid.GridControl GC_Lots;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private System.Windows.Forms.TabPage tabPage5;
        private DevExpress.XtraGrid.GridControl GC_LotsMovements;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
    }
}