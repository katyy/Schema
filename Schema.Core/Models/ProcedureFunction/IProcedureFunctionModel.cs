using System.Collections.Generic;
using Schema.Core.Models.ProcedureFunction.Column;

namespace Schema.Core.Models.ProcedureFunction
{
    public interface IProcedureFunctionModel<T> where T : IProcedureFunctionColumnModel
    {
        string Name { get; set; }
        List<T> ProcedureFunctionColumn { get; set; }
       
    }
   }