using System.Collections.Generic;

namespace Schema.Core.Models
{
    public class DatabaseModel
    {
        public List<TableModel> Tables { get; set; }
        public List<ViewModel> Views { get; set; }
    }
}
