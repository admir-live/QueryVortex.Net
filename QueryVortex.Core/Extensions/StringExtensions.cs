using System;
using System.Linq;
using QueryVortex.Core.Models;

namespace QueryVortex.Core.Extensions;

/// <summary>
///     Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Removes all white spaces from the input string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The string with all white spaces removed.</returns>
    public static string RemoveWhiteSpace(this string input)
    {
        return string.IsNullOrEmpty(input)
            ? input
            : new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
    }

    /// <summary>
    ///     Converts a string representation of an operator to a corresponding ComparisonOperator enum value.
    /// </summary>
    /// <param name="operatorAlias">The string representation of the operator.</param>
    /// <returns>The ComparisonOperator enum value corresponding to the given operator.</returns>
    /// <exception cref="ArgumentException">Thrown when the operatorAlias is invalid.</exception>
    public static ComparisonOperator ToComparisonOperator(this string operatorAlias)
    {
        return operatorAlias?.ToLower() switch
        {
            "$or" => ComparisonOperator.Or,
            "in" => ComparisonOperator.In,
            "notin" => ComparisonOperator.NotIn,
            "eq" => ComparisonOperator.Equals,
            "like" => ComparisonOperator.Like,
            "neq" => ComparisonOperator.NotEquals,
            "notlike" => ComparisonOperator.NotLike,
            "lt" => ComparisonOperator.LessThan,
            "gt" => ComparisonOperator.GreaterThan,
            "lte" => ComparisonOperator.LessThanOrEqual,
            "gte" => ComparisonOperator.GreaterThanOrEqual,
            "startswith" => ComparisonOperator.StartsWith,
            "endswith" => ComparisonOperator.EndsWith,
            _ => throw new ArgumentException($"Invalid operator alias: {operatorAlias}")
        };
    }
}
