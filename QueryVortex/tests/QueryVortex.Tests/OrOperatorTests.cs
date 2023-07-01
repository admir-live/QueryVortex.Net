using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class OrOperatorTests
{
    [Fact]
    public void Apply_WhereClauseAppliedToQuery_ShouldCombineConditionsUsingOrOperator()
    {
        // Arrange
        var query = new Query();

        // Act
        var result = new OrOperator().Apply(query);

        // Assert
        result.Should().BeEquivalentTo(query.Where(query1 => query.From(query1)));
    }
}
