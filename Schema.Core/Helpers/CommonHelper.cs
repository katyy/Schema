// -----------------------------------------------------------------------
// <copyright file="CommonHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Schema.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using Schema.Core.Reader;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CommonHelper
    {
        public static void SetDataAdapterSettings(IReader reader, string query, DataSet dataSet, string dataSetTableName)
        {
            using (var dataAdapter = reader.DataAdapter)
            {
                dataAdapter.SelectCommand = reader.Command;
                dataAdapter.SelectCommand.Connection = reader.Conection;
                dataAdapter.SelectCommand.CommandText = query;
                dataAdapter.Fill(dataSet, dataSetTableName);
            }
        }
    }
}
