using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class InOperatorTests
{
    [Fact]
    public void Apply_ShouldAddInConditionToQuery()
    {
        // Arrange
        var column = "testColumn";
        var values = new List<object> { "value1", "value2", "value3" };
        var inOperator = new InOperator(column, values);
        var query = new Query();

        // Act
        inOperator.Apply(query);

        // Assert
        var inCondition = query.Clauses[0] as InCondition<object>;
        inCondition.Should().NotBeNull();
        inCondition.Column.Should().Be(column);
        inCondition.Component.Should().Be("where");
        inCondition.Values.Should().BeEquivalentTo(values);
    }

    [Fact]
    public void Apply_ShouldAddInConditionToQuery_WithSingleValue()
    {
        // Arrange
        var column = "testColumn";
        var value = "testValue";
        var inOperator = new InOperator(column, new List<object> { value });
        var query = new Query();

        // Act
        inOperator.Apply(query);

        // Assert
        var inCondition = query.Clauses[0] as InCondition<object>;
        inCondition.Should().NotBeNull();
        inCondition.Column.Should().Be(column);
        inCondition.Component.Should().Be("where");
        inCondition.Values.Should().BeEquivalentTo(new List<object> { value });
    }

    [Fact]
    public void Apply_ShouldAddInConditionToQuery_WithEmptyValues()
    {
        // Arrange
        var column = "testColumn";
        var inOperator = new InOperator(column, new List<object>());
        var query = new Query();

        // Act
        inOperator.Apply(query);

        // Assert
        var inCondition = query.Clauses[0] as InCondition<object>;
        inCondition.Should().NotBeNull();
        inCondition.Column.Should().Be(column);
        inCondition.Component.Should().Be("where");
        inCondition.Values.Should().BeEmpty();
    }
}
