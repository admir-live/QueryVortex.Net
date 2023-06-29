using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Expressions;

public class LogicalExpressionDivider
{
    private const char OpeningParenthesis = '(';
    private const char ClosingParenthesis = ')';

    public LogicalExpressionDivider(string expression, IList<ILogicalOperatorMatcher> operatorMatchers, LogicalOperator currentOperator = LogicalOperator.And, int start = 0, int end = -1)
    {
        LogicalExpression = expression;
        CurrentOperator = currentOperator;
        ExpressionStart = start;
        ExpressionEnd = end == -1 ? expression.Length : end;
        LastOperatorEnd = start;
        ParenthesisNestingDepth = 0;
        OperatorMatchers = operatorMatchers;
    }
    private int ExpressionEnd { get; }
    private string LogicalExpression { get; }
    private IList<ILogicalOperatorMatcher> OperatorMatchers { get; }
    private int ExpressionStart { get; }
    private int ParenthesisNestingDepth { get; set; }
    private int LastOperatorEnd { get; set; }
    private LogicalOperator CurrentOperator { get; set; }
    
    private IEnumerable<string> ProcessClosingParenthesisAndReturnResults(int i)
    {
        var result = ProcessClosingParenthesis(i);
        foreach (var substring in result.Substrings)
        {
            yield return substring;
        }

        LastOperatorEnd = result.EndIndex;
    }

    private IEnumerable<string> CheckForLogicalOperatorsAndReturnResults(int i)
    {
        foreach (var operatorMatcher in OperatorMatchers)
        {
            if (operatorMatcher.MatchesOperator(LogicalExpression, i, ExpressionEnd, ParenthesisNestingDepth))
            {
                var substring = HandleLogicalOperator(i, operatorMatcher);
                if (!string.IsNullOrEmpty(substring))
                {
                    yield return substring;
                }

                break;
            }
        }
    }

    public IEnumerable<string> DivideExpression()
    {
        for (var i = ExpressionStart; i < ExpressionEnd; ++i)
        {
            if (IsOpeningParenthesis(i))
            {
                HandleOpeningParenthesis(i);
            }
            else if (IsClosingParenthesis(i))
            {
                foreach (var substring in ProcessClosingParenthesisAndReturnResults(i))
                {
                    yield return substring;
                }
            }
            else
            {
                foreach (var substring in CheckForLogicalOperatorsAndReturnResults(i))
                {
                    yield return substring;
                }
            }
        }

        foreach (var substring in ReturnRemainingExpressionIfNotEmpty())
        {
            yield return substring;
        }
    }

    private void HandleOpeningParenthesis(int i)
    {
        ParenthesisNestingDepth++;
        if (ParenthesisNestingDepth == 1)
        {
            LastOperatorEnd = i + 1;
        }
    }

    private IEnumerable<string> ReturnRemainingExpressionIfNotEmpty()
    {
        if (ExpressionEnd > LastOperatorEnd)
        {
            var substring = LogicalExpression.Substring(LastOperatorEnd, ExpressionEnd - LastOperatorEnd).Trim();
            if (!string.IsNullOrEmpty(substring))
            {
                yield return substring;
            }
        }
    }

    private bool IsOpeningParenthesis(int i)
    {
        return LogicalExpression[i] == OpeningParenthesis;
    }

    private bool IsClosingParenthesis(int i)
    {
        return LogicalExpression[i] == ClosingParenthesis;
    }

    private (List<string> Substrings, int EndIndex) ProcessClosingParenthesis(int i)
    {
        ParenthesisNestingDepth--;
        var substrings = new List<string>();
        if (ParenthesisNestingDepth == 0)
        {
            substrings.AddRange(new LogicalExpressionDivider(LogicalExpression, OperatorMatchers, CurrentOperator, LastOperatorEnd, i).DivideExpression());
            LastOperatorEnd = i + 1;
        }

        return (substrings, LastOperatorEnd);
    }

    private string HandleLogicalOperator(int i, ILogicalOperatorMatcher operatorMatcher)
    {
        var substring = string.Empty;
        if (i > LastOperatorEnd)
        {
            substring = LogicalExpression.Substring(LastOperatorEnd, i - LastOperatorEnd).Trim() + operatorMatcher.OperatorKeyword;
        }

        CurrentOperator = operatorMatcher.OperatorType;
        LastOperatorEnd = i + operatorMatcher.OperatorKeyword.Length;
        return substring;
    }
}
