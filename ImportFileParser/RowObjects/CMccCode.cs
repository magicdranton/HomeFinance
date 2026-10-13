using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.RowObjects
{
    public class CMccCode : CRowObject
    {
        public CMccCode()
        {
            //
        }

        public CMccCode(string p_Code, string p_Name, string p_Description, string p_Category) : this()
        {
            m_Code = p_Code;
            m_Name = p_Name;
            m_Description = p_Description;
            m_Category = p_Category;
        }

        private string m_Code;
        public string Code
        {
            get
            {
                if (this.IsBinded) return this.GetString("Code");
                else return m_Code;
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

        private string m_Description;
        public string Description
        {
            get
            {
                if (this.IsBinded) return this.GetString("Description");
                else return m_Description;
            }
        }

        private string m_Category;
        public string Category
        {
            get
            {
                if (this.IsBinded) return this.GetString("Category");
                else return m_Category;
            }
        }

        public override void Assign(DataRow p_Row)
        {
            p_Row["Code"] = this.m_Code;
            p_Row["Name"] = this.m_Name;
            p_Row["Description"] = this.m_Description;
            p_Row["Category"] = this.m_Category;

            base.Assign(p_Row);
        }
    }
}
