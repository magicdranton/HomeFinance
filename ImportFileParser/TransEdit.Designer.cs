namespace ImportFileParser
{
    partial class TransEdit
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
            this.lblDate = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.lblToAccount = new System.Windows.Forms.Label();
            this.cbToAccount = new System.Windows.Forms.ComboBox();
            this.lblFromAccount = new System.Windows.Forms.Label();
            this.cbFromAccount = new System.Windows.Forms.ComboBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.lbCategory = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.nmAmountFrom = new System.Windows.Forms.NumericUpDown();
            this.lblAmountFrom = new System.Windows.Forms.Label();
            this.lblAmountTo = new System.Windows.Forms.Label();
            this.nmAmountTo = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nmAmountFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAmountTo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(301, 9);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 19;
            this.lblDate.Text = "Дата";
            // 
            // dtDate
            // 
            this.dtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDate.Location = new System.Drawing.Point(304, 25);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(170, 20);
            this.dtDate.TabIndex = 18;
            // 
            // lblToAccount
            // 
            this.lblToAccount.AutoSize = true;
            this.lblToAccount.Location = new System.Drawing.Point(174, 9);
            this.lblToAccount.Name = "lblToAccount";
            this.lblToAccount.Size = new System.Drawing.Size(46, 13);
            this.lblToAccount.TabIndex = 17;
            this.lblToAccount.Text = "На счёт";
            // 
            // cbToAccount
            // 
            this.cbToAccount.FormattingEnabled = true;
            this.cbToAccount.Location = new System.Drawing.Point(177, 25);
            this.cbToAccount.Name = "cbToAccount";
            this.cbToAccount.Size = new System.Drawing.Size(121, 21);
            this.cbToAccount.TabIndex = 16;
            // 
            // lblFromAccount
            // 
            this.lblFromAccount.AutoSize = true;
            this.lblFromAccount.Location = new System.Drawing.Point(9, 9);
            this.lblFromAccount.Name = "lblFromAccount";
            this.lblFromAccount.Size = new System.Drawing.Size(51, 13);
            this.lblFromAccount.TabIndex = 15;
            this.lblFromAccount.Text = "Со счёта";
            // 
            // cbFromAccount
            // 
            this.cbFromAccount.FormattingEnabled = true;
            this.cbFromAccount.Location = new System.Drawing.Point(12, 25);
            this.cbFromAccount.Name = "cbFromAccount";
            this.cbFromAccount.Size = new System.Drawing.Size(121, 21);
            this.cbFromAccount.TabIndex = 14;
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(12, 90);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(77, 13);
            this.lbDescription.TabIndex = 22;
            this.lbDescription.Text = "Комментарий";
            // 
            // lbCategory
            // 
            this.lbCategory.AutoSize = true;
            this.lbCategory.Location = new System.Drawing.Point(230, 49);
            this.lbCategory.Name = "lbCategory";
            this.lbCategory.Size = new System.Drawing.Size(60, 13);
            this.lbCategory.TabIndex = 24;
            this.lbCategory.Text = "Категория";
            // 
            // cbCategory
            // 
            this.cbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(233, 66);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(240, 21);
            this.cbCategory.TabIndex = 23;
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.Location = new System.Drawing.Point(12, 106);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(461, 182);
            this.tbDescription.TabIndex = 25;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(12, 294);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 34);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(381, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 34);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSwap
            // 
            this.btnSwap.BackgroundImage = global::ImportFileParser.Properties.Resources.refresh_512;
            this.btnSwap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSwap.Location = new System.Drawing.Point(144, 24);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(21, 23);
            this.btnSwap.TabIndex = 28;
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // nmAmountFrom
            // 
            this.nmAmountFrom.DecimalPlaces = 2;
            this.nmAmountFrom.Location = new System.Drawing.Point(12, 67);
            this.nmAmountFrom.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmAmountFrom.Name = "nmAmountFrom";
            this.nmAmountFrom.Size = new System.Drawing.Size(108, 20);
            this.nmAmountFrom.TabIndex = 20;
            // 
            // lblAmountFrom
            // 
            this.lblAmountFrom.AutoSize = true;
            this.lblAmountFrom.Location = new System.Drawing.Point(9, 51);
            this.lblAmountFrom.Name = "lblAmountFrom";
            this.lblAmountFrom.Size = new System.Drawing.Size(93, 13);
            this.lblAmountFrom.TabIndex = 21;
            this.lblAmountFrom.Text = "Сумма (со счёта)";
            // 
            // lblAmountTo
            // 
            this.lblAmountTo.AutoSize = true;
            this.lblAmountTo.Location = new System.Drawing.Point(116, 51);
            this.lblAmountTo.Name = "lblAmountTo";
            this.lblAmountTo.Size = new System.Drawing.Size(87, 13);
            this.lblAmountTo.TabIndex = 30;
            this.lblAmountTo.Text = "Сумма (на счёт)";
            // 
            // nmAmountTo
            // 
            this.nmAmountTo.DecimalPlaces = 2;
            this.nmAmountTo.Location = new System.Drawing.Point(119, 67);
            this.nmAmountTo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmAmountTo.Name = "nmAmountTo";
            this.nmAmountTo.Size = new System.Drawing.Size(108, 20);
            this.nmAmountTo.TabIndex = 29;
            // 
            // TransEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 340);
            this.Controls.Add(this.lblAmountTo);
            this.Controls.Add(this.nmAmountTo);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lbCategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.lblAmountFrom);
            this.Controls.Add(this.nmAmountFrom);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.lblToAccount);
            this.Controls.Add(this.cbToAccount);
            this.Controls.Add(this.lblFromAccount);
            this.Controls.Add(this.cbFromAccount);
            this.Name = "TransEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новая транзакция";
            ((System.ComponentModel.ISupportInitialize)(this.nmAmountFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAmountTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label lblToAccount;
        private System.Windows.Forms.ComboBox cbToAccount;
        private System.Windows.Forms.Label lblFromAccount;
        private System.Windows.Forms.ComboBox cbFromAccount;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Label lbCategory;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.NumericUpDown nmAmountFrom;
        private System.Windows.Forms.Label lblAmountFrom;
        private System.Windows.Forms.Label lblAmountTo;
        private System.Windows.Forms.NumericUpDown nmAmountTo;
    }
}