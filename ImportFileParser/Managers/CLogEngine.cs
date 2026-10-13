using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Managers
{
    public class CLogEngine
    {
        public CLogEngine()
        {
            //
        }

        private static CLogEngine m_Log = null;
        public static CLogEngine Log
        {
            get
            {
                if (m_Log == null)
                {
                    m_Log = new CLogEngine();
                }
                return m_Log;
            }
        }

        public delegate void NewMessage(string p_Message);
        public event NewMessage OnNewMessage;

        public void DebugWrite(string p_Message)
        {
            // If we are not in Debug mode then do nothin here
            if (!CConfigManager.Manager.IsDevMode) return;
            string v_FormattedMsg = string.Format("DBG [{0}]    {1}", DateTime.Now.ToString("HH:mm:ss"), p_Message);
            OnNewMessage(v_FormattedMsg);
        }

        public void Write(string p_Message)
        {
            string v_FormattedMsg = string.Format("[{0}]    {1}", DateTime.Now.ToString("HH:mm:ss"), p_Message);
            OnNewMessage(v_FormattedMsg);
        }

        public void WriteLogList(CObjectList<CLogEntry> p_LogEntries)
        {
            foreach (CLogEntry v_LogEntry in p_LogEntries)
            {
                string v_FormattedMsg = string.Format("[{0}]    {1}", v_LogEntry.Date.ToString("HH:mm:ss"), v_LogEntry.Message);
                OnNewMessage(v_FormattedMsg);
            }
        }
    }
}
