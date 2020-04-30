using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Mono.Data.Sqlite;

namespace SQLite
{
    public abstract class AbstractDatabase : IDisposable
    {
        const string _COLUMN_ID = "_id";
        SqLiteHelper _sqLiteHelper;
        readonly Dictionary<string, SqlField> _allFields=new Dictionary<string, SqlField>();
        protected internal string TableName;
        
        public abstract string GetTableName();
        
        public abstract List<SqlField> GetAllFields();
        public abstract string GetDatabaseName();
        public abstract string GetDatabasePath();
        private protected abstract SqlDatabase InitiateSqlDatabase();
        private protected abstract List<SqlTable> InitiateSqlTable();
        void OpenOrCreate()
        {
            if (_sqLiteHelper == null)
            {
                _sqLiteHelper=new SqLiteHelper(GetDatabasePath(),GetDatabaseName());
                TableName = GetTableName();
                List<SqlField> fields = GetAllFields();
                bool isExistIdField = false;
                foreach (var t in fields)
                {
                    if (string.Equals(t.Name, _COLUMN_ID))
                    {
                        isExistIdField = true;
                    }
                    _allFields.Add(t.Name,t);
                }

                if (!isExistIdField)
                {
                    //default set integer _id as primary key
                    _allFields.Add(_COLUMN_ID, new SqlField(_COLUMN_ID).SetType(SqlFieldType.Int).SetPrimaryKey(true));
                }
            }

            if (_sqLiteHelper.IsTableExist(TableName)) return;
            
            {
                SqlStatement sqlStatement=new SqlStatement(TableName);
                foreach (var t in _allFields)
                {
                    sqlStatement.CreateFields(t.Value);
                }

                SqliteDataReader reader = _sqLiteHelper.CreateTable(sqlStatement);
                _sqLiteHelper.CloseReader(reader);
            }
        }
        
        public void Dispose()
        {
            if (_sqLiteHelper == null) return;
            _sqLiteHelper.CloseDatabaseConnection();
            _sqLiteHelper = null;
        }
        

        #region insert
        
        public void Insert(params SqlField[] fields)
        {
            if (IsEmpty(fields))
            {
                return;
            }
            
            OpenOrCreate();

            SqliteDataReader reader = _sqLiteHelper.InsertData(TableName, fields);
            _sqLiteHelper.CloseReader(reader);
        }
        #endregion

        #region delete
        
        public void DeleteTable(bool isDrop = false)
        {
            OpenOrCreate();
            SqliteDataReader reader = _sqLiteHelper.DeleteTable(TableName, isDrop);
            _sqLiteHelper.CloseReader(reader);
        }

        public void Delete(SqlStatement sqlStatement)
        {
            if (sqlStatement == null)
                return;
            OpenOrCreate();
            SqliteDataReader reader = _sqLiteHelper.DeleteData(sqlStatement);
            _sqLiteHelper.CloseReader(reader);
        }

        /// <summary>
        /// select the field which to drop
        /// </summary>
        /// <param name="field"></param>
        /// <param name="operation"></param>
        public void Delete(SqlField field, SqlOperation operation = SqlOperation.Equal)
        {
            if (field == null)
            {
                return;
            }
            
            SqlStatement sqlStatement=new SqlStatement(GetTableName());
            sqlStatement.AddConditions(operation, SqlUnion.None, field);
            Delete(sqlStatement);
        }

        public void Delete(SqlField[] fields, SqlUnion union, SqlOperation operation = SqlOperation.Equal)
        {
            if (fields == null)
                return;
            SqlStatement sqlStatement=new SqlStatement(GetTableName());
            sqlStatement.AddConditions(operation, union, fields);
            Delete(sqlStatement);
        }
        #endregion

        #region update

        public void Update(SqlStatement sqlStatement)
        {
            if (sqlStatement == null)
                return;
            OpenOrCreate();

            SqliteDataReader reader = _sqLiteHelper.UpdateData(sqlStatement);
            _sqLiteHelper.CloseReader(reader);
        }

        public void Update(SqlField[] fields, SqlUnion union, SqlOperation operation, params SqlField[] conditionFields)
        {
            if ((fields == null || fields.Length <= 0) || IsEmpty(conditionFields))
            {
                return;
            }
            SqlStatement sqlStatement=new SqlStatement(GetTableName());
            sqlStatement.UpdateFields(fields).AddConditions(operation, union, conditionFields);
            Update(sqlStatement);
        }

        public void Update(SqlField[] fields, SqlUnion union, params SqlField[] conditionFields)
        {
            Update(fields,union,SqlOperation.Equal,conditionFields);
        }

        public void Update(SqlField[] fields, SqlField conditionField)
        {
            Update(fields,SqlUnion.None,SqlOperation.Equal,conditionField);
        }

        public void Update(SqlField field, SqlUnion union, SqlOperation operation, params SqlField[] conditionFields)
        {
            Update(new SqlField[]{field},union,operation,conditionFields);
        }

        public void Update(SqlField field, SqlField conditionField)
        {
            Update(field,SqlUnion.None,SqlOperation.Equal,conditionField);
        }
        #endregion

        #region select

        public List<SqlField[]> Select(SqlStatement sqlStatement)
        {
            String[] fieldNames = sqlStatement?.GetSelectFieldArray();
            
            if (fieldNames == null || fieldNames.Length <= 0)
                return null;
            OpenOrCreate();
            
            List<SqlField[]> result=new List<SqlField[]>();
            SqliteDataReader reader = _sqLiteHelper.SelectData(sqlStatement);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SqlField[] resultFields=new SqlField[fieldNames.Length];
                    for(int i=0;i<fieldNames.Length;i++)
                    {
                        string fieldName = fieldNames[i].Trim();
                        int ordinal = reader.GetOrdinal(fieldName);

                        if (reader.IsDBNull(ordinal))
                        {
                            resultFields[i]=new SqlField(fieldName);
                        }
                        else
                        {
                            SqlFieldType fieldType = _allFields[fieldName].Type;
                            switch (fieldType)
                            {
                                case SqlFieldType.Binary:
                                    resultFields[i]=new SqlField(fieldName).SetValue(BinaryTools.BytesToString( (byte[])reader.GetValue(ordinal) ));
                                    break;
                                case SqlFieldType.Float:
                                    resultFields[i]=new SqlField(fieldName).SetValue(Convert.ToString(reader.GetFloat(ordinal), CultureInfo.CurrentCulture));
                                    break;
                                case SqlFieldType.Int:
                                    resultFields[i]=new SqlField(fieldName).SetValue(Convert.ToString(reader.GetInt32(ordinal)));
                                    break;
                                case SqlFieldType.Long:
                                    resultFields[i]=new SqlField(fieldName).SetValue(Convert.ToString(reader.GetInt64(ordinal)));
                                    break;
                                case SqlFieldType.Text:
                                    resultFields[i]=new SqlField(fieldName).SetValue(reader.GetString(ordinal));
                                    break;
                                default:
                                    resultFields[i]=new SqlField(fieldName);
                                    break;
                            }

                            resultFields[i].SetType(fieldType);
                        }
                    }
                    result.Add(resultFields);
                }
            }
            _sqLiteHelper.CloseReader(reader);
            return result;
        }

        public List<SqlField[]> Select(string[] fieldNames, SqlUnion union, SqlOperation operation,
            params SqlField[] conditionFields)
        {
            if (fieldNames == null || fieldNames.Length <= 0)
            {
                return null;
            }
            SqlStatement sqlStatement=new SqlStatement(GetTableName());
            sqlStatement.SelectFields(fieldNames);

            if (!IsEmpty(conditionFields))
            {
                sqlStatement.AddConditions(operation, union, conditionFields);
            }

            return Select(sqlStatement);
        }

        public List<SqlField[]> Select(string[] fieldNames,SqlUnion union,params SqlField[] conditionFields)
        {
            return Select(fieldNames, union, SqlOperation.Equal, conditionFields);
        }

        public List<SqlField[]> Select(string[] fieldNames, SqlField conditionField)
        {
            return Select(fieldNames,SqlUnion.None,SqlOperation.Equal,conditionField);
        }

        public List<SqlField[]> Select(string fieldName, SqlUnion union, SqlOperation operation,
            params SqlField[] conditionFields)
        {
            return Select(new string[]{ fieldName },union,operation,conditionFields);
        }

        public List<SqlField[]> Select(string fieldName, SqlUnion union, params SqlField[] conditionFields)
        {
            return Select(fieldName, union, SqlOperation.Equal, conditionFields);
        }

        public List<SqlField[]> Select(string fieldName, SqlField conditionField)
        {
            return Select(fieldName, SqlUnion.None, SqlOperation.Equal, conditionField);
        }

        public bool Select(SqlField field)
        {
            if (field == null)
                return false;
            OpenOrCreate();
            return _sqLiteHelper.SelectData(TableName, field);
        }
        
        #endregion

        public bool IsTableExist()
        {
            OpenOrCreate();
            return _sqLiteHelper.IsTableExist(TableName);
        }

        public bool IsFieldExist(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return false;
            }
            OpenOrCreate();
            return _sqLiteHelper.IsFieldExist(TableName, fieldName);
        }
        
        static bool IsEmpty(ICollection collection)
        {
            if (collection == null || collection.Count <= 0) return true;
            
            IEnumerator e = collection.GetEnumerator();
            while (e.MoveNext())
            {
                var obj = e.Current;
                if (obj != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}