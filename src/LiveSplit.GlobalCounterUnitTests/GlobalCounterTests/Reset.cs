using LiveSplit.UI.Components;
using Xunit;

namespace LiveSplit.GlobalCounterUnitTests.GlobalCounterTests
{
    public class Reset
    {
        [Fact]
        public void SetsCountToInitialValue()
        {
            const int initialValue = 7;

            var counter = new GlobalCounter(initialValue);

            counter.Increment(77);
            counter.Decrement();
            counter.Decrement();
            counter.Reset();

            Assert.Equal(initialValue, counter.Count);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-2)]
        [InlineData(-1)]
        public void SetsCountTo0GivenNegativeInitialValue(int initialValue)
        {
            const int expectedCount = 0;

            var counter = new GlobalCounter(initialValue);
            counter.Reset();

            Assert.Equal(expectedCount, counter.Count);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(100)]
        public void SetsCountTo9GivenInitialValueGreaterThan9(int initialValue)
        {
            const int expectedCount = 9;

            var counter = new GlobalCounter(initialValue);
            counter.Reset();

            Assert.Equal(expectedCount, counter.Count);
        }
    }
}
