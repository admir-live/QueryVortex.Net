using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using QueryVortex.Core.Extensions;
using QueryVortex.Core.Models;
using QueryVortex.Core.Operators;
using static System.Int32;

namespace QueryVortex.Core.Parsers;

public class DefaultQueryVortexParser : IQueryVortexParser
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
        catch (Exception)
        {
            return Array.Empty<string>();
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
                    "$AND" => LogicalOperator.And,
                    "$OR" => LogicalOperator.Or,
                    _ => LogicalOperator.And
                };

                var parsedValue = value.TryParseToNumericObject();
                filterConditions.Add(parsedValue.Success
                    ? new FilterCondition(field, @operator.ToComparisonOperator(), parsedValue.Value, filterLogicalOperator)
                    : new FilterCondition(field, @operator.ToComparisonOperator(), value, filterLogicalOperator));
            }
        }
        catch (Exception)
        {
            return Array.Empty<FilterCondition>();
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
        catch (Exception)
        {
            return Array.Empty<OrderSpecification>();
        }

        return orderSpecifications;
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
    public QueryVortexObject Parse(string sqlQuery, string tableName)
    {
        return new QueryVortexObject
        {
            TableName = tableName,
            Pagination = ParsePaginationClause(sqlQuery),
            SelectedFields = ParseSelectClause(sqlQuery).ToList(),
            SortingOrders = ParseOrderByClause(sqlQuery).ToList(),
            FilterConditions = ParseFilterClause(sqlQuery).ToList()
        };
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
}
