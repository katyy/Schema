﻿namespace Schema.Core.Models.View
{
    using System.Collections.Generic;
    using Schema.Core.Models.Column;
    using Schema.Core.Models.Table;
    using Schema.Core.Models.Trigger;
    public class MySqlViewModel : IViewModel, ITable 
    {
        public string Name { get; set; }

        public List<IColumnModel> Columns { get; set; }

        public List<ITriggerModel> Trigers { get; set; }

        //todo delete
        public List<IndexModel> Indexes{get; set; }//todo delete
    }
}
