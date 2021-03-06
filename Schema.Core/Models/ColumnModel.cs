﻿namespace Schema.Core.Models
{
    public class ColumnModel 
    {
        public string ColumnName { get; set; }

        public string TypeName { get; set; }

        public int? MaxLength { get; set; }

        public bool AllowNull { get; set; }

        public string IsIdentity { get; set; }
     }
}
