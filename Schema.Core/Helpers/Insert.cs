using System.Collections.Generic;
using System.Data;
using Schema.Core.Models;

namespace Schema.Core.Helpers
{
   public class Insert
    {

       public static DatabaseModel GetModel(DataSet dataSet, string cnString)
       {
           var columnModels = ModelHelper.GetColumn(dataSet, cnString, TableNames.Tables);
           var keyModel = ModelHelper.GetKeys(dataSet, cnString, TableNames.Keys);
           var forigenKey = ModelHelper.GetForigenKey(dataSet, cnString, TableNames.ForigenKey);
           var trigers = ModelHelper.GetTrigers(dataSet, cnString,TableNames.Trigers);
           var indexes = ModelHelper.GetIndexes(dataSet, cnString, TableNames.Indexes);
           var view = ModelHelper.GetViews(dataSet, cnString, TableNames.Views);

           InsertModels(columnModels, keyModel, forigenKey, trigers, indexes);

          return new DatabaseModel
           {
               Tables = columnModels,
               Views = view
           };
           
       }

       private static void InsertModels(IEnumerable<ColumnModel> columnModel, IList<KeyModel> keyModel, IList<KeyModel> forigenKey, IList<TrigerModel> trigerModel, IList<IndexModel> indexModel)
       {
           foreach (var column in columnModel)
           {
               if (column.Keys == null)
               {
                   column.Keys = new List<KeyModel>();
               }
               if (column.Trigers == null)
               {
                   column.Trigers = new List<TrigerModel>();
               }
               if (column.Indexes == null)
               {
                   column.Indexes = new List<IndexModel>();
               }
               Key(keyModel, column);
               Key(forigenKey, column);
               Triger(trigerModel, column);
               Index(indexModel, column);
           }
       }

        private static void Key(IList<KeyModel> keyModel, ColumnModel column)
        {
            for (var index = 0; index < keyModel.Count; index++)
            {
                var key = keyModel[index];
                if (key.TableName != column.Name) continue;
                column.Keys.Add(key);
                keyModel.Remove(key);
            }
        }

        private static void Triger(IList<TrigerModel> trigerModel, ColumnModel column)
        {
            for (var index = 0; index < trigerModel.Count; index++)
            {
                var triger = trigerModel[index];
                if (triger.TableName != column.Name) continue;
                column.Trigers.Add(triger);
                trigerModel.Remove(triger);
            }
        }

        private static void Index(IList<IndexModel> indexModel, ColumnModel column)
        {
            for (var i = 0; i < indexModel.Count; i++)
            {
                var index = indexModel[i];
                if (index.TableName != column.Name) continue;
                column.Indexes.Add(index);
                indexModel.Remove(index);
            }
        }
    }
}
