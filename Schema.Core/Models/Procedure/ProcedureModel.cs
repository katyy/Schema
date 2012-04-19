namespace Schema.Core.Models.Procedure
{
    using System.Collections.Generic;

    public class ProcedureModel 
    {
        public string Name { get; set; }

        public List<ParametrModel> ProcedureFunctionColumn { get; set; }
    }
}
