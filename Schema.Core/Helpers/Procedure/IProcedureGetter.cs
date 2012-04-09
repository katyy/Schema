namespace Schema.Core.Helpers.Procedure
{
    using System.Collections.Generic;
    using System.Data;

    using Schema.Core.Models.Procedure;
    using Schema.Core.Reader;

    public interface IProcedureGetter
    {
         List<IProcedureModel> GetProcedure(IReader reader, DataSet dataSet, string query, string tableName);

    }
}
