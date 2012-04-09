namespace Schema.Core.SqlQueries
{
    public interface ISqlQueries
    {
        string SelectColumn { get; }

        string SelectKey { get; }

        // string SelectFk { get; }
        string SelectTrigger { get; }

        string SelectIndex { get; }

        string SelectView { get; }

        string SelectProcedure { get; }

        string SelectFunction { get; }
    }
}
