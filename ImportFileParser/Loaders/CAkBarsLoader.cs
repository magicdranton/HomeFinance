using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Loaders
{
    public class CAkBarsLoader : CFileLoader
    {
        private bool m_EnableProcessing = false;

        private const int CONST_TRANS_ID_OLD = 65;
        private const int CONST_TRANS_ID = 69; //70; // 68;
        private const int CONST_TRANS_DATE = 58;    // 57;
        private const int CONST_TRANS_DESC = 59;    //58;
        private const int CONST_TRANS_AMOUNT = 61;  //60;
        private const int CONST_TRANS_FEE_AMOUNT = 62;

        public CAkBarsLoader(string p_FilePath, string p_BankAccount) : base(p_FilePath, p_BankAccount)
        {
            // We will skip Transasctions with same IDs because initially file contains lots of duplicates
            this.SkipDuplicates = true;
        }

        

        public override CTransRec LineConvert()
        {
            // Process only body of the table from file (ignore headers/footers)
            if (!m_EnableProcessing) return null;

            CTransRec v_Result = null;

            decimal v_Amount = this.SafeGetDecimal(CONST_TRANS_AMOUNT) + this.SafeGetDecimal(CONST_TRANS_FEE_AMOUNT);

            // Ignore zero transactions
            if (v_Amount == 0) return null;

            v_Result = CreateTransRec_WithId
                (
                    string.Format("{0}_{1}", this.SafeGetParsedValue(CONST_TRANS_ID), this.SafeGetParsedValue(CONST_TRANS_DATE)), // User Reference + Date as a Key
                    SafeGetDateTime(CONST_TRANS_DATE),
                    Math.Abs(v_Amount),
                    this.SafeGetParsedValue(CONST_TRANS_DESC),
                    //string.Format("{0}_{1}", this.SafeGetParsedValue(CONST_TRANS_ID_OLD), this.SafeGetParsedValue(CONST_TRANS_DATE)), // User Reference + Date as a Key
                    null,            
                    null,
                    v_Amount < 0
                );

            return v_Result;
        }

        private readonly char[] m_SplitChars = { '"' };
        private readonly char CONST_Separator = ';';
        // Eliminate all commas inside quotes
        private string EliminateCommas()
        {
            string[] v_Splitted = CurrentLine.Replace("\"\"","").Split(m_SplitChars, StringSplitOptions.RemoveEmptyEntries);

            for (int v_Index = 1; v_Index < v_Splitted.Length; v_Index+=2)
            {
                v_Splitted[v_Index] = v_Splitted[v_Index].Replace(CONST_Separator, ' ');
            }

            return string.Concat(v_Splitted);
        }

        public override string[] ParseLine()
        {
            // Start/Stop rules
            if (!CurrentLine.Contains("P_ACNT_ID"))
            {
                m_EnableProcessing = true;
            }

            if (!m_EnableProcessing) return null;

            string v_Eliminated = this.EliminateCommas();

            string[] v_Result = v_Eliminated.Split(CONST_Separator);
            v_Result[CONST_TRANS_AMOUNT] = v_Result[CONST_TRANS_AMOUNT].Replace(".", ",");            
            return v_Result;
        }
    }
}
