using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class LessOrEqualThanOperatorTests
{
    [Fact]
    public void Apply_ShouldAddLessOrEqualThanConditionToQuery_WithNumericValue()
    {
        // Arrange
        var column = "testColumn";
        var value = 123;
        var lessOrEqualThanOperator = new LessOrEqualThanOperator(column, value);
        var query = new Query();

        // Act
        lessOrEqualThanOperator.Apply(query);

        // Assert
        var basicCondition = query.Clauses[0] as BasicCondition;
        basicCondition.Should().NotBeNull();
        basicCondition.Column.Should().Be(column);
        basicCondition.Component.Should().Be("where");
        basicCondition.Operator.Should().Be("<=");
        basicCondition.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_ShouldAddLessOrEqualThanConditionToQuery_WithStringValue()
    {
        // Arrange
        var column = "testColumn";
        var value = "testValue";
        var lessOrEqualThanOperator = new LessOrEqualThanOperator(column, value);
        var query = new Query();

        // Act
        lessOrEqualThanOperator.Apply(query);

        // Assert
        var basicCondition = query.Clauses[0] as BasicCondition;
        basicCondition.Should().NotBeNull();
        basicCondition.Column.Should().Be(column);
        basicCondition.Component.Should().Be("where");
        basicCondition.Operator.Should().Be("<=");
        basicCondition.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_ShouldAddLessOrEqualThanConditionToQuery_WithNullValue()
    {
        // Arrange
        var column = "testColumn";
        var lessOrEqualThanOperator = new LessOrEqualThanOperator(column, null);
        var query = new Query();

        // Act
        lessOrEqualThanOperator.Apply(query);

        // Assert
        var basicCondition = query.Clauses[0] as NullCondition;
        basicCondition.Should().NotBeNull();
        basicCondition.Column.Should().Be(column);
        basicCondition.Component.Should().Be("where");
    }
}
