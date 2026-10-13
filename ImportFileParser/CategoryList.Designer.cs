namespace ImportFileParser
{
    partial class CategoryList
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
            this.CategoryGV = new System.Windows.Forms.DataGridView();
            this.lbName = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.lbCode = new System.Windows.Forms.Label();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CtgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlannedValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryGV)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(330, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 36;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // CategoryGV
            // 
            this.CategoryGV.AllowUserToAddRows = false;
            this.CategoryGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoryGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CategoryGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.CtgName,
            this.PlannedValue});
            this.CategoryGV.Location = new System.Drawing.Point(12, 47);
            this.CategoryGV.Name = "CategoryGV";
            this.CategoryGV.Size = new System.Drawing.Size(432, 482);
            this.CategoryGV.TabIndex = 35;
            this.CategoryGV.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.CategoryGV_DataError);
            this.CategoryGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.CategoryGV_RowsAdded);
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
            // Code
            // 
            this.Code.DataPropertyName = "Id";
            this.Code.HeaderText = "Код";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 70;
            // 
            // CtgName
            // 
            this.CtgName.DataPropertyName = "Name";
            this.CtgName.HeaderText = "Название";
            this.CtgName.Name = "CtgName";
            this.CtgName.Width = 200;
            // 
            // PlannedValue
            // 
            this.PlannedValue.DataPropertyName = "PlannedValue";
            this.PlannedValue.HeaderText = "План";
            this.PlannedValue.Name = "PlannedValue";
            // 
            // CategoryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 536);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.CategoryGV);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.lbCode);
            this.Name = "CategoryList";
            this.Text = "Список категорий";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CategoryList_FormClosing);
            this.Load += new System.EventHandler(this.CategoryList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CategoryGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView CategoryGV;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label lbCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn CtgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlannedValue;
    }
}