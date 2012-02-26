using Schema.Core.Enums;

namespace Schema.Core.Models
{
   public class KeyModel
    {
       public string TableName { get; set; }
       public string ColumnName { get; set; }
       public string Type { get; set; }
       public string Name { get; set; }
       public string TypeDescription { get; set; }
      
      
       public string DeletRule { get; set; }
       public string UpdateRule { get; set; }
  //refferences
    }
}
