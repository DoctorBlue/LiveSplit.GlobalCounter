using LiveSplit.Model.Input;
using System.Windows.Forms;
using Xunit;

namespace LiveSplit.GlobalCounterUnitTests.KeyOrButtonExtensionsTests
{
    public class NumberPadKeyPressed
    {
        [Fact]
        public void ReturnsFalseGivenNullKey()
        {
            bool result = KeyOrButtonExtensions.NumberPadKeyPressed(null);

            Assert.False(result);
        }

        [Theory]
        [InlineData(Keys.NumPad1)]
        [InlineData(Keys.NumPad2)]
        [InlineData(Keys.NumPad3)]
        [InlineData(Keys.NumPad4)]
        [InlineData(Keys.NumPad5)]
        [InlineData(Keys.NumPad6)]
        [InlineData(Keys.NumPad7)]
        [InlineData(Keys.NumPad8)]
        [InlineData(Keys.NumPad9)]
        public void ReturnsTrueWhenNonZeroNumPadNumberPressed(Keys key)
        {
            KeyOrButton keyPressed = new KeyOrButton(key);

            bool result = keyPressed.NumberPadKeyPressed();

            Assert.True(result);
        }

        [Fact]
        public void ReturnsFalseWhenZeroPressedOnNumberPad()
        {
            KeyOrButton zeroKey = new KeyOrButton(Keys.NumPad0);

            bool result = zeroKey.NumberPadKeyPressed();

            Assert.False(result);
        }

        [Theory]
        [InlineData(Keys.M)]
        [InlineData(Keys.O)]
        [InlineData(Keys.N)]
        [InlineData(Keys.K)]
        [InlineData(Keys.E)]
        [InlineData(Keys.Y)]
        public void ReturnsFalseWhenLetterPressed(Keys letterKey)
        {
            KeyOrButton keyPressed = new KeyOrButton(letterKey);

            bool result = keyPressed.NumberPadKeyPressed();

            Assert.False(result);
        }

    }
}
