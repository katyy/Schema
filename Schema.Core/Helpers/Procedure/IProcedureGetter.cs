using System.Collections.Generic;
using System.Data;
using Schema.Core.Models.Procedure;
using Schema.Core.Reader;

namespace Schema.Core.Helpers.Procedure
{
    public interface IProcedureGetter
    {
         List<IProcedureModel> GetProcedure(IReader reader, DataSet dataSet, string query, string tableName);

    }
}
