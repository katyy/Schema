namespace Schema.Core.Models.ProcedureFunction.Column
{
    public class MySqlProcedureFunctionColumnModel : IProcedureFunctionColumnModel
    {
        public string ColumnName { get; set; }
        public  string TypeDescription { get; set; }
        public string DtdIndefier { get; set; }
        public string Body { get; set; }
        public string Definition { get; set; }
        public bool IsDeterministic { get; set; }

    }
}