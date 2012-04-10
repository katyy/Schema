namespace Schema.Core.Models.Column
{
    public interface IColumnModel
    {
        string ColumnName { get; set; }
        string TypeName { get; set; }
        int? MaxLength { get; set; }
        bool AllowNull { get; set; }
        string IsIdentity { get; set; }
    }
}
