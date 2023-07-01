using SqlKata;

namespace QueryVortex.Core;

/// <summary>
///     Represents a condition that can be applied to a query.
/// </summary>
public interface ICondition
{
    /// <summary>
    ///     Applies the condition to the specified query.
    /// </summary>
    /// <param name="query">The query to apply the condition to.</param>
    /// <returns>The modified query with the condition applied.</returns>
    Query Apply(Query query);
}
