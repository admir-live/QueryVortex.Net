// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using QueryVortex.Core.Models;

namespace QueryVortex.Core;

public interface IQueryStringParserStrategy
{
    IDictionary<string, string[]> ParseQueryString(string queryString);
    List<string> GetSelectFields(IDictionary<string, string[]> queryParameters);
    List<Filter> GetFilters(IDictionary<string, string[]> queryParameters);
    List<Order> GetOrders(IDictionary<string, string[]> queryParameters);
}
