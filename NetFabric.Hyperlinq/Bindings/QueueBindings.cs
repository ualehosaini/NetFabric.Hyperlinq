using System;
using System.Collections;
using System.Collections.Generic;

namespace NetFabric.Hyperlinq
{
    public static class QueueBindings
    {
        public static int Count<TSource>(this Queue<TSource> source)
            => source.Count;

        public static int Count<TSource>(this Queue<TSource> source, Func<TSource, bool> predicate)
            => ReadOnlyCollection.Count<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static bool All<TSource>(this Queue<TSource> source, Func<TSource, bool> predicate)
            => ReadOnlyCollection.All<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static bool Any<TSource>(this Queue<TSource> source)
            => source.Count != 0;

        public static bool Any<TSource>(this Queue<TSource> source, Func<TSource, bool> predicate)
            => ReadOnlyCollection.Any<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static bool Contains<TSource>(this Queue<TSource> source, TSource value)
            => source.Contains(value);

        public static bool Contains<TSource>(this Queue<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
            => ReadOnlyCollection.Contains<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), value, comparer);

        public static ReadOnlyCollection.SelectEnumerable<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource, TResult> Select<TSource, TResult>(
            this Queue<TSource> source,
            Func<TSource, TResult> selector) 
            => ReadOnlyCollection.Select<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource, TResult>(new ValueWrapper<TSource>(source), selector);

        public static Enumerable.SelectManyEnumerable<ValueWrapper<TSource>,  Queue<TSource>.Enumerator,  TSource, TSubEnumerable, TSubEnumerator, TResult> SelectMany<TSource, TSubEnumerable, TSubEnumerator, TResult>(
            this Queue<TSource> source,
            Func<TSource, TSubEnumerable> selector) 
            where TSubEnumerable : IValueEnumerable<TResult, TSubEnumerator>
            where TSubEnumerator : struct, IValueEnumerator<TResult>
            => Enumerable.SelectMany<ValueWrapper<TSource>,  Queue<TSource>.Enumerator, TSource, TSubEnumerable, TSubEnumerator, TResult>(new ValueWrapper<TSource>(source), selector);

        public static Enumerable.WhereEnumerable<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource> Where<TSource>(
            this Queue<TSource> source,
            Func<TSource, bool> predicate) 
            => Enumerable.Where<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static TSource First<TSource, TValue>(this Queue<TSource> source)
            => ReadOnlyCollection.First<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static TSource First<TSource, TValue>(this Queue<TSource> source, Func<TSource, bool> predicate)
            => Enumerable.First<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static TSource FirstOrDefault<TSource, TValue>(this Queue<TSource> source)
            => ReadOnlyCollection.FirstOrDefault<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static TSource FirstOrDefault<TSource, TValue>(this Queue<TSource> source, Func<TSource, bool> predicate)
            => Enumerable.FirstOrDefault<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static TSource? FirstOrNull<TSource, TValue>(this Queue<TSource> source)
            where TSource : struct
            => ReadOnlyCollection.FirstOrNull<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static TSource? FirstOrNull<TSource, TValue>(this Queue<TSource> source, Func<TSource, bool> predicate)
            where TSource : struct
            => Enumerable.FirstOrNull<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static TSource Single<TSource, TValue>(this Queue<TSource> source)
            => ReadOnlyCollection.Single<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static TSource Single<TSource, TValue>(this Queue<TSource> source, Func<TSource, bool> predicate)
            => Enumerable.Single<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static TSource SingleOrDefault<TSource, TValue>(this Queue<TSource> source)
            => ReadOnlyCollection.SingleOrDefault<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static TSource SingleOrDefault<TSource, TValue>(this Queue<TSource> source, Func<TSource, bool> predicate)
            => Enumerable.SingleOrDefault<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static TSource? SingleOrNull<TSource, TValue>(this Queue<TSource> source)
            where TSource : struct
            => ReadOnlyCollection.SingleOrNull<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static TSource? SingleOrNull<TSource, TValue>(this Queue<TSource> source, Func<TSource, bool> predicate)
            where TSource : struct
            => Enumerable.SingleOrNull<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source), predicate);

        public static IEnumerable<TSource> AsEnumerable<TSource>(this Queue<TSource> source)
            => source;

        public static IReadOnlyCollection<TSource> AsReadOnlyCollection<TSource>(this Queue<TSource> source)
            => source;

        public static Enumerable.AsValueEnumerableEnumerable<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource> AsValueEnumerable<TSource>(this Queue<TSource> source)
            => Enumerable.AsValueEnumerable<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static ReadOnlyCollection.AsValueReadOnlyCollectionEnumerable<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource> AsValueReadOnlyCollection<TSource>(this Queue<TSource> source)
            => ReadOnlyCollection.AsValueReadOnlyCollection<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static TSource[] ToArray<TSource>(this Queue<TSource> source)
            => ReadOnlyCollection.ToArray<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));

        public static List<TSource> ToList<TSource>(this Queue<TSource> source)
            => ReadOnlyCollection.ToList<ValueWrapper<TSource>, Queue<TSource>.Enumerator, TSource>(new ValueWrapper<TSource>(source));
            
        public readonly struct ValueWrapper<TSource> : IReadOnlyCollection<TSource>
        {
            readonly Queue<TSource> source;

            public ValueWrapper(Queue<TSource> source)
            {
                this.source = source;
            }

            IEnumerator IEnumerable.GetEnumerator() => source.GetEnumerator();
            IEnumerator<TSource> IEnumerable<TSource>.GetEnumerator() => source.GetEnumerator();
            int IReadOnlyCollection<TSource>.Count => source.Count;
        }    
    }
}