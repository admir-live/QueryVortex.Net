using SqlKata;

namespace QueryVortex.Core.Operators;

/// <summary>
///     Represents an operator that checks if the value of a column is in a specified list of values.
/// </summary>
public class InOperator : ICondition
{
    private readonly string _column;
    private readonly IEnumerable<object> _values;

    public InOperator(string column, IEnumerable<object> values)
    {
        _column = column;
        _values = values;
    }

    /// <summary>
    ///     Applies a WHERE clause to the specified query using the IN operator to check if the value of the specified column
    ///     is in the specified list of values.
    /// </summary>
    /// <param name="query">The query to apply the WHERE clause to.</param>
    public Query Apply(Query query)
    {
        return query.WhereIn(_column, _values);
    }
}
