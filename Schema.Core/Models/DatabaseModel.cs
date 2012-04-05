using System.Collections.Generic;
using Schema.Core.Models.Procedure;
using Schema.Core.Models.View;

namespace Schema.Core.Models
{
    public class DatabaseModel
    {
        public List<TableModel> Tables { get; set; }
        public List<IViewModel> Views { get; set; }
        public List<IProcedureModel>Procedures { get; set; }
        public List<IProcedureModel> Functions { get; set; }
    }
}
