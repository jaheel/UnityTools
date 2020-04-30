using System.Collections.Generic;

namespace SQLite
{
    
    public class TestDatabase:AbstractDatabase
    {
        const string _DATABASE_NAME = "test";
        const string _DATABASE_PATH = "/Datas/SQLiteData/";
        SqlDatabase _database;
        SqlField[] _tableSqlFields;
        
        public TestDatabase(string tableName)
        { 
            Initiate();
            TableName = tableName;
        }

        private protected sealed override SqlDatabase InitiateSqlDatabase()
        {
            return new SqlDatabase(_DATABASE_NAME,InitiateSqlTable());
        }

        private protected sealed override List<SqlTable> InitiateSqlTable()
        {
            List<SqlTable> resultSqlTables = new List<SqlTable> {InitiateTableTest_t()};
            return resultSqlTables;
        }
        
        void Initiate()
        {
            _database = InitiateSqlDatabase();
        }
        
        public override string GetTableName()
        {
            return TableName;
        }

        public override string GetDatabasePath()
        {
            return _DATABASE_PATH;
        }

        public override List<SqlField> GetAllFields()
        {
            return _database.SelectSqlTable(TableName).TableFields;
        }

        public override string GetDatabaseName()
        {
            return _DATABASE_NAME;
        }
        
        #region tableList

        SqlTable InitiateTableTest_t()
        {
            string tableName = "test_t";
            
            SqlField id=new SqlField("_id").SetType(SqlFieldType.Int);
            SqlField name=new SqlField("_name").SetType(SqlFieldType.Text);
            SqlField data=new SqlField("_data").SetType(SqlFieldType.Text);
            SqlField floatField=new SqlField("_floatField").SetType(SqlFieldType.Float);
            SqlField blobField=new SqlField("_blobField").SetType(SqlFieldType.Binary);

            List<SqlField> result = new List<SqlField>
            {
                id,
                name,
                data,
                floatField,
                blobField
            };
            
            return new SqlTable(tableName,result);
        }
        
        
        #endregion
        
    }
    
    
}