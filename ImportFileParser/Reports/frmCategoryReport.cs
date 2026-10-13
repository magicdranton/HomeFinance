using ImportFileParser.Managers;
using Microsoft.Reporting.WinForms;
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
    public partial class frmCategoryReport : Form
    {
        public frmCategoryReport()
        {
            InitializeComponent();
        }        

        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            m_ShowButtonClick_Hander = new System.EventHandler(this.btnShow_Click);

            cbCategory.DataSource = CTransactionManager.Manager.GetCategoriesForCriteria();
            cbCategory.ValueMember = "Id";
            cbCategory.DisplayMember = "Name";

            this.Init_Controls();

            ReportDataSource v_ReportDS = new ReportDataSource("DataSet1", CTransactionManager.Manager.GetReportCategories
                (
                    (string)cbCategory.SelectedValue, 
                    dtpFromDate.Value, 
                    dtpToDate.Value.AddMonths(1)
                 ));

            rvReports.ProcessingMode = ProcessingMode.Local;
            rvReports.LocalReport.DataSources.Clear();
            rvReports.LocalReport.DataSources.Add(v_ReportDS);            
            rvReports.LocalReport.ReportEmbeddedResource = "ImportFileParser.Reports.ReportExpenditureChart.rdlc";            
            rvReports.RefreshReport();
        }

        private System.EventHandler m_ShowButtonClick_Hander;
        DateTime CONST_MinTransDate = new DateTime(2019, 01, 01);
        
        private void Init_Controls()
        {
            // Controls init        
            this.cbCategory.SelectedValueChanged -= m_ShowButtonClick_Hander;
            this.dtpToDate.ValueChanged -= m_ShowButtonClick_Hander;
            this.dtpFromDate.ValueChanged -= m_ShowButtonClick_Hander;

            
            dtpToDate.Value = DateTime.Today.AddDays(1);            
            dtpFromDate.Value = DateTime.Today.AddMonths(-12);
            // Adjust to first day of month
            dtpFromDate.Value = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1);
            dtpToDate.Value = new DateTime(dtpToDate.Value.Year, dtpToDate.Value.Month, 1);

            if (DateTime.Compare(dtpFromDate.Value, CONST_MinTransDate) < 0)
            {
                dtpFromDate.Value = CONST_MinTransDate;
            }
            
            cbCategory.ResetText();
            
            this.cbCategory.SelectedValueChanged += m_ShowButtonClick_Hander;
            this.dtpToDate.ValueChanged += m_ShowButtonClick_Hander;
            this.dtpFromDate.ValueChanged += m_ShowButtonClick_Hander;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            rvReports.LocalReport.DataSources[0].Value = CTransactionManager.Manager.GetReportCategories
                (
                    (string)cbCategory.SelectedValue,
                    dtpFromDate.Value,
                    dtpToDate.Value.AddMonths(1)
                 );
            rvReports.RefreshReport();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Init_Controls();
            btnShow_Click(this, e);
        }
    }    
}
