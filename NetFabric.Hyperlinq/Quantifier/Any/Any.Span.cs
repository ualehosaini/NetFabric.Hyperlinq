using System;
using System.Runtime.CompilerServices;

namespace NetFabric.Hyperlinq
{
    public static partial class ArrayExtensions
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<TSource>(this Span<TSource> source)
            => source.Length is not 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<TSource>(this Span<TSource> source, Func<TSource, bool> predicate)
            => source.Any(new FunctionWrapper<TSource, bool>(predicate));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<TSource, TPredicate>(this Span<TSource> source, TPredicate predicate = default)
            where TPredicate : struct, IFunction<TSource, bool>
            => ((ReadOnlySpan<TSource>)source).Any(predicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<TSource>(this Span<TSource> source, Func<TSource, int, bool> predicate)
            => source.AnyAt(new FunctionWrapper<TSource, int, bool>(predicate));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyAt<TSource, TPredicate>(this Span<TSource> source, TPredicate predicate = default)
            where TPredicate : struct, IFunction<TSource, int, bool>
            => ((ReadOnlySpan<TSource>)source).AnyAt(predicate);
    }
}

