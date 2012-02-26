namespace Schema.Core.Models
{
   public class IndexModel
    {
       public string Name { get; set; }
       public string DataType { get; set; }
       public int Size { get; set; }
       public bool Identy { get; set; }
       public bool AllowNulls { get; set; }
    }
}
