namespace Schema.Core.Helpers.Column
{
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;

    using Schema.Core.Models.Column;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class ColumnGetter<TK> where TK : ColumnModel, new()
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
                var tableName = row[ColumnKeys.TableName].ToString();
                var isIdentity = row[ColumnKeys.IsIdentity].ToString();
                
                if (!tables.ContainsKey(tableName))
                {
                    column = new List<TK>();
                }

                column.Add(
                    new TK
                        {
                            ColumnName = row[ColumnKeys.ColumnName].ToString(), 
                            TypeName = row[ColumnKeys.TypeName].ToString(), 
                            MaxLength = Converters.ToInt(row[ColumnKeys.MaxLength]), 
                            AllowNull = Converters.ToBool(row[ColumnKeys.AllowNull]), 
                            IsIdentity =
                                string.IsNullOrEmpty(isIdentity) ? false.ToString(CultureInfo.InvariantCulture) : isIdentity, 
                           /* IdentityIncriment = Converters.ToInt(row[ColumnKeys.IdentityIncriment])*/
                        });

                tables.Remove(tableName);
                tables.Add(tableName, column);
            }

            return tables;
        }
    }
}