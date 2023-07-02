// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using System.Collections.Generic;
using QueryVortex.Core.Models;

namespace QueryVortex.Core;

/// <summary>
///     Represents a parser for extracting information from an SQL query.
/// </summary>
public interface IQueryVortexParser
{
    /// <summary>
    ///     Parses the SELECT clause of an SQL query and returns the list of selected column names.
    /// </summary>
    /// <param name="sqlQuery">The SQL query to parse.</param>
    /// <returns>The list of selected column names.</returns>
    IEnumerable<string> ParseSelectClause(string sqlQuery);

    /// <summary>
    ///     Parses the WHERE clause (filter conditions) of an SQL query and returns the list of filter conditions.
    /// </summary>
    /// <param name="sqlQuery">The SQL query to parse.</param>
    /// <returns>The list of filter conditions.</returns>
    IEnumerable<FilterCondition> ParseFilterClause(string sqlQuery);

    /// <summary>
    ///     Parses the ORDER BY clause of an SQL query and returns the list of order specifications.
    /// </summary>
    /// <param name="sqlQuery">The SQL query to parse.</param>
    /// <returns>The list of order specifications.</returns>
    IEnumerable<OrderSpecification> ParseOrderByClause(string sqlQuery);

    /// <summary>
    ///     Parses the pagination clause of an SQL query and returns the pagination settings.
    /// </summary>
    /// <param name="sqlQuery">The SQL query to parse.</param>
    /// <returns>The pagination settings.</returns>
    PaginationSettings ParsePaginationClause(string sqlQuery);

    /// <summary>
    ///     Parses an SQL query and returns the extracted query parameters.
    /// </summary>
    /// <param name="sqlQuery">The SQL query to parse.</param>
    /// <param name="tableName">The SQL table name.</param>
    /// <returns>The extracted query parameters.</returns>
    QueryVortexObject Parse(string sqlQuery, string tableName = "YourTableName");
}
