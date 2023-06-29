using System.Text.RegularExpressions;
using System.Web;
using QueryVortex.Core.Expressions;
using QueryVortex.Core.Models;
using QueryVortex.Core.Operators;
using System.Collections.Generic;
using System.Linq;
using System;

namespace QueryVortex.Core.Parsers
{
    public class DefaultQueryStringParserStrategy : IQueryStringParserStrategy
    {
        private const string Select = "select";
        private const string Filter = "filter";
        private const string Order = "order";
        private const string Asc = "asc";

        private static Regex FilterPattern { get; } = new(@"(?<field>\w+)\[(?<op>\w+)\]=(?<value>\w+)");

        private IList<ILogicalOperatorMatcher> _logicalOperatorMatchers;

        public DefaultQueryStringParserStrategy()
        {
            _logicalOperatorMatchers = new List<ILogicalOperatorMatcher>
            {
                new AndOperatorMatcher(),
                new OrOperatorMatcher()
            };
        }

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
                var logicalExpressionDivider = new LogicalExpressionDivider(parameter, _logicalOperatorMatchers);
                var filterStrings = logicalExpressionDivider.DivideExpression();
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

            var logicalOperator = LogicalOperator.And;
            if (element.EndsWith(AndOperatorMatcher.AndKeyword, StringComparison.InvariantCultureIgnoreCase))
            {
                logicalOperator = LogicalOperator.And;
                //value = value.Substring(0, value.Length - AndOperatorMatcher.AndKeyword.Length);
            }
            else if (element.EndsWith(OrOperatorMatcher.OrKeyword, StringComparison.InvariantCultureIgnoreCase))
            {
                logicalOperator = LogicalOperator.Or;
                //value = value.Substring(0, value.Length - OrOperatorMatcher.OrKeyword.Length);
            }

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
}
