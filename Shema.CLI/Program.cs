using System.Data;
using Schema.Core.Helpers;

namespace Shema.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dbName = @"d:\App_data\Cars\Cars.UserInterface\App_Data\Parking.mdf";
            const string cnString = @"Data Source=.\LOCALHOST;AttachDbFilename=" + dbName + ";Integrated Security=True";
            var dataSet = new DataSet("dbDataSet");
            var db = Insert.GetModel(dataSet, cnString);

        }
    }
}
