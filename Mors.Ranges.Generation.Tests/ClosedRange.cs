using System.Collections.Generic;

namespace Mors.Ranges.Generation.Tests
{
    internal readonly struct ClosedRange
    {
        private readonly int _start;
        private readonly int _end;
        private readonly bool _nonEmpty;

        public ClosedRange(int start, int end)
        {
            _nonEmpty = true;
            _start = start;
            _end = end;
        }

        public IEnumerable<OpenRange> ToOpenRanges()
        {
            var x = new OpenRanges();
            if (!_nonEmpty)
            {
                yield return x.Empty();
            }
            else
            {
                yield return x.Range(_start, _end, isStartOpen: true, isEndOpen: true);
                yield return x.Range(_start, _end, isStartOpen: true, isEndOpen: false);
                yield return x.Range(_start, _end, isStartOpen: false, isEndOpen: true);
                yield return x.Range(_start, _end, isStartOpen: false, isEndOpen: false);
            }
        }

        public override string ToString() =>
            _nonEmpty ? $"[{_start}, {_end}]" : "∅";
    }
}
