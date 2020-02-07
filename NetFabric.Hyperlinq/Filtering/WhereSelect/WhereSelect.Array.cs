﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class Array
    {
        [Pure]
        internal static ArrayWhereSelectEnumerable<TSource, TResult> WhereSelect<TSource, TResult>(
            this TSource[] source, 
            Predicate<TSource> predicate, 
            Selector<TSource, TResult> selector) 
            => new ArrayWhereSelectEnumerable<TSource, TResult>(source, predicate, selector, 0, source.Length);

        [Pure]
        internal static ArrayWhereSelectEnumerable<TSource, TResult> WhereSelect<TSource, TResult>(
            this TSource[] source,
            Predicate<TSource> predicate,
            Selector<TSource, TResult> selector,
            int skipCount, int takeCount)
            => new ArrayWhereSelectEnumerable<TSource, TResult>(source, predicate, selector, skipCount, takeCount);

        [GeneratorMapping("TSource", "TResult")]
        public readonly partial struct ArrayWhereSelectEnumerable<TSource, TResult>
            : IValueEnumerable<TResult, ArrayWhereSelectEnumerable<TSource, TResult>.DisposableEnumerator>
        {
            internal readonly TSource[] source;
            internal readonly Predicate<TSource> predicate;
            readonly Selector<TSource, TResult> selector;
            readonly int skipCount;
            readonly int takeCount;

            internal ArrayWhereSelectEnumerable(TSource[] source, Predicate<TSource> predicate, Selector<TSource, TResult> selector, int skipCount, int takeCount)
            {
                this.source = source;
                this.predicate = predicate;
                this.selector = selector;
                (this.skipCount, this.takeCount) = Utils.SkipTake(source.Length, skipCount, takeCount);
            }

            [Pure]
            public readonly Enumerator GetEnumerator() 
                => new Enumerator(in this);
            readonly DisposableEnumerator IValueEnumerable<TResult, ArrayWhereSelectEnumerable<TSource, TResult>.DisposableEnumerator>.GetEnumerator() 
                => new DisposableEnumerator(in this);
            readonly IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() 
                => new DisposableEnumerator(in this);
            readonly IEnumerator IEnumerable.GetEnumerator() 
                => new DisposableEnumerator(in this);

            public struct Enumerator
            {
                readonly TSource[] source;
                readonly Predicate<TSource> predicate;
                readonly Selector<TSource, TResult> selector;
                readonly int end;
                int index;

                internal Enumerator(in ArrayWhereSelectEnumerable<TSource, TResult> enumerable)
                {
                    source = enumerable.source;
                    predicate = enumerable.predicate;
                    selector = enumerable.selector;
                    end = enumerable.skipCount + enumerable.takeCount;
                    index = enumerable.skipCount - 1;
                }

                public readonly TResult Current
                    => selector(source[index]);

                public bool MoveNext()
                {
                    while (++index < end)
                    {
                        if (predicate(source[index]))
                            return true;
                    }
                    return false;
                }
            }

            public struct DisposableEnumerator
                : IEnumerator<TResult>
            {
                readonly TSource[] source;
                readonly Predicate<TSource> predicate;
                readonly Selector<TSource, TResult> selector;
                readonly int end;
                int index;

                internal DisposableEnumerator(in ArrayWhereSelectEnumerable<TSource, TResult> enumerable)
                {
                    source = enumerable.source;
                    predicate = enumerable.predicate;
                    selector = enumerable.selector;
                    end = enumerable.skipCount + enumerable.takeCount;
                    index = enumerable.skipCount - 1;
                }

                [MaybeNull]
                public readonly TResult Current
                    => selector(source[index]);
                readonly object? IEnumerator.Current 
                    => selector(source[index]);

                public bool MoveNext()
                {
                    while (++index < end)
                    {
                        if (predicate(source[index]))
                            return true;
                    }
                    return false;
                }

                [ExcludeFromCodeCoverage]
                public readonly void Reset() 
                    => throw new NotSupportedException();

                public readonly void Dispose() { }
            }

            public int Count()
                => Array.Count<TSource>(source, predicate, skipCount, takeCount);
            public int Count(Predicate<TSource> predicate)
                => Array.Count<TSource>(source, Utils.Combine(this.predicate, predicate), skipCount, takeCount);
            public int Count(PredicateAt<TSource> predicate)
                => Array.Count<TSource>(source, Utils.Combine(this.predicate, predicate), skipCount, takeCount);

            public long LongCount()
                => Array.LongCount<TSource>(source, predicate);
            public long LongCount(Predicate<TSource> predicate)
                => Array.LongCount<TSource>(source, Utils.Combine(this.predicate, predicate));

            public bool Any()
                => Array.Any<TSource>(source, predicate, skipCount, takeCount);
            public bool Any(Predicate<TSource> predicate)
                => Array.Any<TSource>(source, Utils.Combine(this.predicate, predicate), skipCount, takeCount);
            public bool Any(PredicateAt<TSource> predicate)
                => Array.Any<TSource>(source, Utils.Combine(this.predicate, predicate), skipCount, takeCount);

            public List<TResult> ToList()
            {
                var list = new List<TResult>();

                var end = skipCount + takeCount;
                for (var index = skipCount; index < end; index++)
                {
                    if (predicate(source[index]))
                        list.Add(selector(source[index]));
                }

                return list;
            }

            public Dictionary<TKey, TResult> ToDictionary<TKey>(Selector<TResult, TKey> keySelector)
                => ToDictionary<TKey>(keySelector, EqualityComparer<TKey>.Default);
            public Dictionary<TKey, TResult> ToDictionary<TKey>(Selector<TResult, TKey> keySelector, IEqualityComparer<TKey> comparer)
            {
                var dictionary = new Dictionary<TKey, TResult>(0, comparer);

                TResult item;
                var end = skipCount + takeCount;
                for (var index = skipCount; index < end; index++)
                {
                    if (predicate(source[index]))
                    {
                        item = selector(source[index]);
                        dictionary.Add(keySelector(item), item);
                    }
                }

                return dictionary;
            }

            public Dictionary<TKey, TElement> ToDictionary<TKey, TElement>(Selector<TResult, TKey> keySelector, Selector<TResult, TElement> elementSelector)
                => ToDictionary<TKey, TElement>(keySelector, elementSelector, EqualityComparer<TKey>.Default);
            public Dictionary<TKey, TElement> ToDictionary<TKey, TElement>(Selector<TResult, TKey> keySelector, Selector<TResult, TElement> elementSelector, IEqualityComparer<TKey> comparer)
            {
                var dictionary = new Dictionary<TKey, TElement>(0, comparer);

                TResult item;
                var end = skipCount + takeCount;
                for (var index = skipCount; index < end; index++)
                {
                    if (predicate(source[index]))
                    {
                        item = selector(source[index]);
                        dictionary.Add(keySelector(item), elementSelector(item));
                    }
                }

                return dictionary;
            }

            public void ForEach(Action<TResult> action)
                => Array.ForEach<TSource, TResult>(source, action, predicate, selector, skipCount, takeCount);
            public void ForEach(ActionAt<TResult> action)
                => Array.ForEach<TSource, TResult>(source, action, predicate, selector, skipCount, takeCount);
        }
    }
}

