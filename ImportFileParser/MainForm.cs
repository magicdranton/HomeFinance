#define _DEBUG_MODE

using ImportFileParser.Common;
using ImportFileParser.Loaders;
using ImportFileParser.Managers;
using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportFileParser
{
    internal enum EnumFilterMode
    {
        Generic = 0,
        ByAccount = 1
    }

    public partial class frmMain : Form
    {
        

        private const int CONST_Color_Index = 8;        
        private EnumFilterMode g_FILTER_MODE = EnumFilterMode.Generic;

        public frmMain()
        {
            InitializeComponent();
        }                

        private void TransDataGV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow v_DR = TransDataGV.Rows[e.RowIndex];
            if (v_DR.Cells[CONST_Color_Index].Value == null) return;   

            v_DR.DefaultCellStyle.BackColor = System.Drawing.Color.FromName(v_DR.Cells[CONST_Color_Index].Value.ToString());            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*
            if (CTransactionManager.Manager.SaveTransactions(m_TransDataset))
            {
                MessageBox.Show("File saved", "Information", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Save failed", "Error!!!", MessageBoxButtons.OK);
            }
            */
        }

        private void DisplayResults()
        {
            //TransDataGV.DataSource = CTransactionManager.Manager.GetTransactions();
            //TransDataGV.DataMember = "Transactions";

            //TransDataGV.Sort(TransDataGV.Columns["Date"], ListSortDirection.Descending);
            TransDataGV.Refresh();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {            
            try
            {
                if (MessageBox.Show(this,
                    String.Format("Загрузить данные из файлов папки {0}?", CConfigManager.Manager.LoadFromFolder), "Подтверждение", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;

                    CTransactionManager.Manager.LoadTransactions(CConfigManager.Manager.LoadFromFolder);
                    this.DisplayResults();
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }             

        private void TransDataGV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //int k = 0;
        }

        private System.EventHandler m_ShowButtonClick_Hander;

        private void Init_Controls()
        {
            // Controls init
            this.cbAccount.SelectedValueChanged -= cbAccount_SelectedValueChanged;
            this.cbCategory.SelectedValueChanged -= m_ShowButtonClick_Hander;
            this.dtpToDate.ValueChanged -= m_ShowButtonClick_Hander;
            this.dtpFromDate.ValueChanged -= m_ShowButtonClick_Hander;

            dtpToDate.Value = DateTime.Today.AddDays(1);
            dtpFromDate.Value = DateTime.Today.AddDays(-30);

            cbAccount.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;
            cbAmountEnabled.Checked = false;
            nmAmount.Value = 0;
            txtTransId.Text = string.Empty;

            this.cbAccount.SelectedValueChanged += cbAccount_SelectedValueChanged;
            this.cbCategory.SelectedValueChanged += m_ShowButtonClick_Hander;
            this.dtpToDate.ValueChanged += m_ShowButtonClick_Hander;
            this.dtpFromDate.ValueChanged += m_ShowButtonClick_Hander;
        }

        public void LogMessage(string p_Message)
        {
            lbLog.Items.Add(p_Message);
            if (splitContainer1.Panel2Collapsed) splitContainer1.Panel2Collapsed = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Enable logging display
            CLogEngine.Log.OnNewMessage += this.LogMessage;
#if DEBUG
            this.miTests.Visible = true;            
#endif

            this.Text = "Home Finance. ver " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            m_ShowButtonClick_Hander = new System.EventHandler(this.btnShow_Click);

            TransDataGV.AutoGenerateColumns = false;

            ((DataGridViewComboBoxColumn)TransDataGV.Columns["FromAccount"]).DataSource = CTransactionManager.Manager.GetAccountsForCombo();
            ((DataGridViewComboBoxColumn)TransDataGV.Columns["FromAccount"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)TransDataGV.Columns["FromAccount"]).DisplayMember = "Name";

            cbAccount.DataSource = CTransactionManager.Manager.GetAccountsForCriteria();
            cbAccount.ValueMember = "Id";
            cbAccount.DisplayMember = "Name";            

            ((DataGridViewComboBoxColumn)TransDataGV.Columns["ToAccount"]).DataSource = CTransactionManager.Manager.GetAccountsForCombo();
            ((DataGridViewComboBoxColumn)TransDataGV.Columns["ToAccount"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)TransDataGV.Columns["ToAccount"]).DisplayMember = "Name";
           

            ((DataGridViewComboBoxColumn)TransDataGV.Columns["Category"]).DataSource = CTransactionManager.Manager.GetCategoriesForCombo();
            ((DataGridViewComboBoxColumn)TransDataGV.Columns["Category"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)TransDataGV.Columns["Category"]).DisplayMember = "Name";

            cbCategory.DataSource = CTransactionManager.Manager.GetCategoriesForCriteria();
            cbCategory.ValueMember = "Id";
            cbCategory.DisplayMember = "Name";            

            //TransDataGV.Sort(TransDataGV.Columns["RowNum"], ListSortDirection.Descending);

            Init_Controls();

            // Do query for Transactions
            btnShow_Click(this, e);

            CTransactionManager.Manager.OnConfirmChanges += this.ConfirmChanges;            
        }        

        private bool ConfirmChanges(string p_Message)
        {
            return MessageBox.Show(p_Message, "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
        }

        private void saveToDBToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            CTransactionManager.Manager.SaveTransactions();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet v_DS = CTransactionManager.Manager.GetTransactions();

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
                    CTransactionManager.Manager.SaveTransactions();
                }
                else
                {
                    CTransactionManager.Manager.CancelTransactions();
                }
            }
        }       

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RulesList v_frmRules = new RulesList();            
            v_frmRules.ShowDialog(this);
        }        

        private void miAddRule_Click(object sender, EventArgs e)
        {            
            RulesList v_frmRules = new RulesList();
            
            if (m_SelectedTrans == null) return;

            v_frmRules.InitValues
                (
                    string.Format("LIKE(Desc, '{0}')", m_SelectedTrans.Description),
                    m_SelectedTrans.Category,
                    m_SelectedTrans.FromAccount, 
                    m_SelectedTrans.ToAccount
                );
            v_frmRules.ShowDialog(this);
        }

        private void TransDataGV_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmnGridMenu.Show(TransDataGV, e.Location);
            }
        }

        private CTransRec m_SelectedTrans;

        public CTransRec SelectedTrans
        {
            get
            {
                return m_SelectedTrans;
            }
        }        

        private CTransRec GetTransByDGV_Row(DataGridViewRow p_DGVRow)
        {
            if (p_DGVRow == null) return null;

            if (!(p_DGVRow.DataBoundItem is DataRowView)) return null;

            DataRowView v_DRV = (DataRowView)p_DGVRow.DataBoundItem;
            CTransRec v_TR = CTransactionManager.Manager.GetTransByRow(v_DRV.Row);

            return v_TR;
        }

        private void TransDataGV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {   
            // Update selected transaction
            m_SelectedTrans = GetTransByDGV_Row(this.TransDataGV.Rows[e.RowIndex]);

            if (m_SelectedTrans == null) return;

            // Check if the row was merged
            miSplit.Enabled = this.SelectedTrans.IsMerged;
            splitBackToSourceToolStripMenuItem.Enabled = miSplit.Enabled;

            if (!string.IsNullOrEmpty(m_SelectedTrans.ProcHint))
            {
                slProcHint.Text = "Rule processing hint: " + m_SelectedTrans.ProcHint;
            }
            else
            {
                slProcHint.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(m_SelectedTrans.MCC))
            {
                CMccCode v_MccCode = CTransactionManager.Manager.FindMcc(m_SelectedTrans.MCC);
                tsslMCC.Text = "MCC: " + ((v_MccCode == null)? m_SelectedTrans.MCC : string.Format("{0} {1}", v_MccCode?.Code, v_MccCode?.Name));
            }
            else
            {
                tsslMCC.Text = "No MCC info";
            }
        }

        private void miSplit_Click(object sender, EventArgs e)
        {
            CTransactionManager.Manager.SplitTransaction(this.SelectedTrans);
        }

        private void applyRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CTransactionManager.Manager.AssignCategories();
        }

        private void applyRulesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CTransactionManager.Manager.AssignCategories();
        }

        private void TransDataGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            TransDataGV.FirstDisplayedScrollingRowIndex = e.RowIndex;
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountsList v_frmAccounts = new AccountsList();
            v_frmAccounts.ShowDialog(this);
        }

        private void manageCagetoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryList v_frmAccounts = new CategoryList();
            v_frmAccounts.ShowDialog(this);
        }

        private void addRuleForSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miAddRule_Click(this, e);
        }

        private void splitBackToSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miSplit_Click(this, e);
        }

        private void toggleReviewedStatusToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool v_Reviewed = m_SelectedTrans.Reviewed;

            foreach (DataGridViewRow v_GridRow in TransDataGV.SelectedRows)
            {
                CTransRec v_TR = this.GetTransByDGV_Row(v_GridRow);
                v_TR.Reviewed = !v_TR.Reviewed;                
            }
        }

        private void toggleReviewedStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleReviewedStatusToolStripMenuItem1_Click(this, e);
        }

        private void newTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransEdit v_frmTransEdit = new TransEdit();            
            v_frmTransEdit.Init
                (
                    cbAccount.SelectedValue.ToString(), 
                    cbCategory.SelectedValue.ToString()
                );

            if (v_frmTransEdit.ShowDialog(this) == DialogResult.Cancel) return;

            using (CTransRecEventParams v_Params = new CTransRecEventParams(v_frmTransEdit.TransRec))
            {
                CTransactionManager.Manager.AddNewTransRec(v_Params);
            }
        }        

        private void btnShow_Click(object sender, EventArgs e)
        {
            CTransactionManager.Manager.ShowTransactions
                (
                    string.IsNullOrEmpty(cbAccount.Text)? null : cbAccount.SelectedValue.ToString(), 
                    dtpFromDate.Value, 
                    dtpToDate.Value,
                    string.IsNullOrEmpty(cbCategory.Text) ? null : cbCategory.SelectedValue.ToString(),
                    cbAmountEnabled.Checked? nmAmount.Value.ToString(CConfigManager.Manager.EnglishCulture): null,
                    txtTransId.Text
                );
            TransDataGV.DataSource = CTransactionManager.Manager.GetTransactions();
            TransDataGV.DataMember = "Transactions";

            TransDataGV.Sort(TransDataGV.Columns["Date"], ListSortDirection.Descending);            
        }        

        private void TransDataGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            CTransRec v_TR = this.GetTransByDGV_Row(e.Row);            
            CTransactionManager.Manager.DeleteTrans(v_TR);
        }

        private void alfaReplaceNonreconcliedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CTransactionManager.Manager.Alfa_ProcessNonReconciled();
            MessageBox.Show("Done", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void cbAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnShow_Click(this, e);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Init_Controls();
            btnShow_Click(this, e);
        }

        private void experditureChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoryReport v_frmReportViewer = new frmCategoryReport();
            v_frmReportViewer.Text = "План/Факт";
            v_frmReportViewer.Show();
        }

        private void тестЗагрузкиИзАльфыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CWebPageWorker v_WPWorker = new CWebPageWorker();
            v_WPWorker.Login_Alfabank();
        }

        private void miMccCodes_Click(object sender, EventArgs e)
        {
            MccCodesList v_frmCodes = new MccCodesList();
            v_frmCodes.ShowDialog(this);
        }

        private void miTestMergeTrans_Click(object sender, EventArgs e)
        {
            CTransactionManager.Manager.Step2_JoinMoneyTransfers();
        }        

        private void tsmiMergeTrans_Click(object sender, EventArgs e)
        {
            this.miMergeTrans_Click(sender, e);
        }

        private void miMergeTrans_Click(object sender, EventArgs e)
        {
            // We work only with 2 selected transactions
            if (TransDataGV.SelectedRows.Count != 2) return;

            CTransRec v_FirstTrans = this.GetTransByDGV_Row(TransDataGV.SelectedRows[0]);
            CTransRec v_SecondTrans = this.GetTransByDGV_Row(TransDataGV.SelectedRows[1]);

            CTransactionManager.Manager.MergeTransactions(v_FirstTrans, v_SecondTrans);                 
        }

        private void остаткиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTailReport v_frmReportViewer = new frmTailReport();
            v_frmReportViewer.Text = "Остатки по месяцам";
            v_frmReportViewer.Show();
        }

        private void CompareTransToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                CompareTrans v_frmCompareTrans = new CompareTrans(openFileDialog1.FileName);
                v_frmCompareTrans.Show();
            }
        }       

        private void cbAmountEnabled_CheckedChanged(object sender, EventArgs e)
        {
            nmAmount.Enabled = cbAmountEnabled.Checked;
        }

        private void miSettings_Click(object sender, EventArgs e)
        {
            frmSettings frmSettings = new frmSettings();
            frmSettings.ShowDialog(this);
        }

        private void miRecalcEndBalance_Click(object sender, EventArgs e)
        {
            CTransactionManager.Manager.AddToMinDateList(this.SelectedTrans);
            CTransactionManager.Manager.SaveTransactions();            
            btnShow_Click(this, e);
        }

        private void miEditTransToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransEdit v_frmTransEdit = new TransEdit();
            v_frmTransEdit.Init(this.SelectedTrans);                    

            // Edit screen will update TransRec on "Save" and thus data row will be updated and saved to DB. Otherwise, no changes happen to Data Row
            if (v_frmTransEdit.ShowDialog(this) == DialogResult.Cancel) return;
        }

        private void TransDataGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {            
            if (this.TransDataGV.Columns[e.ColumnIndex].Name == "Amount")
            {
                CTransRec v_SelTrans = this.GetTransByDGV_Row(this.TransDataGV.Rows[e.RowIndex]);                
                
                string v_Currency = Constants.CONST_RUR;
                v_Currency = v_SelTrans?.TransContext.FromAccountCurrency;

                switch (v_Currency)
                {
                    case Constants.CONST_EUR: e.CellStyle.Format = "€#,##0.00"; break;
                    case Constants.CONST_USD: e.CellStyle.Format = "$#,##0.00"; break;
                    default: e.CellStyle.Format = "#,##0.00"; break;
                }                
            }            
        }        

        private void cbAccount_SelectedValueChanged(object sender, EventArgs e)
        {
            // Setup filtering mode for the whole screen
            this.g_FILTER_MODE = (cbAccount.SelectedValue == Constants.CONST_ALL_VALUE) ? EnumFilterMode.Generic : EnumFilterMode.ByAccount;

            // Setup correct column formatting for "EndBalance" column
            string v_Currency = Constants.CONST_RUR;
            
            if (!string.IsNullOrEmpty(cbAccount.SelectedValue.ToString()))
            {
                v_Currency = CTransactionManager.Manager.GetAccountCurrency(cbAccount.SelectedValue.ToString());
            }

            switch (v_Currency)
            {
                case Constants.CONST_EUR: this.TransDataGV.Columns["EndBalance"].DefaultCellStyle.Format = "€#,##0.00"; break;
                case Constants.CONST_USD: this.TransDataGV.Columns["EndBalance"].DefaultCellStyle.Format = "$#,##0.00"; break;
                default: this.TransDataGV.Columns["EndBalance"].DefaultCellStyle.Format = "#,##0.00"; break;
            }

            this.m_ShowButtonClick_Hander(sender, e);
        }

        private void miUpdateBalance_Click(object sender, EventArgs e)
        {
            miRecalcEndBalance_Click(sender, e);
        }

        private void cmnGridMenu_Opening(object sender, CancelEventArgs e)
        {
            miUpdateBalance.Enabled = g_FILTER_MODE == EnumFilterMode.ByAccount;
        }

        private void actionsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            miRecalcEndBalance.Enabled = g_FILTER_MODE == EnumFilterMode.ByAccount;
        }

        private void ValidateDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CTransactionManager.Manager.CheckDBConsistency();
        }        

        private void tsmiClear_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
        }

        private void tsmiHideLog_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
        }        

        private void lbLog_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmnLogMenu.Show(lbLog, e.Location);
            }
        }

        private void CopyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lbLog.Text);
        }

        private void showDebugPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = false;
        }
    }
}
