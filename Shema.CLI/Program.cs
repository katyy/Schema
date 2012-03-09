using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Schema.Core.Helpers;
using Schema.Core.Models;

namespace Shema.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            const string cnString = @"Data Source=.\LOCALHOST;AttachDbFilename=d:\App_data\Cars\Cars.UserInterface\App_Data\Parking.mdf;Integrated Security=True";
            var dataSet = new DataSet("dbDataSet");
           var model = ModelHelper.GetColumn(dataSet, cnString,"Tables");
           var keyModel = ModelHelper.GetKeys(dataSet, cnString, "Keys");
           var forigenKey = ModelHelper.GetForigenKey(dataSet, cnString, "ForigenKey");
            var trigers = ModelHelper.GetTrigers(dataSet, cnString, "Trigers");
        }

    }
}
