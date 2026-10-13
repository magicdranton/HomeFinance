namespace ImportFileParser
{
    partial class frmCategoryReport
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
            this.rvReports = new Microsoft.Reporting.WinForms.ReportViewer();
            this.gbCriteria = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.gbCriteria.SuspendLayout();
            this.SuspendLayout();
            // 
            // rvReports
            // 
            this.rvReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvReports.Location = new System.Drawing.Point(0, 0);
            this.rvReports.Name = "rvReports";
            this.rvReports.ServerReport.BearerToken = null;
            this.rvReports.Size = new System.Drawing.Size(1081, 746);
            this.rvReports.TabIndex = 0;
            // 
            // gbCriteria
            // 
            this.gbCriteria.Controls.Add(this.btnClear);
            this.gbCriteria.Controls.Add(this.cbCategory);
            this.gbCriteria.Controls.Add(this.lblCategory);
            this.gbCriteria.Controls.Add(this.lblToDate);
            this.gbCriteria.Controls.Add(this.dtpToDate);
            this.gbCriteria.Controls.Add(this.lblFromDate);
            this.gbCriteria.Controls.Add(this.dtpFromDate);
            this.gbCriteria.Controls.Add(this.btnShow);
            this.gbCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCriteria.Location = new System.Drawing.Point(0, 0);
            this.gbCriteria.Name = "gbCriteria";
            this.gbCriteria.Size = new System.Drawing.Size(1081, 65);
            this.gbCriteria.TabIndex = 1;
            this.gbCriteria.TabStop = false;
            this.gbCriteria.Text = "Параметры выборки";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(485, 16);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 39);
            this.btnClear.TabIndex = 29;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cbCategory.Location = new System.Drawing.Point(203, 34);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(177, 21);
            this.cbCategory.TabIndex = 27;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(200, 18);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(60, 13);
            this.lblCategory.TabIndex = 28;
            this.lblCategory.Text = "Категория";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(102, 18);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(83, 13);
            this.lblToDate.TabIndex = 26;
            this.lblToDate.Text = "Конец периода";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "MMM yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(105, 34);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.ShowUpDown = true;
            this.dtpToDate.Size = new System.Drawing.Size(92, 20);
            this.dtpToDate.TabIndex = 25;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(4, 18);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(74, 13);
            this.lblFromDate.TabIndex = 24;
            this.lblFromDate.Text = "Нач. периода";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "MMM yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(7, 34);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowUpDown = true;
            this.dtpFromDate.Size = new System.Drawing.Size(92, 20);
            this.dtpFromDate.TabIndex = 23;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(386, 16);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(93, 39);
            this.btnShow.TabIndex = 22;
            this.btnShow.Text = "Показать";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // frmCategoryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 746);
            this.Controls.Add(this.gbCriteria);
            this.Controls.Add(this.rvReports);
            this.Name = "frmCategoryReport";
            this.Text = "Отчёт";
            this.Load += new System.EventHandler(this.ReportViewerForm_Load);
            this.gbCriteria.ResumeLayout(false);
            this.gbCriteria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvReports;
        private System.Windows.Forms.GroupBox gbCriteria;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Button btnShow;
    }
}