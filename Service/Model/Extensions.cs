using System;
using System.Collections;
using System.Collections.Generic;

namespace Service.Model
{
    public static class Extensions
    {
        public static bool EqualsNoCase(this string left, string right) =>
            string.Equals(left, right, StringComparison.InvariantCultureIgnoreCase);

        public static bool IsEmpty<T>(this ICollection<T> collection) =>
            collection.Count == 0;

        public static bool IsNotEmpty<T>(this ICollection<T> collection) =>
            !collection.IsEmpty();
    }
}