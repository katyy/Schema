namespace Schema.Core.Helpers.View
{
    using System.Collections.Generic;
    using System.Data;
    using Schema.Core.Helpers.Column;
    using Schema.Core.Models.Column;
    using Schema.Core.Models.View;
    using Schema.Core.Models.View.Column;
    using Schema.Core.Reader;

    public class MySqlViewGetter<TK> : MySqlColumnGetter<MySqlViewColumnModel>, IViewGetter
                                        where TK : MySqlViewColumnModel, new()
    {
        public List<IViewModel> GetView(IReader reader, DataSet dataSet, string TableName) //todo union to columngetter
        {
            var columns = new List<IViewModel>();
            var dataAdapter = reader.DataAdapter;
            dataAdapter.SelectCommand = reader.Command;
            dataAdapter.SelectCommand.Connection = reader.Conection;
            dataAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectView;
            dataAdapter.Fill(dataSet, TableName);
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
                    columns.Add(new MySqlViewModel { Name = name, Columns = new List<IColumnModel>(column) });
                    column = new List<TK>();
                    column = AddColumn(column, i, dt);
                }
                if (i == dt.Rows.Count - 1)
                {
                    columns.Add(new MySqlViewModel { Name = name, Columns = new List<IColumnModel>(column) });
                }
                name = tableName;
            }

            return columns;

        }

        public List<TK> AddColumn(List<TK> column, int i, DataTable dt)
        {
            
            column.Add(new TK
               {
                  ColumnName = dt.Rows[i].ItemArray[1].ToString(),//c.COLUMN_NAME 
                  TypeName = dt.Rows[i].ItemArray[2].ToString(),    //,c.COLUMN_TYPE,
                  MaxLength = Converters.ToInt(dt.Rows[i].ItemArray[3]),//c.CHARACTER_MAXIMUM_LENGTH 
                  AllowNull = Converters.ToBool(dt.Rows[i].ItemArray[4]),// c.IS_NULLABLE,
                  IsIdentity = dt.Rows[i].ItemArray[5].ToString(),// c.EXTRA
                  IsUpdatable = Converters.ToBool(dt.Rows[i].ItemArray[6].ToString()),//`view`.IS_UPDATABLE
                  CollacationConnection = dt.Rows[i].ItemArray[7].ToString(),//`view`.SECURITY_TYPE
                  SecurityType = dt.Rows[i].ItemArray[8].ToString(),//`view`.COLLATION_CONNECTION

               });
            return column;
        }



    }
}
