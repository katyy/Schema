namespace Schema.Core.Models.Trigger
{
    using Schema.Core.Enums;

    public class MySqlTriggerModel : ITriggerModel
    {
        public string TableName { get; set; }

        public string TrigerName { get; set; }

        public TriggerEvent? Event { get; set; }

        /*public string ActionOrientation { get; set; }

        public string ActionTiming { get; set; }

        public string ActionStatement { get; set; }*/
    }
}
