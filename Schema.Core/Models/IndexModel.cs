namespace Schema.Core.Models
{
    using Schema.Core.Enums;

    public class IndexModel
    {
       public string TableName { get; set; }

       public string ColumnName { get; set; }

       public string Name { get; set; }

       public IndexType? TypeDescription { get; set; }

       public bool IsUnique { get; set; }

       public SortOrder? IsDescending { get; set; }
    }
}
