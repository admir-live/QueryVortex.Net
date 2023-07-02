using System;
using System.Collections.Generic;
using QueryVortex.Core.Models;

namespace QueryVortex.Core;

public interface IComparisonOperatorParser
{
    Dictionary<ComparisonOperator, Func<string, object[], ICondition>> OperatorAliases { get; }

    /// <summary>
    ///     Parses the specified operator alias and creates an ICondition object.
    /// </summary>
    /// <param name="operatorAlias">The operator alias to parse.</param>
    /// <param name="column">The column name for the condition.</param>
    /// <param name="values">The array of values for the condition.</param>
    /// <returns>An ICondition object representing the parsed operator.</returns>
    ICondition ParseComparisonOperator(ComparisonOperator operatorAlias, string column, object[] values);
}
