using System.Collections.Generic;
using System.Data;
using Schema.Core;
using Schema.Core.Helpers;
using Schema.Core.Models;
using Schema.Core.Reader;


namespace Shema.CLI
{
    class Program
    {
        static void Main(string[] args)
        {

            var dataSet = new DataSet("dbDataSet");

            //const string dbName = @"|DataDirectory|Parking.mdf";
            //var mssqlReader = new MsSqlReader { DbName = dbName };
            //var db = ModelFiller.GetModel(mssqlReader, dataSet);


            const string mySqlDbName = @"blog";
            var mySqlReader = new MySqlReader { DbName = mySqlDbName };

            var column = ModelsGetter.GetColumn(mySqlReader, dataSet, new List<TableModel>(), TableNames.Tables);
            var forigenKey = ModelsGetter.GetKeys(mySqlReader, dataSet, TableNames.ForigenKey);
            var triggers = ModelsGetter.GetTriggers(mySqlReader, dataSet, TableNames.Triggers);
            var index = ModelsGetter.GetIndexes(mySqlReader, dataSet, TableNames.Indexes);
        }
    }
}
