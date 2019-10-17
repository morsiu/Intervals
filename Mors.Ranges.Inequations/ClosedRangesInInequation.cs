using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Inequations
{
    internal sealed class ClosedRangesInInequation<T, TRange, TPoints, TRanges> : IEnumerable<TRange>
        where TRanges : struct, IClosedRanges<T, TRange>
        where TPoints : struct, IPoints<T>
        where T : IComparable<T>
    {
        private readonly IInequation<T> _inequation;

        public ClosedRangesInInequation(IInequation<T> inequation) => _inequation = inequation;

        public IEnumerator<TRange> GetEnumerator()
        {
            return new OpenRangesInInequation<T, OpenRange, TPoints, OpenRanges>(_inequation)
                .SelectMany(x => x.ToClosedRange())
                .GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private readonly struct OpenRange : IOpenRange<T>
        {
            private readonly T _start;
            private readonly T _end;
            private readonly bool _isStartClosed;
            private readonly bool _isEndClosed;
            private readonly bool _nonEmpty;

            public OpenRange(T start, T end, bool isStartClosed, bool isEndClosed)
            {
                _start = start;
                _end = end;
                _isStartClosed = isStartClosed;
                _isEndClosed = isEndClosed;
                _nonEmpty = true;
            }

            public IEnumerable<TRange> ToClosedRange()
            {
                var ranges = default(TRanges);
                if (!_nonEmpty)
                {
                    return One(ranges.Empty());
                }
                return _start.CompareTo(_end) switch
                {
                    0 when _isStartClosed && _isEndClosed =>
                        One(ranges.NonEmpty(_start, _end)),
                    var x when x < 0 =>
                        One(ranges.NonEmpty(
                            NewStart(_start, _isStartClosed),
                            NewEnd(_end, _isEndClosed))),
                    _ => throw new Exception("Start is greater than the end.")
                };

                static IEnumerable<TRange> One(TRange x) => Enumerable.Repeat(x, 1);

                static T NewStart(T start, bool isStartClosed) =>
                    isStartClosed
                        ? start
                        : default(TPoints).Next(start) switch
                        {
                            (true, var afterStart) => afterStart,
                            (false, _) => throw new Exception("There is no successor to start, even though end is greater than start.")
                        };

                static T NewEnd(T end, bool isEndClosed) =>
                    isEndClosed
                        ? end
                        : default(TPoints).Previous(end) switch
                        {
                            (true, var beforeEnd) => beforeEnd,
                            (false, _) => throw new Exception("There is no predecessor to end, even though start is smaller than end.")
                        };
            }

            bool IOpenRange<T>.Empty() => !_nonEmpty;

            bool IOpenRange<T>.ClosedStart() => _isStartClosed;

            bool IOpenRange<T>.ClosedEnd() => _isEndClosed;

            T IOpenRange<T>.Start() => _start;

            T IOpenRange<T>.End() => _end;
        }

        private readonly struct OpenRanges : IOpenRanges<T, OpenRange>
        {
            public OpenRange NonEmpty(T start, T end, bool isStartClosed, bool isEndClosed) =>
                new OpenRange(start, end, isStartClosed, isEndClosed);

            public OpenRange Empty() => new OpenRange();
        }
    }
}
