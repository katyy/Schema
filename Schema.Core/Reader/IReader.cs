﻿using System.Data.Common;
using Schema.Core.Helpers.ProcedureFunction;
using Schema.Core.Helpers.View;
using Schema.Core.Models.View;
using Schema.Core.SqlQueries;

namespace Schema.Core.Reader
{
    public interface IReader
    {
        string DbName { get; set; }
        string ConnectionString { get; }
        DbDataAdapter DataAdapter { get; }
        DbConnection Conection { get;}
        DbCommand Command { get; }
        ISqlQueries SqlQueries { get; }
        IViewModel ViewModel { get; }
        IViewGetter ViewMethod { get; }
        IProcedureFunctionGetter ProcedureFunctionMethod { get; }

    }
    
}
