// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Models;

public class FilterCondition
{
    public FilterCondition()
    {
    }

    public FilterCondition(string fieldName, ComparisonOperator comparisonOperator, object comparisonValue, LogicalOperator logicalOperator)
    {
        FieldName = fieldName;
        ComparisonOperator = comparisonOperator;
        ComparisonValue = comparisonValue;
        LogicalOperator = logicalOperator;
    }

    /// <summary>
    ///     Gets or sets the field name for the filter condition.
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    ///     Gets or sets the comparison operator for the filter condition.
    /// </summary>
    public ComparisonOperator ComparisonOperator { get; set; }

    /// <summary>
    ///     Gets or sets the comparison value for the filter condition.
    /// </summary>
    public object ComparisonValue { get; set; }

    /// <summary>
    ///     Gets or sets the logical operator for the filter condition.
    /// </summary>
    public LogicalOperator LogicalOperator { get; set; }
}
