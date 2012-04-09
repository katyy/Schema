namespace Schema.Core.Helpers.Column
{
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using Schema.Core.Models.Column;
    using Schema.Core.Reader;

    public class MsSqlColumnGetter<TK> where TK : MsSqlColumnModel, new()
    {
        public Dictionary<string, List<TK>> GetColumn(IReader reader, DataSet dataSet, string dataSetTableName)
        {
            using (var dataAdapter = reader.DataAdapter)
            {
                dataAdapter.SelectCommand = reader.Command;
                dataAdapter.SelectCommand.Connection = reader.Conection;
                dataAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectColumn;
                dataAdapter.Fill(dataSet, dataSetTableName);
            }

            var dt = dataSet.Tables[dataSetTableName];
            var column = new List<TK>();
            var tables = new Dictionary<string, List<TK>>();
            
            foreach (DataRow row in dt.Rows)
            {
                var tableName = row[0].ToString();
                var isIdenty = row[5].ToString();
                
                if (!tables.ContainsKey(tableName))
                {
                    column = new List<TK>();
                }

                column.Add(
                    new TK
                        {
                            ColumnName = row[1].ToString(), 
                            TypeName = row[2].ToString(), 
                            MaxLength = Converters.ToInt(row[3]), 
                            AllowNull = Converters.ToBool(row[4]), 
                            IsIdenty =
                                string.IsNullOrEmpty(isIdenty) ? false.ToString(CultureInfo.InvariantCulture) : isIdenty, 
                            IdentyIncriment = Converters.ToInt(row[6])
                        });
                tables.Remove(tableName);
                tables.Add(tableName, column);
            }

            return tables;
        }
    }
}