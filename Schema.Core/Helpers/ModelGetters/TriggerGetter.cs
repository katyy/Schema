namespace Schema.Core.Helpers.ModelGetters
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class TriggerGetter
    {
        public static Dictionary<string, List<TriggerModel>> GetTriggers(ISqlReader sqlReader, DataSet dataSet, string dataSetTableName)
        {
            if (string.IsNullOrEmpty(sqlReader.SqlQueries.SelectTrigger))
            {
                return new Dictionary<string, List<TriggerModel>>() ;
            }

            CommonHelper.SetDataAdapterSettings(sqlReader, sqlReader.SqlQueries.SelectTrigger, dataSet, dataSetTableName);

            var dt = dataSet.Tables[dataSetTableName];

            var triggerModels = new List<TriggerModel>();
            var triggers = new Dictionary<string, List<TriggerModel>>();
            foreach (DataRow row in dt.Rows)
            {
                var name = row[TriggerNames.TableName].ToString();
                if (!triggers.ContainsKey(name))
                {
                    triggerModels = new List<TriggerModel>();
                }

                triggerModels.Add(
                    new TriggerModel
                    {
                        TrigerName = row[TriggerNames.TriggerName].ToString(),
                        Event = Converters.TriggerEventManipulation(row[TriggerNames.TriggerEvent]),
                    });

                triggers.Remove(name);
                triggers.Add(name, triggerModels);
            }

            return triggers;
        }
    }
}
