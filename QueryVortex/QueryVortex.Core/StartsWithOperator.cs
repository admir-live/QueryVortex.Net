using SqlKata;

namespace QueryVortex.Core;

public class StartsWithOperator : ICondition
{
    private readonly string _column;
    private readonly string _value;

    public StartsWithOperator(string column, string value)
    {
        _column = column;
        _value = value;
    }

    public void Apply(Query query)
    {
        query.Where(_column, "LIKE", $"{_value}%");
    }
}
