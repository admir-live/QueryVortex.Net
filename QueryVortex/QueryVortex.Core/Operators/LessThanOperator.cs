using SqlKata;

namespace QueryVortex.Core.Operators;

/// <summary>
///     Represents an operator that checks if the value of a column is less than a specified value.
/// </summary>
public class LessThanOperator : ICondition
{
    private readonly string _column;
    private readonly object _value;

    public LessThanOperator(string column, object value)
    {
        _column = column;
        _value = value;
    }

    /// <summary>
    ///     Applies a WHERE clause to the specified query using the less than operator to check if the value of the specified
    ///     column
    ///     is less than the specified value.
    /// </summary>
    /// <param name="query">The query to apply the WHERE clause to.</param>
    public Query Apply(Query query)
    {
        if (_column is null)
        {
            throw new ArgumentNullException(nameof(_column));
        }

        return query.Where(_column, "<", _value);
    }
}
