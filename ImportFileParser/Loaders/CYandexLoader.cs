using ImportFileParser.RowObjects;
using System;


namespace ImportFileParser.Loaders
{
    public class CYandexLoader: CFileLoader
    {
		private const int CONST_DATE_INDEX = 1;
        private const int CONST_AMOUNT_INDEX = 4;
        private const int CONST_DESC_INDEX = 0;
		
        public CYandexLoader(string p_FilePath, string p_BankAccount) : base(p_FilePath, p_BankAccount)
        {
            //
        }

        private bool m_EnableProcessing = false;

        public override CTransRec LineConvert()
        {
            // Process only body of the table from file (ignore headers/footers)
            if (!m_EnableProcessing) return null;

            CTransRec v_Result = CreateTransRec
            (            
                this.SafeGetDateTime(CONST_DATE_INDEX),
                this.SafeGetDecimal(CONST_AMOUNT_INDEX),
                this.SafeGetParsedValue(CONST_DESC_INDEX),
                null,
                null,
                this.SafeGetParsedValue(CONST_AMOUNT_INDEX).StartsWith("-")
            );

            return v_Result;
        }

        public override string[] ParseLine()
        {
            // Start/Stop rules
			if (!CurrentLine.Contains("Описание операции"))
            {
                m_EnableProcessing = true;                
            }
                        
            if (!m_EnableProcessing) return null;

            string[] v_Result = CurrentLine.Split(';');
            v_Result[CONST_DATE_INDEX] = v_Result[CONST_DATE_INDEX].Replace(" в", "");
            v_Result[CONST_AMOUNT_INDEX] = v_Result[CONST_AMOUNT_INDEX].Replace("–", "-");

            return v_Result;
        }
    }
}
