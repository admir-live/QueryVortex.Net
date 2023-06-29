using FluentAssertions;
using QueryVortex.Core;
using QueryVortex.Core.Models;
using Xunit;

namespace QueryVortex.Tests;

public class DefaultQueryStringParserStrategyTest
{
    private readonly IQueryStringParserStrategy _strategy;

    public DefaultQueryStringParserStrategyTest()
    {
        _strategy = new DefaultQueryStringParserStrategy();
    }

    [Fact]
    public void ParseQueryString_ShouldParseQueryStringIntoDictionary()
    {
        // Arrange
        var queryString = "select=field1,field2&filter=field1[=]value1&order=field1[asc]";

        // Act
        var result = _strategy.ParseQueryString(queryString);

        // Assert
        result.Should().NotBeNull();
        result.Should().ContainKey("select");
        result["select"].Should().BeEquivalentTo("field1,field2");
        result.Should().ContainKey("filter");
        result["filter"].Should().BeEquivalentTo("field1[=]value1");
        result.Should().ContainKey("order");
        result["order"].Should().BeEquivalentTo("field1[asc]");
    }

    [Fact]
    public void GetSelectFields_ShouldReturnCorrectFields()
    {
        // Arrange
        var queryString = "select=field1,field2&filter=field1[=]value1&order=field1[asc]";
        var queryParameters = _strategy.ParseQueryString(queryString);

        // Act
        var result = _strategy.GetSelectFields(queryParameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(new[] { "field1", "field2" });
    }

    [Fact]
    public void GetOrders_ShouldReturnCorrectOrders()
    {
        // Arrange
        var queryString = "order=field1[asc]";
        var queryParameters = _strategy.ParseQueryString(queryString);

        // Act
        var result = _strategy.GetOrders(queryParameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result.First().Field.Should().Be("field1");
        result.First().OrderType.Should().Be(OrderType.Asc);
    }
}
