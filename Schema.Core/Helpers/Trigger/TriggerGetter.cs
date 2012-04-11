namespace Schema.Core.Helpers.Trigger
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models.Trigger;
    using Schema.Core.Reader;

    public class TriggerGetter 
    {
        public List<TriggerModel> GetTriggers(IReader reader, DataSet dataSet, string tableName)
        {
            var trigerModel = new List<TriggerModel>();
            var dataAdapter = reader.DataAdapter;
            dataAdapter.SelectCommand = reader.Command;
            dataAdapter.SelectCommand.Connection = reader.Conection;
            dataAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectTrigger;
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                trigerModel.Add(new TriggerModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    TrigerName = dt.Rows[i].ItemArray[1].ToString(),
                    Event = Converters.TriggerEventManipulation(dt.Rows[i].ItemArray[2]),
                    /*ActionOrientation = dt.Rows[i].ItemArray[3].ToString(),
                    ActionTiming = dt.Rows[i].ItemArray[4].ToString(),
                    ActionStatement = dt.Rows[i].ItemArray[5].ToString()*/
                });
            }

            return trigerModel;
        }
    }
}
