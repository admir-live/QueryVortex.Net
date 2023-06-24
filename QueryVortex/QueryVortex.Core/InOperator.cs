using SqlKata;

namespace QueryVortex.Core;

public class InOperator : ICondition
{
    private readonly string _column;
    private readonly IEnumerable<object> _values;
    public InOperator(string column, IEnumerable<object> values)
    {
        _column = column;
        _values = values;
    }
    public void Apply(Query query)
    {
        query.WhereIn(_column, _values);
    }
}