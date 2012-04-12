namespace Shema.CLI
{
    using System.Data;

    using Schema.Core.Helpers;
    using Schema.Core.Reader;

    public class Program
    {
        public static void Main(string[] args)
        {
            var dataSet = new DataSet("dbDataSet");
            const string DbName = @"|DataDirectory|Parking.mdf";
            var mssqlReader = new MsSqlReader { DbName = DbName };
            var db = ModelFiller.GetModel(mssqlReader, dataSet);

            // const string MySqlDbName = @"blog";
            // var mySqlReader = new MySqlReader { DbName = MySqlDbName };
            // var db = ModelFiller.GetModel(mySqlReader, dataSet);
        }
    }
}
