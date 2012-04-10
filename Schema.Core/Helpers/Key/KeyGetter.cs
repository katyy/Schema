namespace Schema.Core.Helpers.Key
{
    using System.Collections.Generic;
    using System.Data;
    using Schema.Core.Models.Key;
    using Schema.Core.Names;
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
                    TableName = dt.Rows[i][KeyNames.TableName].ToString(),
                    ColumnName = dt.Rows[i][KeyNames.ColumnName].ToString(),
                    Type = dt.Rows[i][KeyNames.Type].ToString(),
                    Name = dt.Rows[i][KeyNames.KeyName].ToString(),
                    TypeDescription = dt.Rows[i][KeyNames.TypeDescription].ToString(),
                    DeletRule = dt.Rows[i][KeyNames.DeletRule].ToString(),
                    UpdateRule = dt.Rows[i][KeyNames.UpdateRule].ToString(),
                    ReferanceTable = dt.Rows[i][KeyNames.ReferanceTable].ToString(),
                    ReferanceColumn = dt.Rows[i][KeyNames.ReferanceColumn].ToString()
                });
            }

            return keyModel;
        }
    }
}
