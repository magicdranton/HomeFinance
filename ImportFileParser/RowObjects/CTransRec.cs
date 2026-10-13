using System;
using System.Data;
using System.Text.RegularExpressions;
using ImportFileParser.Common;
using ImportFileParser.Loaders;
using ImportFileParser.Managers;

namespace ImportFileParser.RowObjects
{
    public class CTransContext
    {
        public CTransContext(string p_FromAccountCurrency, string p_ToAccountCurrency)
        {
            this.m_FromAccountCurrency = p_FromAccountCurrency;
            this.m_ToAccountCurrency = p_ToAccountCurrency;
        }

        private string m_FromAccountCurrency;
        public string FromAccountCurrency
        {
            get
            {
                return m_FromAccountCurrency;
            }
        }
        
        private string m_ToAccountCurrency;
        public string ToAccountCurrency
        {
            get
            {
                return m_ToAccountCurrency;
            }
        }

        public bool IsEqualCurrency()
        {
            return string.Equals(m_FromAccountCurrency, m_ToAccountCurrency);
        }

        public void UpdateContext(string p_FromAccountCurrency, string p_ToAccountCurrency)
        {
            this.m_FromAccountCurrency = p_FromAccountCurrency;
            this.m_ToAccountCurrency = p_ToAccountCurrency;
        }
    }
    public class CTransRec: CRowObject
    {
        private Regex m_MCCRegex = new Regex("MCC[0-9][0-9][0-9][0-9]");
        private static int m_AdditiveCounter = 0;

        private int m_InternalId = CTransRec.m_AdditiveCounter++;

        public CTransRec()
        {
            // LEAVE THIS!!! IT IS NEEDED FOR  var v_NewRec = Activator.CreateInstance(typeof(T));
        }

        public CTransRec
        (
            string p_Id,
            string p_FromAccount,
            string p_ToAccount,
            DateTime p_Date,
            decimal p_AmountFrom,
            decimal p_AmountTo,
            string p_Description,
            string p_Category,
            string p_MCC
        )
        {
            m_TransContext = CTransactionManager.Manager.GetTransContext(p_FromAccount, p_ToAccount);

            this.m_Id = p_Id;
            this.FromAccount = p_FromAccount;
            this.ToAccount = p_ToAccount;
            this.Date = p_Date;
            this.AmountFrom = p_AmountFrom;
            this.AmountTo = p_AmountTo;
            this.Description = p_Description;
            this.Category = p_Category;
            this.MCC = p_MCC;
        }

        

        public CTransRec
        (
            string p_FromAccount,
            string p_ToAccount,
            DateTime p_Date,
            decimal p_AmountFrom,
            decimal p_AmountTo,
            string p_Description,
            string p_Category,
            string p_MCC
        ): this(string.Empty, p_FromAccount, p_ToAccount, p_Date, p_AmountFrom, p_AmountTo, p_Description, p_Category, p_MCC)
        {
            int v_DescLen = Math.Min(25, m_Description.Length);

            // Setup Transaction's Id - once and forever :)
            this.m_Id = string.Format("{0}_{1}_{2}_{3}_{4}",
                        this.m_FromAccount,
                        this.m_ToAccount,
                        this.m_Date.ToString(CConfigManager.Manager.RussianCulture),
                        this.m_AmountFrom.ToString(CConfigManager.Manager.RussianCulture),
                        this.m_Description.Substring(m_Description.Length - v_DescLen, v_DescLen)
                        );
        }

        CTransContext m_TransContext = null;
        private const string CONST_FromAccountCurrency = "FromAccountCurrency";
        private const string CONST_ToAccountCurrency = "ToAccountCurrency";        

        public CTransContext TransContext
        {
            get
            {                 
                return m_TransContext; 
            }
        }        

        private const string CONST_ID = "Id";
        private string m_Id;
        public string Id
        {
            get
            {
                if (this.IsBinded) return this.GetString(CONST_ID);
                else return m_Id;
            }            
        }

        public bool AppendIdSuffix(string p_Suffix)
        {
            string v_NewId = this.Id + p_Suffix;

            if (this.IsBinded) this.SetValue(CONST_ID, v_NewId);
            m_Id = v_NewId;

            return true;
        }

        public int InternalId
        {
            get
            {
                return this.m_InternalId;
            }
        }

        private const string CONST_FromAccount = "FromAccount";
        private string m_FromAccount;
        public string FromAccount
        {
            get
            {
                if (this.IsBinded) return this.GetString(CONST_FromAccount);
                else return m_FromAccount;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_FromAccount, value);
                m_FromAccount = value;
            }
        }

        private const string CONST_ToAccount = "ToAccount";
        private string m_ToAccount;
        public string ToAccount
        {
            get
            {
                if (this.IsBinded) return this.GetString(CONST_ToAccount);
                else return m_ToAccount;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_ToAccount, value);
                m_ToAccount = value;
            }
        }

        private const string CONST_Date = "Date";
        private DateTime m_Date;
        public DateTime Date
        {
            get
            {
                if (this.IsBinded) return this.GetDate(CONST_Date);
                else return m_Date;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_Date, value);
                m_Date = value;
            }
        }

        private bool m_SyncingAmounts = false;
        private const string CONST_AmountFrom = "AmountFrom";
        private decimal m_AmountFrom;
        public decimal AmountFrom
        {
            get
            {
                if (this.IsBinded) return this.GetDecimal(CONST_AmountFrom);
                else return m_AmountFrom;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_AmountFrom, value);
                m_AmountFrom = value;

                // AmountFrom = AmountTo if same currency                                
                if (this.TransContext.IsEqualCurrency() && !this.m_SyncingAmounts)
                {
                    try
                    {
                        this.m_SyncingAmounts = true;
                        this.AmountTo = this.AmountFrom;
                    }
                    finally
                    {
                        this.m_SyncingAmounts = false;
                    }
                }
            }
        }

        private const string CONST_AmountTo = "AmountTo";
        private decimal m_AmountTo;
        public decimal AmountTo
        {
            get
            {
                if (this.IsBinded) return this.GetDecimal(CONST_AmountTo);
                else return m_AmountTo;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_AmountTo, value);
                m_AmountTo = value;

                // AmountFrom = AmountTo if same currency                
                if (this.TransContext.IsEqualCurrency() && !this.m_SyncingAmounts)
                {
                    try
                    {
                        this.m_SyncingAmounts = true;
                        this.AmountFrom = this.AmountTo;
                    }
                    finally
                    {
                        this.m_SyncingAmounts = false;
                    }
                }
            }
        }

        private const string CONST_Description = "Description";
        private string m_Description;
        public string Description
        {
            get
            {
                if (this.IsBinded) return this.GetString(CONST_Description);
                else return m_Description;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_Description, value);
                m_Description = value;
            }
        }

        private const string CONST_Category = "Category";
        private string m_Category;
        public string Category
        {
            get
            {
                if (this.IsBinded) return this.GetString(CONST_Category);
                else return m_Category;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_Category, value);
                m_Category = value;
            }
        }

        private const string CONST_ProcHint = "ProcHint";
        private string m_ProcHint;
        public string ProcHint
        {
            get
            {
                if (this.IsBinded) return this.GetString(CONST_ProcHint);
                else return m_ProcHint;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_ProcHint, value);
                m_ProcHint = value;
            }
        }

        private const string CONST_Reviewed = "Reviewed";
        private bool m_Reviewed;
        public bool Reviewed
        {
            get
            {
                if (this.IsBinded) return this.GetBool(CONST_Reviewed);
                else return m_Reviewed;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_Reviewed, value);
                m_Reviewed = value;
            }
        }

        public string Color
        {
            get
            {
                System.Drawing.Color v_Color = System.Drawing.Color.White;

                // Case of Internal Transfer
                if (this.IsDuplicate)
                {
                    v_Color = System.Drawing.Color.Red;
                }
                else if (this.Category == Constants.CONST_CATEGORY_INT)
                {
                    v_Color = System.Drawing.Color.LightBlue;
                }
                else
                {
                    switch (this.FromAccount)
                    {
                        case "ALA":
                        case "ALT":
                        case "ALZ":
                        case "ATZ":
                        case "ACA":
                        case "ACT":
                        case "AKT":
                        case "AK2":
                            v_Color = System.Drawing.Color.LightPink; break;
                        case "CSH": v_Color = System.Drawing.Color.Aquamarine; break;
                        case "SBR":
                        case "SBT":
                            v_Color = System.Drawing.Color.LightGreen; break;
                        case "TKF": v_Color = System.Drawing.Color.Yellow; break;
                        case "WLT": v_Color = System.Drawing.Color.LightSeaGreen; break;
                        case "YDX": v_Color = System.Drawing.Color.LightGray; break;
                    }
                }

                return v_Color.Name;
            }
        }

        private void RefreshColor()
        {
            if (this.IsBinded)
            {
                this.BindedRow["Color"] = this.Color;
            }
        }

        private bool m_IsDuplicate = false;
        public bool IsDuplicate
        {
            get
            {
                return m_IsDuplicate;
            }
            set
            {
                m_IsDuplicate = value;

                // Refresh "Color" value in the row
                this.RefreshColor();
            }
        }

        private const string CONST_MCC = "MCC";
        private string m_MCC = null;
        public string MCC
        {
            get
            {
                if (this.IsBinded) return this.GetString(CONST_MCC);
                else return m_MCC;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_MCC, value);
                m_MCC = value;
            }
        }

        private const string CONST_IsMerged = "IsMerged";
        private bool m_IsMerged;
        public bool IsMerged
        {
            get
            {
                if (this.IsBinded) return this.GetBool(CONST_IsMerged);
                else return m_IsMerged;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_IsMerged, value);
                m_IsMerged = value;
            }
        }

        private const string CONST_MergedId = "MergedId";
        private string m_MergedId;
        public string MergedId
        {
            get
            {
                if (this.IsBinded) return this.GetString(CONST_MergedId);
                else return m_MergedId;
            }
            set
            {
                if (this.IsBinded) this.SetValue(CONST_MergedId, value);
                m_MergedId = value;
            }
        }

        public override void Assign(DataRow p_Row)
        {
            p_Row[CONST_ID] = this.Id;
            p_Row[CONST_Reviewed] = this.Reviewed;
            p_Row[CONST_FromAccount] = this.FromAccount;
            p_Row[CONST_ToAccount] = this.ToAccount;
            p_Row[CONST_Date] = this.Date;
            p_Row[CONST_AmountFrom] = this.AmountFrom;
            p_Row[CONST_AmountTo] = this.AmountTo;
            p_Row[CONST_Description] = this.Description;
            p_Row[CONST_Category] = this.Category;
            p_Row["Color"] = this.Color;
            p_Row[CONST_ProcHint] = this.ProcHint;
            p_Row[CONST_MCC] = this.MCC;
            p_Row[CONST_IsMerged] = this.IsMerged;
            p_Row[CONST_MergedId] = this.MergedId;

            base.Assign(p_Row);
        }

        public override void Bind(DataRow p_Row)
        {
            base.Bind(p_Row);

            // Setup correct TransConext
            string v_FromAccCurrency = this.GetString(CONST_FromAccountCurrency);
            string v_ToAccCurrency = this.GetString(CONST_ToAccountCurrency);

            if (this.m_TransContext == null)
            {
                this.m_TransContext = new CTransContext(v_FromAccCurrency, v_ToAccCurrency);
            }
            else
            {
                this.m_TransContext.UpdateContext(v_FromAccCurrency, v_ToAccCurrency);
            }

            // Reset color value from just calculated one
            this.RefreshColor();

            // This is to reset RowState to "unchanged" state to avoid updating all the rows to DB as Color property is Calculated Column
            switch (p_Row.RowState)
            {
                case DataRowState.Modified:
                case DataRowState.Added: p_Row.AcceptChanges(); break;
            }
        }

        public override void RowChanged(string p_FieldName)
        {
            base.RowChanged(p_FieldName);

            // Refresh internal values from binded DataRow
            switch (p_FieldName)
            {
                case CONST_AmountFrom:
                    {
                        this.m_AmountFrom = this.AmountFrom; 
                        
                        // If Account's currencies are equal then AmountFrom must be always equal to AmountTo!!!                        
                        if (this.TransContext.IsEqualCurrency() && !this.m_SyncingAmounts)
                        {
                            try
                            {
                                this.m_SyncingAmounts = true;
                                this.AmountTo = this.AmountFrom;
                            }
                            finally
                            {
                                this.m_SyncingAmounts = false;
                            }
                        }
                        break;
                    }
                case CONST_AmountTo:
                    {                        
                        this.m_AmountTo = this.AmountTo;

                        // If Account's currencies are equal then AmountFrom must be always equal to AmountTo!!!
                        if (this.TransContext.IsEqualCurrency() && !this.m_SyncingAmounts)
                        {
                            try
                            {
                                this.m_SyncingAmounts = true;
                                this.AmountFrom = this.AmountTo;
                            }
                            finally
                            {
                                this.m_SyncingAmounts = false;
                            }
                        }
                        break;
                    }
                case CONST_Category: this.m_Category = this.Category; break;
                case CONST_Date: this.m_Date = this.Date; break;
                case CONST_Description: this.m_Description = this.Description; break;
                case CONST_FromAccount:
                    {
                        this.m_FromAccount = this.FromAccount;
                        // Reload TransContext
                        this.m_TransContext = CTransactionManager.Manager.GetTransContext(this.FromAccount, this.ToAccount);
                        break;
                    }
                case CONST_IsMerged: this.m_IsMerged = this.IsMerged; break;
                case CONST_MCC: this.m_MCC = this.MCC; break;
                case CONST_MergedId: this.m_MergedId = this.MergedId; break;
                case CONST_ProcHint: this.m_ProcHint = this.ProcHint; break;
                case CONST_Reviewed: this.m_Reviewed = this.Reviewed; break;
                case CONST_ToAccount:
                    {
                        this.m_ToAccount = this.ToAccount;
                        // Reload TransContext
                        this.m_TransContext = CTransactionManager.Manager.GetTransContext(this.FromAccount, this.ToAccount);
                        break;
                    }
            }            
        }        

        /// <summary>
        /// This will extract MCC from description and update to MCC field
        /// </summary>
        /// <returns></returns>
        public bool CalcMCCFromDesc()
        {
            Match v_Match = m_MCCRegex.Match(this.Description);

            if (v_Match.Success)
            {
                this.MCC = v_Match.Value.Replace("MCC", string.Empty);
                return true;
            }

            return false;
        }        
    }
}
