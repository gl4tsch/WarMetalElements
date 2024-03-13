using System;
using System.Collections.Generic;
using System.Linq;

namespace WME
{
    public static class Extensions
    {
        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return string.Join(Environment.NewLine, dict);
        }
    }
}