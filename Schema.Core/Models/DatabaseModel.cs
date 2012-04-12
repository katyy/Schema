namespace Schema.Core.Models
{
    using System.Collections.Generic;

    using Schema.Core.Models.Procedure;
    using Schema.Core.Models.Table;

    public class DatabaseModel
    {
        public List<TableModel> Tables { get; set; }

        public List<ViewModel> Views { get; set; }

        public Dictionary<string, List<ParametrModel>> Procedures { get; set; }

        public Dictionary<string, List<ParametrModel>> Functions { get; set; }
    }
}
