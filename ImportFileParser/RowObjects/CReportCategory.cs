using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.RowObjects
{
    public class CReportCategory : CRowObject
    {
        private string m_Category;
        public string Category
        {
            get
            {
                if (this.IsBinded) return this.GetString("Category");
                else return m_Category;
            }
        }

        private string m_Name;
        public string Name
        {
            get
            {
                if (this.IsBinded) return this.GetString("Name");
                else return m_Name;
            }
        }

        private decimal m_Count;
        public decimal Count
        {
            get
            {
                if (this.IsBinded) return this.GetDecimal("Count");
                else return m_Count;
            }
        }

        private string m_Date;
        public string Date
        {
            get
            {
                if (this.IsBinded) return this.GetString("Date");
                else return m_Date;
            }
        }

        private int m_RowNum;
        public int RowNum
        {
            get
            {
                if (this.IsBinded) return this.GetInt("RowNum");
                else return m_RowNum;
            }
        }
    }    
}
