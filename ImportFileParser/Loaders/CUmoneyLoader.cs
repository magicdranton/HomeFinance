using ImportFileParser.RowObjects;
using System;


namespace ImportFileParser.Loaders
{
    public class CUmoneyLoader: CFileLoader
    {
        public CUmoneyLoader(string p_FilePath, string p_BankAccount) : base(p_FilePath, p_BankAccount)
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
                this.SafeGetDateTime(1),
                this.SafeGetDecimal(2),
                this.SafeGetParsedValue(5),
                null,
                null,
                this.SafeGetParsedValue(0) == "-"
            );

            return v_Result;
        }

        public override string[] ParseLine()
        {
            // Start/Stop rules
            if (CurrentLine.Contains("+/-;"))
            {
                m_EnableProcessing = true;
                return null;    // do not process this line as this is just a header
            }
                        
            if (!m_EnableProcessing) return null;

            string[] v_Result = CurrentLine.Split(';');
            v_Result[2] = v_Result[2].Replace(".", ",");

            return v_Result;
        }
    }
}
