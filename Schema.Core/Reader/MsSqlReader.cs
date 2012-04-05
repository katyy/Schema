using System.Data.Common;
using System.Data.SqlClient;
using Schema.Core.Helpers.Column;
using Schema.Core.Helpers.Key;
using Schema.Core.Helpers.Procedure;
using Schema.Core.Helpers.Trigger;
using Schema.Core.Helpers.View;
using Schema.Core.Models.Column;
using Schema.Core.Models.Table;
using Schema.Core.Models.View;
using Schema.Core.SqlQueries;

namespace Schema.Core.Reader
{
    public class MsSqlReader : IReader
    {
        public string DbName{get; set; }

        public string ConnectionString
        {
            get
            {
                return// @"Data Source=SIRICHENKOE\SIRICHENKO;Initial Catalog=Petition;Integrated Security=True;"
                    @"Data Source=.\LOCALHOST;AttachDbFilename=" + DbName + ";Integrated Security=True"
                    ; }
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

        public IViewGetter ViewMethod
        {
            get {return  new MsSqlViewGetter(); }
        }

        public IProcedureGetter ProcedureFunctionMethod
        {
            get { return  new MsSqlProcedureGetter(); }
        }

        public ITriggerGetter TriggerMethod
        {
            get { return new MsSqlTriggerGetter(); }
        }

        public KeyGetter KeyMethod
        {
            get { return new MsSqlKeyGetter(); }
        }

        public IColumnGetter ColumnMethod
        {
            get { return new MsSqlColumnGetter<MsSqlColumnModel>(); }
        }
    }
}
