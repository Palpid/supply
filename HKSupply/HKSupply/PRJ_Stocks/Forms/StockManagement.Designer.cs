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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalStk = new DevExpress.XtraEditors.TextEdit();
            this.txtAsgStk = new DevExpress.XtraEditors.TextEdit();
            this.txtOnwStk = new DevExpress.XtraEditors.TextEdit();
            this.button2 = new System.Windows.Forms.Button();
            this.CB_OwnDEST = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.BtnMoveStk = new System.Windows.Forms.Button();
            this.CB_WareDEST = new System.Windows.Forms.ComboBox();
            this.BtnFreeOwner = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtQttMov = new DevExpress.XtraEditors.TextEdit();
            this.txtIAsgnLot = new DevExpress.XtraEditors.TextEdit();
            this.txtAdjustFreeStk = new DevExpress.XtraEditors.TextEdit();
            this.btnAssignLot = new System.Windows.Forms.Button();
            this.btnAssignOwner = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQttAssignLot = new DevExpress.XtraEditors.TextEdit();
            this.txtQttAssignOwn = new DevExpress.XtraEditors.TextEdit();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnFreeLot = new System.Windows.Forms.Button();
            this.txtLotStk = new DevExpress.XtraEditors.TextEdit();
            this.txtAsgLot = new DevExpress.XtraEditors.TextEdit();
            this.txtFreeLot = new DevExpress.XtraEditors.TextEdit();
            this.CB_Lots = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerateXLTemplate = new System.Windows.Forms.Button();
            this.BtnImportExcel = new System.Windows.Forms.Button();
            this.BtnAdjustFree = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.GC_Stocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWare.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWareType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreeStk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalStk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAsgStk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOnwStk.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQttMov.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIAsgnLot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdjustFreeStk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQttAssignLot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQttAssignOwn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotStk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAsgLot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreeLot.Properties)).BeginInit();
            this.tabStk.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GC_Movs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GC_ImportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // GC_Stocks
            // 
            this.GC_Stocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GC_Stocks.Location = new System.Drawing.Point(3, 3);
            this.GC_Stocks.MainView = this.gridView1;
            this.GC_Stocks.Name = "GC_Stocks";
            this.GC_Stocks.Size = new System.Drawing.Size(1067, 382);
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
            this.label3.Location = new System.Drawing.Point(5, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Warehouse";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Item";
            // 
            // txtWare
            // 
            this.txtWare.Enabled = false;
            this.txtWare.Location = new System.Drawing.Point(69, 20);
            this.txtWare.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtWare.Name = "txtWare";
            this.txtWare.Size = new System.Drawing.Size(210, 21);
            this.txtWare.TabIndex = 35;
            // 
            // txtWareType
            // 
            this.txtWareType.Enabled = false;
            this.txtWareType.Location = new System.Drawing.Point(69, 44);
            this.txtWareType.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtWareType.Name = "txtWareType";
            this.txtWareType.Size = new System.Drawing.Size(210, 21);
            this.txtWareType.TabIndex = 36;
            // 
            // txtItem
            // 
            this.txtItem.Enabled = false;
            this.txtItem.Location = new System.Drawing.Point(69, 71);
            this.txtItem.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(211, 21);
            this.txtItem.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Owner";
            // 
            // CB_Owner
            // 
            this.CB_Owner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Owner.FormattingEnabled = true;
            this.CB_Owner.Location = new System.Drawing.Point(245, 113);
            this.CB_Owner.Name = "CB_Owner";
            this.CB_Owner.Size = new System.Drawing.Size(211, 21);
            this.CB_Owner.TabIndex = 40;
            this.CB_Owner.SelectedIndexChanged += new System.EventHandler(this.CB_Owner_SelectedIndexChanged);
            // 
            // txtFreeStk
            // 
            this.txtFreeStk.EditValue = "0";
            this.txtFreeStk.Enabled = false;
            this.txtFreeStk.Location = new System.Drawing.Point(72, 112);
            this.txtFreeStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtFreeStk.Name = "txtFreeStk";
            this.txtFreeStk.Size = new System.Drawing.Size(83, 21);
            this.txtFreeStk.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(308, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "Total";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(169, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Assigned";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(91, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Free";
            // 
            // txtTotalStk
            // 
            this.txtTotalStk.EditValue = "0";
            this.txtTotalStk.Enabled = false;
            this.txtTotalStk.Location = new System.Drawing.Point(289, 72);
            this.txtTotalStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtTotalStk.Name = "txtTotalStk";
            this.txtTotalStk.Size = new System.Drawing.Size(83, 21);
            this.txtTotalStk.TabIndex = 50;
            // 
            // txtAsgStk
            // 
            this.txtAsgStk.EditValue = "0";
            this.txtAsgStk.Enabled = false;
            this.txtAsgStk.Location = new System.Drawing.Point(157, 112);
            this.txtAsgStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtAsgStk.Name = "txtAsgStk";
            this.txtAsgStk.Size = new System.Drawing.Size(83, 21);
            this.txtAsgStk.TabIndex = 51;
            // 
            // txtOnwStk
            // 
            this.txtOnwStk.EditValue = "0";
            this.txtOnwStk.Enabled = false;
            this.txtOnwStk.Location = new System.Drawing.Point(462, 114);
            this.txtOnwStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtOnwStk.Name = "txtOnwStk";
            this.txtOnwStk.Size = new System.Drawing.Size(83, 21);
            this.txtOnwStk.TabIndex = 56;
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
            // CB_OwnDEST
            // 
            this.CB_OwnDEST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_OwnDEST.FormattingEnabled = true;
            this.CB_OwnDEST.Location = new System.Drawing.Point(550, 28);
            this.CB_OwnDEST.Name = "CB_OwnDEST";
            this.CB_OwnDEST.Size = new System.Drawing.Size(198, 21);
            this.CB_OwnDEST.TabIndex = 65;
            this.CB_OwnDEST.SelectedIndexChanged += new System.EventHandler(this.CB_OwnDEST_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(509, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 66;
            this.label11.Text = "Owner";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(647, 109);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 68;
            this.label12.Text = "Warehouse Dest";
            // 
            // BtnMoveStk
            // 
            this.BtnMoveStk.Location = new System.Drawing.Point(934, 127);
            this.BtnMoveStk.Name = "BtnMoveStk";
            this.BtnMoveStk.Size = new System.Drawing.Size(79, 22);
            this.BtnMoveStk.TabIndex = 59;
            this.BtnMoveStk.Text = "Move";
            this.BtnMoveStk.UseVisualStyleBackColor = true;
            this.BtnMoveStk.Click += new System.EventHandler(this.BtnMoveStk_Click);
            // 
            // CB_WareDEST
            // 
            this.CB_WareDEST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_WareDEST.FormattingEnabled = true;
            this.CB_WareDEST.Location = new System.Drawing.Point(643, 125);
            this.CB_WareDEST.Name = "CB_WareDEST";
            this.CB_WareDEST.Size = new System.Drawing.Size(197, 21);
            this.CB_WareDEST.TabIndex = 72;
            this.CB_WareDEST.SelectedIndexChanged += new System.EventHandler(this.CB_WareDEST_SelectedIndexChanged);
            // 
            // BtnFreeOwner
            // 
            this.BtnFreeOwner.Location = new System.Drawing.Point(549, 113);
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
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.txtQttMov);
            this.groupBox1.Controls.Add(this.txtIAsgnLot);
            this.groupBox1.Controls.Add(this.txtAdjustFreeStk);
            this.groupBox1.Controls.Add(this.btnAssignLot);
            this.groupBox1.Controls.Add(this.btnAssignOwner);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtQttAssignLot);
            this.groupBox1.Controls.Add(this.txtQttAssignOwn);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.BtnFreeLot);
            this.groupBox1.Controls.Add(this.txtLotStk);
            this.groupBox1.Controls.Add(this.txtAsgLot);
            this.groupBox1.Controls.Add(this.txtFreeLot);
            this.groupBox1.Controls.Add(this.CB_Lots);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCerateXLTemplate);
            this.groupBox1.Controls.Add(this.BtnImportExcel);
            this.groupBox1.Controls.Add(this.BtnAdjustFree);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Controls.Add(this.BtnFreeOwner);
            this.groupBox1.Controls.Add(this.CB_WareDEST);
            this.groupBox1.Controls.Add(this.BtnMoveStk);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.CB_OwnDEST);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtOnwStk);
            this.groupBox1.Controls.Add(this.txtAsgStk);
            this.groupBox1.Controls.Add(this.txtTotalStk);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtFreeStk);
            this.groupBox1.Controls.Add(this.CB_Owner);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtItem);
            this.groupBox1.Controls.Add(this.txtWareType);
            this.groupBox1.Controls.Add(this.txtWare);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1082, 192);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stock Actions";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.textBox1.Location = new System.Drawing.Point(637, 159);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(376, 23);
            this.textBox1.TabIndex = 102;
            this.textBox1.Text = "To move Stk to Destination Warehouse both Lot and Owner must be selected.";
            // 
            // txtQttMov
            // 
            this.txtQttMov.EditValue = "0";
            this.txtQttMov.Enabled = false;
            this.txtQttMov.Location = new System.Drawing.Point(846, 126);
            this.txtQttMov.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtQttMov.Name = "txtQttMov";
            this.txtQttMov.Size = new System.Drawing.Size(83, 21);
            this.txtQttMov.TabIndex = 101;
            this.txtQttMov.EditValueChanged += new System.EventHandler(this.txtQttMov_EditValueChanged);
            // 
            // txtIAsgnLot
            // 
            this.txtIAsgnLot.Enabled = false;
            this.txtIAsgnLot.Location = new System.Drawing.Point(550, 52);
            this.txtIAsgnLot.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtIAsgnLot.Name = "txtIAsgnLot";
            this.txtIAsgnLot.Size = new System.Drawing.Size(197, 21);
            this.txtIAsgnLot.TabIndex = 100;
            this.txtIAsgnLot.EditValueChanged += new System.EventHandler(this.txtIAsgnLot_EditValueChanged);
            // 
            // txtAdjustFreeStk
            // 
            this.txtAdjustFreeStk.EditValue = "0";
            this.txtAdjustFreeStk.Enabled = false;
            this.txtAdjustFreeStk.Location = new System.Drawing.Point(462, 161);
            this.txtAdjustFreeStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtAdjustFreeStk.Name = "txtAdjustFreeStk";
            this.txtAdjustFreeStk.Size = new System.Drawing.Size(83, 21);
            this.txtAdjustFreeStk.TabIndex = 96;
            this.txtAdjustFreeStk.EditValueChanged += new System.EventHandler(this.txtAdjustFreeStk_EditValueChanged);
            // 
            // btnAssignLot
            // 
            this.btnAssignLot.Location = new System.Drawing.Point(841, 52);
            this.btnAssignLot.Name = "btnAssignLot";
            this.btnAssignLot.Size = new System.Drawing.Size(79, 22);
            this.btnAssignLot.TabIndex = 94;
            this.btnAssignLot.Text = "Assign Lot";
            this.btnAssignLot.UseVisualStyleBackColor = true;
            this.btnAssignLot.Click += new System.EventHandler(this.btnAssignLot_Click);
            // 
            // btnAssignOwner
            // 
            this.btnAssignOwner.Location = new System.Drawing.Point(841, 28);
            this.btnAssignOwner.Name = "btnAssignOwner";
            this.btnAssignOwner.Size = new System.Drawing.Size(80, 22);
            this.btnAssignOwner.TabIndex = 93;
            this.btnAssignOwner.Text = "Assign Owner";
            this.btnAssignOwner.UseVisualStyleBackColor = true;
            this.btnAssignOwner.Click += new System.EventHandler(this.btnAssignOwner_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(771, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 92;
            this.label6.Text = "Quantity";
            // 
            // txtQttAssignLot
            // 
            this.txtQttAssignLot.EditValue = "0";
            this.txtQttAssignLot.Enabled = false;
            this.txtQttAssignLot.Location = new System.Drawing.Point(753, 53);
            this.txtQttAssignLot.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtQttAssignLot.Name = "txtQttAssignLot";
            this.txtQttAssignLot.Size = new System.Drawing.Size(83, 21);
            this.txtQttAssignLot.TabIndex = 91;
            this.txtQttAssignLot.EditValueChanged += new System.EventHandler(this.txtQttAssignLot_EditValueChanged);
            // 
            // txtQttAssignOwn
            // 
            this.txtQttAssignOwn.EditValue = "0";
            this.txtQttAssignOwn.Enabled = false;
            this.txtQttAssignOwn.Location = new System.Drawing.Point(753, 29);
            this.txtQttAssignOwn.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtQttAssignOwn.Name = "txtQttAssignOwn";
            this.txtQttAssignOwn.Size = new System.Drawing.Size(83, 21);
            this.txtQttAssignOwn.TabIndex = 90;
            this.txtQttAssignOwn.EditValueChanged += new System.EventHandler(this.txtQttAssignOwn_EditValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(482, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 89;
            this.label10.Text = "Quantity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(524, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 86;
            this.label4.Text = "Lot";
            // 
            // BtnFreeLot
            // 
            this.BtnFreeLot.Location = new System.Drawing.Point(549, 137);
            this.BtnFreeLot.Name = "BtnFreeLot";
            this.BtnFreeLot.Size = new System.Drawing.Size(83, 22);
            this.BtnFreeLot.TabIndex = 85;
            this.BtnFreeLot.Text = "Free Lot";
            this.BtnFreeLot.UseVisualStyleBackColor = true;
            this.BtnFreeLot.Click += new System.EventHandler(this.BtnFreeLot_Click);
            // 
            // txtLotStk
            // 
            this.txtLotStk.EditValue = "0";
            this.txtLotStk.Enabled = false;
            this.txtLotStk.Location = new System.Drawing.Point(462, 138);
            this.txtLotStk.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtLotStk.Name = "txtLotStk";
            this.txtLotStk.Size = new System.Drawing.Size(83, 21);
            this.txtLotStk.TabIndex = 84;
            this.txtLotStk.EditValueChanged += new System.EventHandler(this.txtLotStk_EditValueChanged);
            // 
            // txtAsgLot
            // 
            this.txtAsgLot.EditValue = "0";
            this.txtAsgLot.Enabled = false;
            this.txtAsgLot.Location = new System.Drawing.Point(157, 138);
            this.txtAsgLot.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtAsgLot.Name = "txtAsgLot";
            this.txtAsgLot.Size = new System.Drawing.Size(83, 21);
            this.txtAsgLot.TabIndex = 83;
            // 
            // txtFreeLot
            // 
            this.txtFreeLot.EditValue = "0";
            this.txtFreeLot.Enabled = false;
            this.txtFreeLot.Location = new System.Drawing.Point(72, 138);
            this.txtFreeLot.MinimumSize = new System.Drawing.Size(0, 21);
            this.txtFreeLot.Name = "txtFreeLot";
            this.txtFreeLot.Size = new System.Drawing.Size(83, 21);
            this.txtFreeLot.TabIndex = 82;
            // 
            // CB_Lots
            // 
            this.CB_Lots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Lots.FormattingEnabled = true;
            this.CB_Lots.Location = new System.Drawing.Point(245, 138);
            this.CB_Lots.Name = "CB_Lots";
            this.CB_Lots.Size = new System.Drawing.Size(211, 21);
            this.CB_Lots.TabIndex = 81;
            this.CB_Lots.SelectedIndexChanged += new System.EventHandler(this.CB_Lots_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "Lot";
            // 
            // btnCerateXLTemplate
            // 
            this.btnCerateXLTemplate.Location = new System.Drawing.Point(994, 57);
            this.btnCerateXLTemplate.Name = "btnCerateXLTemplate";
            this.btnCerateXLTemplate.Size = new System.Drawing.Size(85, 35);
            this.btnCerateXLTemplate.TabIndex = 79;
            this.btnCerateXLTemplate.Text = "Create XL Template";
            this.btnCerateXLTemplate.UseVisualStyleBackColor = true;
            this.btnCerateXLTemplate.Click += new System.EventHandler(this.btnCerateXLTemplate_Click);
            // 
            // BtnImportExcel
            // 
            this.BtnImportExcel.Location = new System.Drawing.Point(994, 34);
            this.BtnImportExcel.Name = "BtnImportExcel";
            this.BtnImportExcel.Size = new System.Drawing.Size(85, 21);
            this.BtnImportExcel.TabIndex = 78;
            this.BtnImportExcel.Text = "Import XL";
            this.BtnImportExcel.UseVisualStyleBackColor = true;
            this.BtnImportExcel.Click += new System.EventHandler(this.BtnImportExcel_Click);
            // 
            // BtnAdjustFree
            // 
            this.BtnAdjustFree.Location = new System.Drawing.Point(548, 161);
            this.BtnAdjustFree.Name = "BtnAdjustFree";
            this.BtnAdjustFree.Size = new System.Drawing.Size(83, 22);
            this.BtnAdjustFree.TabIndex = 76;
            this.BtnAdjustFree.Text = "Adj. Free Stk";
            this.BtnAdjustFree.UseVisualStyleBackColor = true;
            this.BtnAdjustFree.Click += new System.EventHandler(this.BtnAdjustOwner_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BtnSave.Location = new System.Drawing.Point(994, 6);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(85, 28);
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
            this.tabStk.Controls.Add(this.tabPage3);
            this.tabStk.Location = new System.Drawing.Point(5, 200);
            this.tabStk.Name = "tabStk";
            this.tabStk.SelectedIndex = 0;
            this.tabStk.Size = new System.Drawing.Size(1081, 414);
            this.tabStk.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GC_Stocks);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1073, 388);
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
            this.tabPage2.Size = new System.Drawing.Size(1073, 388);
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
            this.GC_Movs.Size = new System.Drawing.Size(1067, 382);
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
            this.tabPage3.Size = new System.Drawing.Size(1073, 388);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ImportData";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // BtnExecuteImport
            // 
            this.BtnExecuteImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExecuteImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BtnExecuteImport.Location = new System.Drawing.Point(998, 359);
            this.BtnExecuteImport.Name = "BtnExecuteImport";
            this.BtnExecuteImport.Size = new System.Drawing.Size(72, 26);
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
            this.GC_ImportExcel.Size = new System.Drawing.Size(1067, 351);
            this.GC_ImportExcel.TabIndex = 2;
            this.GC_ImportExcel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.GC_ImportExcel;
            this.gridView3.Name = "gridView3";
            // 
            // StockManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 617);
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
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalStk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAsgStk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOnwStk.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQttMov.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIAsgnLot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdjustFreeStk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQttAssignLot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQttAssignOwn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotStk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAsgLot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFreeLot.Properties)).EndInit();
            this.tabStk.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GC_Movs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GC_ImportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit txtTotalStk;
        private DevExpress.XtraEditors.TextEdit txtAsgStk;
        private DevExpress.XtraEditors.TextEdit txtOnwStk;
        private System.Windows.Forms.Button button2;
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
        private System.Windows.Forms.Button BtnAdjustFree;
        private System.Windows.Forms.Button BtnImportExcel;
        private System.Windows.Forms.TabPage tabPage3;
        private DevExpress.XtraGrid.GridControl GC_ImportExcel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.Button BtnExecuteImport;
        private System.Windows.Forms.Button btnCerateXLTemplate;
        private System.Windows.Forms.Button btnAssignLot;
        private System.Windows.Forms.Button btnAssignOwner;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtQttAssignLot;
        private DevExpress.XtraEditors.TextEdit txtQttAssignOwn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnFreeLot;
        private DevExpress.XtraEditors.TextEdit txtLotStk;
        private DevExpress.XtraEditors.TextEdit txtAsgLot;
        private DevExpress.XtraEditors.TextEdit txtFreeLot;
        private System.Windows.Forms.ComboBox CB_Lots;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtAdjustFreeStk;
        private DevExpress.XtraEditors.TextEdit txtIAsgnLot;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.TextEdit txtQttMov;
    }
}