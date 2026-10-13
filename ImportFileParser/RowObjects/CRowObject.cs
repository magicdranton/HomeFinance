using System;
using System.Data;
using ImportFileParser.Loaders;

namespace ImportFileParser.RowObjects
{
    public class CRowObject
    {
        private DataRow m_Row = null;
        private bool m_Binded = false;

        public CRowObject()
        {
            //
        }

        public DataRow BindedRow
        {
            get
            {
                return m_Row;
            }
        }

        public virtual void Bind(DataRow p_Row)
        {
            m_Row = p_Row;
            m_Binded = true;            
        }

        public virtual void Unbind()
        {
            m_Row = null;
            m_Binded = false;
        }

        public virtual void Assign(DataRow p_Row)
        {
            /*
            // Reset importing row to Unchanged state
            switch (p_Row.RowState)
            {
                case DataRowState.Modified:
                case DataRowState.Added: p_Row.AcceptChanges(); break;
            }

            // Copy state from original row
            switch (m_Row?.RowState)
            {
                case DataRowState.Added: p_Row.SetAdded(); break;
                case DataRowState.Modified: p_Row.SetModified(); break;
            }
            */
        
            this.Bind(p_Row);
        }

        public virtual void RowChanged(string p_FieldName)
        {
            if (this.m_Row == null) return;
        }

        public bool IsBinded
        {
            get
            {
                return m_Binded;
            }
        }

        protected void SetValue(string p_FieldName, object p_Value)
        {
            if (!m_Binded) return;
            if (!m_Row.Table.Columns.Contains(p_FieldName)) return;
            m_Row[p_FieldName] = p_Value;            
        }

        protected string GetString(string p_FieldName)
        {
            if (!m_Binded) return string.Empty;

            object v_Result = m_Row[p_FieldName];
            if (v_Result == null) return string.Empty;
            return v_Result.ToString();
        }

        protected DateTime GetDate(string p_FieldName)
        {
            if (!m_Binded) return DateTime.MinValue;

            object v_Result = m_Row[p_FieldName];
            if (v_Result == null) return DateTime.MinValue;

            if (m_Row[p_FieldName].GetType() == typeof(DateTime)) return (DateTime)m_Row[p_FieldName];

            DateTime v_DateResult;
            if (DateTime.TryParse(v_Result.ToString(), CConfigManager.Manager.RussianCulture, System.Globalization.DateTimeStyles.None, out v_DateResult))
            {
                return v_DateResult;
            }

            return DateTime.MinValue;
        }

        protected decimal GetDecimal(string p_FieldName)
        {
            if (!m_Binded) return 0;

            object v_Result = m_Row[p_FieldName];
            if (v_Result == null) return 0;

            if (m_Row[p_FieldName].GetType() == typeof(Decimal)) return (Decimal)m_Row[p_FieldName];
            if (m_Row[p_FieldName].GetType() == typeof(Double)) return Convert.ToDecimal((Double)m_Row[p_FieldName]);

            decimal v_DecResult;
            if (Decimal.TryParse(v_Result.ToString(), System.Globalization.NumberStyles.None, CConfigManager.Manager.RussianCulture, out v_DecResult))
            {
                return v_DecResult;
            }

            return 0;
        }

        protected int GetInt(string p_FieldName)
        {
            if (!m_Binded) return 0;

            object v_Result = m_Row[p_FieldName];
            if (v_Result == null) return 0;

            if (m_Row[p_FieldName].GetType() == typeof(int)) return (int)m_Row[p_FieldName];

            int v_IntResult;
            if (Int32.TryParse(v_Result.ToString(), System.Globalization.NumberStyles.None, CConfigManager.Manager.RussianCulture, out v_IntResult))
            {
                return v_IntResult;
            }

            return 0;
        }

        protected bool GetBool(string p_FieldName)
        {
            if (!m_Binded) return false;

            object v_Result = m_Row[p_FieldName];
            if (v_Result == null) return false;

            if (m_Row[p_FieldName].GetType() == typeof(bool)) return (Boolean)m_Row[p_FieldName];

            Boolean v_BoolResult;
            if (Boolean.TryParse(v_Result.ToString(), out v_BoolResult))
            {
                return v_BoolResult;
            }

            return false;
        }
    }
}
