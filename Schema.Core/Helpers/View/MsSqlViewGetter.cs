namespace Schema.Core.Helpers.View
{
    using System.Collections.Generic;
    using System.Data;
    using Schema.Core.Models.View;
    using Schema.Core.Reader;

    public class MsSqlViewGetter : IViewGetter
    {
        public List<IViewModel> GetView(IReader reader, DataSet dataSet, string tableName)
        {
            var views = reader.ColumnMethod.GetColumn(reader, dataSet, new List<MsSqlViewModel>(), TableNames.Views);// ModelsGetter.GetView(reader, dataSet, new List<MsSqlViewModel>(), TableNames.Views);
            var viewTriggers = reader.TriggerMethod.GetTriggers(reader, dataSet, TableNames.ViewTriggers);
            var viewIndexes = ModelsGetter.GetIndexes(reader, dataSet, TableNames.ViewIndexes);
            ModelFiller.InsertModels(views, viewTriggers, viewIndexes);
            return new List<IViewModel>(views);
        }
    }
}
