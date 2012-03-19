using System.Collections.Generic;
using System.Data;
using Schema.Core.Models;

namespace Schema.Core.Helpers
{
    public class ModelFiller
    {
        public static DatabaseModel GetModel(DataSet dataSet, string cnString)
        {
            var columnModels = ModelsGetter.GetColumn(dataSet, cnString, new List<TableModel>(), TableNames.Tables, SQL.SelectColumn);
            var keyModel = ModelsGetter.GetKeys(dataSet, cnString, TableNames.Keys);
            var forigenKey = ModelsGetter.GetForigenKey(dataSet, cnString, TableNames.ForigenKey);
            var trigers = ModelsGetter.GetTriggers(dataSet, cnString, TableNames.Triggers, SQL.SelectTrigger);
            var indexes = ModelsGetter.GetIndexes(dataSet, cnString, TableNames.Indexes, SQL.SelectIndex);
            var procedures = ModelsGetter.GetProcedures(dataSet, cnString, TableNames.Procedures);

            var views = ModelsGetter.GetColumn(dataSet, cnString, new List<ViewModel>(), TableNames.Views, SQL.SelectView);
            var viewTriggers = ModelsGetter.GetTriggers(dataSet, cnString, TableNames.ViewTriggers, SQL.SelectViewTriggers);
            var viewIndexes = ModelsGetter.GetIndexes(dataSet, cnString, TableNames.ViewIndexes, SQL.SelectViewIndexes);
            InsertModels(views, viewTriggers, viewIndexes);

            InsertModels(columnModels, keyModel, forigenKey, trigers, indexes);
            return new DatabaseModel
             {
                 Tables = columnModels,
                 Views = views,
                 Procedures = procedures
             };

        }
        private static void InsertModels<T>(IEnumerable<T> columnModel, IList<TriggerModel> triggerModel, IList<IndexModel> indexModel) where T : ITable
        {
            foreach (var column in columnModel)
            {
                Trigger(triggerModel, column);
                Index(indexModel, column);
            }
        }


        private static void InsertModels(IEnumerable<TableModel> columnModel, IList<KeyModel> keyModel, IList<KeyModel> forigenKey, IList<TriggerModel> triggerModel, IList<IndexModel> indexModel)
        {
            foreach (var column in columnModel)
            {
                Key(keyModel, column);
                Key(forigenKey, column);
                Trigger(triggerModel, column);
                Index(indexModel, column);
            }
        }

        
        private static void Key(IList<KeyModel> keyModel, TableModel table)
        {
            if (table.Keys == null)
            {
                table.Keys = new List<KeyModel>();
            }
            for (var i = 0; i < keyModel.Count; i++)
            {
                var key = keyModel[i];
                if (key.TableName != table.Name) continue;
                table.Keys.Add(key);
                keyModel.Remove(key);
            }
        }

        private static void Trigger(IList<TriggerModel> trigerModel, ITable table)
        {
            if (table.Trigers == null)
            {
                table.Trigers = new List<TriggerModel>();
            }
            for (var i = 0; i < trigerModel.Count; i++)
            {
                var triger = trigerModel[i];
                if (triger.TableName != table.Name) continue;
                table.Trigers.Add(triger);
                trigerModel.Remove(triger);
            }
        }

        private static void Index(IList<IndexModel> indexModel, ITable table)
        {
            if (table.Indexes == null)
            {
                table.Indexes = new List<IndexModel>();
            }
            for (var i = 0; i < indexModel.Count; i++)
            {
                var index = indexModel[i];
                if (index.TableName != table.Name) continue;
                table.Indexes.Add(index);
                indexModel.Remove(index);
            }
        }
    }
}
