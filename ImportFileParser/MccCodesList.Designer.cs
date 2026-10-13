namespace ImportFileParser
{
    partial class MccCodesList
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.CodesGV = new System.Windows.Forms.DataGridView();
            this.lbName = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.lbCode = new System.Windows.Forms.Label();
            this.lbCategory = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CodesGV)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(487, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 36;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // CodesGV
            // 
            this.CodesGV.AllowUserToAddRows = false;
            this.CodesGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodesGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CodesGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.colName,
            this.Category});
            this.CodesGV.Location = new System.Drawing.Point(12, 47);
            this.CodesGV.Name = "CodesGV";
            this.CodesGV.Size = new System.Drawing.Size(876, 718);
            this.CodesGV.TabIndex = 35;
            this.CodesGV.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.MccCodesListGV_DataError);
            this.CodesGV.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.CodesGV_RowEnter);
            this.CodesGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.MccCodesListGV_RowsAdded);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(69, 3);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(57, 13);
            this.lbName.TabIndex = 34;
            this.lbName.Text = "Название";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(72, 21);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(252, 20);
            this.tbDescription.TabIndex = 33;
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(12, 21);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(54, 20);
            this.tbCode.TabIndex = 32;
            // 
            // lbCode
            // 
            this.lbCode.AutoSize = true;
            this.lbCode.Location = new System.Drawing.Point(9, 5);
            this.lbCode.Name = "lbCode";
            this.lbCode.Size = new System.Drawing.Size(26, 13);
            this.lbCode.TabIndex = 31;
            this.lbCode.Text = "Код";
            // 
            // lbCategory
            // 
            this.lbCategory.AutoSize = true;
            this.lbCategory.Location = new System.Drawing.Point(327, 4);
            this.lbCategory.Name = "lbCategory";
            this.lbCategory.Size = new System.Drawing.Size(60, 13);
            this.lbCategory.TabIndex = 38;
            this.lbCategory.Text = "Категория";
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(330, 21);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(151, 21);
            this.cbCategory.TabIndex = 37;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Код";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 70;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Название";
            this.colName.Name = "colName";
            this.colName.Width = 200;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "Category";
            this.Category.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Category.HeaderText = "Категория";
            this.Category.Name = "Category";
            this.Category.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Category.Width = 200;
            // 
            // MccCodesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 772);
            this.Controls.Add(this.lbCategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.CodesGV);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.lbCode);
            this.Name = "MccCodesList";
            this.Text = "MCC коды";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MccCodesListList_FormClosing);
            this.Load += new System.EventHandler(this.MccCodesListList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CodesGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView CodesGV;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn PlannedValue;
        private System.Windows.Forms.Label lbCategory;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn Category;
    }
}