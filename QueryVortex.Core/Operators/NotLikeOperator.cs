using System;
using SqlKata;

namespace QueryVortex.Core.Operators;

/// <summary>
///     Represents an operator that checks if the value of a column does not match a specified pattern.
/// </summary>
public class NotLikeOperator : ICondition
{
    private readonly string _column;
    private readonly string _value;

    public NotLikeOperator(string column, string value)
    {
        _column = column ?? throw new ArgumentNullException(nameof(column));
        _value = value;
    }

    /// <summary>
    ///     Applies a WHERE clause to the specified query using the NOT LIKE operator to check if the value of the specified
    ///     column
    ///     does not match the specified pattern.
    /// </summary>
    /// <param name="query">The query to apply the WHERE clause to.</param>
    public Query Apply(Query query)
    {
        return query.Where(_column, "NOT LIKE", _value);
    }
}
