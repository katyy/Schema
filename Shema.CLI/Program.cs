using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Schema.Core.Models;

namespace Shema.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new DatabaseModel();
            var columns=new List<ColumnModel>();
            const string cnString = "Data Source=.\\LOCALHOST;AttachDbFilename=d:\\App_data\\Cars\\Cars.UserInterface\\App_Data\\Parking.mdf;Integrated Security=True";

            var dataSet = new DataSet("dbDataSet");


            var dAdapter1 = new SqlDataAdapter("SELECT t.name,c.name,ty.name, c.max_length, c.is_nullable,c.is_identity FROM sys.columns c,sys.tables t,sys.types ty  WHERE c.object_id=t.object_id  and ty.user_type_id=c.system_type_id;", cnString);
            dAdapter1.Fill(dataSet, "Tables");
            var column = new List<Column>();
            foreach (DataTable dt in dataSet.Tables)
            {
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
                                           AllowNull = (bool) dt.Rows[i].ItemArray[4],
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

            }


            //var dAdapter1 = new SqlDataAdapter("SELECT t.name,c.name FROM sys.columns c,sys.tables t WHERE c.object_id=t.object_id GROUP BY t.name,c.name;", cnString);
            //dAdapter1.Fill(dataSet, "Tables");
            //List<Column> column=new List<Column>();
            //foreach (DataTable dt in dataSet.Tables)
            //{
            //    string name=null;

            //   for(int i=0;i<dt.Rows.Count;i++)//(DataRow row in dt.Rows)
            //    {
            //        var tableName = dt.Rows[i].ItemArray[0].ToString();
            //        if (name == tableName||name==null)
            //        {
            //            column.Add(new Column { ColumnName = dt.Rows[i].ItemArray[1].ToString() });
            //         //   columns.Add(new ColumnModel {TableName = row.ItemArray[0].ToString(),Columns = });
            //        }else
            //        {
            //         columns.Add(new ColumnModel { TableName = name, Columns = column });
            //         column = new List<Column> { new Column { ColumnName = dt.Rows[i].ItemArray[1].ToString() } };
            //        }
            //       if(i==dt.Rows.Count-1)
            //       {
            //           columns.Add(new ColumnModel { TableName = name, Columns = column });
            //       }
            //        name = tableName;
            //    }
            //    model.Tables = columns;

            //}


//            SqlDataAdapter dAdapter1 = new SqlDataAdapter("SELECT  name FROM sys.tables;", cnString);
//            dAdapter1.Fill(dataSet, "Tables");
//            foreach (DataTable dt in dataSet.Tables)
//            {
//
//                foreach (DataRow row in dt.Rows)
//                {
//
//                    column.Add(new ColumnModel { TableName = row.ItemArray[0].ToString() });
//                  
//                }
//                model.Tables=column;
//
//            }

         
        }

        private static void PrintDataSet(DataSet dataset)
        {
            var t = dataset.Relations;
            Console.WriteLine(dataset.Relations);
            foreach (DataTable dt in dataset.Tables)
            {
                Console.WriteLine(dt.TableName);
                //foreach (DataColumn column in dt.Columns)
                //{
                //    Console.WriteLine(column.ColumnName);
                //    Type datatype = column.DataType;
                //    Console.WriteLine(column.DataType);
                //}
            }
        }
    }
}
