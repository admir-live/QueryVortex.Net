using SqlKata;

namespace QueryVortex.Core.Operators;

public class LessThanOperator : ICondition
{
    private readonly string _column;
    private readonly object _value;
    public LessThanOperator(string column, object value)
    {
        _column = column;
        _value = value;
    }
    public Query Apply(Query query)
    {
        if (_column is null)
        {
            throw new ArgumentNullException(nameof(_column));
        }

        return query.Where(_column, "<", _value);
    }
}
