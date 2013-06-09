namespace Schema.Core.Helpers.ModelGetters
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class IndexGetter
    {
        public static Dictionary<string, List<IndexModel>> GetIndexes(ISqlReader sqlReader, DataSet dataSet, string dataSetTableName)
        {
            if (string.IsNullOrEmpty(sqlReader.SqlQueries.SelectIndex))
            {
                return new Dictionary<string, List<IndexModel>>();
            }

            CommonHelper.SetDataAdapterSettings(sqlReader, sqlReader.SqlQueries.SelectIndex, dataSet, dataSetTableName);

            var dt = dataSet.Tables[dataSetTableName];


            var indexModels = new List<IndexModel>();
            var indexes = new Dictionary<string, List<IndexModel>>();
            foreach (DataRow row in dt.Rows)
            {
                var name = row[IndexNames.TableName].ToString();
                if (!indexes.ContainsKey(name))
                {
                    indexModels = new List<IndexModel>();
                }

                indexModels.Add(
                    new IndexModel
                        {
                            ColumnName = row[IndexNames.ColumnName].ToString(),
                            Name = row[IndexNames.IndexName].ToString(),
                            TypeDescription = Converters.IndexTypeDescription(row[IndexNames.IndexType]),
                            IsUnique = Convert.ToBoolean(row[IndexNames.Unique]),
                            IsDescending = Converters.OrderDirection(row[IndexNames.SortOrder])
                        });

                indexes.Remove(name);
                indexes.Add(name, indexModels);
            }

            return indexes;
        }
    }
}
