namespace Schema.Core.Models.Trigger
{
    using Schema.Core.Enums;

    public class TriggerModel 
    {
        public string TableName { get; set; }

        public string TrigerName { get; set; }

        public TriggerEvent? Event { get; set; }
     }
}
