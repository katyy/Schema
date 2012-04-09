namespace Schema.Core.Helpers.Column
{
    using System.Collections.Generic;
    using System.Data;
    using Schema.Core.Models.Table;
    using Schema.Core.Reader;

    public interface IColumnGetter
    {
        List<T> GetColumn<T>(IReader reader, DataSet dataSet, List<T> columns, string TableName) where T : ITable, new();
    }
}
