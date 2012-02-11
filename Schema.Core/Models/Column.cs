namespace Schema.Core.Models
{
   public class Column
    {
       public string ColumnName { get; set; }
       public string DataType { get; set; }
       public string DefaultValue { get; set; }
       public int MaxLength { get; set; }
       public bool IsUnique { get; set; }
    }
}
