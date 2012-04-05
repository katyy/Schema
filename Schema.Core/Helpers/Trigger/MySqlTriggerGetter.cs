using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.Trigger;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.Trigger
{
    public class MySqlTriggerGetter:ITriggerGetter
    {
        public List<ITriggerModel> GetTriggers(IReader reader, DataSet dataSet, string tableName)
        {
            var trigerModel = new List<ITriggerModel>();
            var dAdapter = reader.DataAdapter;
            dAdapter.SelectCommand = reader.Command;
            dAdapter.SelectCommand.Connection = reader.Conection;
            dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectTrigger;
            dAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                trigerModel.Add(new MySqlTriggerModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    TrigerName = dt.Rows[i].ItemArray[1].ToString(),
                    Event = dt.Rows[i].ItemArray[2].ToString(),
                    ActionOrientation = dt.Rows[i].ItemArray[3].ToString(),
                    ActionTiming = dt.Rows[i].ItemArray[4].ToString(),
                    ActionStatement = dt.Rows[i].ItemArray[5].ToString()
                });
            }
            return trigerModel;
        }
    }
}
