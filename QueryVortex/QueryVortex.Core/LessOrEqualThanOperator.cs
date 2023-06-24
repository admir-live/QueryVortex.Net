using SqlKata;

namespace QueryVortex.Core;

public class LessOrEqualThanOperator : ICondition
{
    private readonly string _column;
    private readonly object _value;
    public LessOrEqualThanOperator(string column, object value)
    {
        _column = column;
        _value = value;
    }
    public void Apply(Query query)
    {
        query.Where(_column, "<=", _value);
    }
}