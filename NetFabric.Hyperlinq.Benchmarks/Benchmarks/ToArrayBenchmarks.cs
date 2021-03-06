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
    public class ToArrayBenchmarks: RandomBenchmarksBase
    {
        [BenchmarkCategory("Array")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Array()
            => Enumerable.ToArray(array);

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Enumerable_Value()
            => Enumerable.ToArray(enumerableValue);

        [BenchmarkCategory("Collection_Value")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Collection_Value()
            => Enumerable.ToArray(collectionValue);

        [BenchmarkCategory("List_Value")]
        [Benchmark(Baseline = true)]
        public int[] Linq_List_Value()
            => Enumerable.ToArray(listValue);

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Enumerable_Reference()
            => Enumerable.ToArray(enumerableReference);

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark(Baseline = true)]
        public int[] Linq_Collection_Reference()
            => Enumerable.ToArray(collectionReference);

        [BenchmarkCategory("List_Reference")]
        [Benchmark(Baseline = true)]
        public int[] Linq_List_Reference()
            => Enumerable.ToArray(listReference);

        // ---------------------------------------------------------------------

        [BenchmarkCategory("Array")]
        [Benchmark]
        public int[] StructLinq_Array()
            => array
                .ToStructEnumerable()
                .ToArray(x => x);

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark]
        public int[] StructLinq_Enumerable_Value()
            => enumerableValue
                .ToStructEnumerable()
                .ToArray(x => x);

        [BenchmarkCategory("Collection_Value")]
        [Benchmark]
        public int[] StructLinq_Collection_Value()
            => collectionValue
                .ToStructEnumerable()
                .ToArray(x => x);

        [BenchmarkCategory("List_Value")]
        [Benchmark]
        public int[] StructLinq_List_Value()
            => listValue
                .ToStructEnumerable()
                .ToArray(x => x);

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark]
        public int[] StructLinq_Enumerable_Reference()
            => enumerableReference
                .ToStructEnumerable()
                .ToArray(x => x);

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark]
        public int[] StructLinq_Collection_Reference()
            => collectionReference
                .ToStructEnumerable()
                .ToArray(x => x);

        [BenchmarkCategory("List_Reference")]
        [Benchmark]
        public int[] StructLinq_List_Reference()
            => listReference
                .ToStructEnumerable()
                .ToArray(x => x);

        // ---------------------------------------------------------------------

        [BenchmarkCategory("Array")]
        [Benchmark]
        public int[] Hyperlinq_Array()
            => array
                .ToArray();

        [BenchmarkCategory("Array")]
        [Benchmark]
        public int[] Hyperlinq_Span()
            => array.AsSpan()
                .ToArray();

        [BenchmarkCategory("Array")]
        [Benchmark]
        public int[] Hyperlinq_Memory()
            => memory
                .ToArray();

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark]
        public int[] Hyperlinq_Enumerable_Value()
            => EnumerableExtensions.AsValueEnumerable<TestEnumerable.Enumerable, TestEnumerable.Enumerable.Enumerator, int>(enumerableValue, enumerable => enumerable.GetEnumerator())
                .ToArray();

        [BenchmarkCategory("Collection_Value")]
        [Benchmark]
        public int[] Hyperlinq_Collection_Value()
            => ReadOnlyCollectionExtensions.AsValueEnumerable<TestCollection.Enumerable, TestCollection.Enumerable.Enumerator, int>(collectionValue, enumerable => enumerable.GetEnumerator())
                .ToArray();

        [BenchmarkCategory("List_Value")]
        [Benchmark]
        public int[] Hyperlinq_List_Value()
            => listValue
                .AsValueEnumerable()
                .ToArray();

        [BenchmarkCategory("AsyncEnumerable_Value")]
        [Benchmark]
        public ValueTask<int[]> Hyperlinq_AsyncEnumerable_Value()
            => asyncEnumerableValue
                .AsAsyncValueEnumerable<TestAsyncEnumerable.Enumerable, TestAsyncEnumerable.Enumerable.Enumerator, int>((enumerable, cancellationToke) => enumerable.GetAsyncEnumerator(cancellationToke))
                .ToArrayAsync();

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark]
        public int[] Hyperlinq_Enumerable_Reference()
            => enumerableReference
                .AsValueEnumerable()
                .ToArray();

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark]
        public int[] Hyperlinq_Collection_Reference()
            => collectionReference
                .AsValueEnumerable()
                .ToArray();

        [BenchmarkCategory("List_Reference")]
        [Benchmark]
        public int[] Hyperlinq_List_Reference()
            => listReference
                .AsValueEnumerable()
                .ToArray();

        [BenchmarkCategory("AsyncEnumerable_Reference")]
        [Benchmark]
        public ValueTask<int[]> Hyperlinq_AsyncEnumerable_Reference()
            => asyncEnumerableReference
                .AsAsyncValueEnumerable()
                .ToArrayAsync();
    }
}
