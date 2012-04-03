using System.Collections.Generic;
using Schema.Core.Models.View;

namespace Schema.Core.Models
{
    public class DatabaseModel
    {
        public List<TableModel> Tables { get; set; }
        public List<IViewModel> Views { get; set; }
        public List<ProcedureModel> Procedures { get; set; }
        public List<ProcedureModel> Functions { get; set; }
    }
}
