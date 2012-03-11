using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Schema.Core.Models;

namespace Schema.Core.Helpers
{
    public class ModelHelper
    {
        public static List<ColumnModel> GetColumn(DataSet dataSet, string cnString, string TableName)
        {
            var columns = new List<ColumnModel>();
            var dAdapter =
                new SqlDataAdapter(
                   SQL.SelectColumn,
                    cnString);
            dAdapter.Fill(dataSet, TableName);
            var column = new List<Column>();
            var dt = dataSet.Tables[TableName];
            string name = null;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var tableName = dt.Rows[i].ItemArray[0].ToString();
                if (name == tableName || name == null)
                {
                    column.Add(new Column
                                   {
                                       ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                                       TypeName = dt.Rows[i].ItemArray[2].ToString(),
                                       MaxLength = Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString()),
                                       AllowNull = (bool)dt.Rows[i].ItemArray[4],
                                       IsIdenty = (bool)dt.Rows[i].ItemArray[5]
                                      });

                }
                else
                {
                    columns.Add(new ColumnModel { Name = name, Columns = column });
                    column = new List<Column> { new Column { ColumnName = dt.Rows[i].ItemArray[1].ToString() } };
                }
                if (i == dt.Rows.Count - 1)
                {
                    columns.Add(new ColumnModel { Name = name, Columns = column });
                }
                name = tableName;
            }
            return columns;
        }

        public static List<KeyModel> GetKeys(DataSet dataSet, string cnString, string TableName)
        {
            var keyModel = new List<KeyModel>();

            var dAdapter =
                new SqlDataAdapter(
                   SQL.SelectPk,
                    cnString);
            dAdapter.Fill(dataSet, TableName);
            var dt = dataSet.Tables[TableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                keyModel.Add(new KeyModel
                     {
                         TableName = dt.Rows[i].ItemArray[0].ToString(),
                         ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                         Type = dt.Rows[i].ItemArray[2].ToString(),
                         Name = dt.Rows[i].ItemArray[3].ToString(),
                         TypeDescription = dt.Rows[i].ItemArray[4].ToString(),
                     });

            }
            return keyModel;
        }

        public static List<KeyModel> GetForigenKey(DataSet dataSet, string cnString, string TableName)
        {
            var keyModel = new List<KeyModel>();
            var dataAdapter =
                new SqlDataAdapter(
                    SQL.SelectFk,
                    cnString);
            dataAdapter.Fill(dataSet, TableName);
            var dt = dataSet.Tables[TableName];
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

        public static List<TrigerModel> GetTrigers(DataSet dataSet, string cnString, string TableName)
        {
            var trigerModel = new List<TrigerModel>();
            var dataAdapter =
                new SqlDataAdapter(
                    SQL.SelectTriger,
                cnString);
            dataAdapter.Fill(dataSet, TableName);
            var dt = dataSet.Tables[TableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                trigerModel.Add(new TrigerModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    TrigerName = dt.Rows[i].ItemArray[1].ToString(),
                    Event = dt.Rows[i].ItemArray[2].ToString(),
                    Type = dt.Rows[i].ItemArray[3].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[4].ToString(),
                 });

            }
            return trigerModel;
        }

        public static List<IndexModel> GetIndexes(DataSet dataSet, string cnString, string TableName)
        {
            var indexModel = new List<IndexModel>();
            var dataAdapter =
                new SqlDataAdapter(
                    SQL.SelectIndex,
                cnString);
            dataAdapter.Fill(dataSet, TableName);
            var dt = dataSet.Tables[TableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                indexModel.Add(new IndexModel
                {
                    TableName = dt.Rows[i].ItemArray[0].ToString(),
                    ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                    Name = dt.Rows[i].ItemArray[2].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[3].ToString(),
                    Isunique = Convert.ToBoolean(dt.Rows[i].ItemArray[4].ToString()),
                    IsDescending = Convert.ToBoolean(dt.Rows[i].ItemArray[5].ToString())
                  });

            }
            return indexModel;
        }

        public static List<ViewModel> GetViews(DataSet dataSet, string cnString, string TableName)
        {
            var viewModel = new List<ViewModel>();
            var dataAdapter =
                new SqlDataAdapter(
                   SQL.SelectView,
                cnString);
            dataAdapter.Fill(dataSet, TableName);
            var dt = dataSet.Tables[TableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                viewModel.Add(new ViewModel
                {
                    Name = dt.Rows[i].ItemArray[0].ToString(),
                    TypeDescription = dt.Rows[i].ItemArray[1].ToString(),
                });
            }
            return viewModel;
        }
    }
}
