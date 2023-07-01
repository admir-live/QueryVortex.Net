using FluentAssertions;
using QueryVortex.Core.Operators;
using SqlKata;
using SqlKata.Compilers;
using Xunit;

namespace QueryVortex.Tests;

public class LessThanOperatorTests
{
    private readonly Compiler _compiler;
    private readonly Query _query;

    public LessThanOperatorTests()
    {
        _query = new Query("test"); // assuming "test" as a default table name
        _compiler = new SqlServerCompiler(); // assuming SQL Server as the database
    }

    [Fact]
    public void Apply_WithValidInputs_ShouldApplyConditionToQuery()
    {
        // Arrange
        var column = "test_column";
        var value = "test_value";
        var operatorUnderTest = new LessThanOperator(column, value);

        // Act
        operatorUnderTest.Apply(_query);

        // Assert
        var sql = _compiler.Compile(_query).ToString();
        sql.Should().Contain($"[{column}] < '{value}'");
    }

    [Fact]
    public void Apply_WithNullColumn_ShouldThrowArgumentNullException()
    {
        // Arrange
        var value = "test_value";
        var operatorUnderTest = new LessThanOperator(null, value);

        // Act
        var act = () => operatorUnderTest.Apply(_query);

        // Assert
        act.Should().ThrowExactly<ArgumentNullException>().WithMessage("*_column*");
    }

    [Fact]
    public void Apply_WithDifferentDataType_ShouldApplyConditionToQuery()
    {
        // Arrange
        var column = "test_column";
        var value = 123; // using integer value
        var operatorUnderTest = new LessThanOperator(column, value);

        // Act
        operatorUnderTest.Apply(_query);

        // Assert
        var sql = _compiler.Compile(_query).ToString();
        sql.Should().Contain($"[{column}] < {value}");
    }
}
