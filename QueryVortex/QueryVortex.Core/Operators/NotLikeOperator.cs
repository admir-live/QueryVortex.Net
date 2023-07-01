using SqlKata;

namespace QueryVortex.Core.Operators;

public class NotLikeOperator : ICondition
{
    private readonly string _column;
    private readonly string _value;
    public NotLikeOperator(string column, string value)
    {
        _column = column ?? throw new ArgumentNullException(nameof(column));
        _value = value;
    }
    public Query Apply(Query query)
    {
        return query.Where(_column, "NOT LIKE", _value);
    }
}
