using System;
using System.Collections.Generic;
using System.Text;

namespace Schema.Core.Models.Procedure
{
    public class MySqlProcedureModel:IProcedureModel
    {
        public string Name { get; set; }
        public string ColumnName { get; set; }
        public string TypeDescription { get; set; }
        public string DtdIndefier { get; set; }
        public string Body { get; set; }
        public string Definition { get; set; }
        public bool IsDeterministic { get; set; }

    }
}
