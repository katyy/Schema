using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.ProcedureFunction;
using Schema.Core.Models.ProcedureFunction.Column;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.ProcedureFunction
{
    public class MySqlProcedureFunctionGetter:IProcedureFunctionGetter
    {
        public List<T> GetProcedureFunction<T, TK>(IReader reader, DataSet dataSet, string tableName) 
            where T : IProcedureFunctionModel<TK>, new() 
            where TK : IProcedureFunctionColumnModel
        {
            return null;
        }


        public List<IProcedureFunctionModel<IProcedureFunctionColumnModel>> GetProcedureFunction(IReader reader, DataSet dataSet, string tableName)
        {
            throw new System.NotImplementedException();
        }

        public List<T> GetProcedureFunction<T>(IReader reader, DataSet dataSet, string tableName) where T : IProcedureFunctionModel<IProcedureFunctionColumnModel>
        {
            throw new System.NotImplementedException();
        }
    }
}
