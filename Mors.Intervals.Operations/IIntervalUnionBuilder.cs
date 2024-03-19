namespace Mors.Intervals.Operations
{
    public interface IIntervalUnionBuilder<TIntervalUnion, TInterval>
    {
        void Append(in TInterval interval);

        TIntervalUnion Build();
    }
}