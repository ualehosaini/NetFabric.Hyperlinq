﻿## ContainsBenchmarks

### Source
[ContainsBenchmarks.cs](../NetFabric.Hyperlinq.Benchmarks/Benchmarks/ContainsBenchmarks.cs)

### References:
- Linq: 5.0.2
- System.Linq.Async: [5.0.0](https://www.nuget.org/packages/System.Linq.Async/5.0.0)
- System.Interactive: [5.0.0](https://www.nuget.org/packages/System.Interactive/5.0.0)
- System.Interactive.Async: [5.0.0](https://www.nuget.org/packages/System.Interactive.Async/5.0.0)
- StructLinq: [0.25.3](https://www.nuget.org/packages/StructLinq/0.25.3)
- NetFabric.Hyperlinq: [3.0.0-beta34](https://www.nuget.org/packages/NetFabric.Hyperlinq/3.0.0-beta34)

### Results:
``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-7567U CPU 3.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.200-preview.20614.14
  [Host]        : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

Job=.NET Core 5.0  Runtime=.NET Core 5.0  Categories=Array  

```
|              Method | Count |     Mean |    Error |   StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |------ |---------:|---------:|---------:|------:|--------:|------:|------:|------:|----------:|
|          Linq_Array |   100 | 55.27 ns | 1.138 ns | 1.479 ns |  1.00 |    0.00 |     - |     - |     - |         - |
|     Hyperlinq_Array |   100 | 49.53 ns | 1.536 ns | 4.528 ns |  0.89 |    0.04 |     - |     - |     - |         - |
|      Hyperlinq_Span |   100 | 96.52 ns | 1.965 ns | 3.442 ns |  1.76 |    0.09 |     - |     - |     - |         - |
| Hyperlinq_Span_SIMD |   100 | 19.46 ns | 0.264 ns | 0.449 ns |  0.35 |    0.01 |     - |     - |     - |         - |
