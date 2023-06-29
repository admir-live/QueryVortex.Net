// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

namespace QueryVortex.Core;

public interface IOperatorParser
{
    ICondition ParseOperator(string operatorAlias, string column, object[] values);
}
