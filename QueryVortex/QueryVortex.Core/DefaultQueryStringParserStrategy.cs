using System.Text.RegularExpressions;
using System.Web;
using QueryVortex.Core.Models;
using QueryVortex.Core.Operators;

namespace QueryVortex.Core;

public class DefaultQueryStringParserStrategy : IQueryStringParserStrategy
{
    private const string Select = "select";
    private const string Filter = "filter";
    private const string Order = "order";
    private const string And = "AND";
    private const string Or = "OR";
    private const string Asc = "asc";

    private static Regex FilterPattern { get; } = new(@"(?<field>\w+)\[(?<op>\w+)\]=(?<value>\w+)");

    public IDictionary<string, string[]> ParseQueryString(string queryString)
    {
        var nameValueCollection = HttpUtility.ParseQueryString(queryString);
        return nameValueCollection.AllKeys.ToDictionary(key => key, key => nameValueCollection.GetValues(key));
    }

    public List<string> GetSelectFields(IDictionary<string, string[]> queryParameters)
    {
        return SplitParameter(queryParameters, Select).ToList();
    }

    public List<Filter> GetFilters(IDictionary<string, string[]> queryParameters)
    {
        if (queryParameters.TryGetValue(Filter, out var queryParameter))
        {
            var parameter = queryParameter.First();
            var filterStrings = SplitLogicalExpression(parameter, null).ToList();
            return filterStrings.Select(CreateFilterFrom).ToList();
        }

        return new List<Filter>();
    }

    public List<Order> GetOrders(IDictionary<string, string[]> queryParameters)
    {
        var orderStrings = SplitParameter(queryParameters, Order);
        return orderStrings.Select(CreateOrderFrom).ToList();
    }

    private static IEnumerable<string> SplitParameter(IDictionary<string, string[]> queryParameters, string key, string[] separators = null)
    {
        if (queryParameters.TryGetValue(key, out var queryParameter))
        {
            var parameter = queryParameter.First();
            return parameter.Split(separators ?? new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        return new List<string>();
    }

    private IEnumerable<string> SplitLogicalExpression(string expr, LogicalOperator? logicalOperator, int start = 0, int end = -1)
    {
        end = end == -1 ? expr.Length : end;

        var lastOpEnd = start;
        var depth = 0;

        for (var i = start; i < end; ++i)
        {
            switch (expr[i])
            {
                case '(':
                    if (depth++ == 0)
                    {
                        lastOpEnd = i + 1;
                    }

                    break;
                case ')':
                    if (--depth == 0)
                    {
                        foreach (var s in SplitLogicalExpression(expr, logicalOperator, lastOpEnd, i))
                        {
                            yield return s;
                        }

                        lastOpEnd = i + 1;
                    }

                    break;
                case 'A':
                    if (depth == 0 && i < end - 2 && expr[i + 1] == 'N' && expr[i + 2] == 'D')
                    {
                        if (i > lastOpEnd)
                        {
                            yield return expr.Substring(lastOpEnd, i - lastOpEnd).Trim();
                        }

                        logicalOperator = LogicalOperator.And;
                        lastOpEnd = i + 3;
                    }

                    break;
                case 'O':
                    if (depth == 0 && i < end - 1 && expr[i + 1] == 'R')
                    {
                        if (i > lastOpEnd)
                        {
                            yield return expr.Substring(lastOpEnd, i - lastOpEnd).Trim();
                        }

                        logicalOperator = LogicalOperator.Or;
                        lastOpEnd = i + 2;
                    }

                    break;
            }
        }

        if (end > lastOpEnd)
        {
            var str = expr.Substring(lastOpEnd, end - lastOpEnd).Trim();
            if (!string.IsNullOrEmpty(str))
            {
                yield return str;
            }
        }
    }

    private Filter CreateFilterFrom(string element)
    {
        var match = FilterPattern.Match(element);
        if (!match.Success)
        {
            throw new ArgumentException($"Invalid filter string: {element}");
        }

        var field = match.Groups["field"].Value;
        var op = match.Groups["op"].Value;
        var value = match.Groups["value"].Value;

        var logicalOperator = element.Contains(Or) ? LogicalOperator.Or : LogicalOperator.And;
        return new Filter { Field = field, Operator = op, Value = value, LogicalOperator = logicalOperator };
    }

    private static Order CreateOrderFrom(string element)
    {
        var elements = SplitElement(element, new[] { "[", "]" });

        if (elements.Length < 2)
        {
            throw new ArgumentException($"Invalid order string: {element}");
        }

        return new Order { Field = elements[0], OrderType = elements[1] == Asc ? OrderType.Asc : OrderType.Desc };
    }

    private static string[] SplitElement(string element, string[] separators)
    {
        return element.Trim('(', ')')
            .Split(separators, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();
    }
}
