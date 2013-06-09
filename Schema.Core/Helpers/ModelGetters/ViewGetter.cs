namespace Schema.Core.Helpers.ModelGetters
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class ViewGetter 
    {
        public static List<ViewModel> GetView(ISqlReader sqlReader, DataSet dataSet, string tableName)
        {
            var viewColumn = ColumnGetter<ColumnModel>.GetColumn(sqlReader, dataSet, TableNames.Views);
            var viewTriggers = TriggerGetter.GetTriggers(sqlReader, dataSet, TableNames.ViewTriggers);
            var viewIndexes = IndexGetter.GetIndexes(sqlReader, dataSet, TableNames.ViewIndexes);

            return ModelFiller.GetTable<ViewModel>(viewColumn, viewIndexes, viewTriggers);
           }
        }
}
