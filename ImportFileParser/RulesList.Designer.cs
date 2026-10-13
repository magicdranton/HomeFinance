namespace ImportFileParser
{
    partial class RulesList
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
            this.RulesGV = new System.Windows.Forms.DataGridView();
            this.lbCondition = new System.Windows.Forms.Label();
            this.tbCondition = new System.Windows.Forms.TextBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lbCategory = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.lblToAccount = new System.Windows.Forms.Label();
            this.cbToAccount = new System.Windows.Forms.ComboBox();
            this.lblFromAccount = new System.Windows.Forms.Label();
            this.cbFromAccount = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.RuleOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Condition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FromAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ToAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RulesGV)).BeginInit();
            this.SuspendLayout();
            // 
            // RulesGV
            // 
            this.RulesGV.AllowUserToAddRows = false;
            this.RulesGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RulesGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RulesGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RuleOrder,
            this.Id,
            this.Condition,
            this.Category,
            this.FromAccount,
            this.ToAccount,
            this.Description});
            this.RulesGV.Location = new System.Drawing.Point(12, 50);
            this.RulesGV.Name = "RulesGV";
            this.RulesGV.Size = new System.Drawing.Size(1149, 397);
            this.RulesGV.TabIndex = 0;
            this.RulesGV.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.RulesGV_DataError);
            this.RulesGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.RulesGV_RowsAdded);
            // 
            // lbCondition
            // 
            this.lbCondition.AutoSize = true;
            this.lbCondition.Location = new System.Drawing.Point(9, 7);
            this.lbCondition.Name = "lbCondition";
            this.lbCondition.Size = new System.Drawing.Size(51, 13);
            this.lbCondition.TabIndex = 17;
            this.lbCondition.Text = "Условие";
            // 
            // tbCondition
            // 
            this.tbCondition.Location = new System.Drawing.Point(12, 24);
            this.tbCondition.Name = "tbCondition";
            this.tbCondition.Size = new System.Drawing.Size(398, 20);
            this.tbCondition.TabIndex = 16;
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(822, 7);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(77, 13);
            this.lbDescription.TabIndex = 19;
            this.lbDescription.Text = "Комментарий";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(825, 25);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(252, 20);
            this.tbDescription.TabIndex = 18;
            // 
            // lbCategory
            // 
            this.lbCategory.AutoSize = true;
            this.lbCategory.Location = new System.Drawing.Point(413, 6);
            this.lbCategory.Name = "lbCategory";
            this.lbCategory.Size = new System.Drawing.Size(60, 13);
            this.lbCategory.TabIndex = 21;
            this.lbCategory.Text = "Категория";
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(416, 23);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(151, 21);
            this.cbCategory.TabIndex = 20;
            // 
            // lblToAccount
            // 
            this.lblToAccount.AutoSize = true;
            this.lblToAccount.Location = new System.Drawing.Point(695, 8);
            this.lblToAccount.Name = "lblToAccount";
            this.lblToAccount.Size = new System.Drawing.Size(46, 13);
            this.lblToAccount.TabIndex = 25;
            this.lblToAccount.Text = "На счёт";
            // 
            // cbToAccount
            // 
            this.cbToAccount.FormattingEnabled = true;
            this.cbToAccount.Location = new System.Drawing.Point(698, 24);
            this.cbToAccount.Name = "cbToAccount";
            this.cbToAccount.Size = new System.Drawing.Size(121, 21);
            this.cbToAccount.TabIndex = 24;
            // 
            // lblFromAccount
            // 
            this.lblFromAccount.AutoSize = true;
            this.lblFromAccount.Location = new System.Drawing.Point(568, 8);
            this.lblFromAccount.Name = "lblFromAccount";
            this.lblFromAccount.Size = new System.Drawing.Size(51, 13);
            this.lblFromAccount.TabIndex = 23;
            this.lblFromAccount.Text = "Со счёта";
            // 
            // cbFromAccount
            // 
            this.cbFromAccount.FormattingEnabled = true;
            this.cbFromAccount.Location = new System.Drawing.Point(571, 24);
            this.cbFromAccount.Name = "cbFromAccount";
            this.cbFromAccount.Size = new System.Drawing.Size(121, 21);
            this.cbFromAccount.TabIndex = 22;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(1085, 22);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 26;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // RuleOrder
            // 
            this.RuleOrder.DataPropertyName = "RuleOrder";
            this.RuleOrder.HeaderText = "Номер";
            this.RuleOrder.Name = "RuleOrder";
            this.RuleOrder.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 50;
            // 
            // Condition
            // 
            this.Condition.DataPropertyName = "Condition";
            this.Condition.HeaderText = "Условие";
            this.Condition.Name = "Condition";
            this.Condition.Width = 340;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "Category";
            this.Category.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Category.HeaderText = "Категория";
            this.Category.Name = "Category";
            // 
            // FromAccount
            // 
            this.FromAccount.DataPropertyName = "FromAccount";
            this.FromAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FromAccount.HeaderText = "Со счёта";
            this.FromAccount.Name = "FromAccount";
            // 
            // ToAccount
            // 
            this.ToAccount.DataPropertyName = "ToAccount";
            this.ToAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ToAccount.HeaderText = "На счёт";
            this.ToAccount.Name = "ToAccount";
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Комментарий";
            this.Description.Name = "Description";
            this.Description.Width = 300;
            // 
            // RulesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 459);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblToAccount);
            this.Controls.Add(this.cbToAccount);
            this.Controls.Add(this.lblFromAccount);
            this.Controls.Add(this.cbFromAccount);
            this.Controls.Add(this.lbCategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lbCondition);
            this.Controls.Add(this.tbCondition);
            this.Controls.Add(this.RulesGV);
            this.Name = "RulesList";
            this.Text = "Список правил";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RulesList_FormClosing);
            this.Load += new System.EventHandler(this.RulesList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RulesGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView RulesGV;
        private System.Windows.Forms.Label lbCondition;
        private System.Windows.Forms.TextBox tbCondition;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label lbCategory;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Label lblToAccount;
        private System.Windows.Forms.ComboBox cbToAccount;
        private System.Windows.Forms.Label lblFromAccount;
        private System.Windows.Forms.ComboBox cbFromAccount;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn RuleOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Condition;
        private System.Windows.Forms.DataGridViewComboBoxColumn Category;
        private System.Windows.Forms.DataGridViewComboBoxColumn FromAccount;
        private System.Windows.Forms.DataGridViewComboBoxColumn ToAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}