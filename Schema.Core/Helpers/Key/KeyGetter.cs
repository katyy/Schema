namespace Schema.Core.Helpers.Key
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models.Key;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class KeyGetter
    {
        public static Dictionary<string, List<KeyModel>> GetKeys(IReader reader, DataSet dataSet, string tableName)
        {
            var foriegenKey = GetForigenKey(reader, dataSet, tableName);
            var primaryKey = GetPK(reader, dataSet, TableNames.ForigenKey);
            foreach (var pk in primaryKey)
            {
                if (!foriegenKey.ContainsKey(pk.Key))
                {
                    foriegenKey.Add(pk.Key, pk.Value);
                }
                else
                {
                    var value = foriegenKey[pk.Key];
                    var primaryValue = pk.Value;
                    value.AddRange(primaryValue);
                    foriegenKey.Remove(pk.Key);
                    foriegenKey.Add(pk.Key, value);
                }
            }

            return foriegenKey;
        }

        public static Dictionary<string, List<KeyModel>> GetForigenKey(IReader reader, DataSet dataSet, string dataSetTableName)
        {
            CommonHelper.SetDataAdapterSettings(reader, reader.SqlQueries.SelectFk, dataSet, dataSetTableName);

            var dt = dataSet.Tables[dataSetTableName];

            var keyColumn = new List<KeyModel>();
            var keyDictionary = new Dictionary<string, List<KeyModel>>();
            foreach (DataRow row in dt.Rows)
            {
                var name = row[KeyNames.TableName].ToString();
                if (!keyDictionary.ContainsKey(name))
                {
                    keyColumn = new List<KeyModel>();
                }

                keyColumn.Add(
                    new KeyModel
                    {
                        ColumnName = row[KeyNames.ColumnName].ToString(),
                        Name = row[KeyNames.KeyName].ToString(),
                        TypeDescription = Converters.ConstraintType(row[KeyNames.TypeDescription]),
                        DeletRule = Converters.UpdateDeleteRule(row[KeyNames.DeletRule]),
                        UpdateRule = Converters.UpdateDeleteRule(row[KeyNames.UpdateRule]),
                        ReferanceTable = row[KeyNames.ReferanceTable].ToString(),
                        ReferanceColumn = row[KeyNames.ReferanceColumn].ToString()
                    });

                keyDictionary.Remove(name);
                keyDictionary.Add(name, keyColumn);
            }

            return keyDictionary;
        }

        public static Dictionary<string, List<KeyModel>> GetPK(IReader reader, DataSet dataSet, string dataSetTableName)
        {
            CommonHelper.SetDataAdapterSettings(reader, reader.SqlQueries.SelectPk, dataSet, dataSetTableName);

            var dt = dataSet.Tables[dataSetTableName];

            var keyColumn = new List<KeyModel>();
            var keyDictionary = new Dictionary<string, List<KeyModel>>();
            foreach (DataRow row in dt.Rows)
            {
                var name = row[KeyNames.TableName].ToString();
                if (!keyDictionary.ContainsKey(name))
                {
                    keyColumn = new List<KeyModel>();
                }

                keyColumn.Add(
                    new KeyModel
                    {
                        ColumnName = row[KeyNames.ColumnName].ToString(),
                        Name = row[KeyNames.KeyName].ToString(),
                        TypeDescription = Converters.ConstraintType(row[KeyNames.TypeDescription]),
                    });

                keyDictionary.Remove(name);
                keyDictionary.Add(name, keyColumn);
            }

            return keyDictionary;
        }
    }
}
