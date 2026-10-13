using ImportFileParser.Managers;
using ImportFileParser.RowObjects;
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
    public partial class CompareTrans : Form
    {
        CTransRec m_TransRec = null;
        string m_FilePath = null;

        public CompareTrans(string p_FilePath)
        {
            m_FilePath = p_FilePath;
            InitializeComponent();
        }

        private CTransList m_TransInDB = null;
        private List<CTransRec> m_TransInFile = null;

        private void CompareTrans_Load(object sender, EventArgs e)
        {
            dgvExtraDBTrans.AutoGenerateColumns = false;
            dgvExtraFileTrans.AutoGenerateColumns = false;

            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["FromAccount"]).DataSource = CTransactionManager.Manager.GetAccountsForCombo();
            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["FromAccount"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["FromAccount"]).DisplayMember = "Name";            

            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["ToAccount"]).DataSource = CTransactionManager.Manager.GetAccountsForCombo();
            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["ToAccount"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["ToAccount"]).DisplayMember = "Name";


            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["Category"]).DataSource = CTransactionManager.Manager.GetCategoriesForCombo();
            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["Category"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)dgvExtraDBTrans.Columns["Category"]).DisplayMember = "Name";

            colTransInFileFromAccount.DataSource = CTransactionManager.Manager.GetAccountsForCombo();
            colTransInFileFromAccount.ValueMember = "Id";
            colTransInFileFromAccount.DisplayMember = "Name";

            colTransInFileToAccount.DataSource = CTransactionManager.Manager.GetAccountsForCombo();
            colTransInFileToAccount.ValueMember = "Id";
            colTransInFileToAccount.DisplayMember = "Name";            

            // Query data from CTransManager          

            if (CTransactionManager.Manager.CompareTransactions(m_FilePath, ref m_TransInDB, ref m_TransInFile))
            {
                dgvExtraDBTrans.DataSource = m_TransInDB.Dataset;
                dgvExtraDBTrans.DataMember = "Transactions";
                dgvExtraFileTrans.DataSource = m_TransInFile;
            }
        }

        private void dgvExtraDBTrans_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //int k = 0;
        }

        private void dgvExtraFileTrans_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //int k = 0;
        }

        private void dgvExtraFileTrans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView v_SenderGrid = (DataGridView)sender;

            if (v_SenderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (dgvExtraFileTrans.Rows[e.RowIndex].DataBoundItem is CTransRec)
                {
                    // 1. Add this TR into main screen's dataset
                    CTransRec v_SelTR = (CTransRec)dgvExtraFileTrans.Rows[e.RowIndex].DataBoundItem;
                    CTransactionManager.Manager.AddNewTransRec(new Loaders.CTransRecEventParams(v_SelTR));

                    // 2. Remove this TR from ExtraFileTrans dataset
                    m_TransInFile.Remove(v_SelTR);
                    dgvExtraFileTrans.Refresh();                    
                }
            }
        }

        private void dgvExtraDBTrans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView v_SenderGrid = (DataGridView)sender;

            if (v_SenderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (dgvExtraDBTrans.Rows[e.RowIndex].DataBoundItem is DataRowView)
                {
                    DataRowView v_Drw = (DataRowView)dgvExtraDBTrans.Rows[e.RowIndex].DataBoundItem;
                    // Remove this TR from DB
                    dgvExtraDBTrans.Rows.RemoveAt(e.RowIndex);

                    m_TransInDB.Remove(v_Drw.Row);
                    m_TransInDB.Update();                    
                }
            }
        }
    }
}
