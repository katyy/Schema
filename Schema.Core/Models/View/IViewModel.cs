using System.Collections.Generic;
using Schema.Core.Models.Column;

namespace Schema.Core.Models.View
{
    public interface IViewModel
    {
        string Name { get; set; }
        List<IColumnModel> Columns { get; set; }
    }
}
