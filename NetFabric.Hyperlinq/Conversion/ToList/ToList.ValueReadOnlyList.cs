﻿using System;
using System.Collections.Generic;

namespace NetFabric.Hyperlinq
{
    public static partial class ValueReadOnlyList
    {
        public static List<TSource> ToList<TEnumerable, TEnumerator, TSource>(this TEnumerable source)
            where TEnumerable : IValueReadOnlyList<TSource, TEnumerator>
            where TEnumerator : struct, IValueEnumerator<TSource>
        {
            var count = source.Count;
            if (count > int.MaxValue) 
                ThrowHelper.ThrowArgumentTooLargeException(nameof(source), count);
                
            var list = new List<TSource>((int)count);
            if (count != 0)
                list.AddRange(source.AsList<TEnumerable, TEnumerator, TSource>());
            return list;
        }
    }
}
