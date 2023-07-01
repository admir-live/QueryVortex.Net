using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class GreaterThanOperatorTests
{
    [Fact]
    public void Apply_ShouldAddGreaterThanConditionToQuery()
    {
        // Arrange
        var column = "testColumn";
        var value = "testValue";
        var greaterThanOperator = new GreaterThanOperator(column, value);
        var query = new Query();

        // Act
        greaterThanOperator.Apply(query);

        // Assert
        var basicCondition = query.Clauses[0] as BasicCondition;

        basicCondition.Should().NotBeNull();
        basicCondition.Column.Should().Be(column);
        basicCondition.Component.Should().Be("where");
        basicCondition.Operator.Should().Be(">");
        basicCondition.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_ShouldAddConditionWithNumericValueToQuery()
    {
        // Arrange
        var column = "numericColumn";
        var value = 10;
        var greaterThanOperator = new GreaterThanOperator(column, value);
        var query = new Query();

        // Act
        greaterThanOperator.Apply(query);

        // Assert
        var basicCondition = query.Clauses[0] as BasicCondition;

        basicCondition.Should().NotBeNull();
        basicCondition.Column.Should().Be(column);
        basicCondition.Component.Should().Be("where");
        basicCondition.Operator.Should().Be(">");
        basicCondition.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_ShouldAddConditionWithDateTimeValueToQuery()
    {
        // Arrange
        var column = "dateColumn";
        var value = new DateTime(2023, 6, 30);
        var greaterThanOperator = new GreaterThanOperator(column, value);
        var query = new Query();

        // Act
        greaterThanOperator.Apply(query);

        // Assert
        var basicCondition = query.Clauses[0] as BasicCondition;

        basicCondition.Should().NotBeNull();
        basicCondition.Column.Should().Be(column);
        basicCondition.Component.Should().Be("where");
        basicCondition.Operator.Should().Be(">");
        basicCondition.Value.Should().Be(value);
    }

    [Fact]
    public void Apply_ShouldAddConditionWithNullValueToQuery()
    {
        // Arrange
        var column = "nullColumn";
        object value = null;
        var greaterThanOperator = new GreaterThanOperator(column, value);
        var query = new Query();

        // Act
        greaterThanOperator.Apply(query);

        // Assert
        var basicCondition = query.Clauses[0] as NullCondition;

        basicCondition.Should().NotBeNull();
        basicCondition.Column.Should().Be(column);
        basicCondition.Component.Should().Be("where");
    }
}
