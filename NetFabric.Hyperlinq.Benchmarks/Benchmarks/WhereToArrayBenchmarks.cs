using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using StructLinq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetFabric.Hyperlinq.Benchmarks
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class WhereToArrayBenchmarks: RandomBenchmarksBase
    {
        [BenchmarkCategory("Array")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Array()
            => Enumerable.Where(array, item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Enumerable_Value()
            => Enumerable.Where(enumerableValue, item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("Collection_Value")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Collection_Value()
            => Enumerable.Where(collectionValue, item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("List_Value")]
        [Benchmark(Baseline = true)]
        public int[] Linq_List_Value()
            => Enumerable.Where(listValue, item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("AsyncEnumerable_Value")]
        [Benchmark(Baseline = true)]
        public ValueTask<int[]> Linq_AsyncEnumerable_Value()
            => AsyncEnumerable.Where(asyncEnumerableValue, item => (item & 0x01) == 0)
                .ToArrayAsync();

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Enumerable_Reference()
            => Enumerable.Where(enumerableReference, item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Collection_Reference()
            => Enumerable.Where(collectionReference, item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("List_Reference")]
        [Benchmark(Baseline = true)]
        public int[] Linq_List_Reference()
            => Enumerable.Where(listReference, item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("AsyncEnumerable_Reference")]
        [Benchmark(Baseline = true)]
        public ValueTask<int[]> Linq_AsyncEnumerable_Reference()
            => AsyncEnumerable.Where(asyncEnumerableReference, item => (item & 0x01) == 0)
                .ToArrayAsync();

        // ---------------------------------------------------------------------

        [BenchmarkCategory("Array")]
        [Benchmark]
        public int[] StructLinq_Array()
            => array
                .ToStructEnumerable()
                .Where(item => (item & 0x01) == 0, x => x)
                .ToArray(x => x);

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark]
        public int[] StructLinq_Enumerable_Value()
            => enumerableValue
                .ToStructEnumerable()
                .Where(item => (item & 0x01) == 0, x => x)
                .ToArray(x => x);

        [BenchmarkCategory("Collection_Value")]
        [Benchmark]
        public int[] StructLinq_Collection_Value()
            => collectionValue
                .ToStructEnumerable()
                .Where(item => (item & 0x01) == 0, x => x)
                .ToArray(x => x);

        [BenchmarkCategory("List_Value")]
        [Benchmark]
        public int[] StructLinq_List_Value()
            => listValue
                .ToStructEnumerable()
                .Where(item => (item & 0x01) == 0, x => x)
                .ToArray(x => x);

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark]
        public int[] StructLinq_Enumerable_Reference()
            => enumerableReference
                .ToStructEnumerable()
                .Where(item => (item & 0x01) == 0, x => x)
                .ToArray(x => x);

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark]
        public int[] StructLinq_Collection_Reference()
            => collectionReference
                .ToStructEnumerable()
                .Where(item => (item & 0x01) == 0, x => x)
                .ToArray(x => x);

        [BenchmarkCategory("List_Reference")]
        [Benchmark]
        public int[] StructLinq_List_Reference()
            => listReference
                .ToStructEnumerable()
                .Where(item => (item & 0x01) == 0, x => x)
                .ToArray(x => x);

        // ---------------------------------------------------------------------

        [BenchmarkCategory("Array")]
        [Benchmark]
        public int[] Hyperlinq_Array()
            => array
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("Array")]
        [Benchmark]
        public int[] Hyperlinq_Span()
            => array.AsSpan()
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("Array")]
        [Benchmark]
        public int[] Hyperlinq_Memory()
            => memory.AsValueEnumerable()
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark]
        public int[] Hyperlinq_Enumerable_Value()
            => EnumerableExtensions.AsValueEnumerable<TestEnumerable.Enumerable, TestEnumerable.Enumerable.Enumerator, int>(enumerableValue, enumerable => enumerable.GetEnumerator())
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("Collection_Value")]
        [Benchmark]
        public int[] Hyperlinq_Collection_Value()
            => ReadOnlyCollectionExtensions.AsValueEnumerable<TestCollection.Enumerable, TestCollection.Enumerable.Enumerator, int>(collectionValue, enumerable => enumerable.GetEnumerator())
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("List_Value")]
        [Benchmark]
        public int[] Hyperlinq_List_Value()
            => listValue
                .AsValueEnumerable()
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("AsyncEnumerable_Value")]
        [Benchmark]
        public ValueTask<int[]> Hyperlinq_AsyncEnumerable_Value()
            => asyncEnumerableValue
                .AsAsyncValueEnumerable<TestAsyncEnumerable.Enumerable, TestAsyncEnumerable.Enumerable.Enumerator, int>((enumerable, cancellationToke) => enumerable.GetAsyncEnumerator(cancellationToke))
                .Where(item => (item & 0x01) == 0)
                .ToArrayAsync();

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark]
        public int[] Hyperlinq_Enumerable_Reference()
            => enumerableReference
                .AsValueEnumerable()
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark]
        public int[] Hyperlinq_Collection_Reference()
            => collectionReference
                .AsValueEnumerable()
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("List_Reference")]
        [Benchmark]
        public int[] Hyperlinq_List_Reference()
            => listReference
                .AsValueEnumerable()
                .Where(item => (item & 0x01) == 0)
                .ToArray();

        [BenchmarkCategory("AsyncEnumerable_Reference")]
        [Benchmark]
        public ValueTask<int[]> Hyperlinq_AsyncEnumerable_Reference()
            => asyncEnumerableReference
                .AsAsyncValueEnumerable()
                .Where(item => (item & 0x01) == 0)
                .ToArrayAsync();
    }
}
