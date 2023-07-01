// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using SqlKata;

namespace QueryVortex.Core.Operators;

/// <summary>
///     Represents a logical operator that combines multiple conditions using the OR operator.
/// </summary>
public class OrOperator : ICondition
{
    /// <summary>
    ///     Applies a WHERE clause to the specified query using the OR operator to combine multiple conditions.
    /// </summary>
    /// <param name="query">The query to apply the WHERE clause to.</param>
    public Query Apply(Query query)
    {
        return query.Where(query1 => query.From(query1));
    }
}
