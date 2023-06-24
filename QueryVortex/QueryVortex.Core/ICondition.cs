using SqlKata;

namespace QueryVortex.Core;

public interface ICondition
{
    void Apply(Query query);
}