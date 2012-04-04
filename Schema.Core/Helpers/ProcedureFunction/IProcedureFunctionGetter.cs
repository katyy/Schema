using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.ProcedureFunction;
using Schema.Core.Models.ProcedureFunction.Column;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.ProcedureFunction
{
    public interface IProcedureFunctionGetter
    {
        List<IProcedureFunctionModel<IProcedureFunctionColumnModel>> GetProcedureFunction(IReader reader,
                                                                                          DataSet dataSet,
                                                                                          string tableName);


    }
}
