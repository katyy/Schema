using System;
using System.Data.Sql;

namespace Shema.CLI
{
    using System.Data;

    using Schema.Core.Helpers;
    using Schema.Core.Reader;

    using Shema.Server;

    public class Program
    {
        public static void Main(string[] args)
        {
          /*  var dataSet = new DataSet("dbDataSet");
            //const string DbName = @"|DataDirectory|Parking.mdf";
            //var db = ModelFiller.GetModel(mssqlReader, dataSet);

            const string MySqlDbName = @"blog";
            var mySqlReader = new MySqlSqlReader { DbName = MySqlDbName };
            var db = ModelFiller.GetModel(mySqlReader, dataSet);

           
            // var server= ServerGetter.GetServices();
            */
            //

            // Retrieve the enumerator instance and then the data.
            var instance = SqlDataSourceEnumerator.Instance;
            var table = instance.GetDataSources();

            // Display the contents of the table.
            DisplayData(table);

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private static void DisplayData(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                   // Console.WriteLine("test {0} {1}", table.Columns, table.Rows[2]);
                }
                Console.WriteLine("============================");
            }
        }

    }
}
