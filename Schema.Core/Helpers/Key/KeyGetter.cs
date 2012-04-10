namespace Schema.Core.Helpers.Key
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Schema.Core.Models.Key;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class KeyGetter
    {
        public virtual List<KeyModel> GetKeys(IReader reader, DataSet dataSet, string tableName)
        {
            var dataAdapter = reader.DataAdapter;
            dataAdapter.SelectCommand = reader.Command;
            dataAdapter.SelectCommand.Connection = reader.Conection;
            dataAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectKey;
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            var keyModel = (from DataRow row in dt.Rows
                            select
                                new KeyModel
                                    {
                                        TableName = row[KeyNames.TableName].ToString(),
                                        ColumnName = row[KeyNames.ColumnName].ToString(),
                                        Name = row[KeyNames.KeyName].ToString(),
                                        TypeDescription = Converters.ConstraintType(row[KeyNames.TypeDescription]),
                                        DeletRule = Converters.UpdateDeleteRule(row[KeyNames.DeletRule]),
                                        UpdateRule = Converters.UpdateDeleteRule(row[KeyNames.UpdateRule]),
                                        ReferanceTable = row[KeyNames.ReferanceTable].ToString(),
                                        ReferanceColumn = row[KeyNames.ReferanceColumn].ToString()
                                    }).ToList();

            return keyModel;
        }
    }
}
