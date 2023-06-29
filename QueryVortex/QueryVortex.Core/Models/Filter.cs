using QueryVortex.Core.Operators;

namespace QueryVortex.Core.Models;

public class Filter
{
    public string Field { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }
    public LogicalOperator LogicalOperator { get; set; }
}
