namespace LiveSplit.UI.Components
{
    public class GlobalCounter : ICounter
    {
        private readonly decimal _initialValue;

        public GlobalCounter(decimal initialValue = 0)
        {
            _initialValue = initialValue;
            Reset();
        }

        public decimal Count { get; private set; }

        public bool Increment(decimal amount)
        {
            Count += amount;
            return true;
        }

        public bool Decrement()
        {
            Count -= .5m;
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
