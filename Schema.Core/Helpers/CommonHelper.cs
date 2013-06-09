namespace Schema.Core.Helpers
{
    using System.Data;

    using Schema.Core.Reader;

   public class CommonHelper
    {
        public static void SetDataAdapterSettings(ISqlReader sqlReader, string query, DataSet dataSet, string dataSetTableName)
        {
            using (var dataAdapter = sqlReader.DataAdapter)
            {
                dataAdapter.SelectCommand = sqlReader.Command;
                dataAdapter.SelectCommand.Connection = sqlReader.Conection;
                dataAdapter.SelectCommand.CommandText = query;
                dataAdapter.Fill(dataSet, dataSetTableName);
            }
        }
     }
}
