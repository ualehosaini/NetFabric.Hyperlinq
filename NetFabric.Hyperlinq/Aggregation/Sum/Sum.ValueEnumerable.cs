using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class ValueEnumerableExtensions
    {
        internal static TSum Sum<TEnumerable, TEnumerator, TSource, TSum>(this TEnumerable source)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
                sum = GenericsOperator.Sum(enumerator.Current, sum);
            return sum;
        }

        internal static TSum Sum<TEnumerable, TEnumerator, TSource, TSum, TPredicate>(this TEnumerable source, TPredicate predicate)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TPredicate : struct, IFunction<TSource, bool>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (predicate.Invoke(item))
                    sum = GenericsOperator.Sum(item, sum);
            }
            return sum;
        }

        internal static TSum SumRef<TEnumerable, TEnumerator, TSource, TSum, TPredicate>(this TEnumerable source, TPredicate predicate)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TPredicate : struct, IFunctionIn<TSource, bool>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (predicate.Invoke(in item))
                    sum = GenericsOperator.Sum(item, sum);
            }
            return sum;
        }

        internal static TSum SumAt<TEnumerable, TEnumerator, TSource, TSum, TPredicate>(this TEnumerable source, TPredicate predicate)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TPredicate : struct, IFunction<TSource, int, bool>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            for (var index = 0; enumerator.MoveNext(); index++)
            {
                var item = enumerator.Current;
                if (predicate.Invoke(item, index))
                    sum = GenericsOperator.Sum(item, sum);
            }
            return sum;
        }

        internal static TSum SumAtRef<TEnumerable, TEnumerator, TSource, TSum, TPredicate>(this TEnumerable source, TPredicate predicate)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TPredicate : struct, IFunctionIn<TSource, int, bool>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            for (var index = 0; enumerator.MoveNext(); index++)
            {
                var item = enumerator.Current;
                if (predicate.Invoke(in item, index))
                    sum = GenericsOperator.Sum(item, sum);
            }
            return sum;
        }

        internal static TSum Sum<TEnumerable, TEnumerator, TSource, TResult, TSum, TSelector>(this TEnumerable source, TSelector selector)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TSelector : struct, IFunction<TSource, TResult>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                sum = GenericsOperator.Sum(selector.Invoke(item), sum);
            }
            return sum;
        }

        internal static TSum SumRef<TEnumerable, TEnumerator, TSource, TResult, TSum, TSelector>(this TEnumerable source, TSelector selector)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TSelector : struct, IFunctionIn<TSource, TResult>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                sum = GenericsOperator.Sum(selector.Invoke(in item), sum);
            }
            return sum;
        }

        static TSum SumAt<TEnumerable, TEnumerator, TSource, TResult, TSum, TSelector>(this TEnumerable source, TSelector selector)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TSelector : struct, IFunction<TSource, int, TResult>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            for (var index = 0; enumerator.MoveNext(); index++)
            {
                var item = enumerator.Current;
                sum = GenericsOperator.Sum(selector.Invoke(item, index), sum);
            }
            return sum;
        }

        internal static TSum SumAtRef<TEnumerable, TEnumerator, TSource, TResult, TSum, TSelector>(this TEnumerable source, TSelector selector)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TSelector : struct, IFunctionIn<TSource, int, TResult>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            for (var index = 0; enumerator.MoveNext(); index++)
            {
                var item = enumerator.Current;
                sum = GenericsOperator.Sum(selector.Invoke(in item, index), sum);
            }
            return sum;
        }


        internal static TSum Sum<TEnumerable, TEnumerator, TSource, TResult, TSum, TPredicate, TSelector>(this TEnumerable source, TPredicate predicate, TSelector selector)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TPredicate : struct, IFunction<TSource, bool>
            where TSelector : struct, IFunction<TSource, TResult>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (predicate.Invoke(item))
                    sum = GenericsOperator.Sum(selector.Invoke(item), sum);
            }
            return sum;
        }

        internal static TSum SumRef<TEnumerable, TEnumerator, TSource, TResult, TSum, TPredicate, TSelector>(this TEnumerable source, TPredicate predicate, TSelector selector)
            where TEnumerable : IValueEnumerable<TSource, TEnumerator>
            where TEnumerator : struct, IEnumerator<TSource>
            where TPredicate : struct, IFunctionIn<TSource, bool>
            where TSelector : struct, IFunctionIn<TSource, TResult>
            where TSum : struct
        {
            var sum = default(TSum);
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (predicate.Invoke(in item))
                    sum = GenericsOperator.Sum(selector.Invoke(in item), sum);
            }
            return sum;
        }
    }
}

