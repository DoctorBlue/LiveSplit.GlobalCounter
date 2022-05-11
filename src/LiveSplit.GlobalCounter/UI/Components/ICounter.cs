namespace LiveSplit.UI.Components
{
    public interface ICounter
    {
        int Count { get; }

        bool Increment(int amount);
        bool Decrement();
        void Reset();
    }
}
