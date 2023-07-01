// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using SqlKata;

namespace QueryVortex.Core.Operators;

public class OrOperator : ICondition
{
    public Query Apply(Query query)
    {
        return query.Where(query1 => query.From(query1));
    }
}
