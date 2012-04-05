namespace Schema.Core.Models.Trigger
{
    public interface ITriggerModel
    {
        string TableName { get; set; }
        string TrigerName { get; set; }
        string Event { get; set; }
    }
}
