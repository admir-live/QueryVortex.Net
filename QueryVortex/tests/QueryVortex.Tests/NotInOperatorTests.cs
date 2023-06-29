using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class NotInOperatorTests
{
    [Fact]
    public void Apply_WhenCalled_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = "column";
        var values = new object[] { "value1", "value2", "value3" };
        var notInOperator = new NotInOperator(column, values);
        var query = new Query();

        // Act
        notInOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as InCondition<object>;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Values.Should().Equal(values);
    }

    [Fact]
    public void Apply_WithEmptyColumn_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = string.Empty;
        var values = new object[] { "value1", "value2", "value3" };
        var notInOperator = new NotInOperator(column, values);
        var query = new Query();

        // Act
        notInOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as InCondition<object>;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Values.Should().Equal(values);
    }

    [Fact]
    public void Apply_WithEmptyValues_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = "column";
        var values = new object[] { };
        var notInOperator = new NotInOperator(column, values);
        var query = new Query();

        // Act
        notInOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as InCondition<object>;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.Values.Should().Equal(values);
    }

    [Fact]
    public void Apply_WithNullValues_ShouldAddCorrectCondition()
    {
        // Arrange
        var column = "column";
        var values = (IEnumerable<object>)null;
        var notInOperator = new NotInOperator(column, values);
        var query = new Query();

        // Act
        notInOperator.Apply(query);

        // Assert
        query.Clauses.Should().HaveCount(1);
        var whereClause = query.Clauses.First() as NullCondition;
        whereClause.Should().NotBeNull();
        whereClause.Column.Should().Be(column);
        whereClause.IsOr.Should().BeFalse();
    }
}
