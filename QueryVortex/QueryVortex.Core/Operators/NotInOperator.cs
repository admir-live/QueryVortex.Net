using SqlKata;

namespace QueryVortex.Core.Operators;

/// <summary>
///     Represents an operator that checks if the value of a column is not in a specified list of values.
/// </summary>
public class NotInOperator : ICondition
{
    private readonly string _column;
    private readonly IEnumerable<object> _values;

    public NotInOperator(string column, IEnumerable<object> values)
    {
        _column = column;
        _values = values;
    }

    /// <summary>
    ///     Applies a WHERE clause to the specified query using the NOT IN operator to check if the value of the specified
    ///     column
    ///     is not in the specified list of values.
    /// </summary>
    /// <param name="query">The query to apply the WHERE clause to.</param>
    public Query Apply(Query query)
    {
        return _values == null ? query.WhereNull(_column) : query.WhereNotIn(_column, _values);
    }
}
