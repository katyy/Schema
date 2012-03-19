using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Schema.Core.Models;

namespace Schema.Core.Helpers
{
    public class ModelsGetter
    {
        public static List<ITable> GetColumns(DataSet dataSet, string cnString, string TableName,string sql)
        {
            var columns = new List<ITable>();
            var dAdapter = new SqlDataAdapter(sql, cnString);
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
                    columns.Add(new TableModel { Name = name, Columns = column});
                    column = new List<ColumnModel>();
                    column = AddColumn(column, i, dt);
                }
                if (i == dt.Rows.Count - 1)
                {
                    columns.Add(new TableModel { Name = name, Columns = column});
                }
                name = tableName;
            }

            return columns;
        }

        public static List<TableModel> GetColumn(DataSet dataSet, string cnString, string TableName)
        {
            var columns = new List<TableModel>();
            var dAdapter = new SqlDataAdapter(SQL.SelectColumn, cnString);
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
                    columns.Add(new TableModel { Name = name, Columns = column, Indexes = new List<IndexModel>(), Keys = new List<KeyModel>(), Trigers = new List<TrigerModel>() });
                    column = new List<ColumnModel>();
                    column = AddColumn(column, i, dt);
                 }
                if (i == dt.Rows.Count - 1)
                {
                    columns.Add(new TableModel { Name = name, Columns = column, Indexes = new List<IndexModel>(), Keys = new List<KeyModel>(), Trigers = new List<TrigerModel>() });
                }
                name = tableName;
            }

            return columns;
        }

        private static List<ColumnModel> AddColumn(List<ColumnModel> column, int i, DataTable dt)
        {
            var identy = Converter(dt.Rows[i].ItemArray[6]);
            column.Add(new ColumnModel
            {
                ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                TypeName = dt.Rows[i].ItemArray[2].ToString(),
                MaxLength = Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString()),
                AllowNull = (bool)dt.Rows[i].ItemArray[4],
                IsIdenty = (bool)dt.Rows[i].ItemArray[5],
                IdentyIncriment = identy
            });
            return column;
        }

        public static List<KeyModel> GetKeys(DataSet dataSet, string cnString, string tableName)
        {
            var keyModel = new List<KeyModel>();

            var dAdapter = new SqlDataAdapter(SQL.SelectPk, cnString);
            dAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                keyModel.Add(new KeyModel
                     {
                         TableName = dt.Rows[i].ItemArray[0].ToString(),
                         ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                         Type = dt.Rows[i].ItemArray[2].ToString(),
                         Name = dt.Rows[i].ItemArray[3].ToString(),
                         TypeDescription = dt.Rows[i].ItemArray[4].ToString()
                     });
            }

            return keyModel;
        }

        public static List<KeyModel> GetForigenKey(DataSet dataSet, string cnString, string tableName)
        {
            var keyModel = new List<KeyModel>();
            var dataAdapter =
                new SqlDataAdapter(SQL.SelectFk, cnString);
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                keyModel.Add(new KeyModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                    Type = dt.Rows[i].ItemArray[2].ToString(),
                    Name = dt.Rows[i].ItemArray[3].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[4].ToString(),
                    DeletRule = dt.Rows[i].ItemArray[5].ToString(),
                    UpdateRule = dt.Rows[i].ItemArray[6].ToString(),
                    ReferanceTable = dt.Rows[i].ItemArray[7].ToString(),
                    ReferanceColumn = dt.Rows[i].ItemArray[8].ToString()
                });
            }
            return keyModel;
        }

        public static List<TrigerModel> GetTrigers(DataSet dataSet, string cnString, string tableName)
        {
            var trigerModel = new List<TrigerModel>();
            var dataAdapter =new SqlDataAdapter(SQL.SelectTriger, cnString);
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                trigerModel.Add(new TrigerModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    TrigerName = dt.Rows[i].ItemArray[1].ToString(),
                    Event = dt.Rows[i].ItemArray[2].ToString(),
                    Type = dt.Rows[i].ItemArray[3].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[4].ToString()
                });
            }
            return trigerModel;
        }

        public static List<IndexModel> GetIndexes(DataSet dataSet, string cnString, string tableName)
        {
            var indexModel = new List<IndexModel>();
            var dataAdapter =
                new SqlDataAdapter(SQL.SelectIndex, cnString);
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                indexModel.Add(new IndexModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                    Name = dt.Rows[i].ItemArray[2].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[3].ToString(),
                    IsUnique = Convert.ToBoolean(dt.Rows[i].ItemArray[4].ToString()),
                    IsDescending = Convert.ToBoolean(dt.Rows[i].ItemArray[5].ToString())
                });
            }
            return indexModel;
        }

        public static List<ViewModel> GetViews(DataSet dataSet, string cnString, string tableName)
        {
            var viewModel = new List<ViewModel>();
            var dataAdapter = new SqlDataAdapter(SQL.SelectView, cnString);
            dataAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                viewModel.Add(new ViewModel
                {
                    Name = dt.Rows[i].ItemArray[0].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[1].ToString()
                });
            }
            return viewModel;
        }

        public static List<ProcedureModel> GetProcedures(DataSet dataSet, string cnString, string tableName)
        {
            var procedures = new List<ProcedureModel>();
            var dAdapter = new SqlDataAdapter(SQL.SelectProcedure, cnString);
            dAdapter.Fill(dataSet, tableName);
            var columns = new List<ProcedureColumnModel>();
            var dt = dataSet.Tables[tableName];
            string name = null;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var procedureName = dt.Rows[i].ItemArray[0].ToString();
                if (name == procedureName || name == null)
                {
                   columns= AddProcedureColumn(columns, i, dt);
                }
                else
                {
                    procedures.Add(new ProcedureModel { Name = name, ProcedureColumn = columns });
                    columns = new List<ProcedureColumnModel>();
                    columns = AddProcedureColumn(columns, i, dt);
                
                }
                if (i == dt.Rows.Count - 1)
                {
                    procedures.Add(new ProcedureModel { Name = name, ProcedureColumn = columns });
                }
                name = procedureName;
            }

            return procedures;
        }

        private static List<ProcedureColumnModel> AddProcedureColumn(List<ProcedureColumnModel> columns, int i, DataTable dt)
        {
            var maxLength = Converter(dt.Rows[i].ItemArray[5]);
            var precesion = Converter(dt.Rows[i].ItemArray[6]);
            var scale = Converter(dt.Rows[i].ItemArray[7]);
            columns.Add(new ProcedureColumnModel
            {
                ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                Type = dt.Rows[i].ItemArray[2].ToString(),
                TypeDescription = dt.Rows[i].ItemArray[3].ToString(),
                DataType = dt.Rows[i].ItemArray[4].ToString(),
                MaxLength = maxLength,
                Precision = precesion,
                Scale = scale
            });
            return columns;
        }

        private static int? Converter(object intValue)
        {
            int? value = null;

            if (intValue != DBNull.Value)
            {
                value = Convert.ToInt32(intValue);
            }
            return value;

        }
    }
}
