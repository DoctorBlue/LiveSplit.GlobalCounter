using LiveSplit.UI.Components;
using Xunit;

namespace LiveSplit.GlobalCounterUnitTests.GlobalCounterTests
{
    public class Constructor
    {
        [Fact]
        public void DefaultsInitialCountTo0()
        {
            const int expectedCount = 0;

            var counter = new GlobalCounter();
            
            Assert.Equal(expectedCount, counter.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(9)]
        public void SetsCountToInitialValue(int initialValue)
        {
            int expectedCount = initialValue;

            var counter = new GlobalCounter(initialValue);
            
            Assert.Equal(expectedCount, counter.Count);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-2)]
        [InlineData(-1)]
        public void SetsCountTo0GivenNegativeInitialValue(int initialValue)
        {
            const int expectedCount = 0;

            var counter = new GlobalCounter(initialValue);

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

            Assert.Equal(expectedCount, counter.Count);
        }
    }
}
