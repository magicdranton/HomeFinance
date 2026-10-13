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
    public partial class MccCodesList : Form
    {
        public MccCodesList()
        {
            InitializeComponent();
        }

        private void MccCodesListList_Load(object sender, EventArgs e)
        {
            CodesGV.AutoGenerateColumns = false;            

            ((DataGridViewComboBoxColumn)CodesGV.Columns["Category"]).DataSource = CTransactionManager.Manager.GetCategoriesForCombo();
            ((DataGridViewComboBoxColumn)CodesGV.Columns["Category"]).ValueMember = "Id";
            ((DataGridViewComboBoxColumn)CodesGV.Columns["Category"]).DisplayMember = "Name";
            cbCategory.DataSource = CTransactionManager.Manager.GetCategoriesForCombo();
            cbCategory.ValueMember = "Id";
            cbCategory.DisplayMember = "Name";

            CodesGV.DataSource = CTransactionManager.Manager.GetMccCodesDataset();
            CodesGV.DataMember = "MccCodes";

            CodesGV.Sort(CodesGV.Columns["Code"], ListSortDirection.Ascending);
        }

        private void MccCodesListList_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet v_DS = CTransactionManager.Manager.GetMccCodesDataset();

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
                    CTransactionManager.Manager.SaveMccCodes();
                }
                else
                {
                    CTransactionManager.Manager.CancelMccCodes();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CTransactionManager.Manager.AddNewMccCode(tbCode.Text, tbDescription.Text, (string)cbCategory.SelectedValue))
            {
                MessageBox.Show("This Code is already used. Please, choose another one!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MccCodesListGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CodesGV.FirstDisplayedScrollingRowIndex = e.RowIndex;
        }

        private void MccCodesListGV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //int k = 0;
        }

        private void CodesGV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Check if current row is Internal Transfer
            if (CodesGV.SelectedCells.Count <= 0) return;

            DataGridViewRow v_GridRow = CodesGV.Rows[e.RowIndex];
            if (v_GridRow == null) return;

            if (!(v_GridRow.DataBoundItem is DataRowView)) return;

            DataRow v_Row = ((DataRowView)v_GridRow.DataBoundItem).Row;
            if (v_Row == null) return;
            
            CodesGV.SelectedCells[0].ToolTipText = v_Row["Description"].ToString();            
        }
    }
}
