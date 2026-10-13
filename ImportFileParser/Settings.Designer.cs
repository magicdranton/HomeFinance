namespace ImportFileParser
{
    partial class frmSettings
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.nmDollarRate = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tbLoadFrom = new System.Windows.Forms.TextBox();
            this.btnLoadFrom = new System.Windows.Forms.Button();
            this.lblEuroRate = new System.Windows.Forms.Label();
            this.nmEuroRate = new System.Windows.Forms.NumericUpDown();
            this.chbDevMode = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nmDollarRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmEuroRate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 34);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(12, 148);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 34);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // nmDollarRate
            // 
            this.nmDollarRate.DecimalPlaces = 2;
            this.nmDollarRate.Location = new System.Drawing.Point(91, 19);
            this.nmDollarRate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmDollarRate.Name = "nmDollarRate";
            this.nmDollarRate.Size = new System.Drawing.Size(73, 20);
            this.nmDollarRate.TabIndex = 30;
            this.nmDollarRate.ValueChanged += new System.EventHandler(this.nmDollarRate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Курс доллара";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Папка импорта выписок";
            // 
            // tbLoadFrom
            // 
            this.tbLoadFrom.Location = new System.Drawing.Point(146, 69);
            this.tbLoadFrom.Name = "tbLoadFrom";
            this.tbLoadFrom.Size = new System.Drawing.Size(245, 20);
            this.tbLoadFrom.TabIndex = 33;
            this.tbLoadFrom.TextChanged += new System.EventHandler(this.tbLoadFrom_TextChanged);
            // 
            // btnLoadFrom
            // 
            this.btnLoadFrom.Location = new System.Drawing.Point(397, 67);
            this.btnLoadFrom.Name = "btnLoadFrom";
            this.btnLoadFrom.Size = new System.Drawing.Size(26, 23);
            this.btnLoadFrom.TabIndex = 34;
            this.btnLoadFrom.Text = "...";
            this.btnLoadFrom.UseVisualStyleBackColor = true;
            this.btnLoadFrom.Click += new System.EventHandler(this.btnLoadFrom_Click);
            // 
            // lblEuroRate
            // 
            this.lblEuroRate.AutoSize = true;
            this.lblEuroRate.Location = new System.Drawing.Point(9, 45);
            this.lblEuroRate.Name = "lblEuroRate";
            this.lblEuroRate.Size = new System.Drawing.Size(58, 13);
            this.lblEuroRate.TabIndex = 36;
            this.lblEuroRate.Text = "Курс евро";
            // 
            // nmEuroRate
            // 
            this.nmEuroRate.DecimalPlaces = 2;
            this.nmEuroRate.Location = new System.Drawing.Point(91, 43);
            this.nmEuroRate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmEuroRate.Name = "nmEuroRate";
            this.nmEuroRate.Size = new System.Drawing.Size(73, 20);
            this.nmEuroRate.TabIndex = 35;
            this.nmEuroRate.ValueChanged += new System.EventHandler(this.nmEuroRate_ValueChanged);
            // 
            // chbDevMode
            // 
            this.chbDevMode.AutoSize = true;
            this.chbDevMode.Location = new System.Drawing.Point(132, 101);
            this.chbDevMode.Name = "chbDevMode";
            this.chbDevMode.Size = new System.Drawing.Size(15, 14);
            this.chbDevMode.TabIndex = 37;
            this.chbDevMode.UseVisualStyleBackColor = true;
            this.chbDevMode.CheckedChanged += new System.EventHandler(this.chbDevMode_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Режим разработчика";            
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 194);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chbDevMode);
            this.Controls.Add(this.lblEuroRate);
            this.Controls.Add(this.nmEuroRate);
            this.Controls.Add(this.btnLoadFrom);
            this.Controls.Add(this.tbLoadFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmDollarRate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "frmSettings";
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmDollarRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmEuroRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown nmDollarRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tbLoadFrom;
        private System.Windows.Forms.Button btnLoadFrom;
        private System.Windows.Forms.Label lblEuroRate;
        private System.Windows.Forms.NumericUpDown nmEuroRate;
        private System.Windows.Forms.CheckBox chbDevMode;
        private System.Windows.Forms.Label label3;
    }
}