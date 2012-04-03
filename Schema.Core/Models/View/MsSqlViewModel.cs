using System.Collections.Generic;

namespace Schema.Core.Models.View
{
    class MsSqlViewModel : ITable, IViewModel
    {
        public string Name { get; set; }
        public List<ColumnModel> Columns { get; set; }
        public List<TriggerModel> Trigers { get; set; }
        public List<IndexModel> Indexes { get; set; }
    }
}
