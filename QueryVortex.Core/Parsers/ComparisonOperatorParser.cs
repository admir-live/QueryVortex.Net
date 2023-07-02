using System;
using System.Collections.Generic;
using QueryVortex.Core.Models;
using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Parsers;

/// <summary>
///     Represents a parser for operator aliases to create corresponding conditions.
/// </summary>
public sealed class ComparisonOperatorParser : IComparisonOperatorParser
{
    /// <summary>
    ///     Initializes a new instance of the OperatorParser class.
    /// </summary>
    public ComparisonOperatorParser()
    {
        OperatorAliases = InitializeOperatorAliases();
    }

    /// <summary>
    ///     Gets the dictionary of operator aliases mapped to operator factory methods.
    /// </summary>
    public Dictionary<ComparisonOperator, Func<string, object[], ICondition>> OperatorAliases { get; }

    /// <summary>
    ///     Parses the specified operator alias and creates an ICondition object.
    /// </summary>
    /// <param name="operatorAlias">The operator alias to parse.</param>
    /// <param name="column">The column name for the condition.</param>
    /// <param name="values">The array of values for the condition.</param>
    /// <returns>An ICondition object representing the parsed operator.</returns>
    public ICondition ParseComparisonOperator(ComparisonOperator operatorAlias, string column, object[] values)
    {
        if (OperatorAliases.TryGetValue(operatorAlias, out var operatorFactory))
        {
            return CreateOperator(operatorFactory, column, values);
        }

        throw new ArgumentException($"Invalid operator: {operatorAlias}");
    }

    private static Dictionary<ComparisonOperator, Func<string, object[], ICondition>> InitializeOperatorAliases()
    {
        var aliases = new Dictionary<ComparisonOperator, Func<string, object[], ICondition>>();

        AddAlias(aliases, ComparisonOperator.Or, (column, values) => new OrOperator());
        AddAlias(aliases, ComparisonOperator.In, (column, values) => new InOperator(column, values));
        AddAlias(aliases, ComparisonOperator.NotIn, (column, values) => new NotInOperator(column, values));
        AddAlias(aliases, ComparisonOperator.Equals, (column, values) => new EqualOperator(column, values[0]));
        AddAlias(aliases, ComparisonOperator.Like, (column, values) => new LikeOperator(column, values[0].ToString()));
        AddAlias(aliases, ComparisonOperator.NotEquals, (column, values) => new NotEqualOperator(column, values[0]));
        AddAlias(aliases, ComparisonOperator.NotLike, (column, values) => new NotLikeOperator(column, values[0].ToString()));
        AddAlias(aliases, ComparisonOperator.LessThan, (column, values) => new LessThanOperator(column, values[0]));
        AddAlias(aliases, ComparisonOperator.GreaterThan, (column, values) => new GreaterThanOperator(column, values[0]));
        AddAlias(aliases, ComparisonOperator.LessThanOrEqual, (column, values) => new LessOrEqualThanOperator(column, values[0]));
        AddAlias(aliases, ComparisonOperator.GreaterThanOrEqual, (column, values) => new GreaterOrEqualThanOperator(column, values[0]));
        AddAlias(aliases, ComparisonOperator.StartsWith, (column, values) => new StartsWithOperator(column, values[0].ToString()));
        AddAlias(aliases, ComparisonOperator.EndsWith, (column, values) => new EndsWithOperator(column, values[0].ToString()));

        return aliases;
    }

    private static void AddAlias(Dictionary<ComparisonOperator, Func<string, object[], ICondition>> aliases, ComparisonOperator alias, Func<string, object[], ICondition> factory)
    {
        aliases[alias] = factory;
    }

    private static void ValidateValues(object[] values)
    {
        if (values == null || values.Length == 0)
        {
            throw new ArgumentException("Values array cannot be null or empty.", nameof(values));
        }
    }

    private static ICondition CreateOperator<T>(Func<string, object[], T> operatorFactory, string column, object[] values) where T : ICondition
    {
        try
        {
            ValidateValues(values);
            return operatorFactory(column, values);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Failed to create operator.", ex);
        }
    }
}
