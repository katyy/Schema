namespace Schema.Core.Models.ProcedureFunction.Column
{
    public class MsSqlProcedureFunctionColumnModel : IProcedureFunctionColumnModel
    {
        public  string ColumnName { get; set; }
        public string Type { get; set; }
        public string TypeDescription { get; set; }
        public string DataType { get; set; }
        public int? MaxLength { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
    }
}
