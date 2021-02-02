﻿using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFabric.Hyperlinq
{
    public static partial class ArrayExtensions
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector> WhereSelect<TSource, TResult, TPredicate, TSelector>(
            this ReadOnlyMemory<TSource> source, 
            TPredicate predicate, 
            TSelector selector) 
            where TPredicate : struct, IFunction<TSource, bool>
            where TSelector : struct, IFunction<TSource, TResult>
            => new(source, predicate, selector);

        [GeneratorMapping("TSource", "TResult")]
        [StructLayout(LayoutKind.Auto)]
        public readonly partial struct MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>
            : IValueEnumerable<TResult, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator>
            where TPredicate : struct, IFunction<TSource, bool>
            where TSelector : struct, IFunction<TSource, TResult>
        {
            readonly ReadOnlyMemory<TSource> source;
            readonly TPredicate predicate;
            readonly TSelector selector;

            internal MemoryWhereSelectEnumerable(ReadOnlyMemory<TSource> source, TPredicate predicate, TSelector selector)
                => (this.source, this.predicate, this.selector) = (source, predicate, selector);

            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Enumerator GetEnumerator() 
                => new(in this);
            readonly DisposableEnumerator IValueEnumerable<TResult, DisposableEnumerator>.GetEnumerator() 
                => new(in this);
            readonly IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() 
                // ReSharper disable once HeapView.BoxingAllocation
                => new DisposableEnumerator(in this);
            readonly IEnumerator IEnumerable.GetEnumerator() 
                // ReSharper disable once HeapView.BoxingAllocation
                => new DisposableEnumerator(in this);

            [StructLayout(LayoutKind.Sequential)]
            public ref struct Enumerator
            {
                int index;
                readonly int end;
                readonly ReadOnlySpan<TSource> source;
                TPredicate predicate;
                TSelector selector;

                internal Enumerator(in MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector> enumerable)
                {
                    source = enumerable.source.Span;
                    predicate = enumerable.predicate;
                    selector = enumerable.selector;
                    index = -1;
                    end = index + source.Length;
                }

                public TResult Current 
                {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => selector.Invoke(source[index]);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext()
                {
                    while (++index <= end)
                    {
                        if (predicate.Invoke(source[index]))
                            return true;
                    }
                    return false;
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct DisposableEnumerator
                : IEnumerator<TResult>
            {
                int index;
                readonly int end;
                readonly ReadOnlyMemory<TSource> source;
                TPredicate predicate;
                TSelector selector;

                internal DisposableEnumerator(in MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector> enumerable)
                {
                    source = enumerable.source;
                    predicate = enumerable.predicate;
                    selector = enumerable.selector;
                    index = -1;
                    end = index + source.Length;
                }

                public TResult Current 
                {
                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    get => selector.Invoke(source.Span[index]);
                }
                TResult IEnumerator<TResult>.Current 
                    => selector.Invoke(source.Span[index]);
                object? IEnumerator.Current
                    // ReSharper disable once HeapView.PossibleBoxingAllocation
                    => selector.Invoke(source.Span[index]);

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public bool MoveNext()
                {
                    var span = source.Span;
                    while (++index <= end)
                    {
                        if (predicate.Invoke(span[index]))
                            return true;
                    }
                    return false;
                }

                [ExcludeFromCodeCoverage]
                public readonly void Reset() 
                    => throw new NotSupportedException();

                public void Dispose() { }                
            }

            #region Aggregation

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Count()
                => source.Span.Count(predicate);

            #endregion
            #region Quantifier

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool All()
                => source.Span.All(predicate);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool All(Func<TResult, bool> predicate)
                => All(new FunctionWrapper<TResult, bool>(predicate));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool All<TPredicate2>(TPredicate2 predicate)
                where TPredicate2 : struct, IFunction<TResult, bool>
                => this.All<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TPredicate2>(predicate);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool All(Func<TResult, int, bool> predicate)
                => AllAt(new FunctionWrapper<TResult, int, bool>(predicate));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool AllAt<TPredicate2>(TPredicate2 predicate)
                where TPredicate2 : struct, IFunction<TResult, int, bool>
                => this.AllAt<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TPredicate2>(predicate);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Any()
                => source.Span.Any(predicate);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Any(Func<TResult, bool> predicate)
                => Any(new FunctionWrapper<TResult, bool>(predicate));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Any<TPredicate2>(TPredicate2 predicate)
                where TPredicate2 : struct, IFunction<TResult, bool>
                => this.Any<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TPredicate2>(predicate);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Any(Func<TResult, int, bool> predicate)
                => AnyAt(new FunctionWrapper<TResult, int, bool>(predicate));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool AnyAt<TPredicate2>(TPredicate2 predicate)
                where TPredicate2 : struct, IFunction<TResult, int, bool>
                => this.AnyAt<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TPredicate2>(predicate);

            #endregion
            #region Filtering

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ValueEnumerableExtensions.WhereEnumerable<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, FunctionWrapper<TResult, bool>> Where(Func<TResult, bool> predicate)
                => this.Where<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult>(predicate);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ValueEnumerableExtensions.WhereEnumerable<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TPredicate2> Where<TPredicate2>(TPredicate2 predicate = default)
                where TPredicate2 : struct, IFunction<TResult, bool>
                => this.Where<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TPredicate2>(predicate);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ValueEnumerableExtensions.WhereAtEnumerable<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, FunctionWrapper<TResult, int, bool>> Where(Func<TResult, int, bool> predicate)
                => this.Where<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult>(predicate);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ValueEnumerableExtensions.WhereAtEnumerable<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TPredicate2> WhereAt<TPredicate2>(TPredicate2 predicate = default)
                where TPredicate2 : struct, IFunction<TResult, int, bool>
                => this.WhereAt<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TPredicate2>(predicate);

            #endregion
            #region Projection

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public MemoryWhereSelectEnumerable<TSource, TResult2, TPredicate, SelectorSelectorCombination<TSelector, FunctionWrapper<TResult, TResult2>, TSource, TResult, TResult2>> Select<TResult2>(Func<TResult, TResult2> selector)
                => Select<TResult2, FunctionWrapper<TResult, TResult2>>(new FunctionWrapper<TResult, TResult2>(selector));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public MemoryWhereSelectEnumerable<TSource, TResult2, TPredicate, SelectorSelectorCombination<TSelector, TSelector2, TSource, TResult, TResult2>> Select<TResult2, TSelector2>(TSelector2 selector = default)
                where TSelector2 : struct, IFunction<TResult, TResult2>
                => source.WhereSelect<TSource, TResult2, TPredicate, SelectorSelectorCombination<TSelector, TSelector2, TSource, TResult, TResult2>>(predicate, new SelectorSelectorCombination<TSelector, TSelector2, TSource, TResult, TResult2>(this.selector, selector));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ValueEnumerableExtensions.SelectManyEnumerable<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TSubEnumerable, TSubEnumerator, TResult2, FunctionWrapper<TResult, TSubEnumerable>> SelectMany<TSubEnumerable, TSubEnumerator, TResult2>(Func<TResult, TSubEnumerable> selector)
                where TSubEnumerable : IValueEnumerable<TResult2, TSubEnumerator>
                where TSubEnumerator : struct, IEnumerator<TResult2>
                => this.SelectMany<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TSubEnumerable, TSubEnumerator, TResult2>(selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly ValueEnumerableExtensions.SelectManyEnumerable<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TSubEnumerable, TSubEnumerator, TResult2, TSelector2> SelectMany<TSubEnumerable, TSubEnumerator, TResult2, TSelector2>(TSelector2 selector = default)
                where TSubEnumerable : IValueEnumerable<TResult2, TSubEnumerator>
                where TSubEnumerator : struct, IEnumerator<TResult2>
                where TSelector2 : struct, IFunction<TResult, TSubEnumerable>
                => this.SelectMany<MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>, MemoryWhereSelectEnumerable<TSource, TResult, TPredicate, TSelector>.DisposableEnumerator, TResult, TSubEnumerable, TSubEnumerator, TResult2, TSelector2>(selector);

            #endregion
            #region Element

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> ElementAt(int index)
                => source.Span.ElementAt<TSource, TResult, TPredicate, TSelector>(index, predicate, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> First()
                => source.Span.First<TSource, TResult, TPredicate, TSelector>(predicate, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Option<TResult> Single()
                => source.Span.Single<TSource, TResult, TPredicate, TSelector>(predicate, selector);
            
            #endregion
            #region Conversion

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TResult[] ToArray()
                => source.Span.ToArray<TSource, TResult, TPredicate, TSelector>(predicate, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IMemoryOwner<TResult> ToArray(MemoryPool<TResult> memoryPool)
                => source.Span.ToArray(predicate, selector, memoryPool);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public List<TResult> ToList()
                => source.Span.ToList<TSource, TResult, TPredicate, TSelector>(predicate, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Dictionary<TKey, TResult> ToDictionary<TKey>(Func<TResult, TKey> keySelector, IEqualityComparer<TKey>? comparer = default)
                where TKey : notnull
                => ToDictionary<TKey, FunctionWrapper<TResult, TKey>>(new FunctionWrapper<TResult, TKey>(keySelector), comparer);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Dictionary<TKey, TResult> ToDictionary<TKey, TKeySelector>(TKeySelector keySelector, IEqualityComparer<TKey>? comparer = default)
                where TKey : notnull
                where TKeySelector : struct, IFunction<TResult, TKey>
                => source.Span.ToDictionary<TSource, TKey, TKeySelector, TResult, TPredicate, TSelector>(keySelector, comparer, predicate, selector);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Dictionary<TKey, TElement> ToDictionary<TKey, TElement>(Func<TResult, TKey> keySelector, Func<TResult, TElement> elementSelector, IEqualityComparer<TKey>? comparer = default)
                where TKey : notnull
                => ToDictionary<TKey, TElement, FunctionWrapper<TResult, TKey>, FunctionWrapper<TResult, TElement>>(new FunctionWrapper<TResult, TKey>(keySelector), new FunctionWrapper<TResult, TElement>(elementSelector), comparer);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Dictionary<TKey, TElement> ToDictionary<TKey, TElement, TKeySelector, TElementSelector>(TKeySelector keySelector, TElementSelector elementSelector, IEqualityComparer<TKey>? comparer = default)
                where TKey : notnull
                where TKeySelector : struct, IFunction<TResult, TKey>
                where TElementSelector : struct, IFunction<TResult, TElement>
                => source.Span.ToDictionary<TSource, TKey, TElement, TKeySelector, TElementSelector, TResult, TPredicate, TSelector>(keySelector, elementSelector, comparer, predicate, selector);
            
            #endregion

            public bool SequenceEqual(IEnumerable<TResult> other, IEqualityComparer<TResult>? comparer = null)
            {
                comparer ??= EqualityComparer<TResult>.Default;

                var enumerator = GetEnumerator();
                using var otherEnumerator = other.GetEnumerator();
                while (true)
                {
                    var thisEnded = !enumerator.MoveNext();
                    var otherEnded = !otherEnumerator.MoveNext();

                    if (thisEnded != otherEnded)
                        return false;

                    if (thisEnded)
                        return true;

                    if (!comparer.Equals(enumerator.Current!, otherEnumerator.Current))
                        return false;
                }
            }
        }
    }
}

