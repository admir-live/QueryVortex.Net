// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using FluentAssertions;
using QueryVortex.Core;
using QueryVortex.Core.Models;
using QueryVortex.Core.Operators;
using QueryVortex.Core.Parsers;
using Xunit;

namespace QueryVortex.Tests;

public class FilterParserTests
{
    private readonly IQueryVortexParser _filterParser;

    public FilterParserTests()
    {
        _filterParser = new DefaultQueryVortexParser();
    }

    [Fact]
    public void ParseFilterClause_WithValidApiQueryString_ReturnsFilterConditions()
    {
        // Arrange
        var apiQueryString =
            "?sort=price:desc&fields=name&fields=price&fields=description&fields=category&fields=brand&filters[category][$eq]=Electronics[$AND]filters[brand][$eq]=Samsung[$OR]filters[brand][$eq]=Apple[$AND]filters[price][$gte]=500[$AND]filters[price][$lte]=2000[$AND](filters[condition][$eq]=New[$OR]filters[condition][$eq]=Refurbished)&page=1&limit=20";

        // Act
        IEnumerable<FilterCondition> result = _filterParser.ParseFilterClause(apiQueryString);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(7);

        var condition1 = result.ElementAt(0);
        condition1.FieldName.Should().Be("category");
        condition1.ComparisonOperator.Should().Be(ComparisonOperator.Equals);
        condition1.ComparisonValue.Should().Be("Electronics");
        condition1.LogicalOperator.Should().Be(LogicalOperator.And);

        var condition2 = result.ElementAt(1);
        condition2.FieldName.Should().Be("brand");
        condition2.ComparisonOperator.Should().Be(ComparisonOperator.Equals);
        condition2.ComparisonValue.Should().Be("Samsung");
        condition2.LogicalOperator.Should().Be(LogicalOperator.And);

        var condition3 = result.ElementAt(2);
        condition3.FieldName.Should().Be("brand");
        condition3.ComparisonOperator.Should().Be(ComparisonOperator.Equals);
        condition3.ComparisonValue.Should().Be("Apple");
        condition3.LogicalOperator.Should().Be(LogicalOperator.And);

        var condition4 = result.ElementAt(3);
        condition4.FieldName.Should().Be("price");
        condition4.ComparisonOperator.Should().Be(ComparisonOperator.GreaterThanOrEqual);
        condition4.ComparisonValue.Should().Be(500);
        condition4.LogicalOperator.Should().Be(LogicalOperator.And);

        var condition5 = result.ElementAt(4);
        condition5.FieldName.Should().Be("price");
        condition5.ComparisonOperator.Should().Be(ComparisonOperator.LessThanOrEqual);
        condition5.ComparisonValue.Should().Be(2000);
        condition5.LogicalOperator.Should().Be(LogicalOperator.And);

        var condition6 = result.ElementAt(5);
        condition6.FieldName.Should().Be("condition");
        condition6.ComparisonOperator.Should().Be(ComparisonOperator.Equals);
        condition6.ComparisonValue.Should().Be("New");
        condition6.LogicalOperator.Should().Be(LogicalOperator.And);

        var condition7 = result.ElementAt(6);
        condition7.FieldName.Should().Be("condition");
        condition7.ComparisonOperator.Should().Be(ComparisonOperator.Equals);
        condition7.ComparisonValue.Should().Be("Refurbished");
        condition7.LogicalOperator.Should().Be(LogicalOperator.And);
    }

    [Fact]
    public void ParseFilterClause_WithEmptyApiQueryString_ReturnsEmptyFilterConditions()
    {
        // Arrange
        var apiQueryString = "";

        // Act
        IEnumerable<FilterCondition> result = _filterParser.ParseFilterClause(apiQueryString);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseFilterClause_WithInvalidApiQueryString_ReturnsEmptyFilterConditions()
    {
        // Arrange
        var apiQueryString = "?invalidQueryString";

        // Act
        IEnumerable<FilterCondition> result = _filterParser.ParseFilterClause(apiQueryString);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void ParseFilterClause_WithExceptionThrown_ReturnsEmptyFilterConditions()
    {
        // Arrange
        var apiQueryString = "?sort=price:desc";

        // Act
        IEnumerable<FilterCondition> result = _filterParser.ParseFilterClause(apiQueryString);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
