using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static bool EquivalentTo<T>(this IEnumerable<T> l1, IEnumerable<T> l2) { return !l1.Except(l2).Union(l2.Except(l1)).Any(); }
}
