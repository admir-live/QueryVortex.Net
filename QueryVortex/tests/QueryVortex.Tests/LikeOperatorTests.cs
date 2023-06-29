// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class LikeOperatorTests
{
    [Fact]
    public void Apply_WhenCalled_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = "column";
        var value = "value";
        var likeOperator = new LikeOperator(column, value);
        var query = new Query();

        // Act
        likeOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as BasicCondition;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Operator.Should().Be("LIKE");
        whereClause.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_WithEmptyColumn_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = string.Empty;
        var value = "value";
        var likeOperator = new LikeOperator(column, value);
        var query = new Query();

        // Act
        likeOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as BasicCondition;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Operator.Should().Be("LIKE");
        whereClause.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_WithEmptyValue_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = "column";
        var value = string.Empty;
        var likeOperator = new LikeOperator(column, value);
        var query = new Query();

        // Act
        likeOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as BasicCondition;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Operator.Should().Be("LIKE");
        whereClause.Value.Should().Be(value);
    }
}
