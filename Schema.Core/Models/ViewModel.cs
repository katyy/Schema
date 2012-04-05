using System.Collections.Generic;
using Schema.Core.Models.Trigger;

namespace Schema.Core.Models 
{
    public class ViewModel : ITable//todo delete
    {
        public string Name { get; set; }
        public List<ColumnModel> Columns { get; set; }
        public List<ITriggerModel> Trigers { get; set; }
        public List<IndexModel> Indexes { get; set; }
    }
}
