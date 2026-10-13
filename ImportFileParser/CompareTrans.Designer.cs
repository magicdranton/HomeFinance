namespace ImportFileParser
{
    partial class CompareTrans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareTrans));
            this.dgvExtraDBTrans = new System.Windows.Forms.DataGridView();
            this.ColTransIdDBDelFromDB = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsMerged = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FromAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ToAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvExtraFileTrans = new System.Windows.Forms.DataGridView();
            this.btnAddToDB = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colTransInFileId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransInFileFromAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colTransInFileToAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colTransInFileDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransInFileAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransInFileDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtraDBTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtraFileTrans)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvExtraDBTrans
            // 
            this.dgvExtraDBTrans.AllowUserToAddRows = false;
            this.dgvExtraDBTrans.AllowUserToResizeRows = false;
            this.dgvExtraDBTrans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExtraDBTrans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtraDBTrans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTransIdDBDelFromDB,
            this.Id,
            this.IsMerged,
            this.FromAccount,
            this.ToAccount,
            this.Date,
            this.Amount,
            this.Description,
            this.Category});
            this.dgvExtraDBTrans.Location = new System.Drawing.Point(12, 34);
            this.dgvExtraDBTrans.Name = "dgvExtraDBTrans";
            this.dgvExtraDBTrans.Size = new System.Drawing.Size(1295, 340);
            this.dgvExtraDBTrans.TabIndex = 0;
            this.dgvExtraDBTrans.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExtraDBTrans_CellContentClick);
            this.dgvExtraDBTrans.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvExtraDBTrans_DataError);
            // 
            // ColTransIdDBDelFromDB
            // 
            this.ColTransIdDBDelFromDB.HeaderText = "Удал. из базы";
            this.ColTransIdDBDelFromDB.Name = "ColTransIdDBDelFromDB";
            this.ColTransIdDBDelFromDB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColTransIdDBDelFromDB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColTransIdDBDelFromDB.Text = "-";
            this.ColTransIdDBDelFromDB.Width = 40;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // IsMerged
            // 
            this.IsMerged.DataPropertyName = "IsMerged";
            this.IsMerged.FalseValue = "0";
            this.IsMerged.HeaderText = "Объединённая";
            this.IsMerged.Name = "IsMerged";
            this.IsMerged.ReadOnly = true;
            this.IsMerged.TrueValue = "1";
            // 
            // FromAccount
            // 
            this.FromAccount.DataPropertyName = "FromAccount";
            this.FromAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FromAccount.HeaderText = "Со счёта";
            this.FromAccount.Name = "FromAccount";
            this.FromAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FromAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FromAccount.Width = 150;
            // 
            // ToAccount
            // 
            this.ToAccount.DataPropertyName = "ToAccount";
            this.ToAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ToAccount.HeaderText = "На счёт";
            this.ToAccount.Name = "ToAccount";
            this.ToAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ToAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ToAccount.Width = 150;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Дата";
            this.Date.Name = "Date";
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "AmountFrom";
            this.Amount.HeaderText = "Сумма";
            this.Amount.Name = "Amount";
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Комментарий";
            this.Description.Name = "Description";
            this.Description.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Description.Width = 400;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "Category";
            this.Category.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Category.HeaderText = "Категория";
            this.Category.Name = "Category";
            this.Category.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Category.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Транзакции в базе (нет в выписке, либо скрытые)";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Транзакции в выписке (нет в базе)";
            // 
            // dgvExtraFileTrans
            // 
            this.dgvExtraFileTrans.AllowUserToAddRows = false;
            this.dgvExtraFileTrans.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExtraFileTrans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtraFileTrans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnAddToDB,
            this.colTransInFileId,
            this.colTransInFileFromAccount,
            this.colTransInFileToAccount,
            this.colTransInFileDate,
            this.colTransInFileAmount,
            this.colTransInFileDesc});
            this.dgvExtraFileTrans.Location = new System.Drawing.Point(16, 409);
            this.dgvExtraFileTrans.Name = "dgvExtraFileTrans";
            this.dgvExtraFileTrans.Size = new System.Drawing.Size(1291, 287);
            this.dgvExtraFileTrans.TabIndex = 4;
            this.dgvExtraFileTrans.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExtraFileTrans_CellContentClick);
            this.dgvExtraFileTrans.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvExtraFileTrans_DataError);
            // 
            // btnAddToDB
            // 
            this.btnAddToDB.HeaderText = "Доб. в базу";
            this.btnAddToDB.Name = "btnAddToDB";
            this.btnAddToDB.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnAddToDB.Text = "+";
            this.btnAddToDB.Width = 40;
            // 
            // colTransInFileId
            // 
            this.colTransInFileId.DataPropertyName = "Id";
            this.colTransInFileId.HeaderText = "Id";
            this.colTransInFileId.Name = "colTransInFileId";
            this.colTransInFileId.ReadOnly = true;
            this.colTransInFileId.Width = 120;
            // 
            // colTransInFileFromAccount
            // 
            this.colTransInFileFromAccount.DataPropertyName = "FromAccount";
            this.colTransInFileFromAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colTransInFileFromAccount.HeaderText = "Со счёта";
            this.colTransInFileFromAccount.Name = "colTransInFileFromAccount";
            this.colTransInFileFromAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTransInFileFromAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colTransInFileFromAccount.Width = 150;
            // 
            // colTransInFileToAccount
            // 
            this.colTransInFileToAccount.DataPropertyName = "ToAccount";
            this.colTransInFileToAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colTransInFileToAccount.HeaderText = "На счёт";
            this.colTransInFileToAccount.Name = "colTransInFileToAccount";
            this.colTransInFileToAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colTransInFileToAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colTransInFileToAccount.Width = 150;
            // 
            // colTransInFileDate
            // 
            this.colTransInFileDate.DataPropertyName = "Date";
            this.colTransInFileDate.HeaderText = "Дата";
            this.colTransInFileDate.Name = "colTransInFileDate";
            // 
            // colTransInFileAmount
            // 
            this.colTransInFileAmount.DataPropertyName = "AmountFrom";
            this.colTransInFileAmount.HeaderText = "Сумма";
            this.colTransInFileAmount.Name = "colTransInFileAmount";
            // 
            // colTransInFileDesc
            // 
            this.colTransInFileDesc.DataPropertyName = "Description";
            this.colTransInFileDesc.HeaderText = "Комментарий";
            this.colTransInFileDesc.Name = "colTransInFileDesc";
            this.colTransInFileDesc.Width = 400;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Date";
            this.dataGridViewTextBoxColumn2.HeaderText = "Дата";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Amount";
            this.dataGridViewTextBoxColumn3.HeaderText = "Сумма";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn4.HeaderText = "Комментарий";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.Width = 400;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn5.HeaderText = "Id";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Date";
            this.dataGridViewTextBoxColumn6.HeaderText = "Дата";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Amount";
            this.dataGridViewTextBoxColumn7.HeaderText = "Сумма";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn8.HeaderText = "Комментарий";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 400;
            // 
            // CompareTrans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 746);
            this.Controls.Add(this.dgvExtraFileTrans);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvExtraDBTrans);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CompareTrans";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сравнение с выпиской";
            this.Load += new System.EventHandler(this.CompareTrans_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtraDBTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtraFileTrans)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExtraDBTrans;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvExtraFileTrans;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewButtonColumn btnAddToDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransInFileId;
        private System.Windows.Forms.DataGridViewComboBoxColumn colTransInFileFromAccount;
        private System.Windows.Forms.DataGridViewComboBoxColumn colTransInFileToAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransInFileDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransInFileAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransInFileDesc;
        private System.Windows.Forms.DataGridViewButtonColumn ColTransIdDBDelFromDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsMerged;
        private System.Windows.Forms.DataGridViewComboBoxColumn FromAccount;
        private System.Windows.Forms.DataGridViewComboBoxColumn ToAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewComboBoxColumn Category;
    }
}