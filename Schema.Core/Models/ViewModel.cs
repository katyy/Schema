using System.Collections.Generic;

namespace Schema.Core.Models
{
    public class ViewModel : ITable
    {
        public string Name { get; set; }
      //  public string TypeDescription { get; set; }
        public List<ColumnModel> Columns { get; set; }
        public List<TriggerModel> Trigers { get; set; }
        public List<IndexModel> Indexes { get; set; }
    }
}
