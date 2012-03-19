using System.Collections.Generic;

namespace Schema.Core.Models
{
     public interface ITable
    {
         string Name { get; set; }
         List<ColumnModel> Columns { get; set; }
         List<TriggerModel> Trigers { get; set; }
         List<IndexModel> Indexes { get; set; }
    }
}
