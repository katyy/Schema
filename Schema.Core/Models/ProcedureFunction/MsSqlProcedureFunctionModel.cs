using System.Collections.Generic;
using Schema.Core.Models.ProcedureFunction.Column;

namespace Schema.Core.Models.ProcedureFunction
{
   public class MsSqlProcedureFunctionModel:IProcedureFunctionModel<MsSqlProcedureFunctionColumnModel>
   {
       public  string Name { get; set; }
       public  List<MsSqlProcedureFunctionColumnModel> ProcedureFunctionColumn { get; set; }
    }
}
