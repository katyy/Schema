namespace Schema.Core.Helpers.Key
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Schema.Core.Models.Key;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class KeyGetter
    {
        public virtual List<KeyModel> GetForigenKey(IReader reader, DataSet dataSet, string tableName)
        {
            var dataAdapter = reader.DataAdapter;
            dataAdapter.SelectCommand = reader.Command;
            dataAdapter.SelectCommand.Connection = reader.Conection;
            dataAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectKey;
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            var keyModel = (from DataRow row in dt.Rows
                            select
                                new KeyModel
                                    {
                                        TableName = row[KeyNames.TableName].ToString(),
                                        ColumnName = row[KeyNames.ColumnName].ToString(),
                                        Name = row[KeyNames.KeyName].ToString(),
                                        TypeDescription = Converters.ConstraintType(row[KeyNames.TypeDescription]),
                                        DeletRule = Converters.UpdateDeleteRule(row[KeyNames.DeletRule]),
                                        UpdateRule = Converters.UpdateDeleteRule(row[KeyNames.UpdateRule]),
                                        ReferanceTable = row[KeyNames.ReferanceTable].ToString(),
                                        ReferanceColumn = row[KeyNames.ReferanceColumn].ToString()
                                    }).ToList();

            return keyModel;
        }


        public  List<KeyModel> GetKeys(IReader reader, DataSet dataSet, string tableName)
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
                    new KeyModel
                    {
                        TableName = dt.Rows[i].ItemArray[0].ToString(),
                        ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                        /* Type = dt.Rows[i].ItemArray[2].ToString(),*/
                        Name = dt.Rows[i].ItemArray[3].ToString(),
                        TypeDescription = Converters.ConstraintType(dt.Rows[i].ItemArray[4])
                    });
            }

            return keyModel;
        }
    }
}
