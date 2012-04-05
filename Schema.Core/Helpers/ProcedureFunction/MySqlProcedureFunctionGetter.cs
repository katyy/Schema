using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.ProcedureFunction;
using Schema.Core.Models.ProcedureFunction.Column;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.ProcedureFunction
{
    public class MySqlProcedureFunctionGetter : IProcedureFunctionGetter
    {
      


        public List<IProcedureFunctionModel<T>> GetProcedureFunction<T>(IReader reader, DataSet dataSet, string tableName) where T : IProcedureFunctionColumnModel
        {
            throw new System.NotImplementedException();
        }
    }
}
