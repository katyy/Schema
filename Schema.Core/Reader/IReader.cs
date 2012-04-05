using System.Data.Common;
using Schema.Core.Helpers.Column;
using Schema.Core.Helpers.Key;
using Schema.Core.Helpers.Procedure;
using Schema.Core.Helpers.Trigger;
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
        IProcedureGetter ProcedureFunctionMethod { get; }
        ITriggerGetter TriggerMethod { get; }
        KeyGetter KeyMethod { get; }
        IColumnGetter ColumnMethod { get; }

    }
    
}
