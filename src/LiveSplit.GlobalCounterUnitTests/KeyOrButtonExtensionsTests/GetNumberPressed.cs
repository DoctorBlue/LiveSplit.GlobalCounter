using LiveSplit.Model.Input;
using System;
using System.Windows.Forms;
using Xunit;

namespace LiveSplit.GlobalCounterUnitTests.KeyOrButtonExtensionsTests
{
    public class GetNumberPressed
    {
        [Theory]
        [InlineData(Keys.A)]
        [InlineData(Keys.Oem5)]
        public void ThrowsGivenNonNumPadKey(Keys key)
        {
            var keyPressed = new KeyOrButton(key);

            void GetNumberPressedCall() => keyPressed.GetNumberPressed();

            Assert.Throws<InvalidOperationException>(GetNumberPressedCall);
        }

        [Theory]
        [InlineData(1, Keys.NumPad1)]
        [InlineData(2, Keys.NumPad2)]
        [InlineData(3, Keys.NumPad3)]
        [InlineData(4, Keys.NumPad4)]
        [InlineData(5, Keys.NumPad5)]
        [InlineData(6, Keys.NumPad6)]
        [InlineData(7, Keys.NumPad7)]
        [InlineData(8, Keys.NumPad8)]
        [InlineData(9, Keys.NumPad9)]
        public void ReturnsNumPadNumber(int expectedNumber, Keys key)
        {
            var keyPressed = new KeyOrButton(key);

            int numberPressed = keyPressed.GetNumberPressed();

            Assert.Equal(expectedNumber, numberPressed);
        }
    }
}
