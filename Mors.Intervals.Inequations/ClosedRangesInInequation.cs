using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations
{
    internal sealed class ClosedIntervalsInInequation<T, TInterval, TPoints, TIntervals> : IEnumerable<TInterval>
        where TIntervals : struct, IClosedIntervals<T, TInterval>
        where TPoints : struct, IPoints<T>
        where T : IComparable<T>
    {
        private readonly IInequation<T> _inequation;

        public ClosedIntervalsInInequation(IInequation<T> inequation) => _inequation = inequation;

        public IEnumerator<TInterval> GetEnumerator()
        {
            return new OpenIntervalsInInequation<T, OpenInterval, TPoints, OpenIntervals>(_inequation)
                .SelectMany(x => x.ToClosedInterval())
                .GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private readonly struct OpenInterval : IOpenInterval<T>
        {
            private readonly T _start;
            private readonly T _end;
            private readonly bool _isStartClosed;
            private readonly bool _isEndClosed;
            private readonly bool _nonEmpty;

            public OpenInterval(T start, T end, bool isStartClosed, bool isEndClosed)
            {
                _start = start;
                _end = end;
                _isStartClosed = isStartClosed;
                _isEndClosed = isEndClosed;
                _nonEmpty = true;
            }

            public IEnumerable<TInterval> ToClosedInterval()
            {
                var intervals = default(TIntervals);
                if (!_nonEmpty)
                {
                    return Enumerable.Empty<TInterval>();
                }
                return _start.CompareTo(_end) switch
                {
                    0 when _isStartClosed && _isEndClosed =>
                        One(intervals.NonEmpty(_start, _end)),
                    var x when x < 0 =>
                        One(intervals.NonEmpty(
                            NewStart(_start, _isStartClosed),
                            NewEnd(_end, _isEndClosed))),
                    _ => throw new Exception("Start is greater than the end.")
                };

                static IEnumerable<TInterval> One(TInterval x) => Enumerable.Repeat(x, 1);

                static T NewStart(T start, bool isStartClosed) =>
                    isStartClosed
                        ? start
                        : default(TPoints).Next(start) switch
                        {
                            (true, var afterStart) => afterStart,
                            _ => throw new Exception("There is no successor to start, even though end is greater than start.")
                        };

                static T NewEnd(T end, bool isEndClosed) =>
                    isEndClosed
                        ? end
                        : default(TPoints).Previous(end) switch
                        {
                            (true, var beforeEnd) => beforeEnd,
                            _ => throw new Exception("There is no predecessor to end, even though start is smaller than end.")
                        };
            }

            bool IOpenInterval<T>.Empty() => !_nonEmpty;

            bool IOpenInterval<T>.ClosedStart() => _isStartClosed;

            bool IOpenInterval<T>.ClosedEnd() => _isEndClosed;

            T IOpenInterval<T>.Start() => _start;

            T IOpenInterval<T>.End() => _end;
        }

        private readonly struct OpenIntervals : IOpenIntervals<T, OpenInterval>
        {
            public OpenInterval NonEmpty(T start, T end, bool isStartClosed, bool isEndClosed) =>
                new OpenInterval(start, end, isStartClosed, isEndClosed);

            public OpenInterval Empty() => new OpenInterval();
        }
    }
}
