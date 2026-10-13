using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.RowObjects
{
    public class CIdObject : CRowObject
    {
        public CIdObject()
        {
            //
        }
        public CIdObject(string p_Id)
        {
            m_Id = p_Id;
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

        public override void Assign(DataRow p_Row)
        {
            p_Row["Id"] = this.m_Id;
        }
    }
}
