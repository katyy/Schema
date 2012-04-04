using System.Collections.Generic;
using Schema.Core.Models.ProcedureFunction.Column;

namespace Schema.Core.Models.ProcedureFunction
{
    public class MySqlProcedureFunctionModel:IProcedureFunctionModel<MySqlProcedureFunctionColumnModel>
    {
        public string Name { get; set; }
        public List<MySqlProcedureFunctionColumnModel> ProcedureFunctionColumn { get; set; }
    }
}
