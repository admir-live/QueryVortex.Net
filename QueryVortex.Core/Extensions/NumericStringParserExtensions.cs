using System;
using System.Collections.Generic;
using QueryVortex.Core.Models;

namespace QueryVortex.Core.Extensions;

public static class NumericStringParserExtensions
{
    public static ParsedValue<object> TryParseToNumericObject(this string input)
    {
        var parsers = new List<Func<string, ParsedValue<object>>>
        {
            TryParseInt,
            TryParseDouble,
            TryParseLong,
            TryParseDecimal,
            TryParseFloat,
            TryParseShort,
            TryParseByte,
            TryParseUlong,
            TryParseUint,
            TryParseUshort,
            TryParseSbyte
        };

        foreach (var parser in parsers)
        {
            var result = parser(input);
            if (result.Success)
            {
                return result;
            }
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseInt(string input)
    {
        if (int.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseDouble(string input)
    {
        if (double.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseLong(string input)
    {
        if (long.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseDecimal(string input)
    {
        if (decimal.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseFloat(string input)
    {
        if (float.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseShort(string input)
    {
        if (short.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseByte(string input)
    {
        if (byte.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseUlong(string input)
    {
        if (ulong.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseUint(string input)
    {
        if (uint.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseUshort(string input)
    {
        if (ushort.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }

    private static ParsedValue<object> TryParseSbyte(string input)
    {
        if (sbyte.TryParse(input, out var result))
        {
            return new ParsedValue<object> { Success = true, Value = result };
        }

        return new ParsedValue<object> { Success = false };
    }
}
