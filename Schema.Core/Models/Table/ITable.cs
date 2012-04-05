using System.Collections.Generic;
using Schema.Core.Models.Trigger;

namespace Schema.Core.Models.Table
{
     public interface ITable
    {
         string Name { get; set; }
         List<ColumnModel> Columns { get; set; }
         List<ITriggerModel> Trigers { get; set; }
         List<IndexModel> Indexes { get; set; }
    }
}
