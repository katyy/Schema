namespace Schema.Core.Models
{
    using Schema.Core.Enums;

    public class TriggerModel 
    {
        public string TrigerName { get; set; }

        public TriggerEvent? Event { get; set; }
     }
}
