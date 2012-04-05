namespace Schema.Core.Models.Trigger
{
    public class MsSqlTriggerModel : ITriggerModel
    {
        public string TableName { get; set; }
        public string TrigerName { get; set; }
        public string Event { get; set; }
        public string Type { get; set; }
        public string TypeDescription { get; set; }
    }
}
