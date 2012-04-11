namespace Schema.Core.Models.Table
{
    using System.Collections.Generic;

    using Schema.Core.Models.Column;
    using Schema.Core.Models.Key;
    using Schema.Core.Models.Trigger;

    public class TableModel : ITable
    {
        public string Name { get; set; }

        public List<KeyModel> Keys { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public List<ITriggerModel> Trigers { get; set; }

        public List<IndexModel> Indexes { get; set; }
    }
}
