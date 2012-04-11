namespace Schema.Core.Helpers.View
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Helpers.Column;
    using Schema.Core.Helpers.Trigger;
    using Schema.Core.Models.Column;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class ViewGetter 
    {
        public static Dictionary<string, List<ColumnModel>> GetView(IReader reader, DataSet dataSet, string tableName)
        {
            var views = ColumnGetter<ColumnModel>.GetColumn(reader, dataSet, TableNames.Views); 
            var viewTriggers = TriggerGetter.GetTriggers(reader, dataSet, TableNames.ViewTriggers);
            var viewIndexes = ModelsGetter.GetIndexes(reader, dataSet, TableNames.ViewIndexes);
            ModelFiller.InsertModels(views, viewTriggers, viewIndexes);
            return views;
        }
    }
}
