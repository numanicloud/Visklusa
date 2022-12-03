using System;
using System.Linq;

namespace Visklusa.Abstraction;

public static class ArrayHelper
{
    public static string GetStringToPrint<T>(this T[] records)
    {
        return string.Join(", ", records.Select(x => x.ToString()));
    }

    public static int GetDeepHashCode<T>(this T[] records, int seed)
        where T : notnull
    {
        return records.Aggregate(seed, (s, x) => unchecked(x.GetHashCode() * s * 19));
    }
}