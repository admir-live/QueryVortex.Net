namespace QueryVortex.Core;

public static class OperatorParser
{
    private static readonly Dictionary<string, Func<string, object[], ICondition>> OperatorAliases =
        InitializeOperatorAliases();

    private static Dictionary<string, Func<string, object[], ICondition>> InitializeOperatorAliases()
    {
        return new Dictionary<string, Func<string, object[], ICondition>>
        {
            { "in", CreateInOperator },
            { "notin", CreateNotInOperator },
            { "eq", CreateEqualOperator },
            { "like", CreateLikeOperator },
            { "neq", CreateNotEqualOperator },
            { "notlike", CreateNotLikeOperator },
            { "lt", CreateLessThanOperator },
            { "gt", CreateGreaterThanOperator },
            { "lte", CreateLessOrEqualThanOperator },
            { "gte", CreateGreaterOrEqualThanOperator },
            { "startswith", CreateStartsWithOperator },
            { "endswith", CreateEndsWithOperator }
        };
    }

    private static void ValidateValues(object[] values)
    {
        if (values == null || values.Length == 0)
            throw new ArgumentException("Values array cannot be null or empty.", nameof(values));
    }

    private static ICondition CreateOperator(Func<string, object[], ICondition> operatorFactory, string column,
        object[] values)
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

    private static ICondition CreateStartsWithOperator(string column, object[] values)
    {
        return CreateOperator(
            (columnValue, parameterValue) => new StartsWithOperator(columnValue, parameterValue[0].ToString()), column,
            values);
    }

    private static ICondition CreateEndsWithOperator(string column, object[] values)
    {
        return CreateOperator(
            (columnValue, parameterValue) => new EndsWithOperator(columnValue, parameterValue[0].ToString()), column,
            values);
    }

    private static ICondition CreateInOperator(string column, object[] values)
    {
        return CreateOperator((columnValue, parameterValue) => new InOperator(columnValue, parameterValue), column,
            values);
    }

    private static ICondition CreateNotInOperator(string column, object[] values)
    {
        return CreateOperator((columnValue, parameterValue) => new NotInOperator(columnValue, parameterValue), column,
            values);
    }

    private static ICondition CreateEqualOperator(string column, object[] values)
    {
        return CreateOperator((columnValue, parameterValue) => new EqualOperator(columnValue, parameterValue[0]),
            column, values);
    }

    private static ICondition CreateLikeOperator(string column, object[] values)
    {
        return CreateOperator(
            (columnValue, parameterValue) => new LikeOperator(columnValue, parameterValue[0].ToString()), column,
            values);
    }

    private static ICondition CreateNotEqualOperator(string column, object[] values)
    {
        return CreateOperator((columnValue, parameterValue) => new NotEqualOperator(columnValue, parameterValue[0]),
            column, values);
    }

    private static ICondition CreateNotLikeOperator(string column, object[] values)
    {
        return CreateOperator(
            (columnValue, parameterValue) => new NotLikeOperator(columnValue, parameterValue[0].ToString()), column,
            values);
    }

    private static ICondition CreateLessThanOperator(string column, object[] values)
    {
        return CreateOperator((columnValue, parameterValue) => new LessThanOperator(columnValue, parameterValue[0]),
            column, values);
    }

    private static ICondition CreateGreaterThanOperator(string column, object[] values)
    {
        return CreateOperator((columnValue, parameterValue) => new GreaterThanOperator(columnValue, parameterValue[0]),
            column, values);
    }

    private static ICondition CreateLessOrEqualThanOperator(string column, object[] values)
    {
        return CreateOperator(
            (columnValue, parameterValue) => new LessOrEqualThanOperator(columnValue, parameterValue[0]), column,
            values);
    }

    private static ICondition CreateGreaterOrEqualThanOperator(string column, object[] values)
    {
        return CreateOperator(
            (columnValue, parameterValue) => new GreaterOrEqualThanOperator(columnValue, parameterValue[0]), column,
            values);
    }

    public static ICondition ParseOperator(string operatorAlias, string column, object[] values)
    {
        if (OperatorAliases.TryGetValue(operatorAlias.ToLowerInvariant(), out var operatorFactory))
            return CreateOperator(operatorFactory, column, values);

        throw new ArgumentException($"Invalid operator: {operatorAlias}");
    }
}