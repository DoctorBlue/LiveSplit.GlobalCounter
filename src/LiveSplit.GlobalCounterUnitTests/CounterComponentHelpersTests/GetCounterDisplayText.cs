using System;
using Xunit;

namespace LiveSplit.GlobalCounterUnitTests.CounterComponentHelpersTests
{
    public class GetCounterDisplayText
    {
        [Theory]
        [InlineData(-15)]
        [InlineData(-1)]
        [InlineData(10)]
        [InlineData(21)]
        public void ThrowsGivenCountOutOfGlobalRange(int count)
        {
            void GetCall() => CounterComponentHelpers.GetCounterDisplayText(count, false);

            Assert.Throws<ArgumentOutOfRangeException>(GetCall);
        }

        [Fact]
        public void OnlyReturnsCountWhenNotShowingDropGroup()
        {
            const int count = 5;

            string countDisplayText = CounterComponentHelpers.GetCounterDisplayText(count, false);

            Assert.Equal(count.ToString(), countDisplayText);
        }

        [Fact]
        public void IncludesDropsWhenShowingDropGroup()
        {
            const int count = 5;
            string expected = $"(♥ B C ♥) {count}";

            string countDisplayText = CounterComponentHelpers.GetCounterDisplayText(count, true);

            Assert.Equal(expected, countDisplayText);
        }
    }
}
