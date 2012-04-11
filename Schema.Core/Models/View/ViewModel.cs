namespace Schema.Core.Models.View
{
    using System.Collections.Generic;

    using Schema.Core.Models.Column;
    using Schema.Core.Models.Table;
    using Schema.Core.Models.Trigger;

    public class ViewModel : ITable
    {
        public string Name { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public List<TriggerModel> Trigers { get; set; }

        public List<IndexModel> Indexes { get; set; }
    }
}
