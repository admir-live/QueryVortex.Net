using SqlKata;

namespace QueryVortex.Core;

public class GreaterThanOperator : ICondition
{
    private readonly string _column;
    private readonly object _value;
    public GreaterThanOperator(string column, object value)
    {
        _column = column;
        _value = value;
    }
    public void Apply(Query query)
    {
        query.Where(_column, ">", _value);
    }
}