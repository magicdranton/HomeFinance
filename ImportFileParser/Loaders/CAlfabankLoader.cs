using ImportFileParser.Common;
using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Loaders
{
    public class CAlfabankLoader: CFileLoader
    {
        private const int CONST_DATE_INDEX = 0;
        private const int CONST_ID_INDEX = 1;
        private const int CONST_DESC_INDEX = 2;
        private const int CONST_AMOUNT_INDEX = 3;        

        private bool m_EnableProcessing = false;

        public CAlfabankLoader(string p_FilePath, string p_BankAccount) : base(p_FilePath, p_BankAccount)
        {
            //
        }

        private string TryGetMCC(string p_Description)
        {
            if (string.IsNullOrEmpty(p_Description)) return Constants.CONST_EMPTY_VALUE;
            if (!p_Description.Contains("MCC")) return Constants.CONST_EMPTY_VALUE;

            string v_MccValue = p_Description.Substring(p_Description.IndexOf("MCC") + 3);
            if (v_MccValue.Length > 4) v_MccValue = v_MccValue.Substring(0, 4);

            return v_MccValue;
        }

        public override CTransRec LineConvert()
        {
            // Process only body of the table from file (ignore headers/footers)
            if (!m_EnableProcessing) return null;

            CTransRec v_Result = null;
            decimal v_Amount = this.SafeGetDecimal(CONST_AMOUNT_INDEX);

            string v_MCC = this.TryGetMCC(this.SafeGetParsedValue(CONST_DESC_INDEX));

            if (this.SafeGetParsedValue(CONST_DATE_INDEX) == "HOLD")
            {                

                v_Result = CreateTransRec
                (
                    DateTime.Now,
                    v_Amount,
                    this.SafeGetParsedValue(CONST_DESC_INDEX),
                    Constants.CONST_EMPTY_VALUE,
                    v_MCC,
                    v_Amount < 0
                );
            }
            else
            {
                v_Result = CreateTransRec_WithId
                (
                    string.Format("{0}_{1}", this.SafeGetParsedValue(CONST_ID_INDEX), this.SafeGetParsedValue(CONST_DATE_INDEX)), // User Reference + Date as a Key
                    SafeGetDateTime(CONST_DATE_INDEX),
                    v_Amount,
                    this.SafeGetParsedValue(CONST_DESC_INDEX),
                    Constants.CONST_EMPTY_VALUE,
                    v_MCC,
                    v_Amount < 0
                );
            }

            return v_Result;
        }

        public override string[] ParseLine()
        {
            // Start/Stop rules
            if (!CurrentLine.Contains("Дата проводки"))
            {
                m_EnableProcessing = true;
            }

            if (!m_EnableProcessing) return null;

            string[] v_Result = CurrentLine.Split(';');
            v_Result[CONST_AMOUNT_INDEX] = v_Result[CONST_AMOUNT_INDEX].Replace(".", ",").Replace("RUR","");
            return v_Result;
        }
    }
}
