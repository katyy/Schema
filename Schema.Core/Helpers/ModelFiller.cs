namespace Schema.Core.Helpers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Schema.Core.Helpers.ModelGetters;
    using Schema.Core.Models;
    using Schema.Core.Models.Table;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class ModelFiller
    {
        public static DatabaseModel GetModel(ISqlReader sqlReader, DataSet dataSet)
        {
            var columnModels = ColumnGetter<ColumnModel>.GetColumn(sqlReader, dataSet, TableNames.Tables);

            var keyModel = KeyGetter.GetKeys(sqlReader, dataSet, TableNames.Keys);

            var trigers = TriggerGetter.GetTriggers(sqlReader, dataSet, TableNames.Triggers);

            var indexes = IndexGetter.GetIndexes(sqlReader, dataSet, TableNames.Indexes);

            var procedures = ProcedureGetter.GetProcedure(
                sqlReader, dataSet, sqlReader.SqlQueries.SelectProcedure, TableNames.Procedures);

            var functions = ProcedureGetter.GetProcedure(
                sqlReader, dataSet, sqlReader.SqlQueries.SelectFunction, TableNames.Functions);

            var views = ViewGetter.GetView(sqlReader, dataSet, TableNames.Views);

            var tables = GetTable<TableModel>(columnModels, indexes, trigers);
            tables = InsertKey(keyModel, tables);

            return new DatabaseModel
             {
                 Tables = tables,
                 Views = views,
                 Procedures = procedures,
                 Functions = functions
             };
        }

        public static List<T> GetTable<T>(
                                            Dictionary<string, List<ColumnModel>> columnDictionary,
                                            Dictionary<string, List<IndexModel>> indexDictionary,
                                            Dictionary<string, List<TriggerModel>> triggerDictionary) where T : ITable, new()
        {
            var table = InsertColumn<T>(columnDictionary);
            table = InsertIndex(indexDictionary, table);
            table = InsertTrigger(triggerDictionary, table);
            return table;
        }


        public static List<T> InsertColumn<T>(Dictionary<string, List<ColumnModel>> columnDictionary) where T : ITable, new()
        {
            return columnDictionary.Select(column =>
                                            new T
                                                {
                                                    Name = column.Key,
                                                    Columns = column.Value
                                                }).ToList();
        }

        public static List<T> InsertIndex<T>(Dictionary<string, List<IndexModel>> indexDictionary, List<T> table) where T : ITable
        {
            foreach (var t in table.Where(t => indexDictionary.ContainsKey(t.Name)))
            {
                t.Indexes = indexDictionary[t.Name];
            }

            return table;
        }

        public static List<TableModel> InsertKey(Dictionary<string, List<KeyModel>> keyDictionary, List<TableModel> table)
        {
            foreach (var t in table.Where(t => keyDictionary.ContainsKey(t.Name)))
            {
                t.Keys = keyDictionary[t.Name];
            }

            return table;
        }

        public static List<T> InsertTrigger<T>(Dictionary<string, List<TriggerModel>> triggerDictionary, List<T> table) where T : ITable
        {
            foreach (var t in table.Where(t => triggerDictionary.ContainsKey(t.Name)))
            {
                t.Trigers = triggerDictionary[t.Name];
            }

            return table;
        }
    }
}
