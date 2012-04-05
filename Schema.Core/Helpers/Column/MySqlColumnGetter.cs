using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Schema.Core.Helpers.View;
using Schema.Core.Models.Column;
using Schema.Core.Models.Table;
using Schema.Core.Models.View;
using Schema.Core.Models.View.Column;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.Column
{
    public class MySqlColumnGetter<TK> : IColumnGetter where TK : MySqlColumnModel, new()
    {

        public List<T> GetColumn<T>(IReader reader, DataSet dataSet, List<T> columns, string TableName) where T : ITable, new()
        {
            var dAdapter = reader.DataAdapter;
            dAdapter.SelectCommand = reader.Command;
            dAdapter.SelectCommand.Connection = reader.Conection;
            dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectColumn;
            dAdapter.Fill(dataSet, TableName);
            var column = new List<TK>();
            var dt = dataSet.Tables[TableName];
            string name = null;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var tableName = dt.Rows[i].ItemArray[0].ToString();
                if (name == tableName || name == null)
                {
                    column = AddColumn(column, i, dt);
                }
                else
                {
                    columns.Add(new T { Name = name, Columns = new List<IColumnModel>(column) });
                    column = new List<TK>();
                    column = AddColumn(column, i, dt);
                }
                if (i == dt.Rows.Count - 1)
                {
                    columns.Add(new T { Name = name, Columns = new List<IColumnModel>(column) });
                }
                name = tableName;
            }

            return columns;
        }

        public virtual List<TK> AddColumn(List<TK> column, int i, DataTable dt)
        {
            var isIdenty = dt.Rows[i].ItemArray[5].ToString();

            column.Add(new TK
            {
                ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                TypeName = dt.Rows[i].ItemArray[2].ToString(),
                MaxLength = Converters.ToInt(dt.Rows[i].ItemArray[3]),
                AllowNull = Converters.ToBool(dt.Rows[i].ItemArray[4]),
                IsIdenty = string.IsNullOrEmpty(isIdenty) ? false.ToString(CultureInfo.InvariantCulture) : isIdenty,
            });
            return column;
        }

       

        
    }
}
