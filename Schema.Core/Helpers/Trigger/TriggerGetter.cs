namespace Schema.Core.Helpers.Trigger
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models.Trigger;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class TriggerGetter
    {
        public static Dictionary<string, List<TriggerModel>> GetTriggers(IReader reader, DataSet dataSet, string dataSetTableName)
        {

            CommonHelper.SetDataAdapterSettings(reader, reader.SqlQueries.SelectTrigger, dataSet, dataSetTableName);

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

            // for (var i = 0; i < dt.Rows.Count; i++)
            // {
            //    trigerModel.Add(new TriggerModel
            //    {
            //        TableName = dt.Rows[i].ItemArray[0].ToString(),
            //        TrigerName = dt.Rows[i].ItemArray[1].ToString(),
            //        Event = Converters.TriggerEventManipulation(dt.Rows[i].ItemArray[2]),
            //    });
            // }
            // return trigerModel;
        }
    }
}
