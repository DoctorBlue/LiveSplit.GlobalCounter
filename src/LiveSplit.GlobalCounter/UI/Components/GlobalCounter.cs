namespace LiveSplit.UI.Components
{
    public class GlobalCounter : ICounter
    {
        private readonly int _initialValue;

        public GlobalCounter(int initialValue = 0)
        {
            _initialValue = initialValue;
            Reset();
        }

        public int Count { get; private set; }

        public bool Increment(int amount)
        {
            Count = (Count + amount) % 10;
            return true;
        }

        public bool Decrement()
        {
            Count -= 1;
            while (Count < 0)
            {
                Count += 10;
            }
            return true;
        }

        public void Reset()
        {
            if (_initialValue < 0)
            {
                Count = 0;
            }
            else if (_initialValue > 9)
            {
                Count = 9;
            }
            else
            {
                Count = _initialValue;
            }
        }
    }
}
