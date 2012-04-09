namespace Schema.Core.Models.Trigger
{
    public class MySqlTriggerModel : ITriggerModel
    {
        public string TableName { get; set; }

        public string TrigerName { get; set; }

        public string Event { get; set; }

        public string ActionOrientation { get; set; }

        public string ActionTiming { get; set; }

        public string ActionStatement { get; set; }
    }
}
