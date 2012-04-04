using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.View;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.View
{
    public class MySqlViewGetter : IViewGetter
    {
        public List<IViewModel> GetView(IReader reader, DataSet dataSet, string tableName)
        {
            var view = new List<MySqlViewModel>();
            var dAdapter = reader.DataAdapter;
            dAdapter.SelectCommand = reader.Command;
            dAdapter.SelectCommand.Connection = reader.Conection;
            dAdapter.SelectCommand.CommandText = reader.SqlQueries.SelectView;
            dAdapter.Fill(dataSet, tableName);
            var dt = dataSet.Tables[tableName];
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                view.Add(new MySqlViewModel
                {
                    Name = dt.Rows[i].ItemArray[0].ToString(),
                    ViewDefinition = dt.Rows[i].ItemArray[1].ToString(),
                    IsUpdatable = Converters.ToBool(dt.Rows[i].ItemArray[2].ToString()),
                    CollacationConnection = dt.Rows[i].ItemArray[3].ToString(),
                    SecurityType = dt.Rows[i].ItemArray[4].ToString()
                });
            }

            return new List<IViewModel>(view);
        }
    }
}
