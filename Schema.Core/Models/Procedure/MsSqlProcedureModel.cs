using System.Collections.Generic;

namespace Schema.Core.Models.Procedure
{
   public class MsSqlProcedureModel :IProcedureModel
    {
       public string Name{get; set; }
       public List<ProcedureColumnModel> ProcedureFunctionColumn { get; set; }
    }
}
