using System.Data;
using Schema.Core.Helpers;
using Schema.Core.Reader;


namespace Shema.CLI
{
    class Program
    {
        static void Main (string[] args) 
        {

            var dataSet = new DataSet("dbDataSet");

            //const string dbName = @"|DataDirectory|Parking.mdf";
            //var mssqlReader = new MsSqlReader { DbName = dbName };
            //var db = ModelFiller.GetModel(mssqlReader, dataSet);


            const string mySqlDbName = @"blog";
            var mySqlReader = new MySqlReader { DbName = mySqlDbName };
            var db = ModelFiller.GetModel(mySqlReader, dataSet);
        }

        
    }
}
