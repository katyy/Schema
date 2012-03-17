using System.Data;
using Schema.Core.Helpers;

namespace Shema.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dbName = @"|DataDirectory|Parking.mdf";
            const string cnString = @"Data Source=(local);AttachDbFilename=" + dbName + ";Integrated Security=True";
            var dataSet = new DataSet("dbDataSet");
            var db = ModelFiller.GetModel(dataSet, cnString);

        }
    }
}
