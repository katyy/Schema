namespace Schema.Core.Helpers.Procedure
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models.Procedure;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class ProcedureGetter
    {
        public static Dictionary<string, List<ParametrModel>> GetProcedure(IReader reader, DataSet dataSet, string query, string dataSetTableName)
        {
            using (var dataAdapter = reader.DataAdapter)
            {
                dataAdapter.SelectCommand = reader.Command;
                dataAdapter.SelectCommand.Connection = reader.Conection;
                dataAdapter.SelectCommand.CommandText = query;
                dataAdapter.Fill(dataSet, dataSetTableName);
            }

            var dt = dataSet.Tables[dataSetTableName];
            var parametr = new List<ParametrModel>();
            var procedure = new Dictionary<string, List<ParametrModel>>();

            foreach (DataRow row in dt.Rows)
            {
                var tableName = row[ProcedureNames.Name].ToString();
                var maxLength = Converters.ToInt(row[ProcedureNames.MaxLength]);
                var precesion = Converters.ToInt(row[ProcedureNames.Precision]);
                var scale = Converters.ToInt(row[ProcedureNames.Scale]);

                if (!procedure.ContainsKey(tableName))
                {
                    parametr = new List<ParametrModel>();
                }

                parametr.Add(
                    new ParametrModel
                    {
                        Parametr = row[ProcedureNames.Parametr].ToString(),
                        TypeDescription = row[ProcedureNames.TypeDescription].ToString(),
                        DataType = row[ProcedureNames.DataType].ToString(),
                        MaxLength = maxLength,
                        Precision = precesion,
                        Scale = scale
                    });

                procedure.Remove(tableName);
                procedure.Add(tableName, parametr);
            }

            return procedure;
        }
    }
}
