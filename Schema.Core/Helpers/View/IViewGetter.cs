namespace Schema.Core.Helpers.View
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models.View;
    using Schema.Core.Reader;

    public interface IViewGetter
    {
        List<IViewModel> GetView(IReader reader, DataSet dataSet, string tableName);
    }
}
