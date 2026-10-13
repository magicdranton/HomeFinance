using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.RowObjects
{
    public class CTailReportEntries : CRowObject
    {
        private string m_AccountId;
        public string AccountId
        {
            get
            {
                if (this.IsBinded) return this.GetString("AccountId");
                else return m_AccountId;
            }
        }

        private string m_AccountName;
        public string AccountName
        {
            get
            {
                if (this.IsBinded) return this.GetString("AccountName");
                else return m_AccountName;
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

        private decimal m_SumAmount;
        public decimal SumAmount
        {
            get
            {
                if (this.IsBinded) return this.GetDecimal("SumAmount");
                else return m_SumAmount;
            }
        }

        private bool m_IsSavings;
        public bool IsSavings
        {
            get
            {
                if (this.IsBinded) return this.GetBool("IsSavings");
                else return m_IsSavings;
            }
            set
            {
                if (this.IsBinded) this.SetValue("IsSavings", value);
                else m_IsSavings = value;
            }
        }

        private int m_SeqRN;
        public int SeqRN
        {
            get
            {
                if (this.IsBinded) return this.GetInt("SeqRN");
                else return m_SeqRN;
            }
        }


    }
}
