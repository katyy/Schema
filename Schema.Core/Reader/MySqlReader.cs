using System.Data.Common;
using MySql.Data.MySqlClient;
using Schema.Core.Helpers.Column;
using Schema.Core.Helpers.Key;
using Schema.Core.Helpers.Procedure;
using Schema.Core.Helpers.Trigger;
using Schema.Core.Helpers.View;
using Schema.Core.Models.Column;
using Schema.Core.Models.View;
using Schema.Core.Models.View.Column;
using Schema.Core.SqlQueries;

namespace Schema.Core.Reader
{
    public class MySqlReader : IReader
    {
        public string DbName { get; set; }

        public string ConnectionString
        {
            get { return @"Server=localhost;Port=3306;Database=" + DbName + ";User=root; "; }
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
            get { return new MySqlCommand(); }
        }

        public ISqlQueries SqlQueries
        {
            get { return new MySqlQueries { DbName = DbName }; }
        }

        public IViewModel ViewModel
        {
            get { return new MySqlViewModel(); }
        }

        public IViewGetter ViewMethod
        {
            get { return new MySqlViewGetter<MySqlViewColumnModel>(); }
        }

        public IProcedureGetter ProcedureFunctionMethod
        {
            get { return  new MySqlProcedureGetter(); }
        }

        public ITriggerGetter TriggerMethod
        {
            get { return new MySqlTriggerGetter(); }
        }

        public KeyGetter KeyMethod
        {
            get {return new MySqlKeyGetter(); }
        }

        public IColumnGetter ColumnMethod
        {
            get { return new MySqlColumnGetter<MySqlColumnModel>();}
        }
    }
}
