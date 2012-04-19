namespace Schema.Core.Models.Table
{
    using System.Collections.Generic;

    public class TableModel : ITable
    {
        public string Name { get; set; }

        public List<KeyModel> Keys { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public List<TriggerModel> Trigers { get; set; }

        public List<IndexModel> Indexes { get; set; }
    }
}
