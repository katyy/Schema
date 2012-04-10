namespace Schema.Core.Models.Trigger
{
    using Schema.Core.Enums;

    public class MsSqlTriggerModel : ITriggerModel
    {
        public string TableName { get; set; }

        public string TrigerName { get; set; }

        public TriggerEvent? Event { get; set; }

      /*  public string Type { get; set; }

        public string TypeDescription { get; set; }*/
    }
}
