namespace Schema.Core.Helpers
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Helpers.Column;
    using Schema.Core.Helpers.Key;
    using Schema.Core.Helpers.Procedure;
    using Schema.Core.Helpers.Trigger;
    using Schema.Core.Helpers.View;
    using Schema.Core.Models;
    using Schema.Core.Models.Column;
    using Schema.Core.Models.Key;
    using Schema.Core.Models.Table;
    using Schema.Core.Models.Trigger;
    using Schema.Core.Models.View;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class ModelFiller
    {
        public static DatabaseModel GetModel(IReader reader, DataSet dataSet)
        {
            var columnModels = ColumnGetter<ColumnModel>.GetColumn(reader, dataSet, /*new List<TableModel>(),*/ TableNames.Tables);

            // ModelsGetter.GetView(reader,dataSet,new List<TableModel>(), TableNames.Tables);
            var keyModel = KeyGetter.GetKeys(reader, dataSet, TableNames.Keys);

            // var keyModel = ModelsGetter.GetKeys(reader,dataSet,TableNames.Keys);
            //  var forigenKey = ModelsGetter.GetForigenKey(reader,dataSet, TableNames.ForeigenKey);
            var trigers = TriggerGetter.GetTriggers(reader, dataSet, TableNames.Triggers);

            // ModelsGetter.GetTriggers(reader, dataSet, TableNames.Triggers);
            var indexes = ModelsGetter.GetIndexes(reader, dataSet, TableNames.Indexes);
            var procedures = ProcedureGetter.GetProcedure(
                reader, dataSet, reader.SqlQueries.SelectProcedure, TableNames.Procedures);

            // ModelsGetter.GetProcedures(reader,dataSet,  TableNames.Procedures);
            var functions = ProcedureGetter.GetProcedure(
                reader, dataSet, reader.SqlQueries.SelectFunction, TableNames.Functions);

            // ModelsGetter.GetProcedures(reader,dataSet,TableNames.Functions);
            var views = ViewGetter.GetView(reader, dataSet, TableNames.Views); // ModelsGetter.GetView(reader, dataSet, new List<IViewModel>(), TableNames.Views);


            InsertModels(columnModels, keyModel, trigers, indexes);
            return new DatabaseModel
             {
                 Tables = columnModels,
                 Views = views,
                 Procedures = procedures,
                 Functions = functions
             };
        }

        public static void InsertModels<T>(IEnumerable<T> columnModel, List<TriggerModel> triggerModel, IList<IndexModel> indexModel)
        {
            foreach (var column in columnModel)
            {
                Trigger(triggerModel, (ITable)column);
                Index(indexModel, (ITable)column);
            }
        }


        public static void InsertModels(IEnumerable<TableModel> columnModel, List<KeyModel> keyModel/*, IList<IKeyModel> forigenKey*/, IList<TriggerModel> triggerModel, IList<IndexModel> indexModel)
        {
            foreach (var column in columnModel)
            {
                Key(keyModel, column);

                // Key(forigenKey, column);
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
                if (key.TableName != table.Name)
                {
                    continue;
                }

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
                if (triger.TableName != table.Name)
                {
                    continue;
                }

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
                if (index.TableName != table.Name)
                {
                    continue;
                }

                table.Indexes.Add(index);
                indexModel.Remove(index);
            }
        }
    }
}
