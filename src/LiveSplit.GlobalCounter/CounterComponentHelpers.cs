using System;

namespace LiveSplit
{
    public static class CounterComponentHelpers
    {
        public static string GetCounterDisplayText(int count, bool showDropGroupsEnabled)
        {
            if (count < 0 || count > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Count must be within global range of 0-9!");
            }

            string dropGroupText = string.Empty;
            if (showDropGroupsEnabled)
            {
                string[] dropGroups =
                {
                    "1 B 1 ♥",
                    "♥ 1 ♥ F",
                    "1 C 1 1",
                    "F 1 5 ♥",
                    "1 ♥ ♥ F",
                    "♥ B C ♥",
                    "♥ 1 1 ♥",
                    "1 B 1 ♥",
                    "1 ♥ 1 1",
                    "♥ ♥ 5 ♥"
                };
                dropGroupText = $"({dropGroups[count]}) ";
            }

            return $"{dropGroupText}{count}";
        }
    }
}
