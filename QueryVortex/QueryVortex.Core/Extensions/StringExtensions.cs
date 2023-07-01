namespace QueryVortex.Core.Extensions;

/// <summary>
///     Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Removes all white spaces from the input string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The string with all white spaces removed.</returns>
    public static string RemoveWhiteSpace(this string input)
    {
        return string.IsNullOrEmpty(input)
            ? input
            : new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
    }
}
