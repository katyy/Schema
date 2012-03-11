using System.Collections.Generic;

namespace Schema.Core.Models
{
 public class DatabaseModel
 {
     public List<ColumnModel> Tables { get;set; }
  // public List<ColumnModel> Diagrams { get; set; }
    public List<ViewModel> Views { get; set; }


 }
}
