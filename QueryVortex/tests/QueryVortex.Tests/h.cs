// Copyright (c) Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using QueryVortex.Core.Operators;
using SqlKata;

namespace QueryVortex.Tests;

using FluentAssertions;
using Xunit;

public class EndsWithOperatorTests
{
    [Fact]
    public void Apply_ShouldApplyWhereClauseWithCorrectValues()
    {
        // Arrange
        var column = "ColumnName";
        var value = "TestValue";
        var query = new Query();

        var endsWithOperator = new EndsWithOperator(column, value);

        // Act
        endsWithOperator.Apply(query);

        // Assert
        query.Should().NotBeNull();
        var whereClause = query.Clauses.FirstOrDefault() as BasicCondition;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Component.Should().Be("where");
        whereClause.Operator.Should().Be("LIKE");
        whereClause.Value.Should().Be($"%{value}");
    }
}
