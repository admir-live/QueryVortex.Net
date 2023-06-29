// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class NotEqualOperatorTests
{
    [Fact]
    public void Apply_WhenCalled_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = "column";
        var value = "value";
        var notEqualOperator = new NotEqualOperator(column, value);
        var query = new Query();

        // Act
        notEqualOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as BasicCondition;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Operator.Should().Be("<>");
        whereClause.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_WithEmptyColumn_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = string.Empty;
        var value = "value";
        var notEqualOperator = new NotEqualOperator(column, value);
        var query = new Query();

        // Act
        notEqualOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as BasicCondition;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Operator.Should().Be("<>");
        whereClause.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_WithNullValue_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = "column";
        var value = (object)null;
        var notEqualOperator = new NotEqualOperator(column, value);
        var query = new Query();

        // Act
        notEqualOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as NullCondition;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Component.Should().Be("where");
    }
}
