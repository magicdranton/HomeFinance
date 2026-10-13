using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Loaders
{
    public class CSberLoader: CFileLoader
    {
        private const int CONST_ID_INDEX = 4;
        private const int CONST_DATE_INDEX = 2;
        private const int CONST_AMOUNT_INDEX = 11;
        private const int CONST_DESC_INDEX = 8;        
        private const int CONST_MCC_INDEX = 5;

        private bool m_EnableProcessing = false;

        public CSberLoader(string p_FilePath, string p_BankAccount) : base(p_FilePath, p_BankAccount)
        {
            //
        }

        public override CTransRec LineConvert()
        {
            // Process only body of the table from file (ignore headers/footers)
            if (!m_EnableProcessing) return null;           

            CTransRec v_Result = CreateTransRec_WithId
            (
                string.Format("{0}_{1}", this.SafeGetParsedValue(CONST_ID_INDEX), this.SafeGetParsedValue(CONST_DATE_INDEX)), // User Reference + Date as a Key                
                this.SafeGetDateTime(CONST_DATE_INDEX),
                this.SafeGetDecimal(CONST_AMOUNT_INDEX),
                this.SafeGetParsedValue(CONST_DESC_INDEX),
                null,
                this.SafeGetParsedValue(CONST_MCC_INDEX),
                this.SafeGetParsedValue(CONST_AMOUNT_INDEX).StartsWith("-")
            );

            return v_Result;
        }

        public override string[] ParseLine()
        {
            // Start/Stop rules
            if (!CurrentLine.Contains("Тип карты"))
            {
                m_EnableProcessing = true;
            }

            if (!m_EnableProcessing) return null;            

            string[] v_Result = CurrentLine.Split(';');
            v_Result[CONST_AMOUNT_INDEX] = v_Result[CONST_AMOUNT_INDEX].Replace(".", ",");            

            return v_Result;
        }
    }
}
