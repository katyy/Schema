using System.Data.Common;
using System.Data.SqlClient;
using Schema.Core.Models.View;
using Schema.Core.SqlQueries;

namespace Schema.Core.Reader
{
    public class MsSqlReader : IReader
    {
        public string DbName{get; set; }

        public string ConnectionString
        {
            get { return @"Data Source=.\LOCALHOST;AttachDbFilename=" + DbName + ";Integrated Security=True"; }
        }

        public DbDataAdapter DataAdapter
        {
            get { return new SqlDataAdapter(); }
        }

        public DbConnection Conection
        {
            get { return new SqlConnection(ConnectionString); }
            
        }

        public DbCommand Command
        {
            get { return  new SqlCommand();}
        }


        public ISqlQueries SqlQueries
        {
            get { return new MsSqlQueries(); }
        }

        public IViewModel ViewModel
        {
            get{return new MsSqlViewModel();}
        }
    }
}
