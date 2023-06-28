namespace QueryVortex.Core.Extensions;

public static class StringExtensions
{
    public static string RemoveWhiteSpace(this string input)
    {
        return string.IsNullOrEmpty(input)
            ? input
            : new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
    }
}
