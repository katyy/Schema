namespace Schema.Core.Reader
{
    using System.Data.Common;

    using Schema.Core.SqlQueries;

    public interface IReader
    {
        string DbName { get; set; }

        string ConnectionString { get; set; }

        DbDataAdapter DataAdapter { get; }

        DbConnection Conection { get; }

        DbCommand Command { get; }

        ISqlQueries SqlQueries { get; }
    }
}
