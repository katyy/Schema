namespace Schema.Core.Helpers.Trigger
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models.Trigger;
    using Schema.Core.Reader;

    public class TriggerGetter 
    {
        public static List<TriggerModel> GetTriggers(IReader reader, DataSet dataSet, string dataSetTableName)
        {
            var trigerModel = new List<TriggerModel>();
            using (var dataAdapter = reader.DataAdapter)
            {
                dataAdapter.SelectCommand = reader.Command;
                dataAdapter.SelectCommand.Connection = reader.Conection;
                dataAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectTrigger;
                dataAdapter.Fill(dataSet, dataSetTableName);
            }

            var dt = dataSet.Tables[dataSetTableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                trigerModel.Add(new TriggerModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    TrigerName = dt.Rows[i].ItemArray[1].ToString(),
                    Event = Converters.TriggerEventManipulation(dt.Rows[i].ItemArray[2]),
                });
            }

            return trigerModel;
        }
    }
}
