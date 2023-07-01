// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

namespace QueryVortex.Core.Models;

public class PaginationSettings
{
    public PaginationSettings()
    {
    }

    public PaginationSettings(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    ///     Gets or sets the page number for the pagination settings.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    ///     Gets or sets the page size for the pagination settings.
    /// </summary>
    public int PageSize { get; set; }
}
