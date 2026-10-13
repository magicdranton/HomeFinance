using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Reflection;
using ImportFileParser.Loaders;
using System.Xml;
using System.Data.SQLite;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Remoting;
using static ImportFileParser.Loaders.CBaseLoader;
using System.Linq;
using System.Windows.Forms;
using System.Security.Principal;
using System.Runtime.CompilerServices;
using ImportFileParser.RowObjects;
using ImportFileParser.Common;

namespace ImportFileParser.Managers
{
    public class CTransactionManager
    {
        string m_ExportFileName = "Exported_Transactions.csv";

        SQLiteConnection m_DBConnection = null;
        StringBuilder m_LoadedTransIDs_List = new StringBuilder();

        CTransList m_TransList = null;
        CObjectList<CRule> m_RuleList = null;
        CObjectList<CAccount> m_AccountList = null;        
        CObjectList<CCategory> m_CategoryList = null;
        CObjectList<CMccCode> m_MccCodesList = null;        

        private CObjectList<CReportCategory> m_ReportCategories = null;
        private CObjectList<CTailReportEntries> m_TailReportEntries = null;

        private Dictionary<string, DateTime> m_MinDatePerAccount = null;    // This will keep min edited trans Date per Account. This is used to recalculate EndBalance field across all transactions in DB        

        #region Config Settings
         decimal m_DollarRate = 0;
        #endregion

        public CTransactionManager()
        {
            m_MinDatePerAccount = new Dictionary<string, DateTime>();
            m_DBConnection = new SQLiteConnection();

            // Determine main object lists metadata needed to connect to DB (columns & data types)
            m_TransList = new CTransList(m_DBConnection);
            m_TransList.OnConfirmChanges += this.ConfirmChange_Dataset;
            m_TransList.OnAfterSaveDataset += this.AfterSave_Transactions;            

            m_RuleList = new CObjectList<CRule>(m_DBConnection, "Rules");
            m_RuleList.AddFieldRec("Id", 50, DbType.Int32, true);
            m_RuleList.AddFieldRec("Condition", 255, DbType.String);
            m_RuleList.AddFieldRec("Category", 3, DbType.String);
            m_RuleList.AddFieldRec("Description", 255, DbType.String);
            m_RuleList.AddFieldRec("FromAccount", 3, DbType.String);
            m_RuleList.AddFieldRec("ToAccount", 3, DbType.String);
            m_RuleList.AddFieldRec("RuleOrder", 0, DbType.Int32);

            m_RuleList.OnConfirmChanges += this.ConfirmChange_Dataset;  // connect single global change processor not to skip any data change

            m_AccountList = new CObjectList<CAccount>(m_DBConnection, "Accounts");
            m_AccountList.AddFieldRec("Id", 3, DbType.String, true);
            m_AccountList.AddFieldRec("Name", 255, DbType.String);
            m_AccountList.AddFieldRec("StartBalance", 255, DbType.Decimal);
            m_AccountList.AddFieldRec("IsExternal", 0, DbType.Boolean);
            m_AccountList.AddFieldRec("IsSavings", 0, DbType.Boolean);
            m_AccountList.AddFieldRec("FileMask", 50, DbType.String);
            m_AccountList.AddFieldRec("FileLoader", 50, DbType.String);
            m_AccountList.AddFieldRec("Currency", 3, DbType.String);
            m_AccountList.AddFieldRec("IsBuiltin", 0, DbType.Boolean);
            m_AccountList.AddFieldRec("Active", 1, DbType.Boolean);

            m_AccountList.OnConfirmChanges += this.ConfirmChange_Dataset;

            m_CategoryList = new CObjectList<CCategory>(m_DBConnection, "Categories");
            m_CategoryList.AddFieldRec("Id", 3, DbType.String, true);
            m_CategoryList.AddFieldRec("Name", 255, DbType.String);
            m_CategoryList.AddFieldRec("PlannedValue", 0, DbType.Decimal);
            m_CategoryList.AddFieldRec("IsBuiltin", 0, DbType.Boolean);

            m_CategoryList.OnConfirmChanges += this.ConfirmChange_Dataset;            

            m_MccCodesList = new CObjectList<CMccCode>(m_DBConnection, "MCCCodes");
            m_MccCodesList.AddFieldRec("Code", 4, DbType.String, true);
            m_MccCodesList.AddFieldRec("Name", 255, DbType.String);
            m_MccCodesList.AddFieldRec("Description", 0, DbType.String);
            m_MccCodesList.AddFieldRec("Category", 3, DbType.String);

            m_MccCodesList.OnConfirmChanges += this.ConfirmChange_Dataset;

            #region Reports
            m_ReportCategories = new CObjectList<CReportCategory>(m_DBConnection, "ExpChart");
            m_ReportCategories.AddFieldRec("Category", 3, DbType.String);
            m_ReportCategories.AddFieldRec("Name", 255, DbType.String);
            m_ReportCategories.AddFieldRec("Count", 0, DbType.Decimal);
            m_ReportCategories.AddFieldRec("Date", 10, DbType.String);

            m_TailReportEntries = new CObjectList<CTailReportEntries>(m_DBConnection, "TailReport");
            m_ReportCategories.AddFieldRec("SeqRN", 0, DbType.Int32);
            m_ReportCategories.AddFieldRec("Account", 255, DbType.String);
            m_ReportCategories.AddFieldRec("SumAmount", 0, DbType.Decimal);
            m_ReportCategories.AddFieldRec("Date", 10, DbType.String);
            #endregion

            this.LoadConfig();            
        }

        public SQLiteConnection DBConnection => m_DBConnection;

        private void ExecNonQuery(string p_Statement)
        {
            if (m_DBConnection.State != ConnectionState.Open) m_DBConnection.Open();
            if (m_DBConnection.State != ConnectionState.Open) return;

            SQLiteCommand v_Cmd = m_DBConnection.CreateCommand();
            v_Cmd.CommandText = p_Statement;

            // Log statement for DEV mode
            CLogEngine.Log.DebugWrite(p_Statement);

            v_Cmd.ExecuteNonQuery();            
        }

        private DataTable ExecSelect(string p_Statement)
        {
            DataTable v_ResultTable = new DataTable();
            if (m_DBConnection.State != ConnectionState.Open) m_DBConnection.Open();
            if (m_DBConnection.State != ConnectionState.Open) return null;

            CLogEngine.Log.DebugWrite(p_Statement);

            SQLiteDataAdapter v_Adapter = new SQLiteDataAdapter(p_Statement, m_DBConnection);
            v_Adapter.Fill(v_ResultTable);

            return v_ResultTable;
        }

        private void LoadConfig()
        {
            // Load connection string and create DB connection
            m_DBConnection.ConnectionString = CConfigManager.Manager.ConnectionString;
            m_DBConnection.Open();

            if (m_DBConnection.State != ConnectionState.Open) return;

            // load main lists from DB except transactions
            try
            {   
                m_RuleList.LoadFromSelect(@"SELECT * FROM Rules ORDER BY RuleOrder");
                m_AccountList.LoadFromSelect(@"SELECT Id, Name, StartBalance, IsExternal, IsSavings, FileMask, FileLoader, Currency, IsBuiltin, Active FROM Accounts ACC ORDER BY Name ASC");
                m_CategoryList.LoadFromSelect(@"SELECT Id, Name, PlannedValue, IsBuiltin FROM Categories ORDER BY Name ASC");
                m_MccCodesList.LoadFromSelect(@"SELECT Code, Name, Description, Category FROM MccCodes ORDER BY Code ASC");
            }
            finally
            {
                m_DBConnection.Close();
            }
        }

        // Load Transactions from DB with given filters
        public void ShowTransactions(string p_Account, DateTime p_FromDate, DateTime p_ToDate, string p_Category, string p_Amount, string p_TransId)
        {
            string v_SelectTemplate =
                @"SELECT     
    T.Id, 
    Reviewed,
    FromAccount,
    ToAccount,
    Date,
    AmountFrom, 
    AmountTo, 
    Description, 
    Category,    
    ProcHint,
    '' AS Color, -- Color
    0 AS IsDuplicate,  -- IsDuplicate
    MCC,
    IsMerged,
    MergedId,
    FromAcc.Currency AS FromAccountCurrency,
    ToAcc.Currency AS ToAccountCurrency,
    {EndBalance_Part} AS EndBalance    -- EndBalance
FROM Transactions T
LEFT JOIN Accounts FromACC ON FromAcc.Id = T.FromAccount
LEFT JOIN Accounts ToAcc ON ToAcc.Id = T.ToAccount
{Where_Part}
ORDER BY T.Date, T.Id ASC;";

            string v_EndBalance_Part = "CAST(0.0 AS Decimal(10,2))";

            // If either Account is set then we must calculate EndBalance
            if ((p_Account !=Constants.CONST_ALL_VALUE) && (p_Account != Constants.CONST_EMPTY_VALUE))
            {
                v_EndBalance_Part =
  @"ROUND(CAST(case
     when T.FromAccount = '{Account}' then T.FromEndBalance
     else T.ToEndBalance
    end AS Decimal(10,2)), 2)";
            }        

            // Construct where part
            List<string> v_WhereParts = new List<string>();

            switch (p_Account)
            {
                case Constants.CONST_ALL_VALUE:
                    v_WhereParts.Add("(1=1)"); break;
                case Constants.CONST_EMPTY_VALUE:
                    v_WhereParts.Add("((T.FromAccount IN ('NUL', '')) OR (T.FromAccount IS NULL) OR (T.ToAccount IN ('NUL', '')) OR (T.ToAccount IS NULL))"); break;
                default:
                    v_WhereParts.Add(string.Format("(T.FromAccount='{0}' OR T.ToAccount='{0}')", p_Account)); break;
            }            

            switch (p_Category)
            {
                case Constants.CONST_ALL_VALUE:
                    v_WhereParts.Add("(1=1)"); break;
                case Constants.CONST_EMPTY_VALUE:
                    v_WhereParts.Add("(T.Category IN ('NUL', '') OR (T.Category IS NULL))"); break;
                default:
                    v_WhereParts.Add(string.Format("(T.Category='{0}')", p_Category)); break;
            }            

            v_WhereParts.Add(string.IsNullOrEmpty(p_Amount) ? "(1=1)" : string.Format("(T.AmountFrom='{0}')", p_Amount));
            v_WhereParts.Add(string.Format("(T.Date >='{0}' AND T.Date <'{1}')", p_FromDate.ToString("yyyy-MM-dd"), p_ToDate.AddDays(1).ToString("yyyy-MM-dd")));

            if (!string.IsNullOrEmpty(p_TransId))
            {
                v_WhereParts.Add(string.Format("((T.Id like '%{0}%') OR (T.MergedId like '%{0}%'))", p_TransId));
            }
                    
            string v_WherePart = "((T.IsMerged = 0) OR (T.MergedId IS NOT NULL))"; // we exclude merged transactions            
            
            v_WherePart = string.Join(" AND ", v_WherePart, string.Join(" AND ", v_WhereParts));                        

            if (!string.IsNullOrEmpty(v_WherePart)) v_WherePart = "WHERE " + v_WherePart;            

            v_SelectTemplate = v_SelectTemplate.Replace("{EndBalance_Part}", v_EndBalance_Part);
            v_SelectTemplate = v_SelectTemplate.Replace("{Where_Part}", v_WherePart);            
            v_SelectTemplate = v_SelectTemplate.Replace("{Account}", p_Account);           

            //bool v_HasChanges = m_TransList.
            m_TransList.LoadFromSelect(v_SelectTemplate);
        }

        private void CheckDuplicate(CTransRec p_TransRec)
        {
            // Find dup transactions by Id
            List<CTransRec> v_DupTrans = m_TransList.FindAll(x => x.Id == p_TransRec.Id);

            if (v_DupTrans.Count > 1)
            {
                foreach (CTransRec v_Rec in v_DupTrans)
                {
                    v_Rec.IsDuplicate = true;
                }
            }
            else if (p_TransRec.IsDuplicate)
            {
                p_TransRec.IsDuplicate = false;
            }
        }

        public void Alfa_ProcessNonReconciled()
        {            
            this.ExecNonQuery(GetQuery("SP_BUS_AlfaProcessNonReconciled.sql",
                    new SQueryParam("Account", "ALT")
                ));
            this.ExecNonQuery(GetQuery("SP_BUS_AlfaProcessNonReconciled.sql",
                    new SQueryParam("Account", "ATZ")
                ));
        }

        public void AddNewTransRec(CTransRecEventParams p_Params)
        {
            if (p_Params.TransRec == null) return;

            // If Loader does not allow adding duplicates then check duplicates
            if (p_Params.SkipDuplicates)
            {
                // If it is duplicate then skip it from the list
                if (m_TransList.Find(x => string.Equals(x.Id, p_Params.TransRec.Id)) != null)
                {
                    return;
                }
            }

            m_TransList.Add(p_Params.TransRec);
            // Append to list to futher recalculate EndBalance
            AddToMinDateList(p_Params.TransRec);

            this.CheckDuplicate(p_Params.TransRec);

            // Add TransId to Dup Trans query
            m_LoadedTransIDs_List.AppendLine(string.Format(",'{0}'", p_Params.TransRec.Id?.Replace("'","''")));
        }

        public void DeleteTrans(CTransRec p_TransRec)
        {
            // Will recalculate EndBalance later            
            this.AddToMinDateList(p_TransRec);
            m_TransList.Remove(p_TransRec);
        }

        private int RuleToInt(CRule p_Rule)
        {
            return p_Rule.Id;
        }

        public void AddNewRule(string p_Condition, string p_Category, string p_FromAcc, string p_ToAcc, string p_Description)
        {
            // Find out Rule max Id
            int v_MaxId = m_RuleList.FindMaxValue(this.RuleToInt) + 1;

            // Insert new Rule into Table and List
            CRule v_Rule = new CRule
                (
                    v_MaxId,
                    p_Condition,
                    p_Category,
                    p_FromAcc,
                    p_ToAcc,
                    p_Description,
                    v_MaxId
                );

            m_RuleList.Add(v_Rule);
        }

        public bool AddNewAccount(string p_Code, string p_Name, decimal p_StartBalance)
        {
            // Check if p_Code is already used
            foreach (CAccount v_Acc in m_AccountList)
            {
                if (v_Acc.Id.ToLower() == p_Code.ToLower()) return false;
            }
            
            CAccount v_Account = new CAccount(p_Code, p_Name, p_StartBalance);
            m_AccountList.Add(v_Account);

            return true;
        }

        public bool AddNewCategory(string p_Code, string p_Name)
        {
            // Check if p_Code is already used
            foreach (CCategory v_Ctg in m_CategoryList)
            {
                if (v_Ctg.Id.ToLower() == p_Code.ToLower()) return false;
            }

            CCategory v_NewCtg = new CCategory(p_Code, p_Name);
            m_CategoryList.Add(v_NewCtg);

            return true;
        }

        public bool AddNewMccCode(string p_Code, string p_Name, string p_Category)
        {
            // Check if p_Code is already used
            if (m_MccCodesList.Find(x => x.Code == p_Code) != null) return false;

            CMccCode v_NewCode = new CMccCode(p_Code, p_Name, null, p_Category);
            m_MccCodesList.Add(v_NewCode);

            return true;
        }

        // Load transactions from file
        private bool Base_LoadTransactions(string p_FilePath, TransRecEventHandler p_EventHandler)
        {
            CFileLoader v_FileLoader = null;
            string v_FileName = Path.GetFileName(p_FilePath).ToLower();

            // Find the file by FileMask
            CAccount v_MatchedAccount = m_AccountList.Find(x => (!string.IsNullOrEmpty(x.FileMask)) && (v_FileName.Contains(x.FileMask.ToLower())));
            if (v_MatchedAccount == null)
            {
                CLogEngine.Log.Write("No matched accounts found for a file " + v_FileName);
                return false;
            }
            if (string.IsNullOrEmpty(v_MatchedAccount.FileLoader))
            {
                CLogEngine.Log.Write("No FileLoader filled for account " + v_MatchedAccount.Name);
                return false;
            }

            // Create appropriate FileLoader
            Type v_LoaderType = Type.GetType("ImportFileParser.Loaders." + v_MatchedAccount.FileLoader);
            v_FileLoader = (CFileLoader)Activator.CreateInstance(v_LoaderType, p_FilePath, v_MatchedAccount.Id);

            if (v_FileLoader == null) return false;
            v_FileLoader.OnAddTransRec += p_EventHandler;

            v_FileLoader.Load();

            return true;
        }

        private void Step1_LoadTransactions()
        {
            // This will contain SQL to query for duplicate Transactions from DB
            m_LoadedTransIDs_List.Clear();

            // Load Transactions from GMail POP3 server (Not used now. Google voice keep stopped sending notes to email
#if !DEBUG
            //CPop3Loader v_Pop3Loader = new CPop3Loader("WLT", "pop.gmail.com", 995, "email@gmail.com", "password");
            //v_Pop3Loader.OnAddTransRec += this.AddNewTransRec;
            //v_Pop3Loader.Load();
#endif

            // Load Transactions from bank files
            string v_SourceDir = CConfigManager.Manager.LoadFromFolder;
            if (!Directory.Exists(v_SourceDir)) return;

            foreach (string v_FilePath in Directory.GetFiles(v_SourceDir, "*.csv"))
            {
                this.Base_LoadTransactions(v_FilePath, this.AddNewTransRec);
            }
        }

        private void Step11_MarkDuplicates()
        {
            // Query for duplicate IDs
            string v_SelectStatement = m_LoadedTransIDs_List.ToString();

            if (v_SelectStatement != null) v_SelectStatement = v_SelectStatement.TrimStart(',');

            v_SelectStatement = string.Format("SELECT Id FROM Transactions WHERE Id IN ({0})", v_SelectStatement);
            DataTable v_DupTransTable = this.ExecSelect(v_SelectStatement);

            // Mark Transactions as duplicate            
            foreach (DataRow v_Row in v_DupTransTable.Rows)
            {
                foreach(CTransRec v_Rec in m_TransList.FindAll(x => x.Id == v_Row["Id"].ToString()))
                {
                    v_Rec.IsDuplicate = true;
                }               
            }
        }

        private bool internal_MergeTransactions(CTransRec p_FirstRec, CTransRec p_SecondRec, out bool p_IsSecondRemoved)
        {
            p_IsSecondRemoved = false;
            // Transactions must not be already merged
            if (p_FirstRec.IsMerged) return false;  // TODO: Add either message or logging!! No silent returns
            if (p_SecondRec.IsMerged) return false;

            // Transactions must not be already reviewed
            if (p_FirstRec.Reviewed) return false;
            if (p_SecondRec.Reviewed) return false;

            // Special processing (NOT MERGE): if Ids are equal this means that same transation was imported from diff bank accounts: case of money transfer
            // between internal bank accounts
            if (p_FirstRec.Id == p_SecondRec.Id)
            {
                // Case when both trans have same ToAccount
                if ((p_FirstRec.ToAccount == p_SecondRec.ToAccount) &&
                    (p_FirstRec.FromAccount == Constants.CONST_EMPTY_VALUE) &&
                    (p_SecondRec.FromAccount == Constants.CONST_EMPTY_VALUE)
                    )
                {
                    // Summarize transactions
                    p_FirstRec.AmountFrom += p_SecondRec.AmountFrom;
                    p_FirstRec.AmountTo = p_FirstRec.AmountFrom;
                    p_FirstRec.ProcHint = string.Format("Summarized with duplicate: {0}_{1}", p_SecondRec.FromAccount, p_SecondRec.ToAccount);

                    // We must remove duplicate transaction in this case
                    p_IsSecondRemoved = true;
                }
                else if ((p_FirstRec.FromAccount == p_SecondRec.FromAccount) &&
                    (p_FirstRec.ToAccount == Constants.CONST_EMPTY_VALUE) &&
                    (p_SecondRec.ToAccount == Constants.CONST_EMPTY_VALUE)
                    )
                {
                    // Summarize transactions
                    p_FirstRec.AmountFrom += p_SecondRec.AmountFrom;
                    p_FirstRec.AmountTo = p_FirstRec.AmountFrom;
                    p_FirstRec.ProcHint = string.Format("Summarized with duplicate: {0}_{1}", p_SecondRec.FromAccount, p_SecondRec.ToAccount);

                    // We must remove duplicate transaction in this case
                    p_IsSecondRemoved = true;
                }
                // Case when it is money transfer
                else if ((p_FirstRec.AmountFrom == p_SecondRec.AmountFrom) &&
                    (p_FirstRec.FromAccount == Constants.CONST_EMPTY_VALUE) &&
                    (p_SecondRec.ToAccount == Constants.CONST_EMPTY_VALUE)
                    )
                {
                    // We must remove duplicate transaction in this case
                    p_IsSecondRemoved = true;

                    // And copy its "From Account" value
                    p_FirstRec.FromAccount = p_SecondRec.FromAccount;
                    p_FirstRec.Category = Constants.CONST_CATEGORY_INT;
                    p_FirstRec.ProcHint = string.Format("Merged with duplicate: {0}_{1}", p_SecondRec.FromAccount, p_SecondRec.ToAccount);
                }
                // Case when it is duplicate trans from ATM
                else if ((p_FirstRec.ToAccount != p_SecondRec.ToAccount) &&
                    (p_FirstRec.FromAccount == Constants.CONST_EMPTY_VALUE) &&
                    (p_SecondRec.FromAccount == Constants.CONST_EMPTY_VALUE)
                    )
                {
                    p_SecondRec.AppendIdSuffix("_1");
                    p_SecondRec.ProcHint = string.Format("Generated diff ID from duplicate: {0}", p_FirstRec.Id);
                }
            }
            else
			{
				// Otherwise, just mark both transactions as "Merged" and push reference from one transaction to another
				p_FirstRec.FromAccount = p_SecondRec.FromAccount;
				p_FirstRec.Description += " | " + p_SecondRec.Description;
				p_FirstRec.Category = Constants.CONST_CATEGORY_INT;
				p_FirstRec.ProcHint = string.Format("Merged with: {0}", p_SecondRec.Id);
				p_FirstRec.IsMerged = true;
				p_FirstRec.MergedId = p_SecondRec.Id;

				p_SecondRec.IsMerged = true;
                p_SecondRec.ToAccount = p_FirstRec.ToAccount;
                p_SecondRec.Category = Constants.CONST_CATEGORY_INT;
            }
			
            return true;
        }

        public bool MergeTransactions(CTransRec p_FirstRec, CTransRec p_SecondRec)
        {
            bool v_IsSecondRemoved = false;           

            if (p_FirstRec == null) return false;
            if (p_SecondRec == null) return false;

            bool v_MergeResult = false;
            CTransRec v_FirstRec = p_FirstRec;
            CTransRec v_SecondRec = p_SecondRec;

            // Swap first and second to make correct processing
            if (p_FirstRec.FromAccount != Constants.CONST_EMPTY_VALUE)
            {
                v_FirstRec = p_SecondRec;
                v_SecondRec = p_FirstRec;                
            }
            
            v_MergeResult = internal_MergeTransactions(v_FirstRec, v_SecondRec, out v_IsSecondRemoved);
            

            if (v_MergeResult)
            {
                if (v_IsSecondRemoved)
                {
                    m_TransList.Remove(v_SecondRec);
                }

                return true;
            }

            return false;
        }

        // Merge trans rows if it is physically a single trans but reported by each bank ("from bank" and "to bank")
        public void Step2_JoinMoneyTransfers()
        {            
            List<CTransRec> v_TransToCheck = new List<CTransRec>();
            // List<CTransRec> v_TransToRemove = new List<CTransRec>();

            int v_CurTransIdx = m_TransList.Count<CTransRec>();

            while (v_CurTransIdx > 0)
            {
                v_CurTransIdx--;

                CTransRec v_FirstRec = m_TransList[v_CurTransIdx];

                if (v_FirstRec.Reviewed) continue;
                if (v_FirstRec.IsMerged) continue;
                if (!v_FirstRec.IsBinded) continue;                

                List<CTransRec> v_MatchedRecs = null;

                for (int v_Match_Check_Step = 0; v_Match_Check_Step < 5; v_Match_Check_Step++)
                {

                    if (v_Match_Check_Step == 0)    // Anton: First search will be by matched IDs and Amount
                    {
                        if (v_FirstRec.ToAccount == Constants.CONST_EMPTY_VALUE)
                        {
                            continue;
                        }

                        v_MatchedRecs =
                            m_TransList.FindAll(
                                    x => ((!x.Reviewed)
                                    && (!x.IsMerged)
                                    && (x.IsBinded)
                                    && (x.Id == v_FirstRec.Id)
                                    && (x.InternalId != v_FirstRec.InternalId)
                                    && (x.AmountFrom == v_FirstRec.AmountFrom)
                                    && (x.ToAccount == Constants.CONST_EMPTY_VALUE)
                                    && (x.FromAccount != v_FirstRec.ToAccount))
                                    );
                    }
                    else if (v_Match_Check_Step == 1)    // 2nd search:  by matched IDs, Empty FromAcc and ToAccount (we must summarize those trans)
                    {
                        if ((v_FirstRec.ToAccount == Constants.CONST_EMPTY_VALUE) || (v_FirstRec.FromAccount != Constants.CONST_EMPTY_VALUE))
                        {
                            continue;
                        }

                        v_MatchedRecs =
                            m_TransList.FindAll(
                                    x => ((!x.Reviewed)
                                    && (!x.IsMerged)
                                    && (x.IsBinded)
                                    && (x.Id == v_FirstRec.Id)   
                                    && (x.InternalId != v_FirstRec.InternalId)
                                    && (x.ToAccount == v_FirstRec.ToAccount)
                                    && (x.FromAccount == Constants.CONST_EMPTY_VALUE))
                                    );
                    }
                    else if (v_Match_Check_Step == 2)    // 3rd search:  by matched IDs, Empty ToAcc and matched FromAcc (we must summarize those trans)
                    {
                        if ((v_FirstRec.FromAccount == Constants.CONST_EMPTY_VALUE) || (v_FirstRec.ToAccount != Constants.CONST_EMPTY_VALUE))
                        {
                            continue;
                        }

                        v_MatchedRecs =
                            m_TransList.FindAll(
                                    x => ((!x.Reviewed)
                                    && (!x.IsMerged)
                                    && (x.IsBinded)
                                    && (x.Id == v_FirstRec.Id)
                                    && (x.InternalId != v_FirstRec.InternalId)
                                    && (x.FromAccount == v_FirstRec.FromAccount)
                                    && (x.ToAccount == Constants.CONST_EMPTY_VALUE))
                                    );
                    }
                    else if (v_Match_Check_Step == 3)    // 4th search:  by matched IDs, Empty FromAcc and diff ToAcc (we must generate diff Ids for such trans)
                    {
                        if ((v_FirstRec.ToAccount == Constants.CONST_EMPTY_VALUE) || (v_FirstRec.FromAccount != Constants.CONST_EMPTY_VALUE))
                        {
                            continue;
                        }

                        v_MatchedRecs =
                            m_TransList.FindAll(
                                    x => ((!x.Reviewed)
                                    && (!x.IsMerged)
                                    && (x.IsBinded)
                                    && (x.Id == v_FirstRec.Id)
                                    && (x.InternalId != v_FirstRec.InternalId)
                                    && (x.ToAccount != v_FirstRec.ToAccount)
                                    && (x.FromAccount == Constants.CONST_EMPTY_VALUE))
                                    );
                    }
                    else    // Anton: Second search will be by From-To Accounts match within 2 days back and forth
                    {
                        if (v_FirstRec.ToAccount == Constants.CONST_EMPTY_VALUE)
                        {
                            continue;
                        }

                        v_MatchedRecs =
                            m_TransList.FindAll(
                                    x => ((!x.Reviewed)
                                    && (!x.IsMerged)
                                    && (x.IsBinded)
                                    && (x.InternalId != v_FirstRec.InternalId)
                                    && (Math.Abs((x.Date.Date - v_FirstRec.Date.Date).TotalDays) <= 2)  // Finding same trans within 2 days. TODO: move this to settings.                                     
                                    && (x.AmountFrom == v_FirstRec.AmountFrom)
                                    && (x.ToAccount == Constants.CONST_EMPTY_VALUE)
                                    && (x.FromAccount != v_FirstRec.ToAccount))
                                    );
                    }
                    

                    if (v_MatchedRecs.Count > 1)
                    {
                        string v_DupTransIds = string.Empty;
                        foreach (CTransRec v_Rec in v_MatchedRecs)
                        {
                            v_DupTransIds += " " + v_Rec.Id;
                        }

                        CLogEngine.Log.Write("More than one duplicate trans found: " + v_DupTransIds + ". Skipping..");
                    }
                    else if (v_MatchedRecs.Count == 1)
                    {
                        bool v_IsSecondRemoved = false;
                        internal_MergeTransactions(v_FirstRec, v_MatchedRecs[0], out v_IsSecondRemoved);

                        if (v_IsSecondRemoved)
                        {
                            m_TransList.Remove(v_MatchedRecs[0]);
                        }
                    }

                    if (v_MatchedRecs.Count > 0)
                    {
                        v_TransToCheck.Add(v_FirstRec);     // Cannot check for dup right now because duplicates not yet cleared from list                                            
                    }
                }                
            }

            //foreach (CTransRec v_Trans in v_TransToRemove)
            //{
            //    m_TransList.Remove(v_Trans);
            //}

            foreach (CTransRec v_CheckTrans in v_TransToCheck)
            {
                this.CheckDuplicate(v_CheckTrans);
            }

            // v_TransToRemove.Clear();
            v_TransToCheck.Clear();
            return;
        }

        // Apply rules to assign trans category
        private void Step3_AssignCategories()
        {
            foreach (CTransRec v_TR in m_TransList)
            {
                if (v_TR.Reviewed) continue;    // skip applying category if trans already reviewed

                bool v_WasRuleApplied = false;

                using (IEnumerator<CRule> v_RuleEnum = m_RuleList.GetEnumerator())
                {                    
                    string v_RulesList = string.Empty;                    

                    // while (!v_RuleApplied && v_RuleEnum.MoveNext())  // "First working rule" conception
                    while (v_RuleEnum.MoveNext())  // "Full working rule set"  conception
                    {
                        CRule v_CurRule = (CRule)v_RuleEnum.Current;

                        if (v_CurRule.ApplyRule(v_TR))
                        {
                            v_RulesList += ", " + v_CurRule.RuleOrder.ToString();
                            v_WasRuleApplied = true;
                        }
                    }

                    if (v_WasRuleApplied)
                    {
                        v_TR.ProcHint += " Rules: " + v_RulesList.TrimStart(',');
                    }
                }

                // Try assign category from MCC if no rules applied
                if (Constants.IsEmpty(v_TR.Category) && !Constants.IsEmpty(v_TR.MCC))
                {
                    CMccCode v_FoundMCC = m_MccCodesList.Find(x => (x.Code == v_TR.MCC) && (!Constants.IsEmpty(x.Category)));

                    if (v_FoundMCC != null)
                    {
                        v_TR.Category = v_FoundMCC.Category;
                        v_TR.ToAccount = Constants.CONST_SHOP;
                        v_TR.ProcHint += " Category assigned by MCC: " + v_FoundMCC.Code;
                    }
                }
            }
        }

        private void internal_SplitTransRecs(CTransRec v_MainTrans, CTransRec v_PairTrans)
        {
            v_PairTrans.IsMerged = false;
            v_PairTrans.ToAccount = Constants.CONST_EMPTY_VALUE;
            v_PairTrans.Category = Constants.CONST_EMPTY_VALUE;

            v_MainTrans.IsMerged = false;
            v_MainTrans.MergedId = null;
            v_MainTrans.FromAccount = Constants.CONST_EMPTY_VALUE;
            v_MainTrans.Category = Constants.CONST_EMPTY_VALUE;
            v_MainTrans.ProcHint = null;

            if (!string.IsNullOrEmpty(v_MainTrans.Description))
            {
                v_MainTrans.Description = v_MainTrans.Description.Split('|')[0];
            }
        }

        public void SplitTransaction(CTransRec p_TransRec)
        {
            CTransRec v_Rec = p_TransRec;

            // Try find merged pair Trans in m_TransList first
            CTransRec v_PairRec = m_TransList.Find(x => (x.Id == v_Rec.MergedId) && (x.FromAccount == v_Rec.FromAccount));

            if (v_PairRec != null)
            {
                internal_SplitTransRecs(v_Rec, v_PairRec);
                return;
            }

            // If not found in TransList then get paired trans from DB
            using (CTransList v_PairTrans = new CTransList(m_DBConnection))
            {
                v_PairTrans.LoadFromSelect(
                     string.Format(
                     @"
                SELECT T.Id,
                    Reviewed,
                    FromAccount,
                    ToAccount,
                    Date,
                    AmountFrom,
                    AmountTo,
                    Description,
                    Category,                    
                    ProcHint,                    
                    MCC,
                    IsMerged,
                    MergedId,
                    FromAcc.Currency AS FromAccountCurrency,
                    ToAcc.Currency AS ToAccountCurrency,
                    '' AS Color
                FROM Transactions T
                LEFT JOIN Accounts FromACC ON FromAcc.Id = T.FromAccount
                LEFT JOIN Accounts ToAcc ON ToAcc.Id = T.ToAccount
                WHERE (T.Id = '{0}' and (T.FromAccount = '{1}'))
                LIMIT 1",
                     v_Rec.MergedId,
                     v_Rec.FromAccount
                     )
                );

                // If found, unmerge original and found one transactions
                foreach (CTransRec v_MergedTrans in v_PairTrans)
                {
                    m_TransList.Add(v_MergedTrans);
                    internal_SplitTransRecs(v_Rec, v_MergedTrans);                    
                    break;
                }                
            }
        }

        public void LoadTransactions(string p_Directory)
        {            
            this.Step1_LoadTransactions();
            this.Step11_MarkDuplicates();
            
            this.Step2_JoinMoneyTransfers();
            this.Step3_AssignCategories();                        
        }       

        public void AssignCategories()
        {
            this.Step3_AssignCategories();
        }     
        

        public void SaveTransactions()
        {
            m_TransList.Update();

            // Recalculate EndBalances if needed
            this.RecalculateEndBalance();
        }

        public void CancelTransactions()
        {
            m_TransList.CancelUpdate();
        }

        public void SaveRules()
        {
            m_RuleList.Update();
        }

        public void CancelRules()
        {
            m_RuleList.CancelUpdate();
        }

        public void SaveAccounts()
        {
            m_AccountList.Update();
        }

        public void CancelAccounts()
        {
            m_AccountList.CancelUpdate();
        }

        public void SaveCategories()
        {
            m_CategoryList.Update();
        }

        public void CancelCategories()
        {
            m_CategoryList.CancelUpdate();
        }

        public void SaveMccCodes()
        {
            m_MccCodesList.Update();
        }

        public void CancelMccCodes()
        {
            m_MccCodesList.CancelUpdate();
        }

        public delegate bool ConfirmChanges(string p_Message);
        public event ConfirmChanges OnConfirmChanges;

        private bool ConfirmChange_Dataset(IObjectList p_SourceList)
        {
            string p_Message = p_SourceList.GetTableName() + " was changed. Save changes?";
            return this.OnConfirmChanges(p_Message);
        }

        private void AfterSave_Transactions(IObjectList p_SourceList)
        {
            this.RecalculateEndBalance();
        }

        public DataSet GetTransactions()
        {
            return m_TransList.Dataset;            
        }

        public CTransRec GetTransByRow(DataRow p_Row)
        {
            if (p_Row == null) return null;

            return m_TransList.GetObjectByRow(p_Row);
        }

        public CAccount GetAccountByRow(DataRow p_Row)
        {
            if (p_Row == null) return null;

            return m_AccountList.GetObjectByRow(p_Row);
        }

        public DataSet GetRulesDataset()
        {
            return m_RuleList.Dataset;
        }

        public DataSet GetAccountsDataset()
        {
            return m_AccountList.Dataset;
        }

        public DataSet GetCategoriesDataset()
        {
            return m_CategoryList.Dataset;
        }

        public DataSet GetMccCodesDataset()
        {
            return m_MccCodesList.Dataset;
        }

        public CMccCode FindMcc(string p_MccCode)
        {
            return m_MccCodesList.Find(x => x.Code == p_MccCode);
        }

        private DataTable CloneTableFromRows(List<DataRow> p_List, DataTable p_Table)
        {
            return p_List.Any() ? p_List.CopyToDataTable() : p_Table.Clone();
        }

        public DataTable GetAccountsForCombo()
        {
            DataTable v_srcTable = m_AccountList.Dataset.Tables[0];
            var v_Rows = v_srcTable.AsEnumerable()
                .Where(row => (
                            row.Field<String>("Id") != Constants.CONST_DEFAULT_VALUE
                            && row.Field<bool>("Active")
                        ))
                .OrderBy(row => row.Field<String>("Name")).ToList<DataRow>();
                
            return CloneTableFromRows(v_Rows, v_srcTable);
        }

        public DataTable GetAccountsForCriteria()
        {
            DataTable v_srcTable = m_AccountList.Dataset.Tables[0];

            var v_Rows = v_srcTable.AsEnumerable()
                .Where(row => (
                        row.Field<String>("Id") != Constants.CONST_DEFAULT_VALUE
                        && row.Field<bool>("Active")
                    ))
                .OrderBy(row => row.Field<String>("Name")).ToList();

            DataTable v_tblFiltered = CloneTableFromRows(v_Rows, v_srcTable);

            // Append table with 'ALL' option to show all transactions
            DataRow v_NewRow = v_tblFiltered.NewRow();
            v_NewRow[0] =Constants.CONST_ALL_VALUE;
            v_NewRow[1] = "<Все>";
            v_tblFiltered.Rows.InsertAt(v_NewRow, 0);
            return v_tblFiltered;
        }

        public DataTable GetAccountsForRules()
        {
            DataTable v_ComboTable = m_AccountList.Dataset.Tables[0].Copy();                        
            return v_ComboTable;
        }

        public DataTable GetCategoriesForCriteria()
        {
            DataTable v_srcTable = m_CategoryList.Dataset.Tables[0];

            var v_Rows = v_srcTable.AsEnumerable()
              .Where(row => row.Field<String>("Id") != Constants.CONST_DEFAULT_VALUE)
              .OrderBy(row => row.Field<String>("Name")).ToList();

            DataTable v_tblFiltered = CloneTableFromRows(v_Rows, v_srcTable);

            // Append table with 'ALL' option to show all transactions
            DataRow v_NewRow = v_tblFiltered.NewRow();
            v_NewRow[0] =Constants.CONST_ALL_VALUE;
            v_NewRow[1] = "<Все>";
            v_tblFiltered.Rows.InsertAt(v_NewRow, 0);

            return v_tblFiltered;
        }

        public DataTable GetCategoriesForRules()
        {
            DataTable v_ComboTable = m_CategoryList.Dataset.Tables[0].Copy();            

            return v_ComboTable;
        }

        public DataTable GetCategoriesForCombo()
        {
            DataTable v_srcTable = m_CategoryList.Dataset.Tables[0];

            var v_Rows = v_srcTable.AsEnumerable()
               .Where(row => row.Field<String>("Id") != Constants.CONST_DEFAULT_VALUE)
               .OrderBy(row => row.Field<String>("Name")).ToList();
               
            return CloneTableFromRows(v_Rows, v_srcTable);
        }

        /* Anton: this SP fixes old ID field to a new one
        public void Fix_AkBarsIds()
        {
            SourceDirectory = CTransactionManager.Manager.SourceDirectory;

            this.Step1_LoadTransactions();
            string v_UpdateSQL = string.Empty;

            foreach (CTransRec v_TransRec in m_TransList)
            {
                v_UpdateSQL += string.Format("UPDATE Transactions SET Id = '{0}' WHERE Id = '{1}';" + Environment.NewLine, v_TransRec.Id, v_TransRec.Description);
                
            }

            //this.ExecNonQuery(v_UpdateSQL, null);
        }
        */
        

        private static CTransactionManager m_IntManager = null;

        public static CTransactionManager Manager
        {
            get
            {
                if (m_IntManager == null) m_IntManager = new CTransactionManager();
                return m_IntManager;
            }
        }

        #region Utility Functions
        public string GetAccountCurrency(string p_Account)
        {
            CAccount v_Account = m_AccountList.Find(x => x.Id == p_Account);
            return v_Account?.Currency;
        }

        public CTransContext GetTransContext(string p_FromAccount, string p_ToAcccount)
        {
            string v_FromAccCurrency = this.GetAccountCurrency(p_FromAccount);
            string v_ToAccCurrency = this.GetAccountCurrency(p_ToAcccount);

            return new CTransContext(v_FromAccCurrency, v_ToAccCurrency);
        }        
        #endregion

        #region Compare (Verify) transactions        

        // This var is filled from AddTransToCompareList event handler
        private string m_AccountToCompare = null;
        public void AddTransToCompareList(CTransRecEventParams p_Params)
        {
            if (p_Params.TransRec == null) return;

            // If it is duplicate then skip it from the list
            if (m_LoadedTransList.ContainsKey(p_Params.TransRec.Id))
            {
                if (!p_Params.SkipDuplicates)
                {
                    CLogEngine.Log.Write("Найден дубль транзакции ID: " + p_Params.TransRec.Id + "   Пропущена");
                }

                return;
            }
            


            LoadedTransList.Add(p_Params.TransRec.Id, p_Params.TransRec);

            if (string.IsNullOrEmpty(m_AccountToCompare))
            {
                m_AccountToCompare = p_Params.BankAccount;
            }
        }

        private CTransList m_ExtraTransInDB = null;
        private CTransList ExtraTransInDB
        {
            get
            {
                if (m_ExtraTransInDB == null) m_ExtraTransInDB = new CTransList(m_DBConnection);
                return m_ExtraTransInDB;
            }
        }

        private CObjectList<CIdObject> m_ExtraTransInFile = null;
        private CObjectList<CIdObject> ExtraTransInFile
        {
            get
            {
                if (m_ExtraTransInFile == null)
                {
                    m_ExtraTransInFile = new CObjectList<CIdObject>(m_DBConnection, "TmpSelTrans");
                    m_ExtraTransInFile.AddFieldRec("Id", 70, DbType.String, true);
                }
                return m_ExtraTransInFile;
            }
        }

        private Dictionary<string, CTransRec> m_LoadedTransList = null;
        private Dictionary<string, CTransRec> LoadedTransList
        {
            get
            {
                if (m_LoadedTransList == null) m_LoadedTransList = new Dictionary<string, CTransRec>();
                return m_LoadedTransList;
            }
        }

        public bool CompareTransactions(string p_SourceFile, ref CTransList p_TransInDB, ref List<CTransRec> p_TransInFile)
        {
            // 1. Load transactions from file to "LoadedTransList"
            LoadedTransList.Clear();
            m_AccountToCompare = null;

            this.Base_LoadTransactions(p_SourceFile, AddTransToCompareList);

            if (m_LoadedTransList.Count <= 0) return false;

            // 2. Select min and max dates from loaded transactions
            DateTime v_MinDate = (from T in m_LoadedTransList.Values select T.Date).Min();
            DateTime v_MaxDate = (from T in m_LoadedTransList.Values select T.Date).Max();
            v_MaxDate = v_MaxDate.AddDays(1);

            // 3. Upload all transactions from DB for m_AccountToCompare between Min and Max dates
            // Get Ids of loaded transactions
            // Prepare "IN" SQL clause
            string v_IdClause = string.Format("'{0}'", string.Join("','", m_LoadedTransList.Keys));

            // 4. Select Extra transactions from DB
            string v_ExtraTransInDB_SQL = string.Format(@"
                SELECT SelTrans.*,
                    FromAcc.Currency AS FromAccountCurrency,
                    ToAcc.Currency AS ToAccountCurrency   
                FROM
                (
                    SELECT 'ExtraTransInSQL', *, '' AS Color
                    FROM Transactions T                                
                    WHERE (T.Date >= '{0}') AND (T.Date < '{1}')
                        AND (T.FromAccount = '{2}' OR T.ToAccount = '{2}')
                        AND (T.Id NOT IN ({3}))
                    UNION ALL
                    SELECT 'HiddenTransInSQL', *, '' AS Color 
                    FROM Transactions T                    
                    WHERE T.Id IN
                    (
                        {1}
                    ) 
                    AND (
                            (T.IsMerged = 1) AND (T.MergedId IS NULL)
                            OR (T.FromAccount <> '{2}') AND (T.ToAccount <> '{2}')
                        )
                )   SelTrans
                LEFT JOIN Accounts FromACC ON FromAcc.Id = SelTrans.FromAccount
                LEFT JOIN Accounts ToAcc ON ToAcc.Id = SelTrans.ToAccount",
               v_MinDate.ToString("yyyy-MM-dd"),
               v_MaxDate.ToString("yyyy-MM-dd"),
                m_AccountToCompare,
                v_IdClause);

            ExtraTransInDB.Clear();
            ExtraTransInDB.LoadFromSelect(v_ExtraTransInDB_SQL);           

            p_TransInDB = ExtraTransInDB;            

            // 4. Select Extra transactions from File            
            v_IdClause = string.Join(@"
                ", (from T in m_LoadedTransList.Keys select string.Format("INSERT INTO TmpSelTrans(Id) VALUES('{0}');", T)));

            string v_ExtraTransInFile_SQL = string.Format(@"
                DROP TABLE IF EXISTS TmpSelTrans;
                CREATE TEMPORARY TABLE TmpSelTrans (Id CHAR(70));

               {0}

               SELECT Id
               FROM TmpSelTrans TT
               WHERE TT.Id NOT IN
               (
                   SELECT Id
                   FROM Transactions T
                   WHERE (T.Date >= '{1}') AND (T.Date < '{2}')
                    AND (T.FromAccount = '{3}' OR T.ToAccount = '{3}')
               );",
               v_IdClause,
               v_MinDate.ToString("yyyy-MM-dd"),
               v_MaxDate.ToString("yyyy-MM-dd"),
               m_AccountToCompare
                );

            ExtraTransInFile.Clear();
            ExtraTransInFile.LoadFromSelect(v_ExtraTransInFile_SQL);

            List<CTransRec> v_ExtraTransInFile = new List<CTransRec>();

            foreach (CIdObject v_IdObj in ExtraTransInFile)
            {
                v_ExtraTransInFile.Add(LoadedTransList[v_IdObj.Id]);
            }

            p_TransInFile = v_ExtraTransInFile;

            return true;
        }

        #endregion

        private struct SQueryParam
        {
            public SQueryParam(string p_ParamName, string p_Value)
            {
                this.ParamName = p_ParamName;
                this.Value = p_Value;
            }

            public string ParamName;
            public string Value;
        }

        private string GetQuery(string p_FileName, params SQueryParam[] p_QueryParams)
        {
#if DEBUG
            string v_FilePath = Path.Combine("..\\ImportFileParser\\SQL", p_FileName);
#else
            string v_FilePath = Path.Combine(".\\SQL", p_FileName);
#endif

            if (!File.Exists(v_FilePath)) return null;

            string v_QueryText = File.ReadAllText(v_FilePath);

            foreach (SQueryParam v_Param in p_QueryParams)
            {
                v_QueryText = v_QueryText.Replace('{' + v_Param.ParamName + '}', v_Param.Value);
            }

            return v_QueryText;
        }

        #region Database Consistency checks
        public void CheckDBConsistency()
        {
            using (CObjectList<CLogEntry> v_LogEntries = new CObjectList<CLogEntry>(this.m_DBConnection, "Log"))
            {
                v_LogEntries.LoadFromSelect(GetQuery("SP_CHK_DbConsistency.sql"));
                CLogEngine.Log.WriteLogList(v_LogEntries);
            }
        }
        #endregion

        #region Reports
        public CObjectList<CReportCategory> GetReportCategories(string p_Category, DateTime p_FromDate, DateTime p_ToDate)
        {
            // Preprocess category
            if (string.IsNullOrEmpty(p_Category) ||
                (p_Category == Constants.CONST_ALL_VALUE))
            {
                p_Category = String.Empty;
            }

            m_ReportCategories.LoadFromSelect(GetQuery("SP_RPT_PlanFact.sql",                 
                new SQueryParam("FROM_DATE", p_FromDate.ToString("yyyy-MM-dd")),
                new SQueryParam("TO_DATE", p_ToDate.ToString("yyyy-MM-dd")),
                new SQueryParam("CATEGORY", p_Category)
                ));

            return m_ReportCategories;
        }        

        public CObjectList<CTailReportEntries> GetTailReportEntries(DateTime p_FromDate, DateTime p_ToDate)
        {
            // Construct where part            
            string v_FromDate = p_FromDate.ToString("yyyy-MM-dd");

            m_TailReportEntries.LoadFromSelect(GetQuery("SP_RPT_Tails.sql",
                new SQueryParam("CFG_DollarRate", CConfigManager.Manager.StrDollarRate),
                new SQueryParam("CFG_EuroRate", CConfigManager.Manager.StrEuroRate),
                new SQueryParam("FROM_DATE", p_FromDate.ToString("yyyy-MM-01")),
                new SQueryParam("TO_DATE", p_ToDate.ToString("yyyy-MM-01"))
                ));

            return m_TailReportEntries;
        }

#endregion

#region Recalculate Balance methods
        public void AddToMinDateList(CTransRec p_Rec)
        {   
            // First, check if Dictionary already has this entry            
            if (!m_MinDatePerAccount.ContainsKey(p_Rec.FromAccount))
            {
                m_MinDatePerAccount.Add(p_Rec.FromAccount, p_Rec.Date);
            }
            else if (DateTime.Compare(p_Rec.Date, m_MinDatePerAccount[p_Rec.FromAccount]) < 0) // If exists then check for min date
            {
                m_MinDatePerAccount[p_Rec.FromAccount] = p_Rec.Date;
            }

            if (!m_MinDatePerAccount.ContainsKey(p_Rec.ToAccount))
            {
                m_MinDatePerAccount.Add(p_Rec.ToAccount, p_Rec.Date);
            }
            else if (DateTime.Compare(p_Rec.Date, m_MinDatePerAccount[p_Rec.ToAccount]) < 0) // If exists then check for min date
            {
                m_MinDatePerAccount[p_Rec.ToAccount] = p_Rec.Date;
            }

        }

        public void RecalculateEndBalance()
        {
            // Return if nothing to recalculate
            if (m_MinDatePerAccount.Count == 0) return;

            //for each Account-Date pair do call specific SQL that will do recalculation
            foreach (KeyValuePair<string, DateTime> v_KeyValuePair in m_MinDatePerAccount)
            {
                string v_SQL = GetQuery("SP_BUS_UpdateEndBalance.sql",
                        new SQueryParam("Account", v_KeyValuePair.Key),
                        new SQueryParam("Date", v_KeyValuePair.Value.ToString("yyyy-MM-dd HH:mm:ss"))
                        );
                this.ExecNonQuery(v_SQL);
            }

            m_MinDatePerAccount.Clear();
        }
#endregion
    }
}
