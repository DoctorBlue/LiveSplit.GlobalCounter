using LiveSplit.Model.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit
{
    public static class KeyOrButtonExtensions
    {
        public static bool NumberPadKeyPressed(this KeyOrButton keyPressed)
        {
            if (keyPressed == null)
            {
                return false;
            }
            var numberPadKeys = new List<KeyOrButton>
            {
                new KeyOrButton(Keys.NumPad1),
                new KeyOrButton(Keys.NumPad2),
                new KeyOrButton(Keys.NumPad3),
                new KeyOrButton(Keys.NumPad4),
                new KeyOrButton(Keys.NumPad5),
                new KeyOrButton(Keys.NumPad6),
                new KeyOrButton(Keys.NumPad7),
                new KeyOrButton(Keys.NumPad8),
                new KeyOrButton(Keys.NumPad9),
            };

            return numberPadKeys.Any(numberPadKey => numberPadKey == keyPressed);
        }

        public static int GetNumberPressed(this KeyOrButton keyPressed)
        {
            if (!keyPressed.NumberPadKeyPressed())
            {
                throw new InvalidOperationException("Can only get number pressed from number pad keys!");
            }

            switch (keyPressed.Key)
            {
                case Keys.NumPad1: return 1;
                case Keys.NumPad2: return 2;
                case Keys.NumPad3: return 3;
                case Keys.NumPad4: return 4;
                case Keys.NumPad5: return 5;
                case Keys.NumPad6: return 6;
                case Keys.NumPad7: return 7;
                case Keys.NumPad8: return 8;
                case Keys.NumPad9: return 9;
                default: throw new NotSupportedException("Key pressed could not be converted to a number.");
            }
        }
    }
}
