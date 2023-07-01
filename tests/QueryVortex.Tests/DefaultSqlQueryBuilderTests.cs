using FluentAssertions;
using QueryVortex.Core.Builders;
using QueryVortex.Core.Models;
using Xunit;

namespace QueryVortex.Tests;

public class DefaultSqlQueryBuilderTests
{
    [Fact]
    public void CreateSqlQuery_WithNoSelectedFields_ShouldReturnQueryWithoutSelectClause()
    {
        // Arrange
        var blueprint = new QueryVortexObject { TableName = "Customers", SelectedFields = new List<string>() };

        var queryBuilder = new DefaultSqlQueryBuilder();

        // Act
        var result = queryBuilder.CreateSqlQuery(blueprint);

        // Assert
        result.QueryText.Should().Be("SELECT * FROM [Customers]");
    }

    [Fact]
    public void CreateSqlQuery_WithSelectedFields_ShouldReturnQueryWithSelectClause()
    {
        // Arrange
        var blueprint = new QueryVortexObject { TableName = "Customers", SelectedFields = new List<string> { "Name", "Email" } };

        var queryBuilder = new DefaultSqlQueryBuilder();

        // Act
        var result = queryBuilder.CreateSqlQuery(blueprint);

        // Assert
        result.QueryText.Should().Be("SELECT [Name], [Email] FROM [Customers]");
    }

    [Fact]
    public void CreateSqlQuery_WithFilterConditions_ShouldApplyFilterConditionsToQuery()
    {
        // Arrange
        var blueprint = new QueryVortexObject
        {
            TableName = "Customers",
            FilterConditions = new List<FilterCondition>
            {
                new() { ComparisonOperator = ComparisonOperator.Equals, FieldName = "Status", ComparisonValue = "Active" },
                new() { ComparisonOperator = ComparisonOperator.GreaterThan, FieldName = "Age", ComparisonValue = "18" }
            }
        };

        var queryBuilder = new DefaultSqlQueryBuilder();

        // Act
        var result = queryBuilder.CreateSqlQuery(blueprint);

        // Assert
        result.QueryText.Should().Be("SELECT * FROM [Customers] WHERE [Status] = 'Active' AND [Age] > '18'");
    }

    [Fact]
    public void CreateSqlQuery_WithSortingOrders_ShouldApplySortingOrdersToQuery()
    {
        // Arrange
        var blueprint = new QueryVortexObject
        {
            TableName = "Customers",
            SortingOrders = new List<OrderSpecification>
            {
                new() { SortFieldName = "Name", SortOrder = SortOrderType.Ascending }, new() { SortFieldName = "Age", SortOrder = SortOrderType.Descending }
            }
        };

        var queryBuilder = new DefaultSqlQueryBuilder();

        // Act
        var result = queryBuilder.CreateSqlQuery(blueprint);

        // Assert
        result.QueryText.Should().Be("SELECT * FROM [Customers] ORDER BY [Name], [Age] DESC");
    }

    [Fact]
    public void CreateSqlQuery_WithPagination_ShouldApplyPaginationToQuery()
    {
        // Arrange
        var blueprint = new QueryVortexObject { TableName = "Customers", Pagination = new PaginationSettings { PageNumber = 2, PageSize = 10 } };

        var queryBuilder = new DefaultSqlQueryBuilder();

        // Act
        var result = queryBuilder.CreateSqlQuery(blueprint);

        // Assert
        result.QueryText.Should().Be("SELECT * FROM [Customers] ORDER BY (SELECT 0) OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY");
    }

    [Fact]
    public void CreateSqlQuery_WithMultipleFilterConditions_ShouldApplyAllConditionsToQuery()
    {
        // Arrange
        var blueprint = new QueryVortexObject
        {
            TableName = "Products",
            FilterConditions = new List<FilterCondition>
            {
                new() { ComparisonOperator = ComparisonOperator.Equals, FieldName = "Category", ComparisonValue = "Electronics" },
                new() { ComparisonOperator = ComparisonOperator.Like, FieldName = "Name", ComparisonValue = "iPhone" },
                new() { ComparisonOperator = ComparisonOperator.LessThan, FieldName = "Price", ComparisonValue = "1000" }
            }
        };

        var queryBuilder = new DefaultSqlQueryBuilder();

        // Act
        var result = queryBuilder.CreateSqlQuery(blueprint);

        // Assert
        result.QueryText.Should().Be("SELECT * FROM [Products] WHERE [Category] = 'Electronics' AND [Name] like 'iPhone' AND [Price] < '1000'");
    }

    [Fact]
    public void CreateSqlQuery_WithSortingOrderAndPagination_ShouldApplySortingAndPaginationToQuery()
    {
        // Arrange
        var blueprint = new QueryVortexObject
        {
            TableName = "Orders",
            SortingOrders = new List<OrderSpecification> { new() { SortFieldName = "OrderDate", SortOrder = SortOrderType.Descending } },
            Pagination = new PaginationSettings { PageNumber = 3, PageSize = 20 }
        };

        var queryBuilder = new DefaultSqlQueryBuilder();

        // Act
        var result = queryBuilder.CreateSqlQuery(blueprint);

        // Assert
        result.QueryText.Should().Be("SELECT * FROM [Orders] ORDER BY [OrderDate] DESC OFFSET 40 ROWS FETCH NEXT 20 ROWS ONLY");
    }

    [Fact]
    public void CreateSqlQuery_WithComplexBlueprint_ShouldReturnCorrectQuery()
    {
        // Arrange
        var blueprint = new QueryVortexObject
        {
            TableName = "Employees",
            SelectedFields = new List<string> { "Name", "Salary" },
            FilterConditions = new List<FilterCondition>
            {
                new() { ComparisonOperator = ComparisonOperator.GreaterThanOrEqual, FieldName = "Salary", ComparisonValue = "5000" },
                new() { ComparisonOperator = ComparisonOperator.In, FieldName = "Department", ComparisonValue = "Sales,Marketing,HR" }
            },
            SortingOrders = new List<OrderSpecification> { new() { SortFieldName = "Name", SortOrder = SortOrderType.Ascending } },
            Pagination = new PaginationSettings { PageNumber = 1, PageSize = 50 }
        };

        var queryBuilder = new DefaultSqlQueryBuilder();

        // Act
        var result = queryBuilder.CreateSqlQuery(blueprint);

        // Assert
        result.QueryText.Should()
            .Be("SELECT [Name], [Salary] FROM [Employees] WHERE [Salary] >= '5000' AND [Department] IN ('Sales,Marketing,HR') ORDER BY [Name] OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY");
    }
}
