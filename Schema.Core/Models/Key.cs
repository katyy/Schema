using Schema.Core.Enums;

namespace Schema.Core.Models
{
   public class Key
    {
       public string Name { get; set; }
       public bool IsIdenty { get; set; }
       public int IdentyIncriment { get; set; }
       public string DataType { get; set; }
       public string TableName { get; set; }
       public string ColumnName { get; set; }
       public Rule DeletRule { get; set; }
       public Rule UpdateRule { get; set; }
    }
}
