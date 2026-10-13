using ImportFileParser.Common;
using ImportFileParser.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser
{
    enum EnumOper
    {
        opNone,
        opAnd,
        opOr,
        opLike,
        opEq,
        opNeq,
        opIsNull
    }

    class CTreeNode
    {
        private string NodeValue = string.Empty;
        private EnumOper m_NodeOper = EnumOper.opNone;

        private CTreeNode Arg1;
        private CTreeNode Arg2;

        public CTreeNode(string p_NodeValue)
        {
            this.NodeValue = p_NodeValue.ToLower();
            this.ParseExpression();
        }

        // Parse string expression formula into expression tree
        private void ParseExpression()
        {
            string v_Arg1 = null;
            string v_Arg2 = null;
            string[] v_Args = null;

            // Guard stop clauses 
            if (string.IsNullOrEmpty(this.NodeValue)) return;

            // 1. try to split OR operation
            if (this.NodeValue.Contains(" or "))
            {
                int v_OrIdx = this.NodeValue.IndexOf(" or ");
                v_Arg1 = this.NodeValue.Substring(0, v_OrIdx).Trim();
                v_Arg2 = this.NodeValue.Substring(v_OrIdx + 4).Trim();

                this.m_NodeOper = EnumOper.opOr;
                this.Arg1 = new CTreeNode(v_Arg1);
                this.Arg2 = new CTreeNode(v_Arg2);
            }
            else if (this.NodeValue.Contains(" and "))
            {
                int v_AndIdx = this.NodeValue.IndexOf(" and ");
                v_Arg1 = this.NodeValue.Substring(0, v_AndIdx).Trim();
                v_Arg2 = this.NodeValue.Substring(v_AndIdx + 5).Trim();

                this.m_NodeOper = EnumOper.opAnd;
                this.Arg1 = new CTreeNode(v_Arg1);
                this.Arg2 = new CTreeNode(v_Arg2);
            }
            // In this case we already working with simple functions
            else if (this.NodeValue.Contains("like("))
            {
                this.m_NodeOper = EnumOper.opLike;
                v_Arg1 = this.NodeValue.Replace("like(", "").Replace(")", "");
                v_Args = v_Arg1.Split(',');
                this.Arg1 = new CTreeNode(v_Args[0]);
                this.Arg2 = new CTreeNode(v_Args[1]);
            }
            else if (this.NodeValue.Contains("neq("))
            {
                this.m_NodeOper = EnumOper.opNeq;
                v_Arg1 = this.NodeValue.Replace("neq(", "").Replace(")", "");
                v_Args = v_Arg1.Split(',');
                this.Arg1 = new CTreeNode(v_Args[0]);
                this.Arg2 = new CTreeNode(v_Args[1]);
            }
            else if (this.NodeValue.Contains("eq("))
            {
                this.m_NodeOper = EnumOper.opEq;
                v_Arg1 = this.NodeValue.Replace("eq(", "").Replace(")", "");
                v_Args = v_Arg1.Split(',');
                this.Arg1 = new CTreeNode(v_Args[0]);
                this.Arg2 = new CTreeNode(v_Args[1]);
            }            
            else if (this.NodeValue.Contains("isnull("))
            {
                this.m_NodeOper = EnumOper.opIsNull;
                v_Arg1 = this.NodeValue.Replace("isnull(", "").Replace(")", "");
                this.Arg1 = new CTreeNode(v_Arg1);
            }
            // Process simplest values
            else
            {
                this.NodeValue = this.NodeValue.Replace("'", "").Trim();
            }
        }

        public string GetValue(Dictionary<string, string> p_VarsDict)
        {
            if (p_VarsDict.ContainsKey(this.NodeValue)) return p_VarsDict[this.NodeValue];
            else return this.NodeValue == null ? null : this.NodeValue.Replace("'", "");
        }

        public bool Evaluate(Dictionary<string, string> p_VarsDict)
        {
            string v_Dummy = null;
            return this.Evaluate(p_VarsDict, ref v_Dummy);
        }

        // evaluate expression tree into bool result
        public bool Evaluate(Dictionary<string, string> p_VarsDict, ref string p_ParsedName)
        {
            bool v_Result = false;

            // Guard stop clauses 
            if (string.IsNullOrEmpty(this.NodeValue)) return true;

            // 1. try to split OR operation
            if (this.m_NodeOper == EnumOper.opOr)
            {
                v_Result = (this.Arg1.Evaluate(p_VarsDict) || this.Arg2.Evaluate(p_VarsDict));
            }
            else if (this.m_NodeOper == EnumOper.opAnd)
            {
                v_Result = (this.Arg1.Evaluate(p_VarsDict) && this.Arg2.Evaluate(p_VarsDict));
            }
            // In this case we already working with simple functions
            else if (this.m_NodeOper == EnumOper.opLike)
            {
                string v_Str1 = this.Arg1.GetValue(p_VarsDict);
                if (string.IsNullOrEmpty(v_Str1)) return false;

                string v_Str2 = this.Arg2.GetValue(p_VarsDict);
                v_Result = v_Str1.Contains(v_Str2);
            }
            else if (this.m_NodeOper == EnumOper.opEq)
            {
                string v_Str1 = this.Arg1.GetValue(p_VarsDict);
                string v_Str2 = this.Arg2.GetValue(p_VarsDict);
                v_Result = string.Equals(v_Str1, v_Str2);
            }
            else if (this.m_NodeOper == EnumOper.opNeq)
            {
                string v_Str1 = this.Arg1.GetValue(p_VarsDict);
                string v_Str2 = this.Arg2.GetValue(p_VarsDict);
                v_Result = !string.Equals(v_Str1, v_Str2);
            }
            else if (this.m_NodeOper == EnumOper.opIsNull)
            {
                string v_Str1 = this.Arg1.GetValue(p_VarsDict);
                v_Result = Constants.IsEmpty(v_Str1);
            }
            // Process simplest values
            else
            {
                p_ParsedName = this.NodeValue.Replace("'", "");
                p_ParsedName = this.GetValue(p_VarsDict);
                v_Result = true;
            }

            return v_Result;
        }
    }
}
