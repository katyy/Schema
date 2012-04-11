namespace Shema.CLI
{
    using System.Data;

    using Schema.Core.Helpers.Column;
    using Schema.Core.Helpers.Key;
    using Schema.Core.Models.Column;
    using Schema.Core.Names;
    using Schema.Core.Reader;

    public class Program
    {
        public static void Main(string[] args)
        {

            var dataSet = new DataSet("dbDataSet");

            const string DbName = @"|DataDirectory|Parking.mdf";
            var mssqlReader = new MsSqlReader { DbName = DbName };

            // var db = ModelFiller.GetModel(mssqlReader, dataSet);
            const string MySqlDbName = @"blog";

           var mySqlReader = new MySqlReader { DbName = MySqlDbName };

            // var db = ModelFiller.GetModel(mySqlReader, dataSet);
            var m = new MsSqlColumnGetter<MsSqlColumnModel>();
            var yu = m.GetColumn(mssqlReader, dataSet, TableNames.Tables);
            var key = new KeyGetter().GetKeys(mssqlReader, dataSet, TableNames.Keys);
        }
    }
}
