// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using QueryVortex.Core.Models;

namespace QueryVortex.Core;

public interface ISqlQueryParser
{
    IEnumerable<string> ParseSelectClause(string sqlQuery);
    IEnumerable<FilterCondition> ParseFilterClause(string sqlQuery);
    IEnumerable<OrderSpecification> ParseOrderByClause(string sqlQuery);
    PaginationSettings ParsePaginationClause(string sqlQuery);
    QueryParameters Parse(string sqlQuery);
}
