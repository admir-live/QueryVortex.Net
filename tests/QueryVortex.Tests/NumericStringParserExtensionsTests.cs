using FluentAssertions;
using QueryVortex.Core.Extensions;
using Xunit;

namespace QueryVortex.Tests;

public class NumericStringParserExtensionsTests
{
    [Fact]
    public void TryParseToNumericObject_ValidIntegerInput_ReturnsTrueAndCorrectValue()
    {
        // Arrange
        var validInteger = "42";

        // Act
        var result = validInteger.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeTrue();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void TryParseToNumericObject_InvalidIntegerInput_ReturnsFalse()
    {
        // Arrange
        var invalidInteger = "abc";

        // Act
        var result = invalidInteger.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_EmptyInput_ReturnsFalse()
    {
        // Arrange
        var emptyInput = "";

        // Act
        var result = emptyInput.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_NullInput_ReturnsFalse()
    {
        // Arrange
        string nullInput = null;

        // Act
        var result = nullInput.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_ValidShortInput_ReturnsTrueAndCorrectValue()
    {
        // Arrange
        var validShort = "32767";

        // Act
        var result = validShort.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeTrue();
        result.Value.Should().Be((short)32767);
    }

    [Fact]
    public void TryParseToNumericObject_ValidByteInput_ReturnsTrueAndCorrectValue()
    {
        // Arrange
        var validByte = "255";

        // Act
        var result = validByte.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeTrue();
        result.Value.Should().Be((byte)255);
    }

    [Fact]
    public void TryParseToNumericObject_ValidUintInput_ReturnsTrueAndCorrectValue()
    {
        // Arrange
        var validUint = "4294967295";

        // Act
        var result = validUint.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeTrue();
        result.Value.Should().Be(4294967295U);
    }

    [Fact]
    public void TryParseToNumericObject_ValidUshortInput_ReturnsTrueAndCorrectValue()
    {
        // Arrange
        var validUshort = "65535";

        // Act
        var result = validUshort.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeTrue();
        result.Value.Should().Be((ushort)65535);
    }

    [Fact]
    public void TryParseToNumericObject_InvalidDoubleInput_ReturnsFalse()
    {
        // Arrange
        var invalidDouble = "42,abc";

        // Act
        var result = invalidDouble.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_InvalidShortInput_ReturnsFalse()
    {
        // Arrange
        var invalidShort = "32767a";

        // Act
        var result = invalidShort.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_InvalidByteInput_ReturnsFalse()
    {
        // Arrange
        var invalidByte = "255a";

        // Act
        var result = invalidByte.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_InvalidUintInput_ReturnsFalse()
    {
        // Arrange
        var invalidUint = "4294967295a";

        // Act
        var result = invalidUint.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_InvalidUshortInput_ReturnsFalse()
    {
        // Arrange
        var invalidUshort = "65535a";

        // Act
        var result = invalidUshort.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_OverflowDoubleInput_ReturnsTrueAndValueIsInfinity()
    {
        // Arrange
        var overflowInteger =
            "814748364881474836488147483648147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648814748364881474836488147483648881474836488147483648814748364881474836488147483648";

        // Act
        var result = overflowInteger.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeTrue();
        double.IsInfinity(Convert.ToDouble(result.Value)).Should().BeTrue();
    }

    [Fact]
    public void TryParseToNumericObject_InvalidLongInput_ReturnsFalse()
    {
        // Arrange
        var invalidLong = "abc";

        // Act
        var result = invalidLong.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_InvalidDecimalInput_ReturnsFalse()
    {
        // Arrange
        var invalidDecimal = "42,abc";

        // Act
        var result = invalidDecimal.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void TryParseToNumericObject_InvalidFloatInput_ReturnsFalse()
    {
        // Arrange
        var invalidFloat = "42,abc";

        // Act
        var result = invalidFloat.TryParseToNumericObject();

        // Assert
        result.Success.Should().BeFalse();
    }
}
