using System;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Schema.Core.SqlQueries;

namespace Schema.Core.Reader
{
    public class MySqlReader :IReader
    {
        public string DbName { get;set;}

        public string ConnectionString
        {
            get { return @"Server=localhost;Port=3306;Database="+DbName+";User=root; "; }
        }

        public DbDataAdapter DataAdapter
        {
            get { return new MySqlDataAdapter(); }
        }

        public DbConnection Conection
        {
            get { return new MySqlConnection(ConnectionString); }
           
        }

        public DbCommand Command
        {
            get {return new MySqlCommand();}
        }

        public ISqlQueries SqlQueries
        {
            get {return new MySqlQueries();}
        }
    }
}
