using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using ImportFileParser.RowObjects;

namespace ImportFileParser
{
    internal class CFieldRec
    {
        string m_FieldName = string.Empty;
        int m_Width = 0;
        DbType m_Type = DbType.String;
        bool m_IsKey = false;

        public CFieldRec(string p_FieldName, int p_Width, DbType p_Type, bool p_IsKey = false)
        {
            m_FieldName = p_FieldName;
            m_Width = p_Width;
            m_Type = p_Type;
            m_IsKey = p_IsKey;
        }

        public string FieldName
        {
            get
            {
                return m_FieldName;
            }
        }

        public int Width
        {
            get
            {
                return m_Width;
            }
        }


        public DbType Type
        {
            get
            {
                return m_Type;
            }
        }


        public bool IsKey
        {
            get
            {
                return m_IsKey;
            }
        }
    }

    public interface IObjectList
    {
        string GetTableName();
    }

    public class CObjectList<T>: IObjectList, IEnumerable<T>, IDisposable
    {
        private Dictionary<string, CFieldRec> m_FieldRecs = new Dictionary<string, CFieldRec>();
        private string m_TableName = string.Empty;

        private List<T> m_RowObjectList = null;
        private SQLiteConnection m_DBConnection = null;
        private DataTable m_DataTable = null;
        private DataSet m_Dataset = null;
                
        private SQLiteDataAdapter m_Adapter = null;
        
        public CObjectList(SQLiteConnection p_Connection, string p_TableName)
        {
            m_DBConnection = p_Connection;
            m_TableName = p_TableName;            
        }

        public void Dispose()
        {
            m_FieldRecs.Clear();
            m_FieldRecs = null;

            m_RowObjectList.Clear();
            m_RowObjectList = null;

            if (m_DBConnection.State != ConnectionState.Closed) m_DBConnection.Close();

            m_Adapter.Dispose();
            m_Dataset.Dispose();
            m_DataTable.Dispose();            
        }

        string IObjectList.GetTableName()
        {            
            return m_TableName;         
        }

        public bool AddFieldRec(string p_FieldName, int p_Width, DbType p_Type, bool p_IsKey = false)
        {
            // Check if this record is already added
            if (m_FieldRecs.ContainsKey(p_FieldName)) return false;

            CFieldRec v_NewRec = new CFieldRec(p_FieldName, p_Width, p_Type, p_IsKey);
            m_FieldRecs.Add(p_FieldName, v_NewRec);
            return true;
        }

        private SQLiteCommand GetInsertCmd()
        {
            SQLiteCommand v_Cmd = m_DBConnection.CreateCommand();

            string v_FieldNames = string.Empty;
            string v_Values = string.Empty;

            foreach (CFieldRec v_Rec in m_FieldRecs.Values)
            {
                v_FieldNames += "," + v_Rec.FieldName;
                v_Values += ",@" + v_Rec.FieldName;

                v_Cmd.Parameters.Add("@" + v_Rec.FieldName, v_Rec.Type, v_Rec.Width, v_Rec.FieldName);
            }

            v_FieldNames = v_FieldNames.TrimStart(',');
            v_Values = v_Values.TrimStart(',');

            v_Cmd.CommandText = string.Format("INSERT OR IGNORE INTO {0} ({1}) VALUES ({2})", m_TableName, v_FieldNames, v_Values);

            return v_Cmd;
        }

        private SQLiteCommand GetUpdateCmd()
        {
            SQLiteCommand v_Cmd = m_DBConnection.CreateCommand();

            string v_Fields = string.Empty;
            string v_KeyClause = string.Empty;

            foreach (CFieldRec v_Rec in m_FieldRecs.Values)
            {
                if (v_Rec.IsKey)
                {
                    v_KeyClause = string.Format("{0}=@{1}", v_Rec.FieldName, v_Rec.FieldName);
                }
                else
                {
                    v_Fields += string.Format(",{0}=@{1}", v_Rec.FieldName, v_Rec.FieldName);
                }

                v_Cmd.Parameters.Add("@" + v_Rec.FieldName, v_Rec.Type, v_Rec.Width, v_Rec.FieldName);
            }

            v_Fields = v_Fields.TrimStart(',');

            v_Cmd.CommandText = string.Format("UPDATE {0} SET {1} WHERE {2}", m_TableName, v_Fields, v_KeyClause);

            return v_Cmd;
        }

        private SQLiteCommand GetDeleteCmd()
        {
            SQLiteCommand v_Cmd = m_DBConnection.CreateCommand();

            string v_KeyClause = string.Empty;

            foreach (CFieldRec v_Rec in m_FieldRecs.Values)
            {
                if (v_Rec.IsKey)
                {
                    v_KeyClause = string.Format("{0}=@{1}", v_Rec.FieldName, v_Rec.FieldName);

                    v_Cmd.Parameters.Add("@" + v_Rec.FieldName, v_Rec.Type, v_Rec.Width, v_Rec.FieldName);
                }
            }

            v_Cmd.CommandText = string.Format("DELETE FROM {0} WHERE {1}", m_TableName, v_KeyClause);

            return v_Cmd;            
        }       

        public List<T> ObjectList
        {
            get
            {
                if (this.m_RowObjectList == null) m_RowObjectList = new List<T>();
                return m_RowObjectList;
            }
        }

        public T this[int p_Index]
        {
            get
            {
                if (p_Index >= ObjectList.Count) return default(T);
                return ObjectList[p_Index];
            }
            set
            {
                if (p_Index >= ObjectList.Count) return;
                ObjectList[p_Index] = value;
            }
        }
        
        public DataSet Dataset
        {
            get
            {
                return m_DataTable.DataSet;
            }
        }

        private void RefillList()
        {
            ObjectList.Clear();

            foreach (DataRow v_Row in m_DataTable.Rows)
            {
                var v_NewRec = Activator.CreateInstance(typeof(T));
                (v_NewRec as CRowObject).Bind(v_Row);
                ObjectList.Add((T)v_NewRec);
            }
        }

        public delegate bool ConfirmChanges(IObjectList p_SourceList);
        public event ConfirmChanges OnConfirmChanges;

        public delegate void AfterSaveDataset(IObjectList p_SourceList);
        public event AfterSaveDataset OnAfterSaveDataset;

        public void LoadFromSelect(string p_SelectCmd)
        {
            //
            if (m_Dataset != null)
            {
                if (m_Dataset.HasChanges())
                {
                    if (OnConfirmChanges(this))
                    {
                        this.Update();
                        OnAfterSaveDataset(this);
                    }
                }
            }

            if (m_DBConnection.State != ConnectionState.Open) m_DBConnection.Open();
            if (m_DBConnection.State != ConnectionState.Open) return;

            // Prepare select cmd


            try
            {
                if (m_Dataset != null)
                {
                    m_Dataset.Clear();

                    if (m_Dataset.HasChanges())
                    {
                        m_Dataset.RejectChanges();
                    }

                    m_Adapter.SelectCommand.CommandText = p_SelectCmd;
                }
                else
                {

                    m_Dataset = new DataSet("NewDataSet");
                    m_Adapter = new SQLiteDataAdapter(p_SelectCmd, m_DBConnection);

                    m_Adapter.InsertCommand = this.GetInsertCmd();
                    m_Adapter.UpdateCommand = this.GetUpdateCmd();
                    m_Adapter.DeleteCommand = this.GetDeleteCmd();                    
                }                

                m_Adapter.Fill(m_Dataset, m_TableName);
                m_DataTable = m_Dataset.Tables[0];                                

                this.RefillList();
         
                // Reflect row col changes into internal RowObject properties
                m_DataTable.ColumnChanged += M_DataTable_ColumnChanged;
            }
            finally
            {
                m_DBConnection.Close();
            }
        }        

        private void M_DataTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            T v_FoundRO = this.GetObjectByRow(e.Row);

            if (v_FoundRO == null) return;
            (v_FoundRO as CRowObject).RowChanged(e.Column.ColumnName);
        }

        public void Add(T p_NewRec)
        {
            CRowObject v_NewRec = (p_NewRec as CRowObject);
            // Optimization: if BindedRow exsts then use ImportRow
            if (v_NewRec.IsBinded)
            {
                m_DataTable.ImportRow(v_NewRec.BindedRow);
                DataRow v_ImportedRow = m_DataTable.Rows[m_DataTable.Rows.Count - 1];
                v_NewRec.Bind(v_ImportedRow);
            }
            else
            {
                // Otherwise, manually append new row and fill from object with binding
                DataRow v_NewRow = m_DataTable.NewRow();
                // Fill row from CRowObject props and bind record to this DataRow
                v_NewRec.Assign(v_NewRow);
                m_DataTable.Rows.Add(v_NewRow);
            }            
            
            m_RowObjectList.Add(p_NewRec);
        }

        public void Remove(T p_Rec)
        {
            // Anton: Below commented because it causes table changes not be saved in DB
            CRowObject v_Rec = (p_Rec as CRowObject);

            if (v_Rec.IsBinded)
            {
                v_Rec.BindedRow.Delete();
                v_Rec.Unbind();                
            }

            m_RowObjectList.Remove(p_Rec);
        }

        public void RemoveRows(T[] p_Recs)
        {
            //
        }

        // Removes T based on DataRow
        public void Remove(DataRow p_Row)
        {
            foreach (T v_Rec in m_RowObjectList)
            {
                if ((v_Rec as CRowObject).BindedRow == p_Row)
                {
                    m_RowObjectList.Remove(v_Rec);
                    break;
                }
            }            
        }

        public T Find(Predicate<T> p_Match)
        {
            return m_RowObjectList.Find(p_Match);
        }

        public List<T> FindAll(Predicate<T> p_Match)
        {
            return m_RowObjectList.FindAll(p_Match);
        }

        
        public IEnumerable<DateTime> Select(Func<T, DateTime> p_Selector)
        {            
            return m_RowObjectList.Select(p_Selector);            
        }         

        public void Clear()
        {            
            m_RowObjectList?.Clear();
            m_DataTable?.Clear();
        }

        public DataTable Table
        {
            get
            {
                return m_DataTable;
            }
        }

        public void Update()
        {
            if (m_DBConnection.State != ConnectionState.Open) m_DBConnection.Open();
            if (m_DBConnection.State != ConnectionState.Open) return;

            try
            {             
                int v_UpdateResult = m_Adapter.Update(m_DataTable);
            }
            finally
            {
                m_DBConnection.Close();
            }

            this.RefillList();
        }

        public void CancelUpdate()
        {            
            m_Dataset.RejectChanges();

            this.RefillList();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ObjectList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int FindMaxValue(Converter<T, int> projection)
        {
            if (m_RowObjectList.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            int maxValue = int.MinValue;

            foreach (T item in m_RowObjectList)
            {
                int value = projection(item);
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
            return maxValue;
        }

        public T GetObjectByRow(DataRow p_Row)
        {
            T v_Found = m_RowObjectList.Find(x => (x as CRowObject).BindedRow == p_Row);

            if (v_Found != null) return v_Found;

            return default(T);
        }
    }
}
