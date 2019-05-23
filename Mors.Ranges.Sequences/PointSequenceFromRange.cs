using System;
using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Sequences
{
    public sealed class PointSequenceFromRange : IPointSequence
    {
        private readonly int _end;
        private readonly bool _hasOpenStart;
        private readonly bool _hasOpenEnd;

        public PointSequenceFromRange(int start, int end, bool hasOpenStart, bool hasOpenEnd)
        {
            if (start > end) throw new ArgumentException("Parameter end has to be greater than or equal to the start parameter.");
            Start = start;
            _end = end;
            _hasOpenStart = hasOpenStart;
            _hasOpenEnd = hasOpenEnd;
        }

        public int Start { get; }

        public int Length => _end - Start + 1;

        public IEnumerator<PointType> GetEnumerator()
        {
            return
                Start == _end
                    ? OnePointRange()
                    : MultiplePointsRange();
            
            IEnumerator<PointType> OnePointRange()
            {
                yield return
                    _hasOpenStart || _hasOpenEnd
                        ? PointType.Outside
                        : PointType.ClosedStartAndEnd;
            }

            IEnumerator<PointType> MultiplePointsRange()
            {
                yield return _hasOpenStart ? PointType.OpenStart : PointType.ClosedStart;
                for (var point = Start + 1; point < _end; point++)
                {
                    yield return PointType.Inside;
                }
                yield return _hasOpenEnd ? PointType.OpenEnd : PointType.ClosedEnd;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}