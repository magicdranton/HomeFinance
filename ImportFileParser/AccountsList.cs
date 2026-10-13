using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using ImportFileParser.Managers;

namespace ImportFileParser
{
    public partial class AccountsList : Form
    {
        public AccountsList()
        {
            InitializeComponent();
        }

        private void AccountsList_Load(object sender, EventArgs e)
        {
            AccountsGV.AutoGenerateColumns = false;

            AccountsGV.DataSource = CTransactionManager.Manager.GetAccountsDataset();
            AccountsGV.DataMember = "Accounts";

            AccountsGV.Sort(AccountsGV.Columns["AccName"], ListSortDirection.Ascending);
        }

        private void AccountsList_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet v_DS = CTransactionManager.Manager.GetAccountsDataset();

            if (v_DS.HasChanges())
            {
                DialogResult v_DR = MessageBox.Show("Сохранить изменения?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (v_DR == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (v_DR == DialogResult.Yes)
                {
                    CTransactionManager.Manager.SaveAccounts();
                }
                else
                {
                    CTransactionManager.Manager.CancelAccounts();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CTransactionManager.Manager.AddNewAccount(tbCode.Text, tbDescription.Text, nmStartBalance.Value))
            {
                MessageBox.Show("This Code is already used. Please, choose another one!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AccountsGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            AccountsGV.FirstDisplayedScrollingRowIndex = e.RowIndex;
        }

        private void AccountsGV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //int k = 0;
        }
    }
}
