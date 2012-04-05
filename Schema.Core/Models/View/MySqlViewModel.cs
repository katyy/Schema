namespace Schema.Core.Models.View
{
    public class MySqlViewModel : IViewModel
    {
        public string Name { get; set; }
       // public string ViewDefinition { get; set; }
        public bool IsUpdatable { get; set; }
        public string CollacationConnection { get; set; }
        public string SecurityType { get; set; }
    }
}
