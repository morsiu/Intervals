using System.Linq;
using Mors.Intervals.Inequations;

namespace Mors.Intervals.Operations.Reference
{
    internal static class ClosedIntervalUnionExtensions
    {
        public static Inequation<int> ToInequation<TClosedIntervalUnion, TClosedInterval>(
            this TClosedIntervalUnion intervalUnion)
            where TClosedInterval : IClosedInterval<int>
            where TClosedIntervalUnion : IClosedIntervalUnion<int, TClosedInterval> =>
            Inequation.FromClosedIntervalUnion<int, ClosedInterval<int>>(
                intervalUnion.Select(x => new ClosedInterval<int>(x.Start, x.End)));
    }
}