using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using SqlKata.Compilers;
using Xunit;

namespace QueryVortex.Tests;

public class LessThanOperatorTests
{
    private readonly SqlServerCompiler _compiler;

    public LessThanOperatorTests()
    {
        _compiler = new SqlServerCompiler();
    }

    [Fact]
    public void Apply_WithNonNullValue_AddsLessThanConditionToQuery()
    {
        // Arrange
        var columnName = "Column1";
        var value = 123;
        var expectedSql = $"SELECT * WHERE [{columnName}] < @p0";
        var lessThanOperator = new LessThanOperator(columnName, value);
        var query = new Query();

        // Act
        lessThanOperator.Apply(query);

        // Assert
        var sqlResult = _compiler.Compile(query);
        sqlResult.Sql.Should().Be(expectedSql);
        sqlResult.Bindings.Should().Contain(value);
    }

    [Fact]
    public void Apply_WithStringValue_AddsLessThanConditionToQuery()
    {
        // Arrange
        var columnName = "Column1";
        var value = "Value1";
        var expectedSql = $"SELECT * WHERE [{columnName}] < @p0";
        var lessThanOperator = new LessThanOperator(columnName, value);
        var query = new Query();

        // Act
        lessThanOperator.Apply(query);

        // Assert
        var sqlResult = _compiler.Compile(query);
        sqlResult.Sql.Should().Be(expectedSql);
        sqlResult.Bindings.Should().Contain(value);
    }
}
