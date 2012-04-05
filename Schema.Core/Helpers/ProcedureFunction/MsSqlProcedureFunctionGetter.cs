using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Schema.Core.Models.ProcedureFunction;
using Schema.Core.Models.ProcedureFunction.Column;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.ProcedureFunction
{
    public class MsSqlProcedureFunctionGetter : IProcedureFunctionGetter
    {
        public List<IProcedureFunctionModel<MsSqlProcedureFunctionColumnModel>> GetProcedureFunction(IReader reader, DataSet dataSet, string tableName) 
         {
            var procedures = new List<IProcedureFunctionModel<MsSqlProcedureFunctionColumnModel>>();
            var dAdapter = reader.DataAdapter;
            dAdapter.SelectCommand = new SqlCommand(reader.SqlQueries.SelectProcedure, new SqlConnection(reader.ConnectionString));
            dAdapter.Fill(dataSet, tableName);
            var columns = new List<MsSqlProcedureFunctionColumnModel>();
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
                    procedures.Add(new MsSqlProcedureFunctionModel { Name = name, ProcedureFunctionColumn = columns });
                    columns = new List<MsSqlProcedureFunctionColumnModel>();
                    columns = AddProcedureColumn(columns, i, dt);

                }
                if (i == dt.Rows.Count - 1)
                {
                    procedures.Add( new MsSqlProcedureFunctionModel { Name = name, ProcedureFunctionColumn = columns });
                }
                name = procedureName;
            }

          return  procedures;
        }


        private List<T> AddProcedureColumn<T>(List<T> columns, int i, DataTable dt) where T : MsSqlProcedureFunctionColumnModel, new()
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
