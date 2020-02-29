using NetFabric.Assertive;
using Xunit;

namespace NetFabric.Hyperlinq.UnitTests
{
    public partial class AsyncValueEnumerableTests
    {
        [Fact]
        public void AsAsyncValueEnumerable_With_ValueType_Should_ReturnCopy()
        {
            // Arrange
            var source = new int[0];
            var wrapped = Wrap.AsAsyncValueEnumerable(source);

            // Act
            var result = AsyncValueEnumerable
                .AsAsyncValueEnumerable<Wrap.AsyncValueEnumerable<int>, Wrap.AsyncEnumerator<int>, int>(wrapped);

            // Assert
            _ = result.Must()
                .BeEqualTo(wrapped);
        }
        
        [Fact]
        public void AsAsyncValueEnumerable_With_ReferenceType_Should_ReturnSame()
        {
            // Arrange
            var source = new int[0];
            var wrapped = Wrap
                .AsAsyncValueEnumerable(source) as IAsyncValueEnumerable<int, Wrap.AsyncEnumerator<int>>;

            // Act
            var result = AsyncValueEnumerable
                .AsAsyncValueEnumerable<IAsyncValueEnumerable<int, Wrap.AsyncEnumerator<int>>, Wrap.AsyncEnumerator<int>, int>(wrapped);

            // Assert
            _ = result.Must()
                .BeSameAs(wrapped);
        }
    }
}