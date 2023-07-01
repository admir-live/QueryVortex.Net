using SqlKata;

namespace QueryVortex.Core.Operators;

/// <summary>
///     Represents an operator that checks if the value of a column is less than or equal to a specified value.
/// </summary>
public class LessOrEqualThanOperator : ICondition
{
    private readonly string _column;
    private readonly object _value;

    public LessOrEqualThanOperator(string column, object value)
    {
        _column = column;
        _value = value;
    }

    /// <summary>
    ///     Applies a WHERE clause to the specified query using the less than or equal to operator to check if the value of the
    ///     specified column
    ///     is less than or equal to the specified value.
    /// </summary>
    /// <param name="query">The query to apply the WHERE clause to.</param>
    public Query Apply(Query query)
    {
        return query.Where(_column, "<=", _value);
    }
}
