using System.Collections.Generic;
using System.Data;

namespace Schema.Core.Models
{
   public class ColumnModel
   {
       public string TableName { get; set;}
       public List<Column> Columns { get; set; }
       public List<Key> Keys { get; set; }
       public List<Constraint> Constrains { get; set; }
       public List<Triger> Trigers { get; set; }
       public List<Indexe> Indexes { get; set; }
       public List<Statistic> Statistics { get; set; }
    }
}
