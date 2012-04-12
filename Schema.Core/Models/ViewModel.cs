namespace Schema.Core.Models
{
    using System.Collections.Generic;

    using Schema.Core.Models.Table;

    public class ViewModel : ITable
    {
        public string Name { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public List<TriggerModel> Trigers { get; set; }

        public List<IndexModel> Indexes { get; set; }
    }
}
