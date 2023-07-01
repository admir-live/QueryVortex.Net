using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Parsers;

/// <summary>
///     Represents a parser for operator aliases to create corresponding conditions.
/// </summary>
public sealed class OperatorParser : IOperatorParser
{
    /// <summary>
    ///     Initializes a new instance of the OperatorParser class.
    /// </summary>
    public OperatorParser()
    {
        OperatorAliases = InitializeOperatorAliases();
    }

    /// <summary>
    ///     Gets the dictionary of operator aliases mapped to operator factory methods.
    /// </summary>
    private Dictionary<string, Func<string, object[], ICondition>> OperatorAliases { get; }

    /// <summary>
    ///     Parses the specified operator alias and creates an ICondition object.
    /// </summary>
    /// <param name="operatorAlias">The operator alias to parse.</param>
    /// <param name="column">The column name for the condition.</param>
    /// <param name="values">The array of values for the condition.</param>
    /// <returns>An ICondition object representing the parsed operator.</returns>
    public ICondition ParseOperator(string operatorAlias, string column, object[] values)
    {
        if (OperatorAliases.TryGetValue(operatorAlias, out var operatorFactory))
        {
            return CreateOperator(operatorFactory, column, values);
        }

        throw new ArgumentException($"Invalid operator: {operatorAlias}");
    }

    /// <summary>
    ///     Initializes the dictionary of operator aliases and their corresponding factory methods.
    /// </summary>
    /// <returns>The initialized dictionary of operator aliases.</returns>
    private static Dictionary<string, Func<string, object[], ICondition>> InitializeOperatorAliases()
    {
        var aliases = new Dictionary<string, Func<string, object[], ICondition>>();

        AddAlias(aliases, "$OR", (column, values) => new OrOperator());
        AddAlias(aliases, "in", (column, values) => new InOperator(column, values));
        AddAlias(aliases, "notin", (column, values) => new NotInOperator(column, values));
        AddAlias(aliases, "eq", (column, values) => new EqualOperator(column, values[0]));
        AddAlias(aliases, "like", (column, values) => new LikeOperator(column, values[0].ToString()));
        AddAlias(aliases, "neq", (column, values) => new NotEqualOperator(column, values[0]));
        AddAlias(aliases, "notlike", (column, values) => new NotLikeOperator(column, values[0].ToString()));
        AddAlias(aliases, "lt", (column, values) => new LessThanOperator(column, values[0]));
        AddAlias(aliases, "gt", (column, values) => new GreaterThanOperator(column, values[0]));
        AddAlias(aliases, "lte", (column, values) => new LessOrEqualThanOperator(column, values[0]));
        AddAlias(aliases, "gte", (column, values) => new GreaterOrEqualThanOperator(column, values[0]));
        AddAlias(aliases, "startswith", (column, values) => new StartsWithOperator(column, values[0].ToString()));
        AddAlias(aliases, "endswith", (column, values) => new EndsWithOperator(column, values[0].ToString()));

        return aliases;
    }

    /// <summary>
    ///     Adds an operator alias and its corresponding factory method to the aliases dictionary.
    /// </summary>
    /// <param name="aliases">The dictionary of operator aliases.</param>
    /// <param name="alias">The operator alias to add.</param>
    /// <param name="factory">The factory method to create the operator.</param>
    private static void AddAlias(Dictionary<string, Func<string, object[], ICondition>> aliases, string alias, Func<string, object[], ICondition> factory)
    {
        aliases[alias] = factory;
    }

    /// <summary>
    ///     Validates the values array to ensure it is not null or empty.
    /// </summary>
    /// <param name="values">The array of values to validate.</param>
    private static void ValidateValues(object[] values)
    {
        if (values == null || values.Length == 0)
        {
            throw new ArgumentException("Values array cannot be null or empty.", nameof(values));
        }
    }

    /// <summary>
    ///     Creates an operator instance of type T using the provided factory method, column, and values.
    /// </summary>
    /// <typeparam name="T">The type of the operator.</typeparam>
    /// <param name="operatorFactory">The factory method to create the operator.</param>
    /// <param name="column">The column name for the operator.</param>
    /// <param name="values">The array of values for the operator.</param>
    /// <returns>An instance of type T representing the created operator.</returns>
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
