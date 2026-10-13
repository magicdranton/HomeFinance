using ImportFileParser.Common;
using ImportFileParser.Managers;
using ImportFileParser.RowObjects;
using System;
using System.Globalization;

namespace ImportFileParser.Loaders
{
    public class CTransRecEventParams: IDisposable
    {
        private CTransRec m_TransRec = null;
        public CTransRec TransRec { get { return m_TransRec; } }

        private bool m_SkipDuplicates = false;
        public bool SkipDuplicates { get { return m_SkipDuplicates; } }

        private string m_BankAccount = null;
        public string BankAccount { get { return m_BankAccount; } }

        public CTransRecEventParams(CTransRec p_TransRec, bool p_SkipDuplicates, string p_BankAccount): this(p_TransRec, p_SkipDuplicates)
        {
            this.m_BankAccount = p_BankAccount;
        }

        public CTransRecEventParams(CTransRec p_TransRec, bool p_SkipDuplicates): this(p_TransRec)
        {            
            this.m_SkipDuplicates = p_SkipDuplicates;            
        }

        public CTransRecEventParams(CTransRec p_TransRec)
        {
            this.m_TransRec = p_TransRec;            
        }

        public void Dispose()
        {
            m_TransRec = null;
        }
    }

    public class CBaseLoader
    {        
        protected string m_BankAccount = Constants.CONST_EMPTY_VALUE;

        public delegate void TransRecEventHandler(CTransRecEventParams p_Params);        

        public CBaseLoader(string p_BankAccount)
        {            
            m_BankAccount = p_BankAccount;
        }

        public bool SkipDuplicates { get; protected set; } = false;

        public CTransRec CreateTransRec
        (
            string p_ContragentAccount,
            DateTime p_Date,
            decimal p_Amount,
            string p_Description,
            string p_Category,            
            string p_MCC,
            bool p_IsCredit            
        )
        {
            CTransRec v_Result = this.CreateTransRec(p_Date, p_Amount, p_Description, p_Category, p_MCC, p_IsCredit);

            if (v_Result.FromAccount == Constants.CONST_EMPTY_VALUE)
            {
                v_Result.FromAccount = p_ContragentAccount;
            }
            else
            {
                v_Result.ToAccount = p_ContragentAccount;
            }

            return v_Result;
        }

        public CTransRec CreateTransRec
        (
            DateTime p_Date,
            decimal p_Amount,
            string p_Description,
            string p_Category,
            string p_MCC,
            bool p_IsCredit
        )
        {
            string v_FromAccount = Constants.CONST_EMPTY_VALUE;
            string v_ToAccount = Constants.CONST_EMPTY_VALUE;

            if (p_IsCredit) v_FromAccount = m_BankAccount;
            else v_ToAccount = m_BankAccount;

            return new CTransRec
                (
                    v_FromAccount, 
                    v_ToAccount, 
                    p_Date, 
                    Math.Abs(p_Amount), 
                    Math.Abs(p_Amount), 
                    p_Description, 
                    p_Category, 
                    p_MCC
                );
        }

        public CTransRec CreateTransRec_WithId
        (
            string p_Id,
            DateTime p_Date,
            decimal p_Amount,
            string p_Description,
            string p_Category,
            string p_MCC,
            bool p_IsCredit
        )
        {
            string v_FromAccount = Constants.CONST_EMPTY_VALUE;
            string v_ToAccount = Constants.CONST_EMPTY_VALUE;

            if (p_IsCredit) v_FromAccount = m_BankAccount;
            else v_ToAccount = m_BankAccount;

            return new CTransRec(p_Id, v_FromAccount, v_ToAccount, p_Date, Math.Abs(p_Amount), Math.Abs(p_Amount), p_Description, p_Category, p_MCC);
        }

        public CTransRec CreateTransRec
        (
            DateTime p_Date,
            decimal p_DebitAmount,
            decimal p_CreditAmount,
            string p_Description,
            string p_Category,
            string p_MCC
        )
        {
            string v_FromAccount = Constants.CONST_EMPTY_VALUE;
            string v_ToAccount = Constants.CONST_EMPTY_VALUE;

            if ((p_CreditAmount > 0) && (p_DebitAmount == 0)) v_FromAccount = m_BankAccount;
            else v_ToAccount = m_BankAccount;

            return new CTransRec(v_FromAccount, v_ToAccount, p_Date,
              p_DebitAmount == 0 ? p_CreditAmount : p_DebitAmount,
              p_DebitAmount == 0 ? p_CreditAmount : p_DebitAmount,
              p_Description, p_Category, p_MCC);
        }

        public CTransRec CreateTransRec_WithId
        (
           string p_Id,
           DateTime p_Date,
           decimal p_DebitAmount,
           decimal p_CreditAmount,
           string p_Description,
           string p_Category,
           string p_MCC
        )
        {
            string v_FromAccount = Constants.CONST_EMPTY_VALUE;
            string v_ToAccount = Constants.CONST_EMPTY_VALUE;

            if ((p_CreditAmount > 0) && (p_DebitAmount == 0)) v_FromAccount = m_BankAccount;
            else v_ToAccount = m_BankAccount;

            return new CTransRec(p_Id, v_FromAccount, v_ToAccount, p_Date,
              p_DebitAmount == 0 ? p_CreditAmount : p_DebitAmount,
              p_DebitAmount == 0 ? p_CreditAmount : p_DebitAmount,
              p_Description, p_Category, p_MCC);
        }
    }
}
