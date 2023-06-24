namespace QueryVortex.Terminal 
{
    internal abstract class Program
    {
        static void Main(string[] args)
        {
            var queryString =
                "select=price,category&filter=((price[gte]=10ANDprice[lte]=100)OR(category[eq]=electronicsANDstock[gt]=0))ANDavailability[eq]=in_stock&order=price[desc],category[asc]";
        }
    }
}