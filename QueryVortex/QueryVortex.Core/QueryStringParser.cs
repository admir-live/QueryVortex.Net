using System.Collections.Specialized;
using System.Web;
using QueryVortex.Core.Extensions;

namespace QueryVortex.Core;

public class QueryStringParser
{
    private const string Select = "select";
    private const string Filter = "filter";
    private const string Order = "order";
    private const string And = "AND";
    private const string Or = "OR";
    private const string Asc = "asc";

    private readonly NameValueCollection _queryParameterCollection;

    public QueryStringParser(string queryString)
    {
        _queryParameterCollection = HttpUtility.ParseQueryString(queryString.RemoveWhiteSpace());
    }

    public QueryVortex GetQueryObject()
    {
        var query = new QueryVortex { SelectFields = GetSelectFields(), Filters = GetFilters(), Orders = GetOrders() };

        return query;
    }

    public List<string> GetSelectFields()
    {
        var selectString = _queryParameterCollection.Get(Select);
        return selectString?.Split(',').ToList();
    }

    public List<Filter> GetFilters()
    {
        var filterString = _queryParameterCollection.Get(Filter);
        var filterElements = filterString?.Split(new[] { And, Or }, StringSplitOptions.None);
        var filters = new List<Filter>();

        foreach (var element in filterElements ?? Array.Empty<string>())
        {
            var filter = CreateFilterFrom(element);
            filters.Add(filter);
        }

        return filters;
    }

    public Filter CreateFilterFrom(string element)
    {
        var elements = element.Trim('(', ')').Split(new[] { '[', ']', '=' }, StringSplitOptions.None)
            .Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

        if (elements.Length < 3)
        {
            throw new ArgumentException($"Invalid filter string: {element}");
        }

        return new Filter { Field = elements[0], Operator = elements[1], Value = elements[2], LogicalOperator = element.Contains(Or) ? LogicalOperator.Or : LogicalOperator.And };
    }

    public List<Order> GetOrders()
    {
        var orderString = _queryParameterCollection.Get(Order);
        var orderElements = orderString?.Split(',');
        var orders = new List<Order>();

        foreach (var element in orderElements ?? Array.Empty<string>())
        {
            var order = CreateOrderFrom(element);
            orders.Add(order);
        }

        return orders;
    }

    public Order CreateOrderFrom(string element)
    {
        var elements = element.Split(new[] { '[', ']' }, StringSplitOptions.None);

        if (elements.Length < 2)
        {
            throw new ArgumentException($"Invalid order string: {element}");
        }

        return new Order { Field = elements[0], OrderType = elements[1] == Asc ? OrderType.Asc : OrderType.Desc };
    }
}
