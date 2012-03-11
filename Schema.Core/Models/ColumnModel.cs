using System.Collections.Generic;

namespace Schema.Core.Models
{
    public class ColumnModel//:ViewModel
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
        public List<KeyModel> Keys { get; set; }
        public List<TrigerModel> Trigers { get; set; }
        public List<IndexModel> Indexes { get; set; }
    }
}
