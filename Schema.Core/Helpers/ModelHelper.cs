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
                    "SELECT t.name,c.name,ty.name, c.max_length, c.is_nullable,c.is_identity  FROM sys.types ty ,sys.tables t,sys.columns c WHERE c.object_id=t.object_id  and ty.user_type_id=c.system_type_id;",
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
                    "SELECT t.name,c.name, k.type,k.name,k.type_desc FROM sys.key_constraints k,  sys.all_columns c,sys.tables t where c.object_id=k.parent_object_id and c.column_id=k.unique_index_id and k.parent_object_id=t.object_id;",
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

        public static List<KeyModel> GetForigenKey(DataSet dataSet, string cnString,string TableName)
        {
            var keyModel = new List<KeyModel>();
            var dataAdapter =
                new SqlDataAdapter(
                    "select foriegen.parent_table,parent_column,f.type,name,f.type_desc,f.delete_referential_action_desc,f.update_referential_action_desc, foriegen.referance_table, foriegen.referance_column from sys.foreign_keys f  LEFT OUTER JOIN (select p.parent_table , p.parent_column,r.referance_table,r.referance_column,r.id from (select t.name referance_table,c.name referance_column,fc.constraint_object_id id from sys.foreign_key_columns fc, sys.tables t,sys.all_columns c where fc.referenced_object_id=t.object_id and fc.referenced_column_id=c.column_id and fc.referenced_object_id=c.object_id) r LEFT OUTER JOIN (select t.name parent_table,c.name parent_column,fc.constraint_object_id id from sys.foreign_key_columns fc, sys.tables t,sys.all_columns c where fc.parent_object_id=t.object_id and fc.parent_column_id=c.column_id and fc.parent_object_id=c.object_id) p ON p.id=r.id) foriegen on foriegen.id=f.object_id;",
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
                    "SELECT t.name table_name,triger.triger_name,triger.triger_event,triger.type,triger.type_desc FROM ( SELECT te.type_desc triger_event,tr.name triger_name, tr.parent_id,tr.type,tr.type_desc FROM sys.triggers tr LEFT OUTER JOIN sys.trigger_events te ON te.object_id=tr.object_id) triger ,sys.tables t WHERE t.object_id=triger.parent_id;",
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
                    "SELECT index_table.table_name, c.name column_name,index_table.name,index_table.type_desc,index_table.is_unique,index_table.is_descending_key FROM(SELECT t.name table_name,indexes.* FROM (SELECT i.object_id,i.name,i.type_desc,i.is_unique,ic.index_column_id,ic.column_id,ic.is_descending_key  FROM sys.indexes i,sys.index_columns ic WHERE i.object_id=ic.object_id and i.index_id=ic.index_id) indexes LEFT JOIN sys.tables t ON t.object_id=indexes.object_id WHERE t.name IS NOT NULL) index_table LEFT JOIN sys.columns c  ON c.column_id=index_table.column_id and c.object_id=index_table.object_id ;",
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

    }
}
