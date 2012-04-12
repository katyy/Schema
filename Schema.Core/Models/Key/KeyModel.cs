namespace Schema.Core.Models.Key
{
    using Schema.Core.Enums;

    public class KeyModel
    {
        public string ColumnName { get; set; }

        public string Name { get; set; }

        public KeyType? TypeDescription { get; set; }

        public EventRule DeletRule { get; set; }

        public EventRule UpdateRule { get; set; }

        public string ReferanceTable { get; set; }

        public string ReferanceColumn { get; set; }
    }
}
