// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Expressions;

public class AndOperatorMatcher : LogicalOperatorMatcherBase
{
    public const string AndKeyword = "[AND]";
    public override string OperatorKeyword => AndKeyword;
    public override LogicalOperator OperatorType => LogicalOperator.And;
}
