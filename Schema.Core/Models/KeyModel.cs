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
       public string ReferanceTable { get; set; }
       public string ReferanceColumn { get; set; }
  //refferences
    }
}
