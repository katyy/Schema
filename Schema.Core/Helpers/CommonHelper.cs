namespace Schema.Core.Helpers
{
    using System.Data;

    using Schema.Core.Reader;

   public class CommonHelper
    {
        public static void SetDataAdapterSettings(IReader reader, string query, DataSet dataSet, string dataSetTableName)
        {
            using (var dataAdapter = reader.DataAdapter)
            {
                dataAdapter.SelectCommand = reader.Command;
                dataAdapter.SelectCommand.Connection = reader.Conection;
                dataAdapter.SelectCommand.CommandText = query;
                dataAdapter.Fill(dataSet, dataSetTableName);
            }
        }
     }
}
