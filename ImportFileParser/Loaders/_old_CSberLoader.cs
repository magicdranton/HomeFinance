using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Loaders
{
    /*************************************************** ANTON: OLD DEPRECATED ***********************************/
    //public class _old_CSberLoader: CFileLoader
    //{
    //    private bool m_EnableProcessing = false;
    //    private string[] m_BufferedLine = null;

    //    public _old_CSberLoader(string p_FilePath, string p_BankAccount) : base(p_FilePath, p_BankAccount)
    //    {
    //        //
    //    }

    //    public override CTransRec LineConvert()
    //    {
    //        // Process only body of the table from file (ignore headers/footers)
    //        if (!m_EnableProcessing)
    //        {
    //            if (m_BufferedLine != null)
    //            {
    //                m_BufferedLine = null;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }

    //        string v_Key = string.Empty;

    //        if (this.SafeGetParsedValue(2).Contains("КОМИССИЯ"))
    //        {
    //            v_Key = string.Format("{0}k_{1}", this.SafeGetParsedValue(1), this.SafeGetParsedValue(0)); // Use Reference + Date as a Key;
    //        }
    //        else
    //        {
    //            v_Key = string.Format("{0}_{1}", this.SafeGetParsedValue(1), this.SafeGetParsedValue(0)); // Use Reference + Date as a Key;
    //        }

    //        CTransRec v_Result = CreateTransRec_WithId
    //        (
    //            v_Key,
    //            DateTime.ParseExact(this.SafeGetParsedValue(0), "ddMMMyy", CConfigManager.Manager.RussianCulture),                
    //            this.SafeGetDecimal(3),
    //            this.SafeGetParsedValue(2),
    //            null,
    //            null,
    //            this.SafeGetParsedValue(4) != "CR"
    //        );

    //        return v_Result;
    //    }

    //    public override string[] ParseLine()
    //    {
    //        string[] v_ReturnLine = null;

    //        // Start/Stop rules
    //        if (CurrentLine.Contains(" RUR ") || CurrentLine.Contains(" КОМИССИЯ "))
    //        {
    //            m_EnableProcessing = true;
    //        }

    //        if (CurrentLine.Contains("--------------------+-----+-----+-------+"))
    //        {
    //            m_EnableProcessing = false;
    //        }

    //        // Return last parsed line
    //        if (!m_EnableProcessing && (m_BufferedLine != null)) return m_BufferedLine;            

    //        if (!m_EnableProcessing) return null;

    //        if (CurrentLine.Length < 76) return null;

    //        // Fixed width import for Sber format
    //        string[] v_Result = new string[5];
    //        v_Result[0] = CurrentLine.Substring(26, 7); // Date
    //        v_Result[1] = CurrentLine.Substring(34, 6); // Reference
    //        v_Result[2] = CurrentLine.Substring(41, 23); // Desc

    //        if (CurrentLine.Contains(" КОМИССИЯ "))
    //        {
    //            v_Result[3] = CurrentLine.Substring(84, 11).Replace(".", ",");  // Sum
    //        }
    //        else
    //        {
    //            v_Result[3] = CurrentLine.Substring(68, 15).Replace(".", ",");  // Sum
    //        }
            
    //        v_Result[4] = CurrentLine.EndsWith("CR") ? "CR" : "";   //Debit/Credit            

    //        if (CurrentLine.Contains(" RUR ") || CurrentLine.Contains(" КОМИССИЯ "))    // This case line is the first one wich keeps description
    //        {                  
    //            v_ReturnLine = m_BufferedLine;               
    //            m_BufferedLine = v_Result;
    //        }
    //        else
    //        {   // Append buffered line's desc field with additional desc from new line
    //            m_BufferedLine[2] += CurrentLine.Substring(41, 23);
    //        }

    //        return v_ReturnLine;
    //    }
    //}
}
