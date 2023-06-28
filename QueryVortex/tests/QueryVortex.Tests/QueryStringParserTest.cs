// Copyright (c) Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using FluentAssertions;
using QueryVortex.Core;
using QueryVortex.Core.Operators;
using Xunit;

namespace QueryVortex.Tests;

public class QueryStringParserTest
{
    [Fact]
    public void GetQueryObject_ReturnsCorrectObject_GivenValidQueryString()
    {
        // Arrange
        var queryString = "select=field1,field2&filter=field1[eq]value1&order=field1[asc]";
        var parser = new QueryStringParser(queryString);

        // Act
        var result = parser.GetQueryObject();

        // Assert
        result.Should().NotBeNull();
        result.SelectFields.Should().HaveCount(2).And.Contain(new[] { "field1", "field2" });
        result.Filters.Should().HaveCount(1).And.ContainSingle(f => f.Field == "field1" && f.Operator == "eq" && f.Value == "value1");
        result.Orders.Should().HaveCount(1).And.ContainSingle(o => o.Field == "field1" && o.OrderType == OrderType.Asc);
    }

    [Fact]
    public void GetSelectFields_ReturnsCorrectList_GivenSelectQueryString()
    {
        // Arrange
        var queryString = "select=field1,field2";
        var parser = new QueryStringParser(queryString);

        // Act
        var result = parser.GetSelectFields();

        // Assert
        result.Should().HaveCount(2).And.Contain(new[] { "field1", "field2" });
    }

    [Fact]
    public void GetFilters_ReturnsCorrectList_GivenFilterQueryString()
    {
        // Arrange
        var queryString = "filter=field1[eq]value1 AND field2[lt]value2";
        var parser = new QueryStringParser(queryString);

        // Act
        var result = parser.GetFilters();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(f => f.Field == "field1" && f.Operator == "eq" && f.Value == "value1" && f.LogicalOperator == LogicalOperator.And);
        result.Should().Contain(f => f.Field == "field2" && f.Operator == "lt" && f.Value == "value2" && f.LogicalOperator == LogicalOperator.And);
    }

    [Fact]
    public void CreateFilterFrom_ReturnsCorrectFilter_GivenValidElement()
    {
        // Arrange
        var queryString = "filter=field1[eq]value1";
        var parser = new QueryStringParser(queryString);

        // Act
        var result = parser.CreateFilterFrom("field1[eq]value1");

        // Assert
        result.Should().NotBeNull();
        result.Field.Should().Be("field1");
        result.Operator.Should().Be("eq");
        result.Value.Should().Be("value1");
        result.LogicalOperator.Should().Be(LogicalOperator.And);
    }

    [Fact]
    public void CreateFilterFrom_ThrowsException_GivenInvalidElement()
    {
        // Arrange
        var queryString = "filter=field1[eq]value1";
        var parser = new QueryStringParser(queryString);

        // Act
        Action act = () => parser.CreateFilterFrom("invalidElement");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid filter string: invalidElement");
    }

    [Fact]
    public void GetSelectFields_ReturnsEmptyList_WhenSelectQueryStringIsMissing()
    {
        // Arrange
        var queryString = "filter=field1[eq]value1";
        var parser = new QueryStringParser(queryString);

        // Act
        var result = parser.GetSelectFields();

        // Assert
        result.Should().BeNullOrEmpty();
    }

    [Fact]
    public void GetFilters_ReturnsEmptyList_WhenFilterQueryStringIsMissing()
    {
        // Arrange
        var queryString = "select=field1,field2";
        var parser = new QueryStringParser(queryString);

        // Act
        var result = parser.GetFilters();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void CreateFilterFrom_ThrowsException_WhenFilterStringHasInsufficientElements()
    {
        // Arrange
        var queryString = "filter=field1[eq]";
        var parser = new QueryStringParser(queryString);

        // Act
        Action act = () => parser.CreateFilterFrom("field1[eq]");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid filter string: field1[eq]");
    }

    [Fact]
    public void GetOrders_ReturnsEmptyList_WhenOrderQueryStringIsMissing()
    {
        // Arrange
        var queryString = "select=field1,field2";
        var parser = new QueryStringParser(queryString);

        // Act
        var result = parser.GetOrders();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void CreateOrderFrom_ThrowsException_WhenOrderStringHasInsufficientElements()
    {
        // Arrange
        var queryString = "order=field1";
        var parser = new QueryStringParser(queryString);

        // Act
        Action act = () => parser.CreateOrderFrom("field1");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Invalid order string: field1");
    }

    [Fact]
    public void CreateOrderFrom_CreatesDescendingOrder_WhenOrderTypeIsDesc()
    {
        // Arrange
        var queryString = "order=field1[desc]";
        var parser = new QueryStringParser(queryString);

        // Act
        var result = parser.CreateOrderFrom("field1[desc]");

        // Assert
        result.Should().NotBeNull();
        result.Field.Should().Be("field1");
        result.OrderType.Should().Be(OrderType.Desc);
    }
}
