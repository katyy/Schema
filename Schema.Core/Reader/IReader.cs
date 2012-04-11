namespace Schema.Core.Reader
{
    using System.Data.Common;

    using Schema.Core.SqlQueries;

    public interface IReader
    {
        string DbName { get; set; }

        string ConnectionString { get; }

        DbDataAdapter DataAdapter { get; }

        DbConnection Conection { get; }

        DbCommand Command { get; }

        ISqlQueries SqlQueries { get; }

        // IViewModel ViewModel { get; }
        // IViewGetter ViewMethod { get; }
        // IProcedureGetter ProcedureFunctionMethod { get; }
        // ITriggerGetter TriggerMethod { get; }
        // KeyGetter KeyMethod { get; }
        // IColumnGetter ColumnMethod { get; }
    }

}
