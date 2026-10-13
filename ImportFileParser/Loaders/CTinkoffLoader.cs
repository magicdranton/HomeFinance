using ImportFileParser.Common;
using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Loaders
{
    public class CTinkoffLoader : CFileLoader
    {
        private const int CONST_DATE_INDEX = 0;
        private const int CONST_AMOUNT_INDEX = 4;
        private const int CONST_DESC_INDEX = 11;
        private const int CONST_CATG_INDEX = 9;
        private const int CONST_MCC_INDEX = 10;

        private bool m_EnableProcessing = false;

        public CTinkoffLoader(string p_FilePath, string p_BankAccount) : base(p_FilePath, p_BankAccount)
        {
            //
        }

        public override CTransRec LineConvert()
        {
            // Process only body of the table from file (ignore headers/footers)
            if (!m_EnableProcessing) return null;

            // If Status is not OK then skip this record
            if (this.SafeGetParsedValue(3) != "OK") return null;

            CTransRec v_Result = CreateTransRec
            (
                this.SafeGetDateTime(CONST_DATE_INDEX),
                this.SafeGetDecimal(CONST_AMOUNT_INDEX),
                this.SafeGetParsedValue(CONST_DESC_INDEX),
                Constants.CONST_EMPTY_VALUE,
                this.SafeGetParsedValue(CONST_MCC_INDEX),
                this.SafeGetParsedValue(CONST_AMOUNT_INDEX).StartsWith("-")
            );

            return v_Result;
        }

        public override string[] ParseLine()
        {
            // Start/Stop rules
            if (!CurrentLine.Contains("Дата операции"))
            {
                m_EnableProcessing = true;
            }

            if (!m_EnableProcessing) return null;

            string v_CurrentLine = CurrentLine.Replace(";\"\";", ";;").Replace("\"\"", "{DQUOTE}").Replace("\"", "");
            v_CurrentLine = v_CurrentLine.Replace("{DQUOTE}", "\"");

            string[] v_Result = v_CurrentLine.Split(';');
            v_Result[CONST_AMOUNT_INDEX] = v_Result[CONST_AMOUNT_INDEX].Replace(".", ",");

            // Exclude extra transfers
            if ((v_Result[CONST_DESC_INDEX] == "Перевод с карты другого банка") &&
                (v_Result[CONST_CATG_INDEX] == "Переводы"))
            {
                return null;
            }

            return v_Result;
        }
    }

}
