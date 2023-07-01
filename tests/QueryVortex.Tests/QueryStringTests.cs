using FluentAssertions;
using QueryVortex.Core.Builders;
using QueryVortex.Core.Parsers;
using Xunit;

namespace QueryVortex.Tests;

public class QueryStringTests
{
    private readonly DefaultSqlQueryBuilder _queryBuilder;
    private readonly DefaultQueryVortexParser _queryParser;

    public QueryStringTests()
    {
        _queryBuilder = new DefaultSqlQueryBuilder();
        _queryParser = new DefaultQueryVortexParser();
    }

    [Fact]
    public void Example1_FilterByCategoryAndBrand_SortAndPagination()
    {
        // Arrange
        var queryString = "?filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung&sort=price&page=1&limit=10";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        sqlQuery.QueryText.Should().Be("SELECT * FROM [products] WHERE [category] = 'Electronics' AND [brand] = 'Samsung' ORDER BY [price] OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY");
    }

    [Fact]
    public void Example2_FilterByPriceRangeAndCondition_FieldSelection()
    {
        // Arrange
        var queryString = "?filters[price][$gte]=1000&filters[price][$lte]=2000&filters[condition][$eq]=New&fields=name&fields=price&fields=description";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        sqlQuery.QueryText.Should()
            .Be(
                "SELECT [name], [price], [description] FROM [products] WHERE [price] >= 1000 AND [price] <= 2000 AND [condition] = 'New' ORDER BY (SELECT 0) OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY");
    }

    [Fact]
    public void Example3_FilterWithMultipleBrandOptions_UsingOROperator()
    {
        // Arrange
        var queryString = "?filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$OR]filters[brand][$eq]=Sony";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        //  sqlQuery.QueryText.Should().Be("SELECT * FROM [products] WHERE [brand] = 'Samsung' OR [brand] = 'Apple' OR [brand] = 'Sony'");
        Console.WriteLine(sqlQuery.QueryText);
    }

    [Fact]
    public void Example4_FilterByCategoryAndPriceRange_UsingANDOperator()
    {
        // Arrange
        var queryString = "?filters[category][$eq]=Electronics[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=1000";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        //  sqlQuery.QueryText.Should().Be("SELECT * FROM [products] WHERE [category] = 'Electronics' AND [price] >= 500 AND [price] <= 1000");
        Console.WriteLine(sqlQuery.QueryText);
    }

    [Fact]
    public void Example5_FilterByName_UsingLikeOperator()
    {
        // Arrange
        var queryString = "?filters[name][$like]=iphone";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        //  sqlQuery.QueryText.Should().Be("SELECT * FROM [products] WHERE [name] LIKE '%iphone%'");
        Console.WriteLine(sqlQuery.QueryText);
    }

    [Fact]
    public void Example6_FilterByPrice_UsingLessThanOperator()
    {
        // Arrange
        var queryString = "?filters[price][$lt]=500";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        // sqlQuery.QueryText.Should().Be("SELECT * FROM [products] WHERE [price] < 500");
        Console.WriteLine(sqlQuery.QueryText);
    }

    [Fact]
    public void Example7_SortByName_DescendingOrder()
    {
        // Arrange
        var queryString = "?sort=name:desc";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        // sqlQuery.QueryText.Should().Be("SELECT * FROM [products] ORDER BY [name] DESC");
        Console.WriteLine(sqlQuery.QueryText);
    }

    [Fact]
    public void Example8_SelectOnlyNameAndCategoryFields()
    {
        // Arrange
        var queryString = "?fields=name&fields=category";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        // sqlQuery.QueryText.Should().Be("SELECT [name], [category] FROM [products]");
        Console.WriteLine(sqlQuery.QueryText);
    }

    [Fact]
    public void Example9_Pagination_RetrieveSecondPageWith20Items()
    {
        // Arrange
        var queryString = "?page=2&limit=20";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        // sqlQuery.QueryText.Should().Be("SELECT * FROM [products] OFFSET 20 ROWS FETCH NEXT 20 ROWS ONLY ORDER BY (SELECT 0)");
        Console.WriteLine(sqlQuery.QueryText);
    }

    [Fact]
    public void Example10_CombineFiltersWithDifferentOperators()
    {
        // Arrange
        var queryString =
            "?filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)";

        // Act
        var queryResult = _queryParser.Parse(queryString, "products");
        var sqlQuery = _queryBuilder.CreateSqlQuery(queryResult);

        // Assert
        // sqlQuery.QueryText.Should().Be("SELECT * FROM [products] WHERE [category] = 'Electronics' AND ([brand] = 'Samsung' OR [brand] = 'Apple') AND [price] >= 500 AND [price] <= 2000 AND ([condition] = 'New' OR [condition] = 'Refurbished') ORDER BY (SELECT 0)");
        Console.WriteLine(sqlQuery.QueryText);
    }
}
