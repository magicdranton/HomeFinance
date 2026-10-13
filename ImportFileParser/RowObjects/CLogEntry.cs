using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.RowObjects
{
    public class CLogEntry : CRowObject
    {
        private string m_Message;
        public string Message
        {
            get
            {
                if (this.IsBinded) return this.GetString("Message");
                else return m_Message;
            }
        }

        private DateTime m_Date;
        public DateTime Date
        {
            get
            {
                if (this.IsBinded) return this.GetDate("Date");
                else return m_Date;
            }
        }
    }
}
