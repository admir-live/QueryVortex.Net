using QueryVortex.Core;

namespace QueryVortex.Terminal;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        var queryString =
            "select=price,category&filter=((price[gte]=10ANDprice[lte]=100)OR(category[eq]=electronicsANDstock[gt]=0))ANDavailability[eq]=in_stock&order=price[desc],category[asc]";

        var parser = new DefaultQueryStringParserStrategy();

        var filters = parser.GetFilters(parser.ParseQueryString(queryString));

        foreach (var f in filters)
        {
            Console.WriteLine(f.Field);
            Console.WriteLine(f.Operator);
            Console.WriteLine(f.Value);
            Console.WriteLine(f.LogicalOperator);
        }
    }
}
