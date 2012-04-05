using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Schema.Core.Models;
using Schema.Core.Models.Table;
using Schema.Core.Reader;

namespace Schema.Core.Helpers
{
    public class ModelsGetter
    {
        public static List<T> GetColumn<T>(IReader reader, DataSet dataSet, List<T> columns, string TableName)
            where T : ITable, new()
        {
            var dAdapter = reader.DataAdapter;
            dAdapter.SelectCommand = reader.Command;
            dAdapter.SelectCommand.Connection = reader.Conection;
            dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectColumn;
            dAdapter.Fill(dataSet, TableName);
            var column = new List<ColumnModel>();
            var dt = dataSet.Tables[TableName];
            string name = null;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var tableName = dt.Rows[i].ItemArray[0].ToString();
                if (name == tableName || name == null)
                {
                    column = AddColumn(column, i, dt);
                }
                else
                {
                    columns.Add(new T { Name = name, Columns = column });
                    column = new List<ColumnModel>();
                    column = AddColumn(column, i, dt);
                }
                if (i == dt.Rows.Count - 1)
                {
                    columns.Add(new T { Name = name, Columns = column });
                }
                name = tableName;
            }

            return columns;
        }

        private static List<ColumnModel> AddColumn(List<ColumnModel> column, int i, DataTable dt)
        {
            var isIdenty = dt.Rows[i].ItemArray[5].ToString();

            column.Add(new ColumnModel
             {
                 ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                 TypeName = dt.Rows[i].ItemArray[2].ToString(),
                 MaxLength = Converters.ToInt(dt.Rows[i].ItemArray[3]),
                 AllowNull = Converters.ToBool(dt.Rows[i].ItemArray[4]),
                 IsIdenty = string.IsNullOrEmpty(isIdenty) ? false.ToString(CultureInfo.InvariantCulture) : isIdenty,
                 IdentyIncriment = Converters.ToInt(dt.Rows[i].ItemArray[6])
             });
            return column;
        }

        //public static List<KeyModel> GetKeys(IReader reader, DataSet dataSet, string tableName)
        //{
        //    if (string.IsNullOrEmpty(reader.SqlQueries.SelectPk)) return new List<KeyModel>();
        //    var keyModel = new List<KeyModel>();
        //    var dAdapter = reader.DataAdapter;
        //    dAdapter.SelectCommand = reader.Command;
        //    dAdapter.SelectCommand.Connection = reader.Conection;
        //    dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectPk;
        //    dAdapter.Fill(dataSet, tableName);
        //    var dt = dataSet.Tables[tableName];
        //    for (var i = 0; i < dt.Rows.Count; i++)
        //    {
        //        keyModel.Add(new KeyModel
        //        {
        //            TableName = dt.Rows[i].ItemArray[0].ToString(),
        //            ColumnName = dt.Rows[i].ItemArray[1].ToString(),
        //            Type = dt.Rows[i].ItemArray[2].ToString(),
        //            Name = dt.Rows[i].ItemArray[3].ToString(),
        //            TypeDescription = dt.Rows[i].ItemArray[4].ToString()
        //        });
        //    }

        //    return keyModel;
        //}

        //public static List<KeyModel> GetForigenKey(IReader reader, DataSet dataSet, string tableName)
        //{
        //    var keyModel = new List<KeyModel>();

        //    var dAdapter = reader.DataAdapter;
        //    dAdapter.SelectCommand = reader.Command;
        //    dAdapter.SelectCommand.Connection = reader.Conection;
        //    dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectFk;

        //    dAdapter.Fill(dataSet, tableName);
        //    var dt = dataSet.Tables[tableName];
        //    for (var i = 0; i < dt.Rows.Count; i++)
        //    {
        //        keyModel.Add(new KeyModel
        //        {
        //            TableName = dt.Rows[i].ItemArray[0].ToString(),
        //            ColumnName = dt.Rows[i].ItemArray[1].ToString(),
        //            Type = dt.Rows[i].ItemArray[2].ToString(),
        //            Name = dt.Rows[i].ItemArray[3].ToString(),
        //            TypeDescription = dt.Rows[i].ItemArray[4].ToString(),
        //            DeletRule = dt.Rows[i].ItemArray[5].ToString(),
        //            UpdateRule = dt.Rows[i].ItemArray[6].ToString(),
        //            ReferanceTable = dt.Rows[i].ItemArray[7].ToString(),
        //            ReferanceColumn = dt.Rows[i].ItemArray[8].ToString()
        //        });
        //    }
        //    return keyModel;
        //}

        //public static List<TriggerModel> GetTriggers(IReader reader, DataSet dataSet, string tableName)
        //{
        //    var trigerModel = new List<TriggerModel>();
        //    var dAdapter = reader.DataAdapter;
        //    dAdapter.SelectCommand = reader.Command;
        //    dAdapter.SelectCommand.Connection = reader.Conection;
        //    dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectTrigger;
        //    dAdapter.Fill(dataSet, tableName);
        //    var dt = dataSet.Tables[tableName];
        //    for (var i = 0; i < dt.Rows.Count; i++)
        //    {
        //        trigerModel.Add(new TriggerModel
        //        {
        //            TableName = dt.Rows[i].ItemArray[0].ToString(),
        //            TrigerName = dt.Rows[i].ItemArray[1].ToString(),
        //            Event = dt.Rows[i].ItemArray[2].ToString(),
        //            Type = dt.Rows[i].ItemArray[3].ToString(),
        //            TypeDescription = dt.Rows[i].ItemArray[4].ToString()
        //        });
        //    }
        //    return trigerModel;
        //}

        public static List<IndexModel> GetIndexes(IReader reader, DataSet dataSet, string tableName)
        {
            var indexModel = new List<IndexModel>();
            var dAdapter = reader.DataAdapter;
            dAdapter.SelectCommand = reader.Command;
            dAdapter.SelectCommand.Connection = reader.Conection;
            dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectIndex;
            dAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                indexModel.Add(new IndexModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                    Name = dt.Rows[i].ItemArray[2].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[3].ToString(),
                    IsUnique = Convert.ToBoolean(dt.Rows[i].ItemArray[4]),
                    IsDescending = Converters.AscDescToBool(dt.Rows[i].ItemArray[5])
                });
            }
            return indexModel;
        }

        //public static List<ProcedureModel> GetProcedures(IReader reader, DataSet dataSet, string tableName)
        //{
        //    var procedures = new List<ProcedureModel>();
        //    var dAdapter = reader.DataAdapter;
        //    dAdapter.SelectCommand = new SqlCommand(reader.SqlQueries.SelectProcedure, new SqlConnection(reader.ConnectionString));
        //    dAdapter.Fill(dataSet, tableName);
        //    var columns = new List<ProcedureColumnModel>();
        //    var dt = dataSet.Tables[tableName];
        //    string name = null;
        //    for (var i = 0; i < dt.Rows.Count; i++)
        //    {
        //        var procedureName = dt.Rows[i].ItemArray[0].ToString();
        //        if (name == procedureName || name == null)
        //        {
        //            columns = AddProcedureColumn(columns, i, dt);
        //        }
        //        else
        //        {
        //            procedures.Add(new ProcedureModel { Name = name, ProcedureColumn = columns });
        //            columns = new List<ProcedureColumnModel>();
        //            columns = AddProcedureColumn(columns, i, dt);

        //        }
        //        if (i == dt.Rows.Count - 1)
        //        {
        //            procedures.Add(new ProcedureModel { Name = name, ProcedureColumn = columns });
        //        }
        //        name = procedureName;
        //    }

        //    return procedures;
        //}

        //private static List<ProcedureColumnModel> AddProcedureColumn(List<ProcedureColumnModel> columns, int i, DataTable dt)
        //{
        //    var maxLength = Converters.ToInt(dt.Rows[i].ItemArray[5]);
        //    var precesion = Converters.ToInt(dt.Rows[i].ItemArray[6]);
        //    var scale = Converters.ToInt(dt.Rows[i].ItemArray[7]);
        //    columns.Add(new ProcedureColumnModel
        //    {
        //        ColumnName = dt.Rows[i].ItemArray[1].ToString(),
        //        Type = dt.Rows[i].ItemArray[2].ToString(),
        //        TypeDescription = dt.Rows[i].ItemArray[3].ToString(),
        //        DataType = dt.Rows[i].ItemArray[4].ToString(),
        //        MaxLength = maxLength,
        //        Precision = precesion,
        //        Scale = scale
        //    });
        //    return columns;
        //}
    }
}
