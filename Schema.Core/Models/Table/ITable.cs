using System.Collections.Generic;
using Schema.Core.Models.Column;
using Schema.Core.Models.Trigger;

namespace Schema.Core.Models.Table
{
     public interface ITable
    {
         string Name { get; set; }
         List<IColumnModel> Columns { get; set; }
         List<ITriggerModel> Trigers { get; set; }
         List<IndexModel> Indexes { get; set; }
    }
}
