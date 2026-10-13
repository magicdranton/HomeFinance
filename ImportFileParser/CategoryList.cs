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
    public partial class CategoryList : Form
    {
        public CategoryList()
        {
            InitializeComponent();
        }

        private void CategoryList_Load(object sender, EventArgs e)
        {
            CategoryGV.AutoGenerateColumns = false;

            CategoryGV.DataSource = CTransactionManager.Manager.GetCategoriesDataset();
            CategoryGV.DataMember = "Categories";

            CategoryGV.Sort(CategoryGV.Columns["CtgName"], ListSortDirection.Ascending);
        }

        private void CategoryList_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet v_DS = CTransactionManager.Manager.GetCategoriesDataset();

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
                    CTransactionManager.Manager.SaveCategories();
                }
                else
                {
                    CTransactionManager.Manager.CancelCategories();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CTransactionManager.Manager.AddNewCategory(tbCode.Text, tbDescription.Text))
            {
                MessageBox.Show("This Code is already used. Please, choose another one!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CategoryGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CategoryGV.FirstDisplayedScrollingRowIndex = e.RowIndex;
        }

        private void CategoryGV_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //int k = 0;
        }
    }
}
