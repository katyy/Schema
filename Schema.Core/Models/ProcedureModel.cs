using System.Collections.Generic;

namespace Schema.Core.Models
{
    public class ProcedureModel
    {
        public string Name { get; set; }
        public List<ProcedureColumnModel> ProcedureColumn { get; set; }
      
       
    }
}
