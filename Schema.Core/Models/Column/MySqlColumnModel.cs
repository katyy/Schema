namespace Schema.Core.Models.Column
{
   public class MySqlColumnModel:IColumnModel
    {
        public string ColumnName { get; set; }
        public string TypeName { get; set; }
        public int? MaxLength { get; set; }
        public bool AllowNull { get; set; }
        public string IsIdenty { get; set; }
    }
}
