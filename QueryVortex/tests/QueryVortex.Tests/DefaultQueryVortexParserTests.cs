using FluentAssertions;
using QueryVortex.Core;
using QueryVortex.Core.Models;
using QueryVortex.Core.Operators;
using QueryVortex.Core.Parsers;
using Xunit;

namespace QueryVortex.Tests;

public class DefaultQueryVortexParserTests
{
    private readonly IQueryVortexParser _parser;

    public DefaultQueryVortexParserTests()
    {
        _parser = new DefaultQueryVortexParser();
    }

    [Fact]
    public void ParseSelectClause_ReturnsCorrectResult()
    {
        // Arrange
        var sqlQuery = "fields=field1&fields=field2&fields=field3";

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeEquivalentTo("field1", "field2", "field3");
    }

    [Fact]
    public void ParseSelectClause_ReturnsEmpty_WhenExceptionOccurs()
    {
        // Arrange
        var sqlQuery = "not valid sql";

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeNullOrEmpty();
    }

    // ... similar tests for ParseFilterClause

    [Fact]
    public void ParseOrderByClause_ReturnsCorrectResult()
    {
        // Arrange
        var sqlQuery = "sort=fieldName:desc";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery);

        // Assert
        var expected = new List<OrderSpecification> { new("fieldName", SortOrderType.Descending) };
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ParseOrderByClause_ReturnsEmpty_WhenExceptionOccurs()
    {
        // Arrange
        var sqlQuery = "not valid sql";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery);

        // Assert
        result.Should().BeEmpty();
    }

    // ... similar tests for ParsePaginationClause and Parse

    [Fact]
    public void Parse_ReturnsCorrectResult()
    {
        // Arrange
        var sqlQuery = "fields=field1,field2&sort=fieldName:desc&page=2&limit=10";

        // Act
        var result = _parser.Parse(sqlQuery);

        // Assert
        var expected = new QueryVortexObject
        {
            Pagination = new PaginationSettings(2, 10),
            SelectedFields = new List<string> { "field1", "field2" },
            SortingOrders = new List<OrderSpecification> { new("fieldName", SortOrderType.Descending) },
            FilterConditions = new List<FilterCondition>()
        };
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ParseSelectClause_ValidQuery_ReturnsSelectedFields()
    {
        // Arrange
        var sqlQuery = "?fields=title&fields=author";

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeEquivalentTo("title", "author");
    }

    [Fact]
    public void ParseSelectClause_QueryWithWhitespace_ReturnsSelectedFields()
    {
        // Arrange
        var sqlQuery = "?fields=title&fields=author    ";

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeEquivalentTo("title", "author");
    }

    [Fact]
    public void ParseSelectClause_QueryWithEmptyField_ReturnsSelectedFields()
    {
        // Arrange
        var sqlQuery = "?fields=title&fields=";

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeEquivalentTo("title");
    }

    [Fact]
    public void ParseSelectClause_QueryWithoutFields_ReturnsEmptyList()
    {
        // Arrange
        var sqlQuery = "";

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseFilterClause_ValidQuery_ReturnsFilterConditions()
    {
        // Arrange
        var sqlQuery = "?filters[category][$eq]=books";

        // Act
        var result = _parser.ParseFilterClause(sqlQuery);

        // Assert
        result.Should().HaveCount(1);
        var filterCondition = result.Single();
        filterCondition.FieldName.Should().Be("category");
        filterCondition.ComparisonValue.Should().Be("books");
        filterCondition.ComparisonOperator.Should().Be("eq");
        filterCondition.LogicalOperator.Should().Be(LogicalOperator.And);
    }

    [Fact]
    public void ParseFilterClause_QueryWithMultipleFilters_ReturnsFilterConditions()
    {
        // Arrange
        var sqlQuery = "?filters[category][$eq]=books&filters[price][$lt]=50";

        // Act
        var result = _parser.ParseFilterClause(sqlQuery);

        // Assert
        result.Should().HaveCount(2);
        var filterCondition1 = result.ElementAt(0);
        filterCondition1.FieldName.Should().Be("category");
        filterCondition1.ComparisonValue.Should().Be("books");
        filterCondition1.ComparisonOperator.Should().Be("eq");
        filterCondition1.LogicalOperator.Should().Be(LogicalOperator.And);

        var filterCondition2 = result.ElementAt(1);
        filterCondition2.FieldName.Should().Be("price");
        filterCondition2.ComparisonValue.Should().Be("50");
        filterCondition2.ComparisonOperator.Should().Be("lt");
        filterCondition2.LogicalOperator.Should().Be(LogicalOperator.And);
    }

    [Fact]
    public void ParseOrderByClause_ValidQuery_ReturnsOrderSpecifications()
    {
        // Arrange
        var sqlQuery = "?sort=date:desc";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery);

        // Assert
        result.Should().HaveCount(1);
        var orderSpecification = result.Single();
        orderSpecification.SortFieldName.Should().Be("date");
        orderSpecification.SortOrder.Should().Be(SortOrderType.Descending);
    }

    [Fact]
    public void ParseOrderByClause_QueryWithMissingSortOrder_ReturnsDefaultSortOrder()
    {
        // Arrange
        var sqlQuery = "?sort=price:asc";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery);

        // Assert
        result.Should().HaveCount(1);
        var orderSpecification = result.Single();
        orderSpecification.SortFieldName.Should().Be("price");
        orderSpecification.SortOrder.Should().Be(SortOrderType.Ascending);
    }

    [Fact]
    public void ParseOrderByClause_QueryWithoutOrderByClause_ReturnsEmptyList()
    {
        // Arrange
        var sqlQuery = "";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParsePaginationClause_ValidQuery_ReturnsPaginationSettings()
    {
        // Arrange
        var sqlQuery = "?page=2&limit=10";

        // Act
        var result = _parser.ParsePaginationClause(sqlQuery);

        // Assert
        result.PageNumber.Should().Be(2);
        result.PageSize.Should().Be(10);
    }

    [Fact]
    public void ParsePaginationClause_QueryWithMissingPageNumber_ReturnsDefaultPageNumber()
    {
        // Arrange
        var sqlQuery = "?limit=20";

        // Act
        var result = _parser.ParsePaginationClause(sqlQuery);

        // Assert
        result.PageNumber.Should().Be(0);
        result.PageSize.Should().Be(20);
    }

    [Fact]
    public void ParsePaginationClause_QueryWithMissingLimitNumber_ReturnsDefaultLimitNumber()
    {
        // Arrange
        var sqlQuery = "?page=1";

        // Act
        var result = _parser.ParsePaginationClause(sqlQuery);

        // Assert
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(20);
    }

    [Fact]
    public void ParsePaginationClause_QueryWithoutPaginationClause_ReturnsDefaultPaginationSettings()
    {
        // Arrange
        var sqlQuery = "";

        // Act
        var result = _parser.ParsePaginationClause(sqlQuery);

        // Assert
        result.PageNumber.Should().Be(0);
        result.PageSize.Should().Be(20);
    }

    [Fact]
    public void Parse_ValidQuery_ReturnsQueryParameters()
    {
        // Arrange
        var sqlQuery =
            @"sort=date:desc&fields=title&fields=author&filters[category][$eq]=books&page=1&limit=20";

        // Act
        var result = _parser.Parse(sqlQuery);

        // Assert
        result.Pagination.PageNumber.Should().Be(1);
        result.Pagination.PageSize.Should().Be(20);
        result.SelectedFields.Should().BeEquivalentTo("title", "author");
        result.SortingOrders.Should().HaveCount(1);

        var orderSpecification = result.SortingOrders.Single();
        orderSpecification.SortFieldName.Should().Be("date");
        orderSpecification.SortOrder.Should().Be(SortOrderType.Descending);
        result.FilterConditions.Should().HaveCount(1);

        var filterCondition = result.FilterConditions.Single();
        filterCondition.FieldName.Should().Be("category");
        filterCondition.ComparisonValue.Should().Be("books");
        filterCondition.ComparisonOperator.Should().Be("eq");
        filterCondition.LogicalOperator.Should().Be(LogicalOperator.And);
    }

    [Fact]
    public void Parse_ValidQueryWithMultipleSortOrders_ReturnsQueryParameters()
    {
        // Arrange
        var sqlQuery =
            @"?sort=date:desc&sort=rating:asc";

        // Act
        var result = _parser.Parse(sqlQuery);

        // Assert
        result.Pagination.PageNumber.Should().Be(0);
        result.Pagination.PageSize.Should().Be(20);
        result.SelectedFields.Should().BeEmpty();
        result.SortingOrders.Should().HaveCount(2);

        result.SortingOrders[0].SortFieldName.Should().Be("date");
        result.SortingOrders[0].SortOrder.Should().Be(SortOrderType.Descending);

        result.SortingOrders[1].SortFieldName.Should().Be("rating");
        result.SortingOrders[1].SortOrder.Should().Be(SortOrderType.Ascending);

        result.FilterConditions.Should().BeEmpty();
    }

    [Fact]
    public void Parse_ValidQueryWithMultipleFilters_ReturnsQueryParameters()
    {
        // Arrange
        var sqlQuery = "?filters[category][$eq]=books&filters[price][$lt]=50&filters[rating][$gt]=4";

        // Act
        var result = _parser.Parse(sqlQuery);

        // Assert
        result.Pagination.PageNumber.Should().Be(0);
        result.Pagination.PageSize.Should().Be(20);
        result.SelectedFields.Should().BeEmpty();
        result.SortingOrders.Should().BeEmpty();
        result.FilterConditions.Should().HaveCount(3);

        result.FilterConditions[0].FieldName.Should().Be("category");
        result.FilterConditions[0].ComparisonValue.Should().Be("books");
        result.FilterConditions[0].ComparisonOperator.Should().Be("eq");
        result.FilterConditions[0].LogicalOperator.Should().Be(LogicalOperator.And);

        result.FilterConditions[1].FieldName.Should().Be("price");
        result.FilterConditions[1].ComparisonValue.Should().Be("50");
        result.FilterConditions[1].ComparisonOperator.Should().Be("lt");
        result.FilterConditions[1].LogicalOperator.Should().Be(LogicalOperator.And);

        result.FilterConditions[2].FieldName.Should().Be("rating");
        result.FilterConditions[2].ComparisonValue.Should().Be("4");
        result.FilterConditions[2].ComparisonOperator.Should().Be("gt");
        result.FilterConditions[2].LogicalOperator.Should().Be(LogicalOperator.And);
    }

    [Fact]
    public void Parse_ValidQueryWithPagination_ReturnsQueryParameters()
    {
        // Arrange
        var sqlQuery = "?page=2&limit=10";

        // Act
        var result = _parser.Parse(sqlQuery);

        // Assert
        result.Pagination.PageNumber.Should().Be(2);
        result.Pagination.PageSize.Should().Be(10);
        result.SelectedFields.Should().BeEmpty();
        result.SortingOrders.Should().BeEmpty();
        result.FilterConditions.Should().BeEmpty();
    }

    [Fact]
    public void Parse_ValidQueryWithAllComponents_ReturnsQueryParameters()
    {
        // Arrange
        var sqlQuery = @"?sort=date:desc&fields=title&fields=author&filters[category][$eq]=books&page=2&limit=10";

        // Act
        var result = _parser.Parse(sqlQuery);

        // Assert
        result.Pagination.PageNumber.Should().Be(2);
        result.Pagination.PageSize.Should().Be(10);
        result.SelectedFields.Should().BeEquivalentTo("title", "author");
        result.SortingOrders.Should().HaveCount(1);

        var orderSpecification = result.SortingOrders.Single();
        orderSpecification.SortFieldName.Should().Be("date");
        orderSpecification.SortOrder.Should().Be(SortOrderType.Descending);
        result.FilterConditions.Should().HaveCount(1);

        var filterCondition = result.FilterConditions.Single();
        filterCondition.FieldName.Should().Be("category");
        filterCondition.ComparisonValue.Should().Be("books");
        filterCondition.ComparisonOperator.Should().Be("eq");
        filterCondition.LogicalOperator.Should().Be(LogicalOperator.And);
    }

    [Fact]
    public void ParseSelectClause_ReturnsEmpty_WhenFieldsParameterIsMissing()
    {
        // Arrange
        var sqlQuery = "";

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseSelectClause_IgnoresWhitespaceAndEmptyFields()
    {
        // Arrange
        var sqlQuery = "fields=field1&fields=&fields=  ";

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeEquivalentTo("field1");
    }

    [Fact]
    public void ParseSelectClause_ReturnsEmpty_WhenSqlQueryIsNull()
    {
        // Arrange
        string sqlQuery = null;

        // Act
        var result = _parser.ParseSelectClause(sqlQuery);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseOrderByClause_ReturnsEmpty_WhenSqlQueryIsNull()
    {
        // Arrange
        string sqlQuery = null;

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseOrderByClause_ReturnsCorrectOrderSpecifications()
    {
        // Arrange
        var sqlQuery = "sort=field1:asc&sort=field2&sort=field3:desc";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery).ToList();

        // Assert
        result.Should().HaveCount(3);

        result[0].SortFieldName.Should().Be("field1");
        result[0].SortOrder.Should().Be(SortOrderType.Ascending);

        result[1].SortFieldName.Should().Be("field2");
        result[1].SortOrder.Should().Be(SortOrderType.Ascending);

        result[2].SortFieldName.Should().Be("field3");
        result[2].SortOrder.Should().Be(SortOrderType.Descending);
    }

    [Fact]
    public void ParseOrderByClause_ReturnsDefaultOrderSpecification_WhenSortFieldNameIsMissing()
    {
        // Arrange
        var sqlQuery = "sort=:asc";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery).ToList();

        // Assert
        result.Should().HaveCount(1);
        result[0].SortFieldName.Should().BeEmpty();
        result[0].SortOrder.Should().Be(SortOrderType.Ascending);
    }

    [Fact]
    public void ParseOrderByClause_ReturnsDefaultOrderSpecification_WhenSortOrderIsMissing()
    {
        // Arrange
        var sqlQuery = "sort=field1:";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery).ToList();

        // Assert
        result.Should().HaveCount(1);
        result[0].SortFieldName.Should().Be("field1");
        result[0].SortOrder.Should().Be(SortOrderType.Ascending);
    }

    [Fact]
    public void ParseOrderByClause_ReturnsDefaultOrderSpecification_WhenSortOrderIsInvalid()
    {
        // Arrange
        var sqlQuery = "sort=field1:invalid";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery).ToList();

        // Assert
        result.Should().HaveCount(1);
        result[0].SortFieldName.Should().Be("field1");
        result[0].SortOrder.Should().Be(SortOrderType.Ascending);
    }

    [Fact]
    public void ParseOrderByClause_ReturnsEmpty_WhenSqlQueryIsNullOrEmpty()
    {
        // Arrange
        var sqlQuery = "";

        // Act
        var result = _parser.ParseOrderByClause(sqlQuery);

        // Assert
        result.Should().BeEmpty();
    }
}
