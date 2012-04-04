using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.View;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.View
{
    public interface IViewGetter
    {
        List<IViewModel> GetView(IReader reader, DataSet dataSet, string tableName);
    }
}
