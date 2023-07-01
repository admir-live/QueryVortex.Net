using SqlKata;

namespace QueryVortex.Core.Operators;

/// <summary>
///     Represents an operator that checks if the value of a column ends with a specified value.
/// </summary>
public class EndsWithOperator : ICondition
{
    private readonly string _column;
    private readonly string _value;

    public EndsWithOperator(string column, string value)
    {
        _column = column;
        _value = value;
    }

    /// <summary>
    ///     Applies a WHERE clause to the specified query using the LIKE operator to check if the value of the specified column
    ///     ends with the specified value.
    /// </summary>
    /// <param name="query">The query to apply the WHERE clause to.</param>
    public Query Apply(Query query)
    {
        return query.Where(_column, "LIKE", $"%{_value}");
    }
}
