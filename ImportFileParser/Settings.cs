using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportFileParser
{
    internal enum EnumFormStatus
    {
        fsNonchanged,
        fsChanged,
        fsConfirmed
    }

    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private EnumFormStatus m_FormStatus = EnumFormStatus.fsNonchanged;

        private void frmSettings_Load(object sender, EventArgs e)
        {
            nmDollarRate.Value = CConfigManager.Manager.DollarRate;
            nmEuroRate.Value = CConfigManager.Manager.EuroRate;
            tbLoadFrom.Text = CConfigManager.Manager.LoadFromFolder;
            chbDevMode.Checked = CConfigManager.Manager.IsDevMode;               
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool v_AllowSave = false;

            if (m_FormStatus == EnumFormStatus.fsChanged)
            {
                if (MessageBox.Show("Сохранить изменения?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    v_AllowSave = true;
                }
            }
            else if (m_FormStatus == EnumFormStatus.fsConfirmed) v_AllowSave = true;

            if (v_AllowSave)
            {
                CConfigManager.Manager.DollarRate = nmDollarRate.Value;
                CConfigManager.Manager.EuroRate = nmEuroRate.Value;
                CConfigManager.Manager.LoadFromFolder = tbLoadFrom.Text;
                CConfigManager.Manager.IsDevMode = chbDevMode.Checked;
            }
        }

        private void nmDollarRate_ValueChanged(object sender, EventArgs e)
        {
            if (nmDollarRate.Value != CConfigManager.Manager.DollarRate)
            {
                this.m_FormStatus = EnumFormStatus.fsChanged;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.m_FormStatus = EnumFormStatus.fsConfirmed;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.m_FormStatus = EnumFormStatus.fsNonchanged;
        }

        private void tbLoadFrom_TextChanged(object sender, EventArgs e)
        {
            if (tbLoadFrom.Text != CConfigManager.Manager.LoadFromFolder)
            {
                this.m_FormStatus = EnumFormStatus.fsChanged;
            }
        }

        private void btnLoadFrom_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tbLoadFrom.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void nmEuroRate_ValueChanged(object sender, EventArgs e)
        {
            if (nmEuroRate.Value != CConfigManager.Manager.EuroRate)
            {
                this.m_FormStatus = EnumFormStatus.fsChanged;
            }
        }
        
        private void chbDevMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDevMode.Checked != CConfigManager.Manager.IsDevMode)
            {
                this.m_FormStatus = EnumFormStatus.fsChanged;
            }
        }
    }
}
