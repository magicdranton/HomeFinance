using ImportFileParser.Common;
using ImportFileParser.Managers;
using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Loaders
{
    public class CResultLoader: CFileLoader
    {
        private bool m_EnableProcessing = false;

        public CResultLoader(string p_FilePath) : base(p_FilePath, Constants.CONST_EMPTY_VALUE)
        {
            //m_Color = System.Drawing.Color.LightPink;
        }

        public override CTransRec LineConvert()
        {
            // Process only body of the table from file (ignore headers/footers)
            if (!m_EnableProcessing) return null;

            string v_FromAccount = this.SafeGetParsedValue(0);
            string v_ToAccount = this.SafeGetParsedValue(1);

            CTransRec v_Result = new CTransRec
            (
                v_FromAccount,
                v_ToAccount,
                SafeGetDateTime(2),                
                this.SafeGetDecimal(3),
                this.SafeGetDecimal(3),
                this.SafeGetParsedValue(4),
                this.SafeGetParsedValue(5),
                null
            );

            return v_Result;
        }

        public override string[] ParseLine()
        {
            // Start/Stop rules
            if (!CurrentLine.Contains("FromAccount;"))
            {
                m_EnableProcessing = true;
            }

            if (!m_EnableProcessing) return null;

            string[] v_Result = CurrentLine.Split(';');
            v_Result[3] = v_Result[3].Replace(".", ",");
            return v_Result;
        }
    }
}
