using ImportFileParser.Common;
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
    public partial class TransEdit : Form
    {
        CTransRec m_TransRec = null;

        public TransEdit()
        {
            InitializeComponent();
        }

        private void Internal_Base_Init()
        {
            // Init combos
            cbFromAccount.DataSource = CTransactionManager.Manager.GetAccountsForCombo();
            cbFromAccount.ValueMember = "Id";
            cbFromAccount.DisplayMember = "Name";

            cbToAccount.DataSource = CTransactionManager.Manager.GetAccountsForCombo();
            cbToAccount.ValueMember = "Id";
            cbToAccount.DisplayMember = "Name";
            cbToAccount.SelectedValue = Constants.CONST_SHOP;

            cbCategory.DataSource = CTransactionManager.Manager.GetCategoriesForCombo();
            cbCategory.ValueMember = "Id";
            cbCategory.DisplayMember = "Name";
        }

        public void Init(string p_FromAccount, string p_Category)
        {
            this.Text = "Новая транзакция";
            this.Internal_Base_Init();

            dtDate.Value = DateTime.Now;

            if (!string.IsNullOrEmpty(p_FromAccount) && (p_FromAccount != Constants.CONST_ALL_VALUE))
            {
                cbFromAccount.SelectedValue = p_FromAccount;
            }
            else if (cbFromAccount.Items.Count > 0)
            {
                cbFromAccount.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(p_Category) && (p_Category != Constants.CONST_ALL_VALUE))
            {
                cbCategory.SelectedValue = p_Category;
            }
            else if (cbCategory.Items.Count > 0)
            {
                cbCategory.SelectedIndex = 0;
            }

            this.HandleDisplay();

            this.cbToAccount.SelectedValueChanged += new System.EventHandler(this.cbFromAccount_SelectedValueChanged);
            this.cbFromAccount.SelectedValueChanged += new System.EventHandler(this.cbFromAccount_SelectedValueChanged);
        }

        public void Init(CTransRec p_SourceTransRec)
        {
            this.Text = "Редактирование транзакции";
            // Fill combos
            this.Internal_Base_Init();

            // Save this Trans internally
            this.m_TransRec = p_SourceTransRec;            
            
            cbFromAccount.SelectedValue = m_TransRec.FromAccount;
            cbToAccount.SelectedValue = m_TransRec.ToAccount;
            dtDate.Value = m_TransRec.Date;
            cbCategory.SelectedValue = m_TransRec.Category;
            tbDescription.Text = m_TransRec.Description;
            nmAmountFrom.Value = m_TransRec.AmountFrom;
            nmAmountTo.Value = m_TransRec.AmountTo;

            this.HandleDisplay();

            this.cbToAccount.SelectedValueChanged += new System.EventHandler(this.cbFromAccount_SelectedValueChanged);
            this.cbFromAccount.SelectedValueChanged += new System.EventHandler(this.cbFromAccount_SelectedValueChanged);
        }

        public CTransRec TransRec
        {
            get { return m_TransRec; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_TransRec == null)
            {
                m_TransRec = new CTransRec
                    (
                        cbFromAccount.SelectedValue.ToString(),
                        cbToAccount.SelectedValue.ToString(),
                        dtDate.Value,
                        nmAmountFrom.Value,
                        nmAmountTo.Visible ? nmAmountTo.Value : nmAmountFrom.Value,
                        tbDescription.Text,
                        cbCategory.SelectedValue.ToString(),
                        null
                    );
                m_TransRec.Reviewed = true;
            }
            else
            { // in case of Edit mode of the screen
                m_TransRec.FromAccount = cbFromAccount.SelectedValue.ToString();
                m_TransRec.ToAccount = cbToAccount.SelectedValue.ToString();
                m_TransRec.Date = dtDate.Value;
                m_TransRec.AmountFrom = nmAmountFrom.Value;
                m_TransRec.AmountTo = nmAmountTo.Visible ? nmAmountTo.Value : nmAmountFrom.Value;
                m_TransRec.Description = tbDescription.Text;
                m_TransRec.Category = cbCategory.SelectedValue.ToString();
            }
        }

        private void HandleDisplay()
        {
            // If both From and To accounts are internal then setup Category as Internal too
            DataRow v_SelFromAccRow = ((DataRowView)cbFromAccount.SelectedItem).Row;
            DataRow v_SelToAccRow = ((DataRowView)cbToAccount.SelectedItem).Row;

            if ((v_SelFromAccRow["IsExternal"].ToString() == "False") &&
                    (v_SelToAccRow["IsExternal"].ToString() == "False"))
            {
                cbCategory.SelectedValue = Constants.CONST_CATEGORY_INT;
            }

            // If Accounts have diff currency then make AmountTo visible
            nmAmountTo.Visible = (v_SelFromAccRow["Currency"].ToString() != v_SelToAccRow["Currency"].ToString());

            if (nmAmountTo.Visible)
            {
                lblAmountFrom.Text = "Сумма (со счёта)";
            }
            else
            {
                lblAmountFrom.Text = "Сумма";
            }

            lblAmountTo.Visible = nmAmountTo.Visible;
        }

        private void cbFromAccount_SelectedValueChanged(object sender, EventArgs e)
        {
            this.HandleDisplay();           
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            object v_Sel = cbFromAccount.SelectedValue;
            cbFromAccount.SelectedValue = cbToAccount.SelectedValue;
            cbToAccount.SelectedValue = v_Sel;
        }
    }
}
