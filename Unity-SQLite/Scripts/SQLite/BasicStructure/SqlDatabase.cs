using System.Collections.Generic;

namespace SQLite
{
    public class SqlDatabase
    {
        string _name;

        public string Name => _name;

        public Dictionary<string,SqlTable> TableList;

        public SqlDatabase(string name,IEnumerable<SqlTable> tableList)
        {
            _name = name;
            TableList=new Dictionary<string, SqlTable>();
            foreach (var t in tableList)
            {
                TableList.Add(t.Name,t);
            }
        }

        public SqlTable SelectSqlTable(string name)
        {
            return TableList[name];
        }
        
    }
}