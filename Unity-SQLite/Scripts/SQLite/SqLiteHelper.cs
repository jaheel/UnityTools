using System;
using UnityEngine;
using System.Text;
using Mono.Data.Sqlite;
namespace SQLite
{
    public class SqLiteHelper
    {
        SqliteConnection _connection;
        
        public SqLiteHelper(string databasePath,string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                return;
            }
            
            try
            {
                string dataPath, protocol;
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        protocol = "URI=file:";
                        dataPath = Application.persistentDataPath;
                        break;
                    case RuntimePlatform.IPhonePlayer:
                        protocol = "data source=";
                        dataPath = Application.persistentDataPath;
                        break;
                    //case RuntimePlatform.PS4:
                    //case RuntimePlatform.XboxOne:
                    default:
                        protocol = "data source=";
                        dataPath = Application.dataPath;
                        Debug.Log(dataPath);
                        break;
                }

                var sql = $"{protocol}{dataPath}{databasePath}{databaseName}.db";
                _connection=new SqliteConnection(sql);
                _connection.Open();
                Debug.Log("打开数据库");
                
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                throw;
            }
            
        }

        /// <summary>
        /// create table
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public SqliteDataReader CreateTable(SqlStatement sqlStatement)
        {
            if (sqlStatement == null) return null;
            var sql = $"CREATE TABLE {sqlStatement.TableName} ( {sqlStatement.GetCreateFields()} )";
            return ExecuteCommand(sql);
        }
        

        /// <summary>
        /// insert data
        /// </summary>
        /// <param name="tableName">string</param>
        /// <param name="fields">SqlField[]</param>
        /// <returns></returns>
        public SqliteDataReader InsertData(string tableName, SqlField[] fields)
        {
            if (string.IsNullOrEmpty(tableName) || fields == null || fields.Length <= 0)
                return null;
            
            StringBuilder fieldNames=new StringBuilder();
            foreach (var t in fields)
            {
                if (!string.IsNullOrEmpty(fieldNames.ToString()))
                {
                    fieldNames.Append(", ");
                }
                fieldNames.Append(t.Name);
            }
            
            StringBuilder fieldValues=new StringBuilder();
            foreach (var t in fields)
            {
                if (!string.IsNullOrEmpty(fieldValues.ToString()))
                {
                    fieldValues.Append(", ");
                }

                fieldValues.Append(FormatValue(t.Value));
            }

            var sql = $"INSERT INTO {tableName} ( {fieldNames} ) VALUES ( {fieldValues} )";
            return ExecuteCommand(sql);
        }

        /// <summary>
        /// delete data
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public SqliteDataReader DeleteData(SqlStatement sqlStatement)
        {
            if (sqlStatement == null)
                return null;
            string sql = $"DELETE FROM {sqlStatement.TableName} WHERE {sqlStatement.GetConditions()}";
            return ExecuteCommand(sql);
        }

        /// <summary>
        /// delete table : delete the data in the table, Keep the table construct
        /// drop table : delete the table and the data
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="isDrop"></param>
        /// <returns></returns>
        public SqliteDataReader DeleteTable(string tableName, bool isDrop = false)
        {
            if (string.IsNullOrEmpty(tableName))
                return null;
            var sql = isDrop ? $"DROP TABLE {tableName}" : $"DELETE FROM {tableName}";

            return ExecuteCommand(sql);
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="sqlStatement">SqlStatement</param>
        /// <returns></returns>
        public SqliteDataReader UpdateData(SqlStatement sqlStatement)
        {
            if (sqlStatement == null)
                return null;
            var sql =
                $"UPDATE {sqlStatement.TableName} SET {sqlStatement.GetUpdateFields()} WHERE {sqlStatement.GetConditions()}";

            return ExecuteCommand(sql);
        }

        /// <summary>
        /// select specific fields from table
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public SqliteDataReader SelectData(SqlStatement sqlStatement)
        {
            if (sqlStatement == null)
                return null;
            StringBuilder sql=new StringBuilder();
            sql.Append($"SELECT {sqlStatement.GetSelectFields()} FROM {sqlStatement.TableName}");

            var conditions = sqlStatement.GetConditions();
            if (!string.IsNullOrEmpty(conditions))
            {
                sql.Append($" WHERE {conditions}");
            }

            var groupBy = sqlStatement.GetGroupByFields();
            if (!string.IsNullOrEmpty(groupBy))
            {
                sql.Append($" GROUP BY {groupBy}");
            }

            var orderBy = sqlStatement.GetOrderByFields();
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql.Append($" ORDER BY {orderBy}");
            }

            var limit = sqlStatement.GetLimit();
            if (!string.IsNullOrEmpty(limit))
            {
                sql.Append($" LIMIT {limit}");
            }
            
            return ExecuteCommand(sql.ToString());
        }

        /// <summary>
        /// read the whole table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>The full table</returns>
        public SqliteDataReader SelectData(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }
            var sql = $"select * from {tableName}";
            return ExecuteCommand(sql);
        }


        /// <summary>
        /// query whether contain the field in the specific table
        /// </summary>
        /// <param name="tableName">string</param>
        /// <param name="field">SqlField</param>
        /// <returns></returns>
        public bool SelectData(string tableName, SqlField field)
        {
            if (string.IsNullOrEmpty(tableName) || field == null)
                return false;
            SqlStatement statement=new SqlStatement(tableName);
            statement.SelectFields("*").AddConditions(SqlOperation.Equal, SqlUnion.None, field);
            SqliteDataReader reader = SelectData(statement);
            var hasRows = reader.HasRows;
            CloseReader(reader);
            return hasRows;
        }

        /// <summary>
        /// Judge whether exist the table
        /// </summary>
        /// <param name="tableName">string</param>
        /// <returns></returns>
        public bool IsTableExist(string tableName)
        {
            var sql = $"SELECT name FROM sqlite_master WHERE type='table' and name='{tableName}'";
            SqliteDataReader reader = ExecuteCommand(sql);
            var hasRows = reader.HasRows;
            CloseReader(reader);
            return hasRows;
        }

        /// <summary>
        /// Judge whether exist the field in the specific table
        /// </summary>
        /// <param name="tableName">string</param>
        /// <param name="fieldName">string</param>
        /// <returns></returns>
        public bool IsFieldExist(string tableName, string fieldName)
        {
            var sql = $"SELECT * FROM sqlite_master WHERE name='{tableName}' and sql LIKE '%{fieldName}%'";
            SqliteDataReader reader = ExecuteCommand(sql);
            var hasRows = reader.HasRows;
            CloseReader(reader);
            return hasRows;
        }
        

        /// <summary>
        /// set value type to the specific type of SqLite database
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static string FormatValue(object value)
        {
            return $"'{value}'";
        }

        /// <summary>
        /// Execute sql command
        /// </summary>
        /// <param name="command">the string of sql</param>
        /// <returns></returns>
        SqliteDataReader ExecuteCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                return null;
            }

            Debug.Log(command);
            using (var dbCommand = _connection.CreateCommand())
            {
                dbCommand.CommandText = command;
                return dbCommand.ExecuteReader();
            }
        }

        /// <summary>
        /// close and dispose reader
        /// </summary>
        /// <param name="reader">SqliteDataReader</param>
        public void CloseReader(SqliteDataReader reader)
        {
            if (reader == null) return;
            reader.Close();
            reader.Dispose();
        }
        
        public void CloseDatabaseConnection()
        {
            if (_connection == null) return;
            _connection.Close();
            _connection.Dispose();
            _connection = null;
            Debug.Log("关闭数据库");
        }
    }
}