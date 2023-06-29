// Copyright (c) Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using FluentAssertions;
using QueryVortex.Core.Extensions;
using Xunit;

namespace QueryVortex.Tests;

public class RemoveWhiteSpaceMethod
{
    [Fact]
    public void WhenInputIsNull_ShouldReturnNull()
    {
        // Arrange
        string input = null;

        // Act
        var result = input.RemoveWhiteSpace();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void WhenInputIsEmpty_ShouldReturnEmpty()
    {
        // Arrange
        var input = "";

        // Act
        var result = input.RemoveWhiteSpace();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void WhenInputHasNoWhiteSpace_ShouldReturnSameString()
    {
        // Arrange
        var input = "teststring";

        // Act
        var result = input.RemoveWhiteSpace();

        // Assert
        result.Should().Be(input);
    }

    [Fact]
    public void WhenInputHasWhiteSpace_ShouldReturnStringWithoutWhiteSpace()
    {
        // Arrange
        var input = "test string with spaces";

        // Act
        var result = input.RemoveWhiteSpace();

        // Assert
        result.Should().Be("teststringwithspaces");
    }
}
