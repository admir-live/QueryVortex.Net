using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;
using QueryVortex.Core.Models;
using static System.Int32;

namespace QueryVortex.Core.Parsers;

public class DefaultQueryVortexParser : ISqlQueryParser
{
    private const string FilterClauseRegexPattern = @"filters\[(\w+)\]\[\$([a-z]+)\]=(\w+|\$AND|\$OR)";
    private static Regex FilterClauseRegex { get; } = new(FilterClauseRegexPattern);

    public IEnumerable<string> ParseSelectClause(string sqlQuery)
    {
        try
        {
            var nameValueCollection = HttpUtility.ParseQueryString(sqlQuery);
            var fields = nameValueCollection["fields"];
            return fields?.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()) ?? Array.Empty<string>();
        }
        catch (Exception e)
        {
            return ArraySegment<string>.Empty;
        }
    }

    public IEnumerable<FilterCondition> ParseFilterClause(string sqlQuery)
    {
        IList<FilterCondition> filterConditions = new List<FilterCondition>();
        try
        {
            var filterMatches = FilterClauseRegex.Matches(sqlQuery);

            foreach (Match match in filterMatches)
            {
                var field = match.Groups[1].Value;
                var value = match.Groups[3].Value;
                var @operator = match.Groups[2].Value;
                var logicalOperator = match.Groups[4].Value;

                var filterLogicalOperator = logicalOperator switch
                {
                    "$AND" => FilterLogicalOperator.And,
                    "$OR" => FilterLogicalOperator.Or,
                    _ => FilterLogicalOperator.And
                };

                filterConditions.Add(new FilterCondition(field, @operator, value, filterLogicalOperator));
            }
        }
        catch (Exception e)
        {
            return ImmutableList<FilterCondition>.Empty;
        }

        return filterConditions;
    }

    public IEnumerable<OrderSpecification> ParseOrderByClause(string sqlQuery)
    {
        IList<OrderSpecification> orderSpecifications = new List<OrderSpecification>();

        try
        {
            var queryParameters = HttpUtility.ParseQueryString(sqlQuery);
            var sortValues = queryParameters.GetValues("sort") ?? Array.Empty<string>();

            foreach (var sortValue in sortValues)
            {
                var value = sortValue.Split(':');
                object index;
                var sortFieldName = TryGetSortValue(value, 0);
                var sortOrder = TryGetSortValue(value, 1);
                var sortOrderType = sortOrder switch
                {
                    "asc" => SortOrderType.Ascending,
                    "" => SortOrderType.Ascending,
                    "desc" => SortOrderType.Descending,
                    _ => SortOrderType.Ascending
                };

                orderSpecifications.Add(new OrderSpecification(sortFieldName, sortOrderType));
            }
        }
        catch (Exception e)
        {
            return ImmutableList<OrderSpecification>.Empty;
        }

        return orderSpecifications;
    }
    private static string TryGetSortValue(string[] value, int index)
    {
        try
        {
            return value[index];
        }
        catch (Exception)
        {
            return null;
        }
    }

    public PaginationSettings ParsePaginationClause(string sqlQuery)
    {
        var nameValueCollection = HttpUtility.ParseQueryString(sqlQuery);

        int pageNumber;
        int limitNumber;

        pageNumber = TryParse(nameValueCollection["page"], out pageNumber) ? pageNumber : 0;
        limitNumber = TryParse(nameValueCollection["limit"], out limitNumber) ? limitNumber : 20;

        return new PaginationSettings(pageNumber, limitNumber);
    }
    public QueryParameters Parse(string sqlQuery)
    {
        return new QueryParameters
        {
            Pagination = ParsePaginationClause(sqlQuery),
            SelectedFields = ParseSelectClause(sqlQuery).ToList(),
            SortingOrders = ParseOrderByClause(sqlQuery).ToList(),
            FilterConditions = ParseFilterClause(sqlQuery).ToList()
        };
    }
}
