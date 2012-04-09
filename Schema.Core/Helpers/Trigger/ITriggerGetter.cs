namespace Schema.Core.Helpers.Trigger
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models.Trigger;
    using Schema.Core.Reader;

    public interface ITriggerGetter
    {
        List<ITriggerModel> GetTriggers(IReader reader, DataSet dataSet, string tableName);
    }
}
