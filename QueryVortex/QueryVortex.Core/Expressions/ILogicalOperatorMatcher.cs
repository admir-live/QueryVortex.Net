using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Expressions;

public interface ILogicalOperatorMatcher
{
    string OperatorKeyword { get; }
    LogicalOperator OperatorType { get; }
    bool MatchesOperator(string expression, int index, int endIndex, int nestingLevel);
}
