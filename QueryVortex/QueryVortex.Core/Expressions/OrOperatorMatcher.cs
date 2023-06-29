// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Expressions;

public class OrOperatorMatcher : LogicalOperatorMatcherBase
{
    public const string OrKeyword = "[OR]";
    public override string OperatorKeyword => OrKeyword;
    public override LogicalOperator OperatorType => LogicalOperator.Or;
}
