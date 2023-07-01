// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

namespace QueryVortex.Core;

/// <summary>
///     Represents a parser for operator aliases to create corresponding conditions.
/// </summary>
public interface IOperatorParser
{
    /// <summary>
    ///     Parses the specified operator alias and creates an ICondition object.
    /// </summary>
    /// <param name="operatorAlias">The operator alias to parse.</param>
    /// <param name="column">The column name for the condition.</param>
    /// <param name="values">The array of values for the condition.</param>
    /// <returns>An ICondition object representing the parsed operator.</returns>
    ICondition ParseOperator(string operatorAlias, string column, object[] values);
}
