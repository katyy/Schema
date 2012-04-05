using System.Collections.Generic;
using Schema.Core.Models.Column;
using Schema.Core.Models.Table;
using Schema.Core.Models.Trigger;

namespace Schema.Core.Models.View
{
    public class MsSqlViewModel : ITable, IViewModel
    {
        public string Name { get; set; }
        public List<IColumnModel> Columns { get; set; }
        public List<ITriggerModel> Trigers { get; set; }
        public List<IndexModel> Indexes { get; set; }
    }
}
