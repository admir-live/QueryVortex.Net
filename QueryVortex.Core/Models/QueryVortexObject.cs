// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

namespace QueryVortex.Core.Models;

public sealed class QueryVortexObject
{
    public QueryVortexObject()
    {
        SelectedFields = new List<string>();
        FilterConditions = new List<FilterCondition>();
        SortingOrders = new List<OrderSpecification>();
        Pagination = new PaginationSettings();
    }

    public string TableName { get; set; }

    /// <summary>
    ///     Gets or sets the list of selected fields in the query.
    /// </summary>
    public List<string> SelectedFields { get; set; }

    /// <summary>
    ///     Gets or sets the list of filter conditions in the query.
    /// </summary>
    public List<FilterCondition> FilterConditions { get; set; }

    /// <summary>
    ///     Gets or sets the list of sorting orders in the query.
    /// </summary>
    public List<OrderSpecification> SortingOrders { get; set; }

    /// <summary>
    ///     Gets or sets the pagination settings in the query.
    /// </summary>
    public PaginationSettings Pagination { get; set; }
}
