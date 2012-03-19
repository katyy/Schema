using System.Collections.Generic;
using System.Data;
using Schema.Core.Models;

namespace Schema.Core.Helpers
{
    public class ModelFiller
    {
        public static DatabaseModel GetModel(DataSet dataSet, string cnString)
        {
          //  var columnModels = ModelsGetter.GetColumn(dataSet, cnString, TableNames.Tables);
            var columnModels =ModelsGetter.GetColumns(dataSet, cnString, TableNames.Tables, SQL.SelectColumn);
            var keyModel = ModelsGetter.GetKeys(dataSet, cnString, TableNames.Keys);
            var forigenKey = ModelsGetter.GetForigenKey(dataSet, cnString, TableNames.ForigenKey);
            var trigers = ModelsGetter.GetTrigers(dataSet, cnString, TableNames.Trigers);
            var indexes = ModelsGetter.GetIndexes(dataSet, cnString, TableNames.Indexes);
            var views = ModelsGetter.GetViews(dataSet, cnString, TableNames.Views);
            var procedures = ModelsGetter.GetProcedures(dataSet, cnString, TableNames.Procedures);

            InsertModels( columnModels, keyModel, forigenKey, trigers, indexes);
            return new DatabaseModel
             {
                 Tables = columnModels,
                 Views = views
             };

        }

        private static void InsertModels(IEnumerable<TableModel> columnModel, IList<KeyModel> keyModel, IList<KeyModel> forigenKey, IList<TrigerModel> trigerModel, IList<IndexModel> indexModel)
        {
            foreach (var column in columnModel)
            {
                Key(keyModel, column);
                Key(forigenKey, column);
                Triger(trigerModel, column);
                Index(indexModel, column);
            }
        }

        private static void Key(IList<KeyModel> keyModel, TableModel table)
        {
            for (var i = 0; i < keyModel.Count; i++)
            {
                var key = keyModel[i];
                if (key.TableName != table.Name) continue;
                table.Keys.Add(key);
                keyModel.Remove(key);
            }
        }

        private static void Triger(IList<TrigerModel> trigerModel, TableModel table)
        {
            for (var i = 0; i < trigerModel.Count; i++)
            {
                var triger = trigerModel[i];
                if (triger.TableName != table.Name) continue;
                table.Trigers.Add(triger);
                trigerModel.Remove(triger);
            }
        }

        private static void Index(IList<IndexModel> indexModel, TableModel table)
        {
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
