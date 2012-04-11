namespace Schema.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Schema.Core.Models;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class IndexGetter
    {
        public static List<IndexModel> GetIndexes(IReader reader, DataSet dataSet, string dataSetTableName)
        {
            CommonHelper.SetDataAdapterSettings(reader, reader.SqlQueries.SelectIndex, dataSet, dataSetTableName);

            var dt = dataSet.Tables[dataSetTableName];

            return (from DataRow row in dt.Rows
                    select new IndexModel
                    {
                        TableName = row[IndexNames.TableName].ToString(),
                        ColumnName = row[IndexNames.ColumnName].ToString(),
                        Name = row[IndexNames.IndexName].ToString(),
                        TypeDescription = Converters.IndexTypeDescription(row[IndexNames.IndexType]),
                        IsUnique = Convert.ToBoolean(row[IndexNames.Unique]),
                        IsDescending = Converters.OrderDirection(row[IndexNames.SortOrder])
                    }).ToList();
        }
     }
}
