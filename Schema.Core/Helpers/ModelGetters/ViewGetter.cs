namespace Schema.Core.Helpers.ModelGetters
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class ViewGetter 
    {
        public static List<ViewModel> GetView(IReader reader, DataSet dataSet, string tableName)
        {
            var viewColumn = ColumnGetter<ColumnModel>.GetColumn(reader, dataSet, TableNames.Views);
            var viewTriggers = TriggerGetter.GetTriggers(reader, dataSet, TableNames.ViewTriggers);
            var viewIndexes = IndexGetter.GetIndexes(reader, dataSet, TableNames.ViewIndexes);

            return ModelFiller.GetTable<ViewModel>(viewColumn, viewIndexes, viewTriggers);
           }
        }
}
