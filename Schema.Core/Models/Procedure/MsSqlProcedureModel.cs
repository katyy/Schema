namespace Schema.Core.Models.Procedure
{
    using System.Collections.Generic;

    public class MsSqlProcedureModel : IProcedureModel
    {
        public string Name { get; set; }

        public List<ProcedureColumnModel> ProcedureFunctionColumn { get; set; }
    }
}
