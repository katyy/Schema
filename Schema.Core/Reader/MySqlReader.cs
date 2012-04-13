namespace Schema.Core.Reader
{
    using System.Data.Common;

    using MySql.Data.MySqlClient;
    using Schema.Core.SqlQueries;

    public class MySqlReader : IReader
    {
        public string DbName { get; set; }

        public string ConnectionString
        {
            get
            {
                return @"Server=localhost;Port=3307;Database=" + this.DbName + ";User=root;Pwd=root;";
            }
        }

        public DbDataAdapter DataAdapter
        {
            get
            {
                return new MySqlDataAdapter();
            }
        }

        public DbConnection Conection
        {
            get
            {
                return new MySqlConnection(this.ConnectionString);
            }

        }

        public DbCommand Command
        {
            get
            {
                return new MySqlCommand();
            }
        }

        public ISqlQueries SqlQueries
        {
            get
            {
                return new MySqlQueries { DbName = this.DbName };
            }
        }
    }
}
