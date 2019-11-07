using Mors.Intervals.Inequations;

namespace Mors.Intervals.Operations.Reference
{
    internal static class OpenIntervalExtensions
    {
        public static TOpenInterval ToReferenceInterval<TOpenInterval, TOpenIntervals>(this OpenInterval<int> interval)
            where TOpenIntervals : struct, IOpenIntervals<int, TOpenInterval>, IEmptyIntervals<TOpenInterval>
            where TOpenInterval : IOpenInterval<int>, IEmptyInterval
        {
            var intervals = default(TOpenIntervals);
            return !interval.Empty()
                ? intervals.Interval(interval.Start(), interval.End(), !interval.ClosedStart(), !interval.ClosedEnd())
                : intervals.Empty();
        }

        public static Inequation<int> ToInequation<TOpenInterval>(this TOpenInterval interval)
            where TOpenInterval : IOpenInterval<int>, IEmptyInterval =>
            Inequation.FromOpenInterval<int, OpenInterval<int>>(
                interval.Empty
                    ? new OpenInterval<int>()
                    : new OpenInterval<int>(interval.Start, interval.End, interval.OpenStart, interval.OpenEnd));
    }
}
