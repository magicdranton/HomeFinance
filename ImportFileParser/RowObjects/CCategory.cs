using System;
using System.Data;

namespace ImportFileParser.RowObjects
{
    public class CCategory: CRowObject
    {
        public CCategory()
        {
            //
        }

        public CCategory(string p_Id, string p_Name) : this()
        {
            m_Id = p_Id;
            m_Name = p_Name;
        }

        private string m_Id;
        public string Id
        {
            get
            {
                if (this.IsBinded) return this.GetString("Id");
                else return m_Id;
            }
        }

        private string m_Name;
        public string Name
        {
            get
            {
                if (this.IsBinded) return this.GetString("Name");
                else return m_Name;
            }
        }

        private decimal m_PlannedValue;
        public decimal PlannedValue
        {
            get
            {
                if (this.IsBinded) return this.GetDecimal("PlannedValue");
                else return m_PlannedValue;
            }
            set
            {
                if (this.IsBinded) this.SetValue("PlannedValue", value);
                else m_PlannedValue = value;
            }
        }
        
        #region IsBuiltin
        private bool m_IsBuiltin;
        public bool IsBuiltin
        {
            get
            {
                if (this.IsBinded) return this.GetBool("IsBuiltin");
                else return m_IsBuiltin;
            }
            set
            {
                if (this.IsBinded) this.SetValue("IsBuiltin", value);
                else m_IsBuiltin = value;
            }
        }
        #endregion

        public override void Assign(DataRow p_Row)
        {
            p_Row["Id"] = this.m_Id;
            p_Row["Name"] = this.m_Name;
            p_Row["PlannedValue"] = this.m_PlannedValue;
            p_Row["IsBuiltin"] = this.m_IsBuiltin;

            base.Assign(p_Row);
        }
    }
}
