namespace QueryVortex.Core.Models;

public struct ParsedValue<T>
{
    public bool Success { get; set; }
    public T Value { get; set; }
}
