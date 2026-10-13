using ImportFileParser.RowObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser
{
    public class CTransList: CObjectList<CTransRec>
    {
        public CTransList(SQLiteConnection p_Connection): base(p_Connection, "Transactions")
        {
            this.AddFieldRec("Id", 70, DbType.String, true);
            this.AddFieldRec("Reviewed", 0, DbType.Boolean);
            this.AddFieldRec("FromAccount", 3, DbType.String);
            this.AddFieldRec("ToAccount", 3, DbType.String);
            this.AddFieldRec("Date", 0, DbType.DateTime);
            this.AddFieldRec("AmountFrom", 0, DbType.Decimal);
            this.AddFieldRec("AmountTo", 0, DbType.Decimal);
            this.AddFieldRec("Description", 100, DbType.String);
            this.AddFieldRec("Category", 3, DbType.String);
            this.AddFieldRec("ProcHint", 255, DbType.String);
            this.AddFieldRec("MCC", 4, DbType.String);
            this.AddFieldRec("IsMerged", 0, DbType.Boolean);
            this.AddFieldRec("MergedId", 70, DbType.String);
        }
    }
}
