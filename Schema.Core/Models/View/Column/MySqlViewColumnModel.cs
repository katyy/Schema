namespace Schema.Core.Models.View.Column
{
    using Schema.Core.Models.Column;

    public class MySqlViewColumnModel : MySqlColumnModel
    {
        public bool IsUpdatable { get; set; }

        public string CollacationConnection { get; set; }

        public string SecurityType { get; set; }
    }
}
