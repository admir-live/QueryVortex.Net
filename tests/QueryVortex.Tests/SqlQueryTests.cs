// Copyright (c) Penzle LLC.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using FluentAssertions;
using QueryVortex.Core.Models;
using SqlKata;
using Xunit;

namespace QueryVortex.Tests;

public class SqlQueryTests
{
    [Fact]
    public void ToSqlServerQueryString_WithValidQueryParameters_ReturnsSqlServerQueryString()
    {
        // Arrange
        var queryParameters = new Query();
        var sqlQuery = new SqlQuery(queryParameters);

        // Act
        var result = sqlQuery.ToSqlServerQueryString();

        // Assert
        result.Should().NotBeNull();
        result.Should().Be("SELECT *");
    }

    [Fact]
    public void ToMySqlQueryString_WithValidQueryParameters_ReturnsMySqlQueryString()
    {
        // Arrange
        var queryParameters = new Query();
        var sqlQuery = new SqlQuery(queryParameters);

        // Act
        var result = sqlQuery.ToMySqlQueryString();

        // Assert
        result.Should().NotBeNull();
        result.Should().Be("SELECT *");
    }

    [Fact]
    public void ToPostgreSqlQueryString_WithValidQueryParameters_ReturnsPostgreSqlQueryString()
    {
        // Arrange
        var queryParameters = new Query();
        var sqlQuery = new SqlQuery(queryParameters);

        // Act
        var result = sqlQuery.ToPostgreSqlQueryString();

        // Assert
        result.Should().NotBeNull();
        result.Should().Be("SELECT *");
    }

    [Fact]
    public void ToSqliteQueryString_WithValidQueryParameters_ReturnsSqliteQueryString()
    {
        // Arrange
        var queryParameters = new Query();
        var sqlQuery = new SqlQuery(queryParameters);

        // Act
        var result = sqlQuery.ToSqliteQueryString();

        // Assert
        result.Should().NotBeNull();
        result.Should().Be("SELECT *");
    }

    [Fact]
    public void ToOracleQueryString_WithValidQueryParameters_ReturnsOracleQueryString()
    {
        // Arrange
        var queryParameters = new Query();
        var sqlQuery = new SqlQuery(queryParameters);

        // Act
        var result = sqlQuery.ToOracleQueryString();

        // Assert
        result.Should().NotBeNull();
        result.Should().Be("SELECT *");
    }

    [Fact]
    public void ToFirebirdQueryString_WithValidQueryParameters_ReturnsFirebirdQueryString()
    {
        // Arrange
        var queryParameters = new Query();
        var sqlQuery = new SqlQuery(queryParameters);

        // Act
        var result = sqlQuery.ToFirebirdQueryString();

        // Assert
        result.Should().NotBeNull();
        result.Should().Be("SELECT *");
    }
}
