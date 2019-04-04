using System;
using System.Collections;
using System.Collections.Generic;

namespace NetFabric.Hyperlinq
{
    public static class SortedDictionaryKeysBindings
    {
        public static int Count<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => source.Count;

        public static int Count<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            => ReadOnlyCollection.Count<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static bool All<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            => ReadOnlyCollection.All<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static bool Any<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => source.Count != 0;

        public static bool Any<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            => ReadOnlyCollection.Any<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static bool Contains<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, TKey value)
            => source.Contains(value);

        public static bool Contains<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, TKey value, IEqualityComparer<TKey> comparer)
            => ReadOnlyCollection.Contains<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), value, comparer);

        public static ReadOnlyCollection.SelectEnumerable<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey, TResult> Select<TKey, TValue, TResult>(
            this SortedDictionary<TKey, TValue>.KeyCollection source,
            Func<TKey, TResult> selector) 
            => ReadOnlyCollection.Select<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey, TResult>(new ValueWrapper<TKey, TValue>(source), selector);

        public static Enumerable.SelectManyEnumerable<ValueWrapper<TKey, TValue>,  SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey, TSubEnumerable, TSubEnumerator, TResult> SelectMany<TKey, TValue, TSubEnumerable, TSubEnumerator, TResult>(
            this SortedDictionary<TKey, TValue>.KeyCollection source,
            Func<TKey, TSubEnumerable> selector) 
            where TSubEnumerable : IValueEnumerable<TResult, TSubEnumerator>
            where TSubEnumerator : struct, IValueEnumerator<TResult>
            => Enumerable.SelectMany<ValueWrapper<TKey, TValue>,  SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey, TSubEnumerable, TSubEnumerator, TResult>(new ValueWrapper<TKey, TValue>(source), selector);

        public static Enumerable.WhereEnumerable<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey> Where<TKey, TValue>(
            this SortedDictionary<TKey, TValue>.KeyCollection source,
            Func<TKey, bool> predicate) 
            => Enumerable.Where<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static TKey First<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => ReadOnlyCollection.First<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static TKey First<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            => Enumerable.First<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static TKey FirstOrDefault<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => ReadOnlyCollection.FirstOrDefault<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static TKey FirstOrDefault<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            => Enumerable.FirstOrDefault<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static TKey? FirstOrNull<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            where TKey : struct
            => ReadOnlyCollection.FirstOrNull<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static TKey? FirstOrNull<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            where TKey : struct
            => Enumerable.FirstOrNull<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static TKey Single<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => ReadOnlyCollection.Single<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static TKey Single<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            => Enumerable.Single<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static TKey SingleOrDefault<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => ReadOnlyCollection.SingleOrDefault<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static TKey SingleOrDefault<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            => Enumerable.SingleOrDefault<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static TKey? SingleOrNull<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            where TKey : struct
            => ReadOnlyCollection.SingleOrNull<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static TKey? SingleOrNull<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source, Func<TKey, bool> predicate)
            where TKey : struct
            => Enumerable.SingleOrNull<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source), predicate);

        public static IEnumerable<TValue> AsEnumerable<TKey, TValue>(this SortedDictionary<TKey, TValue> source)
            => source.Values;

        public static IReadOnlyCollection<TValue> AsReadOnlyCollection<TKey, TValue>(this SortedDictionary<TKey, TValue> source)
            => source.Values;

        public static Enumerable.AsValueEnumerableEnumerable<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey> AsValueEnumerable<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => Enumerable.AsValueEnumerable<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static ReadOnlyCollection.AsValueReadOnlyCollectionEnumerable<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey> AsValueReadOnlyCollection<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => ReadOnlyCollection.AsValueReadOnlyCollection<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static TKey[] ToArray<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => ReadOnlyCollection.ToArray<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));

        public static List<TKey> ToList<TKey, TValue>(this SortedDictionary<TKey, TValue>.KeyCollection source)
            => ReadOnlyCollection.ToList<ValueWrapper<TKey, TValue>, SortedDictionary<TKey, TValue>.KeyCollection.Enumerator, TKey>(new ValueWrapper<TKey, TValue>(source));
            
        public readonly struct ValueWrapper<TKey, TValue> : IReadOnlyCollection<TKey>
        {
            readonly SortedDictionary<TKey, TValue>.KeyCollection source;

            public ValueWrapper(SortedDictionary<TKey, TValue>.KeyCollection source)
            {
                this.source = source;
            }

            IEnumerator IEnumerable.GetEnumerator() => source.GetEnumerator();
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => source.GetEnumerator();
            int IReadOnlyCollection<TKey>.Count => source.Count;
        } 
    }
}