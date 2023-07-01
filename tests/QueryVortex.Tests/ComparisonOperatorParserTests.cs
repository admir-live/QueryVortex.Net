using FluentAssertions;
using QueryVortex.Core.Extensions;
using QueryVortex.Core.Operators;
using QueryVortex.Core.Parsers;
using Xunit;

namespace QueryVortex.Tests;

public class ComparisonOperatorParserTests
{
    [Fact]
    public void OperatorParser_InitializeOperatorAliases_ShouldReturnValidAliases()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();

        // Act
        var aliases = parser.OperatorAliases;

        // Assert
        aliases.Should().NotBeNull();
        aliases.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void OperatorParser_ParseComparisonOperator_ValidAlias_ShouldReturnCondition()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "in";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().NotBeNull();
        condition.Should().BeOfType<InOperator>();
    }

    [Fact]
    public void OperatorParser_ParseComparisonOperator_InvalidAlias_ShouldThrowArgumentException()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "invalid_alias";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        Action act = () => parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ParseComparisonOperator_EmptyAlias_ShouldThrowArgumentException()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        Action act = () => parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ParseComparisonOperator_NullAlias_ShouldThrowArgumentException()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        string operatorAlias = null;
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        Action act = () => parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ParseComparisonOperator_NullValues_ShouldThrowArgumentException()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "in";
        var column = "TestColumn";
        object[] values = null;

        // Act
        Action act = () => parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ParseComparisonOperator_OR_ShouldReturnOrOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "$OR";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<OrOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_IN_ShouldReturnInOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "in";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<InOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_NOTIN_ShouldReturnNotInOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "notin";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<NotInOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_EQ_ShouldReturnEqualOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "eq";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<EqualOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_LIKE_ShouldReturnLikeOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "like";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<LikeOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_NEQ_ShouldReturnNotEqualOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "neq";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<NotEqualOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_NOTLIKE_ShouldReturnNotLikeOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "notlike";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<NotLikeOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_LT_ShouldReturnLessThanOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "lt";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<LessThanOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_GT_ShouldReturnGreaterThanOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "gt";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<GreaterThanOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_LTE_ShouldReturnLessOrEqualThanOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "lte";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<LessOrEqualThanOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_GTE_ShouldReturnGreaterOrEqualThanOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "gte";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<GreaterOrEqualThanOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_STARTSWITH_ShouldReturnStartsWithOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "startswith";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<StartsWithOperator>();
    }

    [Fact]
    public void ParseComparisonOperator_ENDSWITH_ShouldReturnEndsWithOperator()
    {
        // Arrange
        var parser = new ComparisonOperatorParser();
        var operatorAlias = "endswith";
        var column = "TestColumn";
        object[] values = { "TestValue" };

        // Act
        var condition = parser.ParseComparisonOperator(operatorAlias.ToComparisonOperator(), column, values);

        // Assert
        condition.Should().BeOfType<EndsWithOperator>();
    }
}
