using System.Collections.Generic;
using UnityEngine;

namespace SQLite
{
    public class SqlTable
    {
        string _name;
        public string Name => _name;
        
        public List<SqlField> TableFields;

        public SqlTable(string name,IEnumerable<string> fields)
        {
            _name = name;
            TableFields=new List<SqlField>();
            
            foreach (var t in fields)
            {
                SqlField temp=new SqlField(t);
                TableFields.Add(temp);
            }
        }

        public SqlTable(string[] tableStrings)
        {
            if (tableStrings == null || tableStrings.Length <= 1)
                return;

            TableFields=new List<SqlField>();
            _name = tableStrings[0];
            for (int i = 1; i < tableStrings.Length; i++)
            {
                SqlField temp=new SqlField(tableStrings[i]);
                TableFields.Add(temp);
            }
        }

        public SqlTable(string name, List<SqlField> tableFields)
        {
            TableFields = tableFields;
            _name = name;
        }
        
        
    }
}