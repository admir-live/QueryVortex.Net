using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using SqlKata.Compilers;
using Xunit;

namespace QueryVortex.Tests;

public class NotLikeOperatorTests
{
    private readonly Compiler _compiler;
    private readonly Query _query;

    public NotLikeOperatorTests()
    {
        _query = new Query("test");
        _compiler = new SqlServerCompiler();
    }

    [Fact]
    public void Apply_WithValidInputs_ShouldApplyConditionToQuery()
    {
        // Arrange
        var column = "test_column";
        var value = "test_value";
        var operatorUnderTest = new NotLikeOperator(column, value);

        // Act
        operatorUnderTest.Apply(_query);

        // Assert
        var sql = _compiler.Compile(_query).ToString();
        sql.Should().Contain($"[{column}] NOT LIKE '{value}'".ToLower());
    }


    [Fact]
    public void Apply_WithNullColumn_ShouldThrowArgumentNullException()
    {
        // Arrange
        var value = "test_value";

        // Act
        var act = () => new NotLikeOperator(null, value);

        // Assert
        act.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Apply_WithEmptyValue_ShouldApplyConditionToQuery()
    {
        // Arrange
        var column = "test_column";
        var value = string.Empty;
        var operatorUnderTest = new NotLikeOperator(column, value);

        // Act
        operatorUnderTest.Apply(_query);

        // Assert
        var sql = _compiler.Compile(_query).ToString();
        sql.Should().Contain($"[{column}] NOT LIKE '{value}'".ToLower());
    }
}
