using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using StructLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetFabric.Hyperlinq.Benchmarks
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class ToListBenchmarks: RandomBenchmarksBase
    {
        [BenchmarkCategory("Array")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Array()
            => Enumerable.ToList(array);

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Enumerable_Value()
            => Enumerable.ToList(enumerableValue);

        [BenchmarkCategory("Collection_Value")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Collection_Value()
            => Enumerable.ToList(collectionValue);

        [BenchmarkCategory("List_Value")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_List_Value()
            => Enumerable.ToList(listValue);

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Enumerable_Reference()
            => Enumerable.ToList(enumerableReference);

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_Collection_Reference()
            => Enumerable.ToList(collectionReference);

        [BenchmarkCategory("List_Reference")]
        [Benchmark(Baseline = true)]
        public List<int> Linq_List_Reference()
            => Enumerable.ToList(listReference);

        // ---------------------------------------------------------------------

        [BenchmarkCategory("Array")]
        [Benchmark]
        public List<int> StructLinq_Array()
            => array
                .ToStructEnumerable()
                .ToList(x => x);

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark]
        public List<int> StructLinq_Enumerable_Value()
            => enumerableValue
                .ToStructEnumerable()
                .ToList(x => x);

        [BenchmarkCategory("Collection_Value")]
        [Benchmark]
        public List<int> StructLinq_Collection_Value()
            => collectionValue
                .ToStructEnumerable()
                .ToList(x => x);

        [BenchmarkCategory("List_Value")]
        [Benchmark]
        public List<int> StructLinq_List_Value()
            => listValue
                .ToStructEnumerable()
                .ToList(x => x);

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark]
        public List<int> StructLinq_Enumerable_Reference()
            => enumerableReference
                .ToStructEnumerable()
                .ToList(x => x);

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark]
        public List<int> StructLinq_Collection_Reference()
            => collectionReference
                .ToStructEnumerable()
                .ToList(x => x);

        [BenchmarkCategory("List_Reference")]
        [Benchmark]
        public List<int> StructLinq_List_Reference()
            => listReference
                .ToStructEnumerable()
                .ToList(x => x);

        // ---------------------------------------------------------------------

        [BenchmarkCategory("Array")]
        [Benchmark]
        public List<int> Hyperlinq_Array()
            => array
                .ToList();

        [BenchmarkCategory("Array")]
        [Benchmark]
        public List<int> Hyperlinq_Span()
            => array.AsSpan()
                .ToList();

        [BenchmarkCategory("Array")]
        [Benchmark]
        public List<int> Hyperlinq_Memory()
            => memory.AsValueEnumerable()
                .ToList();

        [BenchmarkCategory("Enumerable_Value")]
        [Benchmark]
        public List<int> Hyperlinq_Enumerable_Value()
            => EnumerableExtensions.AsValueEnumerable<TestEnumerable.Enumerable, TestEnumerable.Enumerable.Enumerator, int>(enumerableValue, enumerable => enumerable.GetEnumerator())
                .ToList();

        [BenchmarkCategory("Collection_Value")]
        [Benchmark]
        public List<int> Hyperlinq_Collection_Value()
            => ReadOnlyCollectionExtensions.AsValueEnumerable<TestCollection.Enumerable, TestCollection.Enumerable.Enumerator, int>(collectionValue, enumerable => enumerable.GetEnumerator())
                .ToList();

        [BenchmarkCategory("List_Value")]
        [Benchmark]
        public List<int> Hyperlinq_List_Value()
            => listValue
                .AsValueEnumerable()
                .ToList();

        [BenchmarkCategory("AsyncEnumerable_Value")]
        [Benchmark]
        public ValueTask<List<int>> Hyperlinq_AsyncEnumerable_Value()
            => asyncEnumerableValue
                .AsAsyncValueEnumerable<TestAsyncEnumerable.Enumerable, TestAsyncEnumerable.Enumerable.Enumerator, int>((enumerable, cancellationToke) => enumerable.GetAsyncEnumerator(cancellationToke))
                .ToListAsync();

        [BenchmarkCategory("Enumerable_Reference")]
        [Benchmark]
        public List<int> Hyperlinq_Enumerable_Reference()
            => enumerableReference
                .AsValueEnumerable()
                .ToList();

        [BenchmarkCategory("Collection_Reference")]
        [Benchmark]
        public List<int> Hyperlinq_Collection_Reference()
            => collectionReference
                .AsValueEnumerable()
                .ToList();

        [BenchmarkCategory("List_Reference")]
        [Benchmark]
        public List<int> Hyperlinq_List_Reference()
            => listReference
                .AsValueEnumerable()
                .ToList();

        [BenchmarkCategory("AsyncEnumerable_Reference")]
        [Benchmark]
        public ValueTask<List<int>> Hyperlinq_AsyncEnumerable_Reference()
            => asyncEnumerableReference
                .AsAsyncValueEnumerable()
                .ToListAsync();
    }
}
