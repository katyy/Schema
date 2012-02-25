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
      
       public bool IsIdenty { get; set; }
       public int IdentyIncriment { get; set; }
       public Rule DeletRule { get; set; }
       public Rule UpdateRule { get; set; }
     //  public bool IsPrimaryKey { get; set; }
    }
}
