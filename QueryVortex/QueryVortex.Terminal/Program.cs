using QueryVortex.Core.Parsers;
using SqlKata;
using SqlKata.Compilers;

namespace QueryVortex.Terminal;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        var queryString =
            "select=price,category&filter=((price[gte]=10[AND]price[lte]=100)[OR](category[eq]=electronics[AND]stock[gt]=0))[AND]availability[eq]=in_stock&order=price[desc],category[asc]";

        var parser = new DefaultQueryStringParserStrategy();
        var operatorParser = new OperatorParser();
        var sqlCompiler = new SqlServerCompiler();
        var filters = parser.GetFilters(parser.ParseQueryString(queryString));
        var query = new Query();
        foreach (var f in filters)
        {
            Console.WriteLine($"{f.Operator} {f.Field} {f.Value} {f.LogicalOperator}");
            // var condition = operatorParser.ParseOperator(f.Operator, f.Field, new[] { f.Value });
            // condition.Apply(query);
        }

        Console.WriteLine(sqlCompiler.Compile(query).Sql);
    }
}
