namespace Schema.Core.Helpers.Key
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Schema.Core.Keys;
    using Schema.Core.Models.Key;
    using Schema.Core.Names;
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

        public static List<KeyModel> GetPK<T>(IReader reader, DataSet dataSet, string tableName, T selectPk)
            where T : MsSqlQueries
        {
            var keyModel = new List<KeyModel>();
            var dataAdapter = reader.DataAdapter;
            dataAdapter.SelectCommand = reader.Command;
            dataAdapter.SelectCommand.Connection = reader.Conection;
            dataAdapter.SelectCommand.CommandText = selectPk.SelectPk;
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                keyModel.Add(
                    new MsSqlKeyModel
                        {
                            TableName = dt.Rows[i].ItemArray[0].ToString(),
                            ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                           /* Type = dt.Rows[i].ItemArray[2].ToString(),*/
                            Name = dt.Rows[i].ItemArray[3].ToString(),
                            TypeDescription =Converters.ConstraintType(dt.Rows[i].ItemArray[4])
                        });
            }

            return keyModel;
        }
    }
}