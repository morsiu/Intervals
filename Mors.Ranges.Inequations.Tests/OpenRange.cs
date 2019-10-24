using System;
using System.Collections.Generic;

namespace Mors.Ranges.Inequations.Tests
{
    using Implementation = Inequations.Inequation;

    public readonly struct OpenRange : IOpenRange<Point>
    {
        private readonly Point _start;
        private readonly Point _end;
        private readonly bool _isNonEmpty;
        private readonly bool _isStartClosed;
        private readonly bool _isEndClosed;

        private OpenRange(in Point start, in Point end, bool isStartClosed, bool isEndClosed)
        {
            _start = start;
            _end = end;
            _isStartClosed = isStartClosed;
            _isEndClosed = isEndClosed;
            _isNonEmpty = true;
        }

        public static OpenRange LeftOpen(in Point start, in Point end) =>
            new OpenRange(start, end, isStartClosed: false, isEndClosed: true);

        public static OpenRange RightOpen(in Point start, in Point end) =>
            new OpenRange(start, end, isStartClosed: true, isEndClosed: false);

        public static OpenRange Closed(in Point start, in Point end) =>
            new OpenRange(start, end, isStartClosed: true, isEndClosed: true);

        public static OpenRange Open(in Point start, in Point end) =>
            new OpenRange(start, end, isStartClosed: false, isEndClosed: false);

        public static OpenRange Empty() => new OpenRange();

        public static OpenRangeUnion Union(in OpenRange first, in OpenRange other) =>
            Inequation.Or(first.ToInequation(), other.ToInequation()).ToOpenRangeUnion();

        public override bool Equals(object obj) =>
            obj is OpenRange range
            && _isStartClosed == range._isStartClosed
            && (!_isStartClosed
                || (EqualityComparer<Point>.Default.Equals(_start, range._start)
                    && EqualityComparer<Point>.Default.Equals(_end, range._end)
                    && _isNonEmpty == range._isNonEmpty
                    && _isEndClosed == range._isEndClosed));

        public override int GetHashCode() =>
            HashCode.Combine(_start, _end, _isNonEmpty, _isStartClosed, _isEndClosed);

        public Inequation ToInequation() =>
            new Inequation(Implementation.FromOpenRange<Point, OpenRange>(this));

        public override string ToString() =>
            (_isNonEmpty, _isStartClosed, _isEndClosed) switch
            {
                (true, false, false) => $"({_start}; {_end})",
                (true, false, true) => $"({_start}; {_end}]",
                (true, true, false) => $"[{_start}; {_end})",
                (true, true, true) => $"[{_start}; {_end}]",
                _ => "∅",
            };

        bool IOpenRange<Point>.Empty() => !_isNonEmpty;

        bool IOpenRange<Point>.ClosedStart() => _isStartClosed;

        bool IOpenRange<Point>.ClosedEnd() => _isEndClosed;

        Point IOpenRange<Point>.Start() => _start;

        Point IOpenRange<Point>.End() => _end;
    }
}