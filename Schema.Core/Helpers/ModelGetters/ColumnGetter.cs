namespace Schema.Core.Helpers.ModelGetters
{
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;

    using Schema.Core.Models;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class ColumnGetter<TK> where TK : ColumnModel, new()
    {
        public static Dictionary<string, List<TK>> GetColumn(IReader reader, DataSet dataSet, string dataSetTableName)
        {
            CommonHelper.SetDataAdapterSettings(reader, reader.SqlQueries.SelectColumn, dataSet, dataSetTableName);

            var dt = dataSet.Tables[dataSetTableName];
            var column = new List<TK>();
            var tables = new Dictionary<string, List<TK>>();
            
            foreach (DataRow row in dt.Rows)
            {
                var tableName = row[ColumnNames.TableName].ToString();
                var isIdentity = row[ColumnNames.IsIdentity].ToString();
                
                if (!tables.ContainsKey(tableName))
                {
                    column = new List<TK>();
                }

                column.Add(
                    new TK
                        {
                            ColumnName = row[ColumnNames.ColumnName].ToString(), 
                            TypeName = row[ColumnNames.TypeName].ToString(), 
                            MaxLength = Converters.ToInt(row[ColumnNames.MaxLength]), 
                            AllowNull = Converters.ToBool(row[ColumnNames.AllowNull]), 
                            IsIdentity =
                                string.IsNullOrEmpty(isIdentity) ? false.ToString(CultureInfo.InvariantCulture) : isIdentity, 
                           });

                tables.Remove(tableName);
                tables.Add(tableName, column);
            }

            return tables;
        }
    }
}