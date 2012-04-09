namespace Schema.Core.Helpers.Procedure
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Schema.Core.Models.Procedure;
    using Schema.Core.Reader;

    public class MsSqlProcedureGetter:IProcedureGetter
    {
       public List<IProcedureModel> GetProcedure(IReader reader, DataSet dataSet, string query, string tableName)
       {
           var procedures = new List<IProcedureModel>();
           var dAdapter = reader.DataAdapter;
           dAdapter.SelectCommand = new SqlCommand(query, new SqlConnection(reader.ConnectionString));
           dAdapter.Fill(dataSet, tableName);
           var columns = new List<ProcedureColumnModel>();
           var dt = dataSet.Tables[tableName];
           string name = null;
           for (var i = 0; i < dt.Rows.Count; i++)
           {
               var procedureName = dt.Rows[i].ItemArray[0].ToString();
               if (name == procedureName || name == null)
               {
                   columns = AddProcedureColumn(columns, i, dt);
               }
               else
               {
                   procedures.Add(new MsSqlProcedureModel { Name = name, ProcedureFunctionColumn = columns });
                   columns = new List<ProcedureColumnModel>();
                   columns = AddProcedureColumn(columns, i, dt);

               }
               if (i == dt.Rows.Count - 1)
               {
                   procedures.Add(new MsSqlProcedureModel { Name = name, ProcedureFunctionColumn = columns });
               }
               name = procedureName;
           }

           return procedures;
       }

       private List<T> AddProcedureColumn<T>(List<T> columns, int i, DataTable dt) where T : ProcedureColumnModel, new()
       {
           var maxLength = Converters.ToInt(dt.Rows[i].ItemArray[5]);
           var precesion = Converters.ToInt(dt.Rows[i].ItemArray[6]);
           var scale = Converters.ToInt(dt.Rows[i].ItemArray[7]);
           columns.Add(new T
           {
               ColumnName = dt.Rows[i].ItemArray[1].ToString(),
               Type = dt.Rows[i].ItemArray[2].ToString(),
               TypeDescription = dt.Rows[i].ItemArray[3].ToString(),
               DataType = dt.Rows[i].ItemArray[4].ToString(),
               MaxLength = maxLength,
               Precision = precesion,
               Scale = scale
           });
           return columns;
       }

       
    }
}
