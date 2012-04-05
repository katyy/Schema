using System.Collections.Generic;
using System.Data;
using Schema.Core.Models;
using Schema.Core.Reader;

namespace Schema.Core.Helpers
{
    public class ModelFiller
    {
        public static DatabaseModel GetModel(IReader reader,DataSet dataSet) 
         {
            var columnModels = ModelsGetter.GetColumn(reader,dataSet,new List<TableModel>(), TableNames.Tables);
            var keyModel = ModelsGetter.GetKeys(reader,dataSet,TableNames.Keys);
            var forigenKey = ModelsGetter.GetForigenKey(reader,dataSet, TableNames.ForigenKey);
            var trigers = ModelsGetter.GetTriggers(reader, dataSet, TableNames.Triggers);
            var indexes = ModelsGetter.GetIndexes(reader,dataSet,  TableNames.Indexes);
            var procedures = reader.ProcedureFunctionMethod.GetProcedure(reader, dataSet, reader.SqlQueries.SelectProcedure, TableNames.Procedures);// ModelsGetter.GetProcedures(reader,dataSet,  TableNames.Procedures);
            var functions = reader.ProcedureFunctionMethod.GetProcedure(reader, dataSet, reader.SqlQueries.SelectFunction, TableNames.Functions);// ModelsGetter.GetProcedures(reader,dataSet,TableNames.Functions);

            var views = reader.ViewMethod.GetView(reader, dataSet, TableNames.Views); //ModelsGetter.GetColumn(reader, dataSet, new List<IViewModel>(), TableNames.Views);
           

            InsertModels(columnModels, keyModel, forigenKey, trigers, indexes);
            return new DatabaseModel
             {
                 Tables = columnModels,
                 Views = views,
                 Procedures =procedures,
               Functions = functions

             };

        }

        public static void InsertModels<T>(IEnumerable<T> columnModel, IList<TriggerModel> triggerModel, IList<IndexModel> indexModel)
        {
            foreach (var column in columnModel)
            {
                Trigger(triggerModel, (ITable) column);
                Index(indexModel, (ITable) column);
            }
        }


        public static void InsertModels(IEnumerable<TableModel> columnModel, IList<KeyModel> keyModel, IList<KeyModel> forigenKey, IList<TriggerModel> triggerModel, IList<IndexModel> indexModel)
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
