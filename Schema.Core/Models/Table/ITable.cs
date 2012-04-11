namespace Schema.Core.Models.Table
{
    using System.Collections.Generic;

    using Schema.Core.Models.Column;
    using Schema.Core.Models.Trigger;

    public interface ITable
    {
        string Name { get; set; }

        List<ColumnModel> Columns { get; set; }

        List<ITriggerModel> Trigers { get; set; }

        List<IndexModel> Indexes { get; set; }
    }
}
