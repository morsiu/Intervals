using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Generation
{
    public sealed class PairsOfOpenIntervalsOfAllPossibleRelations<TInterval, TPairOfIntervals, TIntervals, TPairsOfIntervals> : IEnumerable<TPairOfIntervals>
        where TIntervals : struct, IOpenIntervals<int, TInterval>
        where TPairsOfIntervals : struct, IPairs<TInterval, TInterval, TPairOfIntervals>
    {
        public IEnumerator<TPairOfIntervals> GetEnumerator()
        {
            return new PairsOfClosedIntervalsOfAllPossibleRelations<int, IEnumerable<TInterval>, IEnumerable<TPairOfIntervals>, Intervals, IntervalPairs>(
                    new[] { 1, 3, 5, 7 })
                .SelectMany(x => x)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly struct Intervals : IClosedIntervals<int, IEnumerable<TInterval>>
        {
            public IEnumerable<TInterval> Empty()
            {
                yield return default(TIntervals).Empty();
            }

            public IEnumerable<TInterval> Interval(int start, int end)
            {
                var x = default(TIntervals);
                yield return x.Interval(start, end, isStartOpen: true, isEndOpen: true);
                yield return x.Interval(start, end, isStartOpen: true, isEndOpen: false);
                yield return x.Interval(start, end, isStartOpen: false, isEndOpen: true);
                yield return x.Interval(start, end, isStartOpen: false, isEndOpen: false);
            }
        }
        
        private readonly struct IntervalPairs : IPairs<IEnumerable<TInterval>, IEnumerable<TInterval>, IEnumerable<TPairOfIntervals>>
        {
            public IEnumerable<TPairOfIntervals> Pair(IEnumerable<TInterval> first, IEnumerable<TInterval> second)
            {
                return first.SelectMany(x => second, (x, y) => default(TPairsOfIntervals).Pair(x, y));
            }
        }
    }
}
