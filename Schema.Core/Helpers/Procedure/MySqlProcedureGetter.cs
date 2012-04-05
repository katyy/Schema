using System;
using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.Procedure;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.Procedure
{
    public class MySqlProcedureGetter : IProcedureGetter
    {
        public List<IProcedureModel> GetProcedure(IReader reader, DataSet dataSet, string query, string tableName)
        {
            var procedure = new List<IProcedureModel>();
            var dAdapter = reader.DataAdapter;
            dAdapter.SelectCommand = reader.Command;
            dAdapter.SelectCommand.Connection = reader.Conection;
            dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectView;
            dAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                procedure.Add(new MySqlProcedureModel
                {
                    Name = dt.Rows[i].ItemArray[0].ToString(),

                    ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[2].ToString(),
                    DtdIndefier = dt.Rows[i].ItemArray[3].ToString(),
                    Body = dt.Rows[i].ItemArray[4].ToString(),
                    Definition = dt.Rows[i].ItemArray[5].ToString(),
                    IsDeterministic = Converters.ToBool(dt.Rows[i].ItemArray[6])

                });
            }
            return procedure;
        }
    }
}
