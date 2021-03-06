﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFabric.Hyperlinq
{
    static class ListExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArraySegment<TSource> AsArraySegment<TSource>(this List<TSource> source)
            => new(source.GetItems(), 0, source.Count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<TSource> AsSpan<TSource>(this List<TSource> source)
#if NET5_0
            => CollectionsMarshal.AsSpan(source);
#else
            => source.GetItems().AsSpan().Slice(0, source.Count);
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<TSource> AsMemory<TSource>(this List<TSource> source)
            => source.GetItems().AsMemory().Slice(0, source.Count);


        public static List<TSource> AsList<TSource>(this TSource[] source)
        {
            var result = new List<TSource>();
            var layout = Unsafe.As<List<TSource>, ListLayout<TSource>>(ref result);
            layout.Items = source;
            layout.Size = source.Length;
            result.Capacity = source.Length;
            return result;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
#if NET5_0        
        [SkipLocalsInit]
#endif
        class ListLayout<TSource>
        {
            public TSource[]? Items;
#if !(NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0)
#pragma warning disable IDE0051 // Remove unused private members
            readonly object? syncRoot;
#pragma warning restore IDE0051 // Remove unused private members
#endif
            public int Size;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TSource[] GetItems<TSource>(this List<TSource> source)
            => Unsafe.As<List<TSource>, ListLayout<TSource>>(ref source).Items!;
    }
}
