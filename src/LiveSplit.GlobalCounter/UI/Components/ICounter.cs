namespace LiveSplit.UI.Components
{
    public interface ICounter
    {
        decimal Count { get; }

        bool Increment(decimal amount);
        bool Decrement();
        void Reset();
    }
}
