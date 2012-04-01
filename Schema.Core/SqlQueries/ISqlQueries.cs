namespace Schema.Core.SqlQueries
{
    public interface ISqlQueries
    {
        string SelectColumn { get; }
        string SelectPk { get; }
        string SelectFk { get; }
        string SelectTrigger { get; }
        string SelectIndex { get; }
        string SelectView { get; }
        string SelectProcedure { get; }
        string SelectViewTriggers { get; }
        string SelectViewIndexes { get; }
        string SelectFunction { get; }
    }
}
