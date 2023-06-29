// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Expressions;

public abstract class LogicalOperatorMatcherBase : ILogicalOperatorMatcher
{
    public abstract string OperatorKeyword { get; }
    public abstract LogicalOperator OperatorType { get; }
    public bool MatchesOperator(string expression, int index, int endIndex, int nestingLevel)
    {
        return nestingLevel == 0 && index <= endIndex - OperatorKeyword.Length && expression.Substring(index, OperatorKeyword.Length).Equals(OperatorKeyword, StringComparison.OrdinalIgnoreCase);
    }
}
