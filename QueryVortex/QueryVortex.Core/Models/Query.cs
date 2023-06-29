namespace QueryVortex.Core.Models;

public class QueryVortex
{
    public List<string> SelectFields { get; set; }

    public List<Filter> Filters { get; set; }

    public List<Order> Orders { get; set; }
}
