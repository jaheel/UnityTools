using System;
using System.Text;

namespace SQLite
{
    public class SqlStatement
    {
        string _tableName;

        public string TableName => _tableName;

        StringBuilder _createFields=new StringBuilder();
        StringBuilder _selectFields=new StringBuilder();
        StringBuilder _updateFields=new StringBuilder();
        StringBuilder _conditions=new StringBuilder();
        StringBuilder _orderByFields=new StringBuilder();
        StringBuilder _groupByFields=new StringBuilder();
        string _limit;

        public SqlStatement(string tableName)
        {
            _tableName = tableName;
        }

        
        public SqlStatement CreateFields(params SqlField[] fields)
        {
            if (fields == null || fields.Length <= 0) return this;
            foreach (var t in fields)
            {
                if (!string.IsNullOrEmpty(_createFields.ToString()))
                {
                    _createFields.Append(",");
                }

                _createFields.Append($"{t.Name} {t.TypeForSqLite}");
                
                if (t.IsPrimaryKey)
                {
                    _createFields.Append(" PRIMARY KEY");
                }
            }

            return this;
        }

        /// <summary>
        /// get the string type of create fields
        /// </summary>
        /// <returns>string of create fields</returns>
        protected internal string GetCreateFields()
        {
            return _createFields.ToString();
        }
        
        public SqlStatement SelectFields(params string[] fieldNames)
        {
            if (fieldNames == null) return this;
            foreach (var t in fieldNames)
            {
                if (!string.IsNullOrEmpty(_selectFields.ToString()))
                {
                    _selectFields.Append(",");
                }

                _selectFields.Append(t);
            }

            return this;
        }

        /// <summary>
        /// select fields
        /// </summary>
        /// <param name="fields">SqlField</param>
        /// <returns></returns>
        public SqlStatement SelectFields(params SqlField[] fields)
        {
            if (fields == null) return this;
            foreach (var t in fields)
            {
                if (!string.IsNullOrEmpty(_selectFields.ToString()))
                {
                    _selectFields.Append(",");
                }

                _selectFields.Append(t.Name);
            }
            return this;
        }

        /// <summary>
        /// get the string of select fields
        /// </summary>
        /// <returns></returns>
        protected internal string GetSelectFields()
        {
            return _selectFields.ToString();
        }

        /// <summary>
        /// get the string set of select fields
        /// </summary>
        /// <returns></returns>
        protected internal string[] GetSelectFieldArray()
        {
            var fields = _selectFields.ToString();
            return !string.IsNullOrEmpty(fields) ? fields.Trim().Split(',') : null;
        }
        
        public SqlStatement UpdateFields(params SqlField[] fields)
        {
            if (fields == null || fields.Length <= 0) return this;

            foreach (var t in fields)
            {
                if (!string.IsNullOrEmpty(_updateFields.ToString()))
                {
                    _updateFields.Append(",");
                }

                _updateFields.Append($"{t.Name} = '{t.Value}'");
            }
            return this;
        }

        /// <summary>
        /// get the string of update fields
        /// </summary>
        /// <returns></returns>
        protected internal string GetUpdateFields()
        {
            return _updateFields.ToString();
        }

        /// <summary>
        /// add conditions sql statement
        /// </summary>
        /// <param name="operation">SqlOperation</param>
        /// <param name="union">SqlUnion</param>
        /// <param name="fields">SqlField</param>
        /// <returns></returns>
        public SqlStatement AddConditions(SqlOperation operation, SqlUnion union, params SqlField[] fields)
        {
            if (fields == null) return this;

            string unionStatement;
            switch (union)
            {
                case SqlUnion.Or:
                    unionStatement = " OR ";
                    break;
                case SqlUnion.And:
                    unionStatement = " AND ";
                    break;
                case SqlUnion.None:
                    unionStatement = "";
                    break;
                default:
                    unionStatement = "";
                    break;
            }

            string operateStatement;
            switch (operation)
            {
                case SqlOperation.Between:
                    operateStatement = " BETWEEN ";
                    break;
                
                case SqlOperation.Greater:
                    operateStatement = " > ";
                    break;
                case SqlOperation.Less:
                    operateStatement = " < ";
                    break;
                case SqlOperation.Like:
                    operateStatement = " LIKE ";
                    break;
                case SqlOperation.Unequal:
                    operateStatement = " <> ";
                    break;
                case SqlOperation.GreaterEqual:
                    operateStatement = " >= ";
                    break;
                case SqlOperation.LessEqual:
                    operateStatement = " <= ";
                    break;
                case SqlOperation.Equal:
                    operateStatement = " = ";
                    break;
                default:
                    operateStatement = " = ";
                    break;
            }

            foreach (var t in fields)
            {
                if (!string.IsNullOrEmpty(_conditions.ToString()))
                {
                    _conditions.Append(unionStatement);
                }

                _conditions.Append(t.Name);
                _conditions.Append(operateStatement);
                _conditions.Append(operation == SqlOperation.Like ? $"'%{t.Value}%'" : $"'{t.Value}'");
            }

            return this;
        }

        /// <summary>
        /// return the string of conditions
        /// </summary>
        /// <returns></returns>
        protected internal string GetConditions()
        {
            return _conditions.ToString();
        }

        /// <summary>
        /// add order by statement
        /// </summary>
        /// <param name="orderBy">SqlOrderBy</param>
        /// <param name="fields">SqlField</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public SqlStatement OrderByFields(SqlOrderBy orderBy, params SqlField[] fields)
        {
            if (fields == null) return this;

            foreach (var t in fields)
            {
                if (!string.IsNullOrEmpty(_orderByFields.ToString()))
                {
                    _orderByFields.Append(",");
                }

                _orderByFields.Append(t.Name);

                switch (orderBy)
                {
                    case SqlOrderBy.Desc:
                        _orderByFields.Append(" DESC");
                        break;
                    case SqlOrderBy.Asc:
                        _orderByFields.Append(" ASC");
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(orderBy), orderBy, null);
                }
            }

            return this;
        }

        /// <summary>
        /// return the string of order by fields
        /// </summary>
        /// <returns></returns>
        protected internal string GetOrderByFields()
        {
            return _orderByFields.ToString();
        }

        /// <summary>
        /// add group by statement
        /// </summary>
        /// <param name="fields">SqlField type</param>
        /// <returns></returns>
        public SqlStatement GroupByFields(params SqlField[] fields)
        {
            if (fields == null) return this;

            foreach (var t in fields)
            {
                if (!string.IsNullOrEmpty(_groupByFields.ToString()))
                {
                    _groupByFields.Append(",");
                }

                _groupByFields.Append(t.Name);
            }

            return this;
        }

        /// <summary>
        /// return the string of group by fields
        /// </summary>
        /// <returns></returns>
        protected internal string GetGroupByFields()
        {
            return _groupByFields.ToString();
        }

        /// <summary>
        /// set the offset and count of limit
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public SqlStatement Limit(int offset, int count)
        {
            _limit = $"{offset},{count}";
            return this;
        }

        /// <summary>
        /// set the limit count
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public SqlStatement Limit(int count)
        {
            _limit = Convert.ToString(count);
            return this;
        }

        /// <summary>
        /// return the limit string
        /// </summary>
        /// <returns></returns>
        protected internal string GetLimit()
        {
            return _limit;
        }
        
        
    }
}