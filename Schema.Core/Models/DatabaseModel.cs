using System.Collections.Generic;

namespace Schema.Core.Models
{
    public class DatabaseModel
    {
        public List<TableModel> Tables { get; set; }
        public List<ViewModel> Views { get; set; }
        public List<ProcedureModel> Procedures { get; set; }
        public List<ProcedureModel> Functions { get; set; }
    }
}
