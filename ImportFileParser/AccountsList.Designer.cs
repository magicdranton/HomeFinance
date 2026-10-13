using ImportFileParser.Common;
using ImportFileParser.Managers;

namespace ImportFileParser
{
    partial class AccountsList
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
            this.lbCode = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.AccountsGV = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblStartBalance = new System.Windows.Forms.Label();
            this.nmStartBalance = new System.Windows.Forms.NumericUpDown();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AccName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccountCurrency = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.StartBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsExternal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsSavings = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FileMask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileLoader = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmStartBalance)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCode
            // 
            this.lbCode.AutoSize = true;
            this.lbCode.Location = new System.Drawing.Point(9, 7);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(26, 13);
            this.lbCode.TabIndex = 25;
            this.lbCode.Text = "Код";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(12, 23);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(54, 20);
            this.tbCode.TabIndex = 26;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(69, 5);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(57, 13);
            this.lbName.TabIndex = 28;
            this.lbName.Text = "Название";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(72, 23);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(252, 20);
            this.tbDescription.TabIndex = 27;
            // 
            // AccountsGV
            // 
            this.AccountsGV.AllowUserToAddRows = false;
            this.AccountsGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountsGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AccountsGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.colActive,
            this.AccName,
            this.colAccountCurrency,
            this.StartBalance,
            this.IsExternal,
            this.colIsSavings,
            this.FileMask,
            this.FileLoader});
            this.AccountsGV.Location = new System.Drawing.Point(12, 49);
            this.AccountsGV.Name = "AccountsGV";
            this.AccountsGV.Size = new System.Drawing.Size(1024, 482);
            this.AccountsGV.TabIndex = 29;
            this.AccountsGV.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.AccountsGV_DataError);
            this.AccountsGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.AccountsGV_RowsAdded);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(432, 21);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 30;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblStartBalance
            // 
            this.lblStartBalance.AutoSize = true;
            this.lblStartBalance.Location = new System.Drawing.Point(327, 7);
            this.lblStartBalance.Name = "lblStartBalance";
            this.lblStartBalance.Size = new System.Drawing.Size(107, 13);
            this.lblStartBalance.TabIndex = 32;
            this.lblStartBalance.Text = "Начальный остаток";
            // 
            // nmStartBalance
            // 
            this.nmStartBalance.DecimalPlaces = 2;
            this.nmStartBalance.Location = new System.Drawing.Point(330, 23);
            this.nmStartBalance.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmStartBalance.Name = "nmStartBalance";
            this.nmStartBalance.Size = new System.Drawing.Size(96, 20);
            this.nmStartBalance.TabIndex = 31;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Id";
            this.Code.HeaderText = "Код";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 50;
            // 
            // colActive
            // 
            this.colActive.DataPropertyName = "Active";
            this.colActive.FalseValue = "false";
            this.colActive.HeaderText = "Активный";
            this.colActive.Name = "colActive";
            this.colActive.TrueValue = "true";
            this.colActive.Width = 60;
            // 
            // AccName
            // 
            this.AccName.DataPropertyName = "Name";
            this.AccName.HeaderText = "Название";
            this.AccName.Name = "AccName";
            this.AccName.Width = 200;
            // 
            // colAccountCurrency
            // 
            this.colAccountCurrency.DataPropertyName = "Currency";
            this.colAccountCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colAccountCurrency.HeaderText = "Валюта";
            this.colAccountCurrency.Items.AddRange(new object[] {
            Constants.CONST_RUR,
            Constants.CONST_USD,
            Constants.CONST_EUR
            });
            this.colAccountCurrency.Name = "colAccountCurrency";
            this.colAccountCurrency.Width = 70;
            // 
            // StartBalance
            // 
            this.StartBalance.DataPropertyName = "StartBalance";
            this.StartBalance.HeaderText = "Нач остаток";
            this.StartBalance.Name = "StartBalance";
            // 
            // IsExternal
            // 
            this.IsExternal.DataPropertyName = "IsExternal";
            this.IsExternal.FalseValue = "false";
            this.IsExternal.HeaderText = "Внешний";
            this.IsExternal.IndeterminateValue = "";
            this.IsExternal.Name = "IsExternal";
            this.IsExternal.TrueValue = "true";
            // 
            // colIsSavings
            // 
            this.colIsSavings.DataPropertyName = "IsSavings";
            this.colIsSavings.FalseValue = "false";
            this.colIsSavings.HeaderText = "Накопительный";
            this.colIsSavings.Name = "colIsSavings";
            this.colIsSavings.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colIsSavings.TrueValue = "true";
            this.colIsSavings.Width = 50;
            // 
            // FileMask
            // 
            this.FileMask.DataPropertyName = "FileMask";
            this.FileMask.HeaderText = "Имя файла";
            this.FileMask.Name = "FileMask";
            this.FileMask.Width = 200;
            // 
            // FileLoader
            // 
            this.FileLoader.DataPropertyName = "FileLoader";
            this.FileLoader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileLoader.HeaderText = "Загрузчик";
            this.FileLoader.Items.AddRange(new object[] {
            "",
            "CAkBarsLoader",
            "CAlfabankLoader",
            "CTinkoffLoader",
            "CSberLoader",
			"CUmoneyLoader",
            "CYandexLoader"});
            this.FileLoader.Name = "FileLoader";
            this.FileLoader.Width = 150;
            // 
            // AccountsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 543);
            this.Controls.Add(this.lblStartBalance);
            this.Controls.Add(this.nmStartBalance);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.AccountsGV);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.lbCode);
            this.Name = "AccountsList";
            this.Text = "Список счетов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountsList_FormClosing);
            this.Load += new System.EventHandler(this.AccountsList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AccountsGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmStartBalance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.DataGridView AccountsGV;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblStartBalance;
        private System.Windows.Forms.NumericUpDown nmStartBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colAccountCurrency;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartBalance;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsExternal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsSavings;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileMask;
        private System.Windows.Forms.DataGridViewComboBoxColumn FileLoader;
    }
}