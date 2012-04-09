namespace Shema.CLI
{
    using System.Data;
   
    using Schema.Core.Helpers;
    using Schema.Core.Helpers.Column;
    using Schema.Core.Models.Column;
    using Schema.Core.Reader;

    public class Program
    {
        public static void Main(string[] args)
        {

            var dataSet = new DataSet("dbDataSet");

            const string dbName = @"|DataDirectory|Parking.mdf";
            var mssqlReader = new MsSqlReader { DbName = dbName };
            //var db = ModelFiller.GetModel(mssqlReader, dataSet);


            const string MySqlDbName = @"blog";

           var mySqlReader = new MySqlReader { DbName = MySqlDbName };

           // var db = ModelFiller.GetModel(mySqlReader, dataSet);

            MsSqlColumnGetter<MsSqlColumnModel> m=new MsSqlColumnGetter<MsSqlColumnModel>();
           var yu= m.GetColumn(mssqlReader, dataSet, "dfds");
        }
    }
}
