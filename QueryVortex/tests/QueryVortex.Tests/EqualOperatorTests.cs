using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class EqualOperatorTests
{
    [Fact]
    public void Apply_ShouldAddConditionToQuery()
    {
        // Arrange
        var column = "testColumn";
        var value = "testValue";
        var equalOperator = new EqualOperator(column, value);

        var query = new Query();

        // Act
        equalOperator.Apply(query);

        // Assert
        var basicCondition = query.Clauses[0] as BasicCondition;
        basicCondition.Should().NotBeNull();
        basicCondition.Column.Should().Be(column);
        basicCondition.Component.Should().Be("where");
        basicCondition.Operator.Should().Be("=");
        basicCondition.Value.Should().Be(value);
    }
}
