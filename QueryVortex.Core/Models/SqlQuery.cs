using SqlKata;
using SqlKata.Compilers;

namespace QueryVortex.Core.Models;

public sealed class SqlQuery
{
    public SqlQuery(Query queryParameters)
    {
        QueryParameters = queryParameters;
    }

    /// <summary>
    ///     Gets or sets the query parameters for the SQL query.
    /// </summary>
    public Query QueryParameters { get; set; }

    public string QueryText => ToSqlServerQueryString();

    /// <summary>
    ///     Converts the query parameters to a SQL Server query string.
    /// </summary>
    /// <returns>The SQL Server query string.</returns>
    public string ToSqlServerQueryString()
    {
        var compiledQuery = new SqlServerCompiler();
        var sqlQueryString = compiledQuery.Compile(QueryParameters).ToString();
        return sqlQueryString;
    }

    /// <summary>
    ///     Converts the query parameters to a MySQL query string.
    /// </summary>
    /// <returns>The MySQL query string.</returns>
    public string ToMySqlQueryString()
    {
        var compiledQuery = new MySqlCompiler();
        var sqlQueryString = compiledQuery.Compile(QueryParameters).ToString();
        return sqlQueryString;
    }

    /// <summary>
    ///     Converts the query parameters to a PostgreSQL query string.
    /// </summary>
    /// <returns>The PostgreSQL query string.</returns>
    public string ToPostgreSqlQueryString()
    {
        var compiledQuery = new PostgresCompiler();
        var sqlQueryString = compiledQuery.Compile(QueryParameters).ToString();
        return sqlQueryString;
    }

    /// <summary>
    ///     Converts the query parameters to a SQLite query string.
    /// </summary>
    /// <returns>The SQLite query string.</returns>
    public string ToSqliteQueryString()
    {
        var compiledQuery = new SqliteCompiler();
        var sqlQueryString = compiledQuery.Compile(QueryParameters).ToString();
        return sqlQueryString;
    }

    /// <summary>
    ///     Converts the query parameters to an Oracle query string.
    /// </summary>
    /// <returns>The Oracle query string.</returns>
    public string ToOracleQueryString()
    {
        var compiledQuery = new OracleCompiler();
        var sqlQueryString = compiledQuery.Compile(QueryParameters).ToString();
        return sqlQueryString;
    }

    /// <summary>
    ///     Converts the query parameters to a Firebird query string.
    /// </summary>
    /// <returns>The Firebird query string.</returns>
    public string ToFirebirdQueryString()
    {
        var compiledQuery = new FirebirdCompiler();
        var sqlQueryString = compiledQuery.Compile(QueryParameters).ToString();
        return sqlQueryString;
    }
}
