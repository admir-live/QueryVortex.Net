using QueryVortex.Core.Models;
using QueryVortex.Core.Parsers;
using SqlKata;

namespace QueryVortex.Core.Builders;

public sealed class DefaultSqlQueryBuilder : ISqlQueryBuilder
{
    private readonly IComparisonOperatorParser _comparisonOperatorParser;

    public DefaultSqlQueryBuilder()
    {
        _comparisonOperatorParser = new ComparisonOperatorParser();
    }

    public SqlQuery CreateSqlQuery(QueryVortexObject blueprint)
    {
        var baseQuery = new Query().From(blueprint.TableName);
        var queryWithSelectedFields = AddSelectedFields(baseQuery, blueprint.SelectedFields);
        var queryWithFilters = AddFilterConditions(queryWithSelectedFields, blueprint.FilterConditions);
        var queryWithSorting = AddSortingOrders(queryWithFilters, blueprint.SortingOrders);
        var finalQuery = AddPagination(queryWithSorting, blueprint.Pagination);

        return new SqlQuery(finalQuery);
    }

    private static Query AddSelectedFields(Query query, List<string> fields)
    {
        if (fields.Any())
        {
            query = query.Select(fields.ToArray());
        }

        return query;
    }

    private Query AddFilterConditions(Query query, IEnumerable<FilterCondition> conditions)
    {
        foreach (var condition in conditions)
        {
            query = ApplyFilterCondition(query, condition);
        }

        return query;
    }

    private Query ApplyFilterCondition(Query query, FilterCondition condition)
    {
        var parsedFilter = _comparisonOperatorParser.ParseComparisonOperator(condition.ComparisonOperator, condition.FieldName, new[] { condition.ComparisonValue });

        return parsedFilter.Apply(query);
    }

    private static Query AddSortingOrders(Query query, IEnumerable<OrderSpecification> sortingOrders)
    {
        foreach (var order in sortingOrders)
        {
            query = ApplySortingOrder(query, order);
        }

        return query;
    }

    private static Query ApplySortingOrder(Query query, OrderSpecification sortingOrder)
    {
        return sortingOrder.SortOrder == SortOrderType.Descending
            ? query.OrderByDesc(sortingOrder.SortFieldName)
            : query.OrderBy(sortingOrder.SortFieldName);
    }

    private static Query AddPagination(Query query, PaginationSettings pagination)
    {
        return query.ForPage(pagination.PageNumber, pagination.PageSize);
    }
}
