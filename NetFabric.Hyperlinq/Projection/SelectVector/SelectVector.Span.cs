﻿using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class ArrayExtensions
    {

        [GeneratorIgnore]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanSelectVectorEnumerable<TSource, TResult, FunctionWrapper<Vector<TSource>, Vector<TResult>>, FunctionWrapper<TSource, TResult>> SelectVector<TSource, TResult>(this Span<TSource> source, Func<Vector<TSource>, Vector<TResult>> vectorSelector, Func<TSource, TResult> selector)
            where TSource : struct
            where TResult : struct
            => ((ReadOnlySpan<TSource>)source).SelectVector(vectorSelector, selector);

        [GeneratorIgnore]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanSelectVectorEnumerable<TSource, TResult, TSelector, TSelector> SelectVector<TSource, TResult, TSelector>(this Span<TSource> source, TSelector selector = default)
            where TSelector : struct, IFunction<Vector<TSource>, Vector<TResult>>, IFunction<TSource, TResult>
            where TSource : struct
            where TResult : struct
            => ((ReadOnlySpan<TSource>)source).SelectVector<TSource, TResult, TSelector, TSelector>(selector, selector);

        [GeneratorIgnore]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanSelectVectorEnumerable<TSource, TResult, TVectorSelector, TSelector> SelectVector<TSource, TResult, TVectorSelector, TSelector>(this Span<TSource> source, TVectorSelector vectorSelector = default, TSelector selector = default)
            where TVectorSelector : struct, IFunction<Vector<TSource>, Vector<TResult>>
            where TSelector : struct, IFunction<TSource, TResult>
            where TSource : struct
            where TResult : struct
            => ((ReadOnlySpan<TSource>)source).SelectVector<TSource, TResult, TVectorSelector, TSelector>(vectorSelector, selector);

    }
}

