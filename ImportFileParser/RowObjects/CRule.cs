using ImportFileParser.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.RowObjects
{
    public class CRule: CRowObject
    {
        private string m_Condition;
        private CTreeNode m_ExpressionRoot;

        private string m_Category = null;
        string m_FromAcc = null;
        string m_ToAcc = null;
        string m_Desc = null;

        CTransRec m_TransRec = null;

        Dictionary<string, string> m_VarsDict = new Dictionary<string, string>();

        public CRule()
        {
            //
        }

        public CRule(string p_Condition, string p_Category): this(0, p_Condition, p_Category, string.Empty, string.Empty, string.Empty, 0)
        {
            //
        }

        public CRule(int p_Id, string p_Condition, string p_Category, string p_FromAcc, string p_ToAcc, string p_Description, int p_RuleOrder)
        {
            this.m_Id = p_Id;
            this.m_Condition = p_Condition;
            this.m_Category = p_Category;
            this.m_Id = p_Id;
            this.m_FromAcc = p_FromAcc;
            this.m_ToAcc = p_ToAcc;
            this.m_Desc = p_Description;
            this.m_RuleOrder = p_RuleOrder;

            // Build expression tree from string
            m_ExpressionRoot = new CTreeNode(this.Condition);
        }

        public override void Bind(DataRow p_Row)
        {
            base.Bind(p_Row);
            
            // Build expression tree from string
            m_ExpressionRoot = new CTreeNode(this.Condition);
        }

        private int m_Id;
        public int Id
        {
            get
            {
                if (this.IsBinded) return this.GetInt("Id");
                else return m_Id;
            }
        }

        public string Condition
        {
            get
            {
                if (this.IsBinded) return this.GetString("Condition");
                else return m_Condition;
            }
        }

        public string Category
        {
            get
            {
                if (this.IsBinded) return this.GetString("Category");
                else return m_Category;
            }
        }

        public string Description
        {
            get
            {
                if (this.IsBinded) return this.GetString("Description");
                else return m_Desc;
            }
        }

        public string FromAccount
        {
            get
            {
                if (this.IsBinded) return this.GetString("FromAccount");
                else return m_FromAcc;
            }
        }

        public string ToAccount
        {
            get
            {
                if (this.IsBinded) return this.GetString("ToAccount");
                else return m_ToAcc;
            }
        }

        private int m_RuleOrder;
        public int RuleOrder
        {
            get
            {
                if (this.IsBinded) return this.GetInt("RuleOrder");
                else return m_RuleOrder;
            }
        }

        private string SafeGetValue(object p_Value)
        {
            if (p_Value == null) return null;

            if (p_Value is string)
            {
                return ((string)p_Value).ToLower();
            }
            else if (p_Value is Enum)
            {
                return Enum.GetName(p_Value.GetType(), p_Value).ToLower();
            }
            else if (p_Value is decimal)
            {
                return ((decimal)p_Value).ToString();
            }

            return null;
        }

        public bool ApplyRule(CTransRec p_ToRec)
        {
            m_TransRec = p_ToRec;
            m_VarsDict.Clear();
            m_VarsDict.Add("category", SafeGetValue(m_TransRec.Category));
            m_VarsDict.Add("desc", SafeGetValue(m_TransRec.Description));
            m_VarsDict.Add("fromacc", SafeGetValue(m_TransRec.FromAccount));
            m_VarsDict.Add("toacc", SafeGetValue(m_TransRec.ToAccount));
            m_VarsDict.Add("amount", SafeGetValue(m_TransRec.AmountFrom));

            bool v_RuleApplied = false;

            if (this.m_ExpressionRoot.Evaluate(m_VarsDict))
            {
                if (!Constants.IsEmpty(this.Category) && (this.Category != Constants.CONST_DEFAULT_VALUE))
                {
                    m_TransRec.Category = this.Category;                    
                }

                if (!string.IsNullOrEmpty(this.Description))
                {
                    m_TransRec.Description = this.Description;
                }

                if (!string.IsNullOrEmpty(this.FromAccount) && (this.FromAccount != Constants.CONST_DEFAULT_VALUE))
                {
                    m_TransRec.FromAccount = this.FromAccount;
                }

                if (!string.IsNullOrEmpty(this.ToAccount) && (this.ToAccount != Constants.CONST_DEFAULT_VALUE))
                {                    
                    m_TransRec.ToAccount = this.ToAccount;                 
                }

                v_RuleApplied = true;
            }
            return v_RuleApplied;
        }

        public override void Assign(DataRow p_Row)
        {
            p_Row["Id"] = this.m_Id;
            p_Row["Condition"] = this.m_Condition;
            p_Row["FromAccount"] = this.m_FromAcc;
            p_Row["ToAccount"] = this.m_ToAcc;
            p_Row["Description"] = this.m_Desc;            
            p_Row["Category"] = this.m_Category;
            p_Row["RuleOrder"] = this.m_RuleOrder;

            base.Assign(p_Row);
        }
    }
}
