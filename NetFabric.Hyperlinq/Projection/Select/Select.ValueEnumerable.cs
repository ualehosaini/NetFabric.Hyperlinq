﻿using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFabric.Hyperlinq
{
    public static partial class ValueEnumerableExtensions
    {

        [GeneratorMapping("TSelector", "NetFabric.Hyperlinq.FunctionWrapper<TSource, TResult>")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, FunctionWrapper<TSource, TResult>> Select<TEnumerable, TEnumerator, TSource, TResult>(this TEnumerable source, Func<TSource, TResult> selector)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            => source.Select<TEnumerable, TEnumerator, TSource, TResult, FunctionWrapper<TSource, TResult>>(new FunctionWrapper<TSource, TResult>(selector));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector> Select<TEnumerable, TEnumerator, TSource, TResult, TSelector>(this TEnumerable source, TSelector selector = default)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TSelector : struct, IFunction<TSource, TResult>
            => new(in source, selector);

        [GeneratorMapping("TSource", "TResult")]
        [StructLayout(LayoutKind.Auto)]
        public readonly partial struct SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector> 
            : IValueEnumerable<TResult, SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>.Enumerator>
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TSelector : struct, IFunction<TSource, TResult>
        {
            readonly TEnumerable source;
            readonly TSelector selector;

            internal SelectEnumerable(in TEnumerable source, TSelector selector)
                => (this.source, this.selector) = (source, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Enumerator GetEnumerator() 
                => new(in this);
            readonly IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() 
                // ReSharper disable once HeapView.BoxingAllocation
                => new Enumerator(in this);
            readonly IEnumerator IEnumerable.GetEnumerator() 
                // ReSharper disable once HeapView.BoxingAllocation
                => new Enumerator(in this);

            [StructLayout(LayoutKind.Auto)]
            public struct Enumerator
                : IEnumerator<TResult>
            {
                [SuppressMessage("Style", "IDE0044:Add readonly modifier")]
                TEnumerator enumerator; // do not make readonly
                TSelector selector;

                internal Enumerator(in SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector> enumerable)
                {
                    enumerator = enumerable.source.GetEnumerator();
                    selector = enumerable.selector;
                }

                public readonly TResult Current
                {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => selector.Invoke(enumerator.Current);
                }
                readonly TResult IEnumerator<TResult>.Current 
                    => selector.Invoke(enumerator.Current)!;
                readonly object? IEnumerator.Current
                    // ReSharper disable once HeapView.PossibleBoxingAllocation
                    => selector.Invoke(enumerator.Current)!;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext()
                    => enumerator.MoveNext();

                [ExcludeFromCodeCoverage]
                public readonly void Reset() 
                    => Throw.NotSupportedException();

                public void Dispose() 
                    => enumerator.Dispose();
            }

            #region Aggregation

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Count()
                => source.Count<TEnumerable, TEnumerator, TSource>();
            
            #endregion
            #region Quantifier

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Any()
                => source.Any<TEnumerable, TEnumerator, TSource>();
            
            #endregion
            #region Filtering
            
            #endregion
            #region Projection

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult2, SelectorSelectorCombination<TSelector, FunctionWrapper<TResult, TResult2>, TSource, TResult, TResult2>> Select<TResult2>(Func<TResult, TResult2> selector)
                => Select<TResult2, FunctionWrapper<TResult, TResult2>>(new FunctionWrapper<TResult, TResult2>(selector));
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult2, SelectorSelectorCombination<TSelector, TSelector2, TSource, TResult, TResult2>> Select<TResult2, TSelector2>(TSelector2 selector = default)
                where TSelector2 : struct, IFunction<TResult, TResult2>
                => source.Select<TEnumerable, TEnumerator, TSource, TResult2, SelectorSelectorCombination<TSelector, TSelector2, TSource, TResult, TResult2>>(new SelectorSelectorCombination<TSelector, TSelector2, TSource, TResult, TResult2>(this.selector, selector));
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public SelectAtEnumerable<TEnumerable, TEnumerator, TSource, TResult2, SelectorSelectorAtCombination<TSelector, FunctionWrapper<TResult, int, TResult2>, TSource, TResult, TResult2>> Select<TResult2>(Func<TResult, int, TResult2> selector)
                => SelectAt<TResult2, FunctionWrapper<TResult, int, TResult2>>(new FunctionWrapper<TResult, int, TResult2>(selector));
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public SelectAtEnumerable<TEnumerable, TEnumerator, TSource, TResult2, SelectorSelectorAtCombination<TSelector, TSelector2, TSource, TResult, TResult2>> SelectAt<TResult2, TSelector2>(TSelector2 selector = default)
                where TSelector2 : struct, IFunction<TResult, int, TResult2>
                => source.SelectAt<TEnumerable, TEnumerator, TSource, TResult2, SelectorSelectorAtCombination<TSelector, TSelector2, TSource, TResult, TResult2>>(new SelectorSelectorAtCombination<TSelector, TSelector2, TSource, TResult, TResult2>(this.selector, selector));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ValueEnumerableExtensions.SelectManyEnumerable<SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>, SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>.Enumerator, TResult, TSubEnumerable, TSubEnumerator, TResult2, FunctionWrapper<TResult, TSubEnumerable>> SelectMany<TSubEnumerable, TSubEnumerator, TResult2>(Func<TResult, TSubEnumerable> selector)
                where TSubEnumerable : IValueEnumerable<TResult2, TSubEnumerator>
                where TSubEnumerator : struct, IEnumerator<TResult2>
                => this.SelectMany<SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>, SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>.Enumerator, TResult, TSubEnumerable, TSubEnumerator, TResult2>(selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ValueEnumerableExtensions.SelectManyEnumerable<SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>, SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>.Enumerator, TResult, TSubEnumerable, TSubEnumerator, TResult2, TSelector2> SelectMany<TSubEnumerable, TSubEnumerator, TResult2, TSelector2>(TSelector2 selector = default)
                where TSubEnumerable : IValueEnumerable<TResult2, TSubEnumerator>
                where TSubEnumerator : struct, IEnumerator<TResult2>
                where TSelector2 : struct, IFunction<TResult, TSubEnumerable>
                => this.SelectMany<SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>, SelectEnumerable<TEnumerable, TEnumerator, TSource, TResult, TSelector>.Enumerator, TResult, TSubEnumerable, TSubEnumerator, TResult2, TSelector2>(selector);

            #endregion
            #region Element

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> ElementAt(int index)
                => source.ElementAt<TEnumerable, TEnumerator, TSource, TResult, TSelector>(index, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> First()
                => source.First<TEnumerable, TEnumerator, TSource, TResult, TSelector>(selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> Single()
                => source.Single<TEnumerable, TEnumerator, TSource, TResult, TSelector>(selector);
            
            #endregion
            #region Conversion

            public TResult[] ToArray()
                => source.ToArray<TEnumerable, TEnumerator, TSource, TResult, TSelector>(selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IMemoryOwner<TResult> ToArray(MemoryPool<TResult> pool)
                => source.ToArray<TEnumerable, TEnumerator, TSource, TResult, TSelector>(selector, pool);

            public List<TResult> ToList()
                => source.ToList<TEnumerable, TEnumerator, TSource, TResult, TSelector>(selector);
            
            #endregion
        }
    }
}

