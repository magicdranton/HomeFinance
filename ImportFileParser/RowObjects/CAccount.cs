using System;
using System.Data;

namespace ImportFileParser.RowObjects
{
    public class CAccount : CRowObject
    {
        public CAccount()
        {
            //
        }

        public CAccount(string p_Id, string p_Name, decimal p_StartBalance) : this()
        {
            m_Id = p_Id;
            m_Name = p_Name;
            m_StartBalance = p_StartBalance;
        }

        #region Id
        private string m_Id;
        public string Id
        {
            get
            {
                if (this.IsBinded) return this.GetString("Id");
                else return m_Id;
            }
        }
        #endregion

        #region Name
        private string m_Name;
        public string Name
        {
            get
            {
                if (this.IsBinded) return this.GetString("Name");
                else return m_Name;
            }
        }
        #endregion

        private decimal m_StartBalance;
        public decimal StartBalance
        {
            get
            {
                if (this.IsBinded) return this.GetDecimal("StartBalance");
                else return m_StartBalance;
            }
            set
            {
                if (this.IsBinded) this.SetValue("StartBalance", value);
                else m_StartBalance = value;
            }
        }

        private bool m_IsExternal;
        public bool IsExternal
        {
            get
            {
                if (this.IsBinded) return this.GetBool("IsExternal");
                else return m_IsExternal;
            }
            set
            {
                if (this.IsBinded) this.SetValue("IsExternal", value);
                else m_IsExternal = value;
            }
        }

        private bool m_IsSavings;
        public bool IsSavings
        {
            get
            {
                if (this.IsBinded) return this.GetBool("IsSavings");
                else return m_IsSavings;
            }
            set
            {
                if (this.IsBinded) this.SetValue("IsSavings", value);
                else m_IsSavings = value;
            }
        }

        private bool m_Active;
        public bool Active
        {
            get
            {
                if (this.IsBinded) return this.GetBool("Active");
                else return m_Active;
            }
            set
            {
                if (this.IsBinded) this.SetValue("Active", value);
                else m_Active = value;
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

        private string m_FileMask;
        public string FileMask
        {
            get
            {
                if (this.IsBinded) return this.GetString("FileMask");
                else return m_FileMask;
            }
        }

        private string m_FileLoader;
        public string FileLoader
        {
            get
            {
                if (this.IsBinded) return this.GetString("FileLoader");
                else return m_FileLoader;
            }
        }

        private string m_Currency;
        public string Currency
        {
            get
            {
                if (this.IsBinded) return this.GetString("Currency");
                else return m_Currency;
            }
        }

        public override void Assign(DataRow p_Row)
        {
            p_Row["Id"] = this.m_Id;
            p_Row["Name"] = this.m_Name;
            p_Row["StartBalance"] = this.m_StartBalance;
            p_Row["IsExternal"] = this.m_IsExternal;
            p_Row["IsSavings"] = this.m_IsSavings;
            p_Row["Active"] = this.m_Active;
            p_Row["IsBuiltin"] = this.m_IsBuiltin;
            p_Row["FileMask"] = this.m_FileMask;
            p_Row["Currency"] = this.m_Currency;

            base.Assign(p_Row);
        }
    }
}
