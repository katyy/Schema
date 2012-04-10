namespace Schema.Core.Helpers.Trigger
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Schema.Core.Models.Trigger;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class MsSqlTriggerGetter : ITriggerGetter
    {
        public List<ITriggerModel> GetTriggers(IReader reader, DataSet dataSet, string tableName)
        {
            var dataAdapter = reader.DataAdapter;
            dataAdapter.SelectCommand = reader.Command;
            dataAdapter.SelectCommand.Connection = reader.Conection;
            dataAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectTrigger;
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];

            return (from DataRow row in dt.Rows
                    select
                        new MsSqlTriggerModel
                            {
                                TableName = row[TriggerNames.TableName].ToString(),
                                TrigerName = row[TriggerNames.TriggerName].ToString(),
                                Event = Converters.TriggerEventManipulation(TriggerNames.TriggerEvent),
                                /* Type = dt.Rows[i].ItemArray[3].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[4].ToString()*/
                            }).Cast<ITriggerModel>().ToList();
        }
    }
}
