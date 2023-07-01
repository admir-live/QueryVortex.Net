using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class StartsWithOperatorTests
{
    [Fact]
    public void Apply_WhereClauseAppliedToQuery_ShouldUseLikeOperatorWithStartsWithValue()
    {
        // Arrange
        var query = new Query();
        var column = "name";
        var value = "John";

        // Act
        var result = new StartsWithOperator(column, value).Apply(query);

        // Assert
        result.Should().BeEquivalentTo(query.Where(column, "LIKE", $"{value}%"));
    }
}
