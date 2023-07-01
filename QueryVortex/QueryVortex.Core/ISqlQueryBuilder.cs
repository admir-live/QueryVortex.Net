// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using QueryVortex.Core.Models;

namespace QueryVortex.Core;

public interface ISqlQueryBuilder
{
    /// <summary>
    ///     Creates an SQL query based on the provided QueryVortexObject blueprint.
    /// </summary>
    /// <param name="blueprint">The QueryVortexObject blueprint.</param>
    /// <returns>An instance of SqlQuery representing the generated SQL query.</returns>
    SqlQuery CreateSqlQuery(QueryVortexObject blueprint);
}
