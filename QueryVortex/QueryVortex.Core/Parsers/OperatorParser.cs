using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Parsers;

public class OperatorParser : IOperatorParser
{
    public OperatorParser()
    {
        OperatorAliases = InitializeOperatorAliases();
    }
    private Dictionary<string, Func<string, object[], ICondition>> OperatorAliases { get; }

    public ICondition ParseOperator(string operatorAlias, string column, object[] values)
    {
        if (OperatorAliases.TryGetValue(operatorAlias, out var operatorFactory))
        {
            return CreateOperator(operatorFactory, column, values);
        }

        throw new ArgumentException($"Invalid operator: {operatorAlias}");
    }

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

    private static void AddAlias(Dictionary<string, Func<string, object[], ICondition>> aliases, string alias, Func<string, object[], ICondition> factory)
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
