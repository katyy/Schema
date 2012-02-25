using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Schema.Core.Models;

namespace Schema.Core.Helpers
{
    public class ModelHelper
    {
        public static DatabaseModel GetColumn(DataSet dataSet, string cnString, string TableName)
        {
            var model = new DatabaseModel();
            var columns = new List<ColumnModel>();
            var dAdapter =
                new SqlDataAdapter(
                    "SELECT t.name,c.name,ty.name, c.max_length, c.is_nullable,c.is_identity FROM sys.columns c,sys.tables t,sys.types ty  WHERE c.object_id=t.object_id  and ty.user_type_id=c.system_type_id;",
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
                                       IsUnique = (bool)dt.Rows[i].ItemArray[5]
                                   });

                }
                else
                {
                    columns.Add(new ColumnModel { TableName = name, Columns = column });
                    column = new List<Column> { new Column { ColumnName = dt.Rows[i].ItemArray[1].ToString() } };
                }
                if (i == dt.Rows.Count - 1)
                {
                    columns.Add(new ColumnModel { TableName = name, Columns = column });
                }
                name = tableName;
            }
            model.Tables = columns;

            return model;
        }
        public static List<KeyModel> GetKeys(DataSet dataSet, string cnString, string TableName)
        {
            var keyModel = new List<KeyModel>();

            var dAdapter =
                new SqlDataAdapter(
                    "SELECT t.name,c.name, k.type,k.name,k.type_desc FROM sys.key_constraints k,  sys.all_columns c,sys.tables t where c.object_id=k.parent_object_id and c.column_id=k.unique_index_id and k.parent_object_id=t.object_id;",
                    cnString);
            dAdapter.Fill(dataSet, TableName);
            var dt = dataSet.Tables["Keys"];



            for (var i = 0; i < dt.Rows.Count; i++)
            {
                keyModel.Add(new KeyModel
                     {
                         TableName = dt.Rows[i].ItemArray[0].ToString(),
                         ColumnName = dt.Rows[i].ItemArray[1].ToString(),
                         Type = dt.Rows[i].ItemArray[2].ToString(),
                         Name = dt.Rows[i].ItemArray[3].ToString(),
                         TypeDescription = dt.Rows[i].ItemArray[4].ToString(),
                         //  IsIdenty = dt.Rows[i].ItemArray[1].ToString(),
                         //IdentyIncriment = dt.Rows[i].ItemArray[1].ToString(),
                         //  DeletRule = dt.Rows[i].ItemArray[1].ToString(),
                         // UpdateRule = dt.Rows[i].ItemArray[1].ToString()

                     });

            }
            return keyModel;
        }
    }
}
