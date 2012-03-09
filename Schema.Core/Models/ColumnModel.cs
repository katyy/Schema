using System.Collections.Generic;

namespace Schema.Core.Models
{
    public class ColumnModel
    {
        public string TableName { get; set; }
        public List<Column> Columns { get; set; }
        public List<KeyModel> Keys { get; set; }
       public List<TrigerModel> Trigers { get; set; }
        //  public List<Index> Indexes { get; set; }
        //  public List<Statistic> Statistics { get; set; }
    }
}
