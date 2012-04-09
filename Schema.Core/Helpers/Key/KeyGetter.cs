namespace Schema.Core.Helpers.Key
{
    using System.Collections.Generic;
    using System.Data;
    using Schema.Core.Models.Key;
    using Schema.Core.Reader;

    public class KeyGetter
    {
        public virtual List<KeyModel> GetKeys(IReader reader, DataSet dataSet, string tableName)
        {
            var keyModel = new List<KeyModel>();
            var dataAdapter = reader.DataAdapter;
            dataAdapter.SelectCommand = reader.Command;
            dataAdapter.SelectCommand.Connection = reader.Conection;
            dataAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectKey;
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                keyModel.Add(new KeyModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                    Type = dt.Rows[i].ItemArray[2].ToString(),
                    Name = dt.Rows[i].ItemArray[3].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[4].ToString(),
                    DeletRule = dt.Rows[i].ItemArray[5].ToString(),
                    UpdateRule = dt.Rows[i].ItemArray[6].ToString(),
                    ReferanceTable = dt.Rows[i].ItemArray[7].ToString(),
                    ReferanceColumn = dt.Rows[i].ItemArray[8].ToString()
                });
            }
            return keyModel;
        }
    }
}
