using ImportFileParser.Managers;
using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Loaders
{
    public class CFileLoader: CBaseLoader
    {
        private string m_SourceFilePath = null;
        private string m_SourceFileName = null;
        private string m_CurrentLine = null;
        private string[] m_CurrentParsedLine = null;

        public event TransRecEventHandler OnAddTransRec;

        public CFileLoader(string p_FilePath, string p_BankAccount): base(p_BankAccount)
        {
            m_SourceFilePath = p_FilePath;
            m_SourceFileName = Path.GetFileNameWithoutExtension(p_FilePath);         
        }
        

        public string SafeGetParsedValue(int p_Index)
        {
            if (m_CurrentParsedLine == null) return string.Empty;
            if (p_Index >= m_CurrentParsedLine.Length) return string.Empty;

            return m_CurrentParsedLine[p_Index];
        }

        public DateTime SafeGetDateTime(int p_Index)
        {            
            return DateTime.Parse(this.SafeGetParsedValue(p_Index), CConfigManager.Manager.RussianCulture);
        }

        public Decimal SafeGetDecimal(int p_Index)
        {
            return Decimal.Parse(this.SafeGetParsedValue(p_Index), CConfigManager.Manager.RussianCulture);
        }

        public string CurrentLine
        {
            get
            {
                if (m_CurrentLine == null) return string.Empty;
                return m_CurrentLine;
            }
        }        

        public virtual CTransRec LineConvert()
        {
            return null;
        }

        public virtual string[] ParseLine()
        {
            return null;
        }

        public void NormalizeParsedLine()
        {
            for (int v_ValueIndex = 0; v_ValueIndex < m_CurrentParsedLine.Length; v_ValueIndex++)
            {
                if (m_CurrentParsedLine[v_ValueIndex] != null)
                {
                    m_CurrentParsedLine[v_ValueIndex] = m_CurrentParsedLine[v_ValueIndex].Trim().Replace("  ", " ");
                }
            }
        }

        public bool Load()
        {
            if (!File.Exists(m_SourceFilePath))
            {
                CLogEngine.Log.Write("File " + m_SourceFilePath + "does not exist! Skipped loading..");
                return false;
            }

            using (FileStream v_InFileStream = File.OpenRead(m_SourceFilePath))
            {
                StreamReader v_InStreamReader = new StreamReader(v_InFileStream, Encoding.GetEncoding(1251));

                bool v_WasSomethingLoaded = false;

                while (!v_InStreamReader.EndOfStream)
                {
                    string v_NextLine = v_InStreamReader.ReadLine();

                    if (string.IsNullOrEmpty(v_NextLine)) continue;

                    m_CurrentLine = v_NextLine;
                    m_CurrentParsedLine = this.ParseLine();
                    if (m_CurrentParsedLine == null) continue;
                    this.NormalizeParsedLine();

                    CTransRec v_NewTrans = this.LineConvert();

                    using (CTransRecEventParams v_Params = new CTransRecEventParams(v_NewTrans, this.SkipDuplicates, this.m_BankAccount))
                    {
                        OnAddTransRec(v_Params);
                        if (!v_WasSomethingLoaded) v_WasSomethingLoaded = true;
                    }
                }
                v_InStreamReader.Close();
                v_InFileStream.Close();

                if (!v_WasSomethingLoaded) 
                {
                    CLogEngine.Log.Write("File " + m_SourceFilePath + " is empty! Skipped loading..");
                }
            }           

            return true;
        }
    }
}
