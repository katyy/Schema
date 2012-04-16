namespace Schema.Core.Reader
{
    using System.Data.Common;
    using System.Data.SqlClient;

    using Schema.Core.SqlQueries;

    public class MsSqlReader : IReader
    {
        public string DbName { get; set; }

        public string ConnectionString
        {
            get; set;

            // {
            //     //return @"Data Source=.\LOCALHOST;AttachDbFilename=" + this.DbName + ";Integrated Security=True";
            //    //return @"Data Source=SIRICHENKOE\SIRICHENKO;Initial Catalog=Petition;Integrated Security=True;";
            // }
        }

        public DbDataAdapter DataAdapter
        {
            get { return new SqlDataAdapter(); }
        }

        public DbConnection Conection
        {
            get
            {
                return new SqlConnection(this.ConnectionString);
            }
        }

        public DbCommand Command
        {
            get
            {
                return new SqlCommand();
            }
        }

        public ISqlQueries SqlQueries
        {
            get { return new MsSqlQueries(); }
        }
    }
}
