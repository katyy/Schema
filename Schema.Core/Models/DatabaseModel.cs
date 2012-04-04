using System.Collections.Generic;
using Schema.Core.Models.ProcedureFunction;
using Schema.Core.Models.ProcedureFunction.Column;
using Schema.Core.Models.View;

namespace Schema.Core.Models
{
    public class DatabaseModel
    {
        public List<TableModel> Tables { get; set; }
        public List<IViewModel> Views { get; set; }
        public List<IProcedureFunctionModel<IProcedureFunctionColumnModel>>Procedures { get; set; }
        public List<IProcedureFunctionModel<IProcedureFunctionColumnModel>> Functions { get; set; }
    }
}
