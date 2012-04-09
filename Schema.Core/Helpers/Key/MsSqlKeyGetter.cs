namespace Schema.Core.Helpers.Key
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Schema.Core.Models.Key;
    using Schema.Core.Reader;
    using Schema.Core.SqlQueries;

    public class MsSqlKeyGetter : KeyGetter
    {
        public override List<KeyModel> GetKeys(IReader reader, DataSet dataSet, string tableName)
        {
            var fkModel = new KeyGetter().GetKeys(reader, dataSet, tableName);
            var pkModel = GetPK(reader, dataSet, TableNames.ForigenKey, (MsSqlQueries)reader.SqlQueries);
            return pkModel.Union(fkModel).ToList();
        }

        public static List<KeyModel> GetPK<T>(IReader reader, DataSet dataSet, string tableName, T selectPk)where T : MsSqlQueries
        {
            var keyModel = new List<KeyModel>();
            var dAdapter = reader.DataAdapter;
            dAdapter.SelectCommand = reader.Command;
            dAdapter.SelectCommand.Connection = reader.Conection;
            dAdapter.SelectCommand.CommandText = selectPk.SelectPk;
            dAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                keyModel.Add(new MsSqlKeyModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                    Type = dt.Rows[i].ItemArray[2].ToString(),
                    Name = dt.Rows[i].ItemArray[3].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[4].ToString()
                });
            }

            return keyModel;
        }

     }
}
