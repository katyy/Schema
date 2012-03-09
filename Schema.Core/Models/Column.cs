using System;

namespace Schema.Core.Models
{
    public class Column
    {
        public string ColumnName { get; set; }
     //    public Type DataType { get; set; }//?
        public string TypeName { get; set; }
        //public string DefaultValue { get; set; }
        public int MaxLength { get; set; }
        public bool AllowNull { get; set; }
       public bool IsIdenty { get; set; }
        public int IdentyIncriment { get; set; }
    }
}
