namespace Schema.Core.Models.Trigger
{
    using Schema.Core.Enums;

    public interface ITriggerModel
    {
        string TableName { get; set; }

        string TrigerName { get; set; }

        TriggerEvent? Event { get; set; }
    }
}
