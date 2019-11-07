using System.Collections.Generic;

namespace Mors.Intervals.Generation.Tests
{
    internal readonly struct ClosedInterval
    {
        private readonly int _start;
        private readonly int _end;
        private readonly bool _nonEmpty;

        public ClosedInterval(int start, int end)
        {
            _nonEmpty = true;
            _start = start;
            _end = end;
        }

        public IEnumerable<OpenInterval> ToOpenIntervals()
        {
            var x = new OpenIntervals();
            if (!_nonEmpty)
            {
                yield return x.Empty();
            }
            else
            {
                yield return x.Interval(_start, _end, isStartOpen: true, isEndOpen: true);
                yield return x.Interval(_start, _end, isStartOpen: true, isEndOpen: false);
                yield return x.Interval(_start, _end, isStartOpen: false, isEndOpen: true);
                yield return x.Interval(_start, _end, isStartOpen: false, isEndOpen: false);
            }
        }

        public override string ToString() =>
            _nonEmpty ? $"[{_start}, {_end}]" : "∅";
    }
}
