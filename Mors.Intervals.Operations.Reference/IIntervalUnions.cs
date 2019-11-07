namespace Mors.Intervals.Operations.Reference
{
    public interface IIntervalUnions<TInterval, out TIntervalUnion> : IEmptyIntervals<TIntervalUnion>
    {
        TIntervalUnion NonEmpty(in TInterval interval);

        TIntervalUnion NonEmpty(in TInterval first, in TInterval second);
    }
}