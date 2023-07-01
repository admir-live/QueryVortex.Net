// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

namespace QueryVortex.Core.Models;

public class OrderSpecification
{
    public OrderSpecification()
    {
    }

    public OrderSpecification(string sortFieldName, SortOrderType sortOrder)
    {
        SortFieldName = sortFieldName;
        SortOrder = sortOrder;
    }

    /// <summary>
    ///     Gets or sets the sort field name for the order specification.
    /// </summary>
    public string SortFieldName { get; set; }

    /// <summary>
    ///     Gets or sets the sort order for the order specification.
    /// </summary>
    public SortOrderType SortOrder { get; set; }
}
