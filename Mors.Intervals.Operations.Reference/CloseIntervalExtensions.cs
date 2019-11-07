using Mors.Intervals.Inequations;

namespace Mors.Intervals.Operations.Reference
{
    internal static class CloseIntervalExtensions
    {
        public static TClosedInterval ToReferenceInterval<TClosedInterval, TClosedIntervals>(
            this ClosedInterval<int> interval)
            where TClosedIntervals : struct, IClosedIntervals<int, TClosedInterval>, IEmptyIntervals<TClosedInterval>
            where TClosedInterval : IClosedInterval<int>, IEmptyInterval
        {
            var intervals = default(TClosedIntervals);
            return !interval.Empty()
                ? intervals.Interval(interval.Start(), interval.End())
                : intervals.Empty();
        }

        public static Inequation<int> ToInequation<TClosedInterval>(this TClosedInterval interval)
            where TClosedInterval : IClosedInterval<int>, IEmptyInterval =>
            Inequation.FromClosedInterval<int, ClosedInterval<int>>(
                interval.Empty
                    ? new ClosedInterval<int>()
                    : new ClosedInterval<int>(interval.Start, interval.End));
    }
}