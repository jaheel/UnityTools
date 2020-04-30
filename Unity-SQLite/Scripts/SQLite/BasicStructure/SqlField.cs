using System;

namespace SQLite
{
    public class SqlField
    {
        string _name;
        public string Name => _name;

        string _value;

        public string Value => _value;

        bool _isPrimaryKey;

        public bool IsPrimaryKey => _isPrimaryKey;

        SqlFieldType _type = SqlFieldType.Text;

        public SqlFieldType Type => _type;
        
        protected internal string TypeForSqLite { get; private set; }

        public SqlField(string name, bool isPrimaryKey)
        {
            _name = name;
            _isPrimaryKey = isPrimaryKey;
        }

        public SqlField(string name)
        {
            _name = name;
        }
        public SqlField SetValue(string value)
        {
            _value = value;
            return this;
        }

        public SqlField SetType(SqlFieldType type)
        {
            _type = type;
            switch (type)
            {
                case SqlFieldType.Binary:
                    TypeForSqLite = "BLOB";
                    break;
                case SqlFieldType.Float:
                    TypeForSqLite = "REAL";
                    break;
                case SqlFieldType.Int:
                    TypeForSqLite = "INTEGER";
                    break;
                case SqlFieldType.Long:
                    TypeForSqLite = "BIGINT";
                    break;
                case SqlFieldType.Null:
                    TypeForSqLite = "NULL";
                    break;
                case SqlFieldType.Text:
                    TypeForSqLite = "TEXT";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return this;
        }

        public SqlField SetPrimaryKey(bool isPrimaryKey)
        {
            _isPrimaryKey = isPrimaryKey;
            return this;
        }
    }
}