using SqlKata;

namespace QueryVortex.Core;

public interface ICondition
{
    Query Apply(Query query);
}
