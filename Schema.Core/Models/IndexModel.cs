namespace Schema.Core.Models
{
   public class IndexModel
    {
       public string TableName { get; set; }
       public string ColumnName { get; set; }
       public string Name { get; set; }
       public string TypeDescription { get; set; }
       public bool Isunique { get; set; }
       public bool IsDescending { get; set; }
    }
}
