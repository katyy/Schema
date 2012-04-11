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
        public static List<KeyModel> GetKeys(IReader reader, DataSet dataSet, string tableName)
        {
            var forigenKey = GetForigenKey(reader, dataSet, tableName);
            var primaryKey = GetPK(reader, dataSet, TableNames.ForigenKey);
            return primaryKey.Union(forigenKey).ToList();
        }

        public static List<KeyModel> GetForigenKey(IReader reader, DataSet dataSet, string dataSetTableName)
        {
            CommonHelper.SetDataAdapterSettings(reader, reader.SqlQueries.SelectFk, dataSet, dataSetTableName);

            var dt = dataSet.Tables[dataSetTableName];
          
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

        public static List<KeyModel> GetPK(IReader reader, DataSet dataSet, string dataSetTableName)
        {
            var keyModel = new List<KeyModel>();
            CommonHelper.SetDataAdapterSettings(reader, reader.SqlQueries.SelectPk, dataSet, dataSetTableName);

            var dt = dataSet.Tables[dataSetTableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                keyModel.Add(
                    new KeyModel
                    {
                        TableName = dt.Rows[i].ItemArray[0].ToString(),
                        ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                        Name = dt.Rows[i].ItemArray[3].ToString(),
                        TypeDescription = Converters.ConstraintType(dt.Rows[i].ItemArray[4])
                    });
            }

            return keyModel;
        }
    }
}
