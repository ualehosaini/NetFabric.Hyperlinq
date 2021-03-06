using NetFabric.Assertive;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NetFabric.Hyperlinq.UnitTests.Projection.Select
{
    public class AsyncValueEnumerableTests
    {
        [Theory]
        [MemberData(nameof(TestData.SelectorEmpty), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.SelectorSingle), MemberType = typeof(TestData))]
        [MemberData(nameof(TestData.SelectorMultiple), MemberType = typeof(TestData))]
        public void Select_Selector_With_ValidData_Must_Succeed(int[] source, Func<int, string> selector)
        {
            // Arrange
            var wrapped = Wrap.AsAsyncValueEnumerable(source);
            var expected = System.Linq.Enumerable
                .Select(source, selector);

            // Act
            var result = AsyncValueEnumerableExtensions
                .Select<Wrap.AsyncValueEnumerableWrapper<int>, Wrap.AsyncEnumerator<int>, int, string>(wrapped, selector.AsAsync());

            // Assert
            _ = result.Must()
                .BeAsyncEnumerableOf<string>()
                .BeEqualTo(expected);
        }
    }
}