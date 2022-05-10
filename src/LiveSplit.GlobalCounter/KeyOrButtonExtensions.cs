using LiveSplit.Model.Input;
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
    }
}
