using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.View;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.View
{
    public class MsSqlViewGetter : IViewGetter
    {
        public List<IViewModel> GetView(IReader reader, DataSet dataSet, string tableName) 
        {
            var views = ModelsGetter.GetColumn(reader, dataSet, new List<MsSqlViewModel>(), TableNames.Views);
            var viewTriggers =reader.TriggerMethod.GetTriggers(reader, dataSet, TableNames.ViewTriggers);
            var viewIndexes = ModelsGetter.GetIndexes(reader, dataSet, TableNames.ViewIndexes);
            ModelFiller.InsertModels(views, viewTriggers, viewIndexes);
            return new List<IViewModel>(views);
        }


        
    }
}
