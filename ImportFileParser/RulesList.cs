using ImportFileParser.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportFileParser
{
    public partial class RulesList : Form
    {
        public RulesList()
        {
            InitializeComponent();
        }

        private void RulesList_Load(object sender, EventArgs e)
        {
            RulesGV.AutoGenerateColumns = false;

            ((DataGridViewComboBoxColumn)RulesGV.Columns["FromAccount"]).DataSource = CTransactionManager.Manager.GetAccountsForRules();
            ((DataGridViewComboBoxColumn)RulesGV.Columns["FromAccount"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)RulesGV.Columns["FromAccount"]).DisplayMember = "Name";
            cbFromAccount.DataSource = CTransactionManager.Manager.GetAccountsForRules();
            cbFromAccount.ValueMember = "Id";
            cbFromAccount.DisplayMember = "Name";

            ((DataGridViewComboBoxColumn)RulesGV.Columns["ToAccount"]).DataSource = CTransactionManager.Manager.GetAccountsForRules();
            ((DataGridViewComboBoxColumn)RulesGV.Columns["ToAccount"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)RulesGV.Columns["ToAccount"]).DisplayMember = "Name";
            cbToAccount.DataSource = CTransactionManager.Manager.GetAccountsForRules();
            cbToAccount.ValueMember = "Id";
            cbToAccount.DisplayMember = "Name";

            ((DataGridViewComboBoxColumn)RulesGV.Columns["Category"]).DataSource = CTransactionManager.Manager.GetCategoriesForRules();
            ((DataGridViewComboBoxColumn)RulesGV.Columns["Category"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)RulesGV.Columns["Category"]).DisplayMember = "Name";
            cbCategory.DataSource = CTransactionManager.Manager.GetCategoriesForRules();
            cbCategory.ValueMember = "Id";
            cbCategory.DisplayMember = "Name";

            RulesGV.DataSource = CTransactionManager.Manager.GetRulesDataset();
            RulesGV.DataMember = "Rules";

            RulesGV.Sort(RulesGV.Columns["RuleOrder"], ListSortDirection.Ascending);            
        }

        private void RulesList_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet v_DS = CTransactionManager.Manager.GetRulesDataset();

            if (v_DS.HasChanges())
            {
                DialogResult v_DR = MessageBox.Show("Do you want to save changes?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (v_DR == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (v_DR == DialogResult.Yes)
                {
                    CTransactionManager.Manager.SaveRules();
                }
                else
                {
                    CTransactionManager.Manager.CancelRules();
                }
            }
        }        

        private void btnAdd_Click(object sender, EventArgs e)
        {          
            CTransactionManager.Manager.AddNewRule(tbCondition.Text,
                    (string)cbCategory.SelectedValue,
                    (string)cbFromAccount.SelectedValue,
                    (string)cbToAccount.SelectedValue,
                    tbDescription.Text);
        }

        public void InitValues(string p_Condition, string p_Category, string p_FromAcc, string p_ToAcc)
        {
            this.tbCondition.Text = p_Condition;
            /*this.cbCategory.SelectedIndex = this.cbCategory.FindString(p_Category);            
            this.cbFromAccount.SelectedText = p_FromAcc;
            this.cbToAccount.SelectedText = p_ToAcc;            
            */
        }

        private void RulesGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {            
            RulesGV.FirstDisplayedScrollingRowIndex = e.RowIndex;
        }

        private void RulesGV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //
        }
    }
}
