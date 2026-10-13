using System.Windows.Forms;

namespace ImportFileParser
{
    partial class frmMain : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cmnGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAddRule = new System.Windows.Forms.ToolStripMenuItem();
            this.newTransactionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleReviewedStatusToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miMergeTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.miSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miApplyRules = new System.Windows.Forms.ToolStripMenuItem();
            this.miUpdateBalance = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slProcHint = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslMCC = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarColumn1 = new ImportFileParser.CalendarColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbCriteria = new System.Windows.Forms.GroupBox();
            this.lblTransId = new System.Windows.Forms.Label();
            this.txtTransId = new System.Windows.Forms.TextBox();
            this.cbAmountEnabled = new System.Windows.Forms.CheckBox();
            this.nmAmount = new System.Windows.Forms.NumericUpDown();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.cbAccount = new System.Windows.Forms.ComboBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromBankFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRuleForSelectedRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTransactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditTransToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleReviewedStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMergeTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.splitBackToSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.applyRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alfaReplaceNonreconcliedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompareTransToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miRecalcEndBalance = new System.Windows.Forms.ToolStripMenuItem();
            this.ValidateDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.experditureChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.остаткиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageCagetoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miMccCodes = new System.Windows.Forms.ToolStripMenuItem();
            this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDebugPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miTests = new System.Windows.Forms.ToolStripMenuItem();
            this.miTestMergeTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.тестЗагрузкиИзАльфыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TransDataGV = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reviewed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FromAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ToAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Date = new ImportFileParser.CalendarColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RowNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.cmnLogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHideLog = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnGridMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmAmount)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransDataGV)).BeginInit();
            this.cmnLogMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cmnGridMenu
            // 
            this.cmnGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddRule,
            this.newTransactionToolStripMenuItem1,
            this.miEditTrans,
            this.toggleReviewedStatusToolStripMenuItem1,
            this.miMergeTrans,
            this.miSplit,
            this.toolStripSeparator1,
            this.miApplyRules,
            this.miUpdateBalance});
            this.cmnGridMenu.Name = "cmnGridMenu";
            this.cmnGridMenu.Size = new System.Drawing.Size(215, 186);
            this.cmnGridMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmnGridMenu_Opening);
            // 
            // miAddRule
            // 
            this.miAddRule.Name = "miAddRule";
            this.miAddRule.Size = new System.Drawing.Size(214, 22);
            this.miAddRule.Text = "Добавить правило";
            this.miAddRule.Click += new System.EventHandler(this.miAddRule_Click);
            // 
            // newTransactionToolStripMenuItem1
            // 
            this.newTransactionToolStripMenuItem1.Name = "newTransactionToolStripMenuItem1";
            this.newTransactionToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.newTransactionToolStripMenuItem1.Text = "Новая транзакция";
            this.newTransactionToolStripMenuItem1.Click += new System.EventHandler(this.newTransactionToolStripMenuItem_Click);
            // 
            // miEditTrans
            // 
            this.miEditTrans.Name = "miEditTrans";
            this.miEditTrans.Size = new System.Drawing.Size(214, 22);
            this.miEditTrans.Text = "Редактировать";
            this.miEditTrans.Click += new System.EventHandler(this.miEditTransToolStripMenuItem_Click);
            // 
            // toggleReviewedStatusToolStripMenuItem1
            // 
            this.toggleReviewedStatusToolStripMenuItem1.Name = "toggleReviewedStatusToolStripMenuItem1";
            this.toggleReviewedStatusToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.toggleReviewedStatusToolStripMenuItem1.Text = "Пометить/Снять пометку";
            this.toggleReviewedStatusToolStripMenuItem1.Click += new System.EventHandler(this.toggleReviewedStatusToolStripMenuItem1_Click);
            // 
            // miMergeTrans
            // 
            this.miMergeTrans.Name = "miMergeTrans";
            this.miMergeTrans.Size = new System.Drawing.Size(214, 22);
            this.miMergeTrans.Text = "Объединить";
            this.miMergeTrans.Click += new System.EventHandler(this.miMergeTrans_Click);
            // 
            // miSplit
            // 
            this.miSplit.Name = "miSplit";
            this.miSplit.Size = new System.Drawing.Size(214, 22);
            this.miSplit.Text = "Разделить на исходные";
            this.miSplit.Click += new System.EventHandler(this.miSplit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
            // 
            // miApplyRules
            // 
            this.miApplyRules.Name = "miApplyRules";
            this.miApplyRules.Size = new System.Drawing.Size(214, 22);
            this.miApplyRules.Text = "Применить правила";
            this.miApplyRules.Click += new System.EventHandler(this.applyRulesToolStripMenuItem1_Click);
            // 
            // miUpdateBalance
            // 
            this.miUpdateBalance.Name = "miUpdateBalance";
            this.miUpdateBalance.Size = new System.Drawing.Size(214, 22);
            this.miUpdateBalance.Text = "Обновить баланс";
            this.miUpdateBalance.Click += new System.EventHandler(this.miUpdateBalance_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slProcHint,
            this.tsslMCC});
            this.statusStrip1.Location = new System.Drawing.Point(0, 727);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1360, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slProcHint
            // 
            this.slProcHint.Name = "slProcHint";
            this.slProcHint.Size = new System.Drawing.Size(1287, 17);
            this.slProcHint.Spring = true;
            this.slProcHint.Text = "Rule processing hint";
            // 
            // tsslMCC
            // 
            this.tsslMCC.Name = "tsslMCC";
            this.tsslMCC.Size = new System.Drawing.Size(58, 17);
            this.tsslMCC.Text = "MCC info";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // calendarColumn1
            // 
            this.calendarColumn1.DataPropertyName = "Date";
            this.calendarColumn1.HeaderText = "Дата";
            this.calendarColumn1.Name = "calendarColumn1";
            this.calendarColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "AmountFrom";
            dataGridViewCellStyle1.Format = "$#,##0.00";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn2.HeaderText = "Сумма";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn3.HeaderText = "Комментарий";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 400;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Color";
            this.dataGridViewTextBoxColumn4.HeaderText = "Color";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "EndBalance";
            this.dataGridViewTextBoxColumn5.HeaderText = "Баланс после";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "RowNum";
            this.dataGridViewTextBoxColumn6.HeaderText = "RowNum";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbCriteria);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1360, 89);
            this.panel1.TabIndex = 17;
            // 
            // gbCriteria
            // 
            this.gbCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCriteria.Controls.Add(this.lblTransId);
            this.gbCriteria.Controls.Add(this.txtTransId);
            this.gbCriteria.Controls.Add(this.cbAmountEnabled);
            this.gbCriteria.Controls.Add(this.nmAmount);
            this.gbCriteria.Controls.Add(this.btnClear);
            this.gbCriteria.Controls.Add(this.cbCategory);
            this.gbCriteria.Controls.Add(this.lblCategory);
            this.gbCriteria.Controls.Add(this.lblToDate);
            this.gbCriteria.Controls.Add(this.dtpToDate);
            this.gbCriteria.Controls.Add(this.lblFromDate);
            this.gbCriteria.Controls.Add(this.dtpFromDate);
            this.gbCriteria.Controls.Add(this.btnShow);
            this.gbCriteria.Controls.Add(this.cbAccount);
            this.gbCriteria.Controls.Add(this.lblAccount);
            this.gbCriteria.Location = new System.Drawing.Point(0, 27);
            this.gbCriteria.Name = "gbCriteria";
            this.gbCriteria.Size = new System.Drawing.Size(1360, 58);
            this.gbCriteria.TabIndex = 15;
            this.gbCriteria.TabStop = false;
            this.gbCriteria.Text = "Параметры выборки";
            // 
            // lblTransId
            // 
            this.lblTransId.AutoSize = true;
            this.lblTransId.Location = new System.Drawing.Point(680, 16);
            this.lblTransId.Name = "lblTransId";
            this.lblTransId.Size = new System.Drawing.Size(16, 13);
            this.lblTransId.TabIndex = 27;
            this.lblTransId.Text = "Id";
            // 
            // txtTransId
            // 
            this.txtTransId.Location = new System.Drawing.Point(683, 33);
            this.txtTransId.Name = "txtTransId";
            this.txtTransId.Size = new System.Drawing.Size(208, 20);
            this.txtTransId.TabIndex = 26;
            // 
            // cbAmountEnabled
            // 
            this.cbAmountEnabled.AutoSize = true;
            this.cbAmountEnabled.Location = new System.Drawing.Point(568, 15);
            this.cbAmountEnabled.Name = "cbAmountEnabled";
            this.cbAmountEnabled.Size = new System.Drawing.Size(60, 17);
            this.cbAmountEnabled.TabIndex = 25;
            this.cbAmountEnabled.Text = "Сумма";
            this.cbAmountEnabled.UseVisualStyleBackColor = true;
            this.cbAmountEnabled.CheckedChanged += new System.EventHandler(this.cbAmountEnabled_CheckedChanged);
            // 
            // nmAmount
            // 
            this.nmAmount.DecimalPlaces = 2;
            this.nmAmount.Enabled = false;
            this.nmAmount.Location = new System.Drawing.Point(568, 33);
            this.nmAmount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmAmount.Name = "nmAmount";
            this.nmAmount.Size = new System.Drawing.Size(108, 20);
            this.nmAmount.TabIndex = 24;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(996, 14);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 39);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(384, 32);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(177, 21);
            this.cbCategory.TabIndex = 19;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(381, 16);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(60, 13);
            this.lblCategory.TabIndex = 20;
            this.lblCategory.Text = "Категория";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(283, 16);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(83, 13);
            this.lblToDate.TabIndex = 18;
            this.lblToDate.Text = "Конец периода";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(286, 32);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(92, 20);
            this.dtpToDate.TabIndex = 17;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(185, 16);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(74, 13);
            this.lblFromDate.TabIndex = 16;
            this.lblFromDate.Text = "Нач. периода";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(188, 32);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(92, 20);
            this.dtpFromDate.TabIndex = 15;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(897, 14);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(93, 39);
            this.btnShow.TabIndex = 14;
            this.btnShow.Text = "Показать";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // cbAccount
            // 
            this.cbAccount.FormattingEnabled = true;
            this.cbAccount.Location = new System.Drawing.Point(6, 32);
            this.cbAccount.Name = "cbAccount";
            this.cbAccount.Size = new System.Drawing.Size(177, 21);
            this.cbAccount.TabIndex = 6;
            this.cbAccount.SelectedValueChanged += new System.EventHandler(this.cbAccount_SelectedValueChanged);
            this.cbAccount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbAccount_KeyPress);
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(3, 16);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(30, 13);
            this.lblAccount.TabIndex = 7;
            this.lblAccount.Text = "Счёт";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.DebugToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1360, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFromBankFilesToolStripMenuItem,
            this.saveToDBToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // loadFromBankFilesToolStripMenuItem
            // 
            this.loadFromBankFilesToolStripMenuItem.Name = "loadFromBankFilesToolStripMenuItem";
            this.loadFromBankFilesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.loadFromBankFilesToolStripMenuItem.Text = "Загрузить из файлов";
            this.loadFromBankFilesToolStripMenuItem.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // saveToDBToolStripMenuItem
            // 
            this.saveToDBToolStripMenuItem.Name = "saveToDBToolStripMenuItem";
            this.saveToDBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToDBToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.saveToDBToolStripMenuItem.Text = "Сохранить";
            this.saveToDBToolStripMenuItem.Click += new System.EventHandler(this.saveToDBToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRuleForSelectedRowToolStripMenuItem,
            this.newTransactionToolStripMenuItem,
            this.miEditTransToolStripMenuItem,
            this.toggleReviewedStatusToolStripMenuItem,
            this.tsmiMergeTrans,
            this.splitBackToSourceToolStripMenuItem,
            this.toolStripSeparator2,
            this.applyRulesToolStripMenuItem,
            this.alfaReplaceNonreconcliedToolStripMenuItem,
            this.CompareTransToolStripMenuItem,
            this.miRecalcEndBalance,
            this.ValidateDBToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.actionsToolStripMenuItem.Text = "Транзакции";
            this.actionsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.actionsToolStripMenuItem_DropDownOpening);
            // 
            // addRuleForSelectedRowToolStripMenuItem
            // 
            this.addRuleForSelectedRowToolStripMenuItem.Name = "addRuleForSelectedRowToolStripMenuItem";
            this.addRuleForSelectedRowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.addRuleForSelectedRowToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.addRuleForSelectedRowToolStripMenuItem.Text = "Добавить правило";
            this.addRuleForSelectedRowToolStripMenuItem.Click += new System.EventHandler(this.addRuleForSelectedRowToolStripMenuItem_Click);
            // 
            // newTransactionToolStripMenuItem
            // 
            this.newTransactionToolStripMenuItem.Name = "newTransactionToolStripMenuItem";
            this.newTransactionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newTransactionToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.newTransactionToolStripMenuItem.Text = "Новая транзакция";
            this.newTransactionToolStripMenuItem.Click += new System.EventHandler(this.newTransactionToolStripMenuItem_Click);
            // 
            // miEditTransToolStripMenuItem
            // 
            this.miEditTransToolStripMenuItem.Name = "miEditTransToolStripMenuItem";
            this.miEditTransToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.miEditTransToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miEditTransToolStripMenuItem.Text = "Редактировать";
            this.miEditTransToolStripMenuItem.Click += new System.EventHandler(this.miEditTransToolStripMenuItem_Click);
            // 
            // toggleReviewedStatusToolStripMenuItem
            // 
            this.toggleReviewedStatusToolStripMenuItem.Name = "toggleReviewedStatusToolStripMenuItem";
            this.toggleReviewedStatusToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.toggleReviewedStatusToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.toggleReviewedStatusToolStripMenuItem.Text = "Пометить/Снять пометку";
            this.toggleReviewedStatusToolStripMenuItem.Click += new System.EventHandler(this.toggleReviewedStatusToolStripMenuItem1_Click);
            // 
            // tsmiMergeTrans
            // 
            this.tsmiMergeTrans.Name = "tsmiMergeTrans";
            this.tsmiMergeTrans.Size = new System.Drawing.Size(280, 22);
            this.tsmiMergeTrans.Text = "Объединить";
            this.tsmiMergeTrans.Click += new System.EventHandler(this.tsmiMergeTrans_Click);
            // 
            // splitBackToSourceToolStripMenuItem
            // 
            this.splitBackToSourceToolStripMenuItem.Name = "splitBackToSourceToolStripMenuItem";
            this.splitBackToSourceToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.splitBackToSourceToolStripMenuItem.Text = "Разделить на исходные";
            this.splitBackToSourceToolStripMenuItem.Click += new System.EventHandler(this.splitBackToSourceToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(277, 6);
            // 
            // applyRulesToolStripMenuItem
            // 
            this.applyRulesToolStripMenuItem.Name = "applyRulesToolStripMenuItem";
            this.applyRulesToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.applyRulesToolStripMenuItem.Text = "Применить правила";
            this.applyRulesToolStripMenuItem.Click += new System.EventHandler(this.applyRulesToolStripMenuItem_Click);
            // 
            // alfaReplaceNonreconcliedToolStripMenuItem
            // 
            this.alfaReplaceNonreconcliedToolStripMenuItem.Name = "alfaReplaceNonreconcliedToolStripMenuItem";
            this.alfaReplaceNonreconcliedToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.alfaReplaceNonreconcliedToolStripMenuItem.Text = "Альфа: заменить на подтверждённые";
            this.alfaReplaceNonreconcliedToolStripMenuItem.Click += new System.EventHandler(this.alfaReplaceNonreconcliedToolStripMenuItem_Click);
            // 
            // CompareTransToolStripMenuItem
            // 
            this.CompareTransToolStripMenuItem.Name = "CompareTransToolStripMenuItem";
            this.CompareTransToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.CompareTransToolStripMenuItem.Text = "Сравнить с выпиской";
            this.CompareTransToolStripMenuItem.Click += new System.EventHandler(this.CompareTransToolStripMenuItem_Click);
            // 
            // miRecalcEndBalance
            // 
            this.miRecalcEndBalance.Name = "miRecalcEndBalance";
            this.miRecalcEndBalance.Size = new System.Drawing.Size(280, 22);
            this.miRecalcEndBalance.Text = "Обновить баланс";
            this.miRecalcEndBalance.Click += new System.EventHandler(this.miRecalcEndBalance_Click);
            // 
            // ValidateDBToolStripMenuItem
            // 
            this.ValidateDBToolStripMenuItem.Name = "ValidateDBToolStripMenuItem";
            this.ValidateDBToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.ValidateDBToolStripMenuItem.Text = "Проверить базу";
            this.ValidateDBToolStripMenuItem.Click += new System.EventHandler(this.ValidateDBToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.experditureChartToolStripMenuItem,
            this.остаткиToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.reportsToolStripMenuItem.Text = "Отчёты";
            // 
            // experditureChartToolStripMenuItem
            // 
            this.experditureChartToolStripMenuItem.Name = "experditureChartToolStripMenuItem";
            this.experditureChartToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.experditureChartToolStripMenuItem.Text = "План/Факт";
            this.experditureChartToolStripMenuItem.Click += new System.EventHandler(this.experditureChartToolStripMenuItem_Click);
            // 
            // остаткиToolStripMenuItem
            // 
            this.остаткиToolStripMenuItem.Name = "остаткиToolStripMenuItem";
            this.остаткиToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.остаткиToolStripMenuItem.Text = "Остатки";
            this.остаткиToolStripMenuItem.Click += new System.EventHandler(this.остаткиToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountsToolStripMenuItem,
            this.manageCagetoriesToolStripMenuItem,
            this.rulesToolStripMenuItem,
            this.miMccCodes,
            this.miSettings});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.settingsToolStripMenuItem.Text = "Настройки";
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.accountsToolStripMenuItem.Text = "Список счетов";
            this.accountsToolStripMenuItem.Click += new System.EventHandler(this.accountsToolStripMenuItem_Click);
            // 
            // manageCagetoriesToolStripMenuItem
            // 
            this.manageCagetoriesToolStripMenuItem.Name = "manageCagetoriesToolStripMenuItem";
            this.manageCagetoriesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.manageCagetoriesToolStripMenuItem.Text = "Список категорий";
            this.manageCagetoriesToolStripMenuItem.Click += new System.EventHandler(this.manageCagetoriesToolStripMenuItem_Click);
            // 
            // rulesToolStripMenuItem
            // 
            this.rulesToolStripMenuItem.Name = "rulesToolStripMenuItem";
            this.rulesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.rulesToolStripMenuItem.Text = "Список правил";
            this.rulesToolStripMenuItem.Click += new System.EventHandler(this.rulesToolStripMenuItem_Click);
            // 
            // miMccCodes
            // 
            this.miMccCodes.Name = "miMccCodes";
            this.miMccCodes.Size = new System.Drawing.Size(174, 22);
            this.miMccCodes.Text = "MCC коды";
            this.miMccCodes.Click += new System.EventHandler(this.miMccCodes_Click);
            // 
            // miSettings
            // 
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(174, 22);
            this.miSettings.Text = "Настройки";
            this.miSettings.Click += new System.EventHandler(this.miSettings_Click);
            // 
            // DebugToolStripMenuItem
            // 
            this.DebugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDebugPanelToolStripMenuItem,
            this.miTests});
            this.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem";
            this.DebugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.DebugToolStripMenuItem.Text = "Debug";
            // 
            // showDebugPanelToolStripMenuItem
            // 
            this.showDebugPanelToolStripMenuItem.Name = "showDebugPanelToolStripMenuItem";
            this.showDebugPanelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showDebugPanelToolStripMenuItem.Text = "Show debug panel";
            this.showDebugPanelToolStripMenuItem.Click += new System.EventHandler(this.showDebugPanelToolStripMenuItem_Click);
            // 
            // miTests
            // 
            this.miTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTestMergeTrans,
            this.тестЗагрузкиИзАльфыToolStripMenuItem});
            this.miTests.Name = "miTests";
            this.miTests.Size = new System.Drawing.Size(180, 22);
            this.miTests.Text = "Tests";
            // 
            // miTestMergeTrans
            // 
            this.miTestMergeTrans.Name = "miTestMergeTrans";
            this.miTestMergeTrans.Size = new System.Drawing.Size(212, 22);
            this.miTestMergeTrans.Text = "Тест слияния транзакций";
            // 
            // тестЗагрузкиИзАльфыToolStripMenuItem
            // 
            this.тестЗагрузкиИзАльфыToolStripMenuItem.Name = "тестЗагрузкиИзАльфыToolStripMenuItem";
            this.тестЗагрузкиИзАльфыToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.тестЗагрузкиИзАльфыToolStripMenuItem.Text = "Тест загрузки из Альфы";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 89);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TransDataGV);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbLog);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(1360, 638);
            this.splitContainer1.SplitterDistance = 532;
            this.splitContainer1.TabIndex = 18;
            // 
            // TransDataGV
            // 
            this.TransDataGV.AllowUserToAddRows = false;
            this.TransDataGV.AllowUserToResizeRows = false;
            this.TransDataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TransDataGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Reviewed,
            this.FromAccount,
            this.ToAccount,
            this.Date,
            this.Amount,
            this.Description,
            this.Category,
            this.Color,
            this.EndBalance,
            this.RowNum});
            this.TransDataGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransDataGV.Location = new System.Drawing.Point(0, 0);
            this.TransDataGV.Name = "TransDataGV";
            this.TransDataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TransDataGV.Size = new System.Drawing.Size(1360, 638);
            this.TransDataGV.TabIndex = 1;
            this.TransDataGV.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TransDataGV_CellFormatting);
            this.TransDataGV.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.TransDataGV_DataError);
            this.TransDataGV.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.TransDataGV_RowEnter);
            this.TransDataGV.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.TransDataGV_RowPrePaint);
            this.TransDataGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.TransDataGV_RowsAdded);
            this.TransDataGV.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.TransDataGV_UserDeletingRow);
            this.TransDataGV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TransDataGV_MouseClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Reviewed
            // 
            this.Reviewed.DataPropertyName = "Reviewed";
            this.Reviewed.FalseValue = "false";
            this.Reviewed.HeaderText = "Подтв";
            this.Reviewed.Name = "Reviewed";
            this.Reviewed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Reviewed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Reviewed.TrueValue = "true";
            this.Reviewed.Width = 45;
            // 
            // FromAccount
            // 
            this.FromAccount.DataPropertyName = "FromAccount";
            this.FromAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FromAccount.HeaderText = "Со счёта";
            this.FromAccount.Name = "FromAccount";
            this.FromAccount.Width = 150;
            // 
            // ToAccount
            // 
            this.ToAccount.DataPropertyName = "ToAccount";
            this.ToAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ToAccount.HeaderText = "На счёт";
            this.ToAccount.Name = "ToAccount";
            this.ToAccount.Width = 150;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Дата";
            this.Date.Name = "Date";
            this.Date.Width = 120;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "AmountFrom";
            dataGridViewCellStyle2.Format = "$#,##0.00";
            dataGridViewCellStyle2.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle2;
            this.Amount.HeaderText = "Сумма";
            this.Amount.Name = "Amount";
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Комментарий";
            this.Description.Name = "Description";
            this.Description.Width = 400;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "Category";
            this.Category.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Category.HeaderText = "Категория";
            this.Category.Name = "Category";
            this.Category.Width = 150;
            // 
            // Color
            // 
            this.Color.DataPropertyName = "Color";
            this.Color.HeaderText = "Color";
            this.Color.Name = "Color";
            this.Color.ReadOnly = true;
            this.Color.Visible = false;
            // 
            // EndBalance
            // 
            this.EndBalance.DataPropertyName = "EndBalance";
            this.EndBalance.HeaderText = "Баланс после";
            this.EndBalance.Name = "EndBalance";
            this.EndBalance.ReadOnly = true;
            // 
            // RowNum
            // 
            this.RowNum.DataPropertyName = "RowNum";
            this.RowNum.HeaderText = "RowNum";
            this.RowNum.Name = "RowNum";
            this.RowNum.ReadOnly = true;
            this.RowNum.Visible = false;
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(0, 0);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(150, 46);
            this.lbLog.Sorted = true;
            this.lbLog.TabIndex = 17;
            this.lbLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbLog_MouseDown);
            // 
            // cmnLogMenu
            // 
            this.cmnLogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyTextToolStripMenuItem,
            this.tsmiClear,
            this.tsmiHideLog});
            this.cmnLogMenu.Name = "cmnLogMenu";
            this.cmnLogMenu.Size = new System.Drawing.Size(140, 70);
            // 
            // CopyTextToolStripMenuItem
            // 
            this.CopyTextToolStripMenuItem.Name = "CopyTextToolStripMenuItem";
            this.CopyTextToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.CopyTextToolStripMenuItem.Text = "Копировать";
            this.CopyTextToolStripMenuItem.Click += new System.EventHandler(this.CopyTextToolStripMenuItem_Click);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(139, 22);
            this.tsmiClear.Text = "Очистить";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // tsmiHideLog
            // 
            this.tsmiHideLog.Name = "tsmiHideLog";
            this.tsmiHideLog.Size = new System.Drawing.Size(139, 22);
            this.tsmiHideLog.Text = "Скрыть лог";
            this.tsmiHideLog.Click += new System.EventHandler(this.tsmiHideLog_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 749);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home Finance";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.cmnGridMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbCriteria.ResumeLayout(false);
            this.gbCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmAmount)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TransDataGV)).EndInit();
            this.cmnLogMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip cmnGridMenu;
        private System.Windows.Forms.ToolStripMenuItem miAddRule;
        private System.Windows.Forms.ToolStripMenuItem miSplit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miApplyRules;
        private System.Windows.Forms.ToolStripMenuItem toggleReviewedStatusToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newTransactionToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel slProcHint;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel tsslMCC;
        private System.Windows.Forms.ToolStripMenuItem miMergeTrans;
        private System.Windows.Forms.ToolStripMenuItem miEditTrans;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private CalendarColumn calendarColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private ToolStripMenuItem miUpdateBalance;
        private Panel panel1;
        private GroupBox gbCriteria;
        private CheckBox cbAmountEnabled;
        private NumericUpDown nmAmount;
        private Button btnClear;
        private ComboBox cbCategory;
        private Label lblCategory;
        private Label lblToDate;
        private DateTimePicker dtpToDate;
        private Label lblFromDate;
        private DateTimePicker dtpFromDate;
        private Button btnShow;
        private ComboBox cbAccount;
        private Label lblAccount;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadFromBankFilesToolStripMenuItem;
        private ToolStripMenuItem saveToDBToolStripMenuItem;
        private ToolStripMenuItem actionsToolStripMenuItem;
        private ToolStripMenuItem addRuleForSelectedRowToolStripMenuItem;
        private ToolStripMenuItem newTransactionToolStripMenuItem;
        private ToolStripMenuItem miEditTransToolStripMenuItem;
        private ToolStripMenuItem toggleReviewedStatusToolStripMenuItem;
        private ToolStripMenuItem tsmiMergeTrans;
        private ToolStripMenuItem splitBackToSourceToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem applyRulesToolStripMenuItem;
        private ToolStripMenuItem alfaReplaceNonreconcliedToolStripMenuItem;
        private ToolStripMenuItem CompareTransToolStripMenuItem;
        private ToolStripMenuItem miRecalcEndBalance;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem experditureChartToolStripMenuItem;
        private ToolStripMenuItem остаткиToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem accountsToolStripMenuItem;
        private ToolStripMenuItem manageCagetoriesToolStripMenuItem;
        private ToolStripMenuItem rulesToolStripMenuItem;
        private ToolStripMenuItem miMccCodes;
        private ToolStripMenuItem miSettings;
        private SplitContainer splitContainer1;
        private DataGridView TransDataGV;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewCheckBoxColumn Reviewed;
        private DataGridViewComboBoxColumn FromAccount;
        private DataGridViewComboBoxColumn ToAccount;
        private CalendarColumn Date;
        private DataGridViewTextBoxColumn Amount;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewComboBoxColumn Category;
        private DataGridViewTextBoxColumn Color;
        private DataGridViewTextBoxColumn EndBalance;
        private DataGridViewTextBoxColumn RowNum;
        private ListBox lbLog;
        private ToolStripMenuItem ValidateDBToolStripMenuItem;
        private ContextMenuStrip cmnLogMenu;
        private ToolStripMenuItem tsmiClear;
        private ToolStripMenuItem tsmiHideLog;
        private ToolStripMenuItem CopyTextToolStripMenuItem;
        private Label lblTransId;
        private TextBox txtTransId;
        private ToolStripMenuItem DebugToolStripMenuItem;
        private ToolStripMenuItem showDebugPanelToolStripMenuItem;
        private ToolStripMenuItem miTests;
        private ToolStripMenuItem miTestMergeTrans;
        private ToolStripMenuItem тестЗагрузкиИзАльфыToolStripMenuItem;
    }
}