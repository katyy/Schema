namespace Schema.Core.Models.View
{
    using System.Collections.Generic;

    using Schema.Core.Models.Column;

    public interface IViewModel
    {
        string Name { get; set; }

        List<IColumnModel> Columns { get; set; }
    }
}
