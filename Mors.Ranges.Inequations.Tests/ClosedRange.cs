using System;
using System.Collections.Generic;

namespace Mors.Ranges.Inequations.Tests
{
    using Implementation = Inequations.Inequation;

    public readonly struct ClosedRange : IClosedRange<Point>, IEquatable<ClosedRange>
    {
        private readonly bool _isNonEmpty;
        private readonly Point _start;
        private readonly Point _end;

        private ClosedRange(in Point start, in Point end)
        {
            _start = start;
            _end = end;
            _isNonEmpty = true;
        }

        public static ClosedRange Closed(in Point start, in Point end) =>
            new ClosedRange(start, end);

        public static ClosedRange Empty() => new ClosedRange();

        public bool Equals(ClosedRange other) =>
            _isNonEmpty == other._isNonEmpty
            && EqualityComparer<Point>.Default.Equals(_start, other._start)
            && EqualityComparer<Point>.Default.Equals(_end, other._end);

        public override bool Equals(object obj) =>
            obj is ClosedRange other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(_start, _end, _isNonEmpty);

        public Inequation ToInequation() =>
            new Inequation(Implementation.FromClosedRange<Point, ClosedRange>(this));

        public override string ToString() =>
            _isNonEmpty 
                ? $"[{_start}; {_end}]"
                : "∅";

        public ClosedRangeUnion ToUnion() => ClosedRangeUnion.FromRange(this);

        bool IClosedRange<Point>.Empty() => !_isNonEmpty;

        Point IClosedRange<Point>.Start() => _start;

        Point IClosedRange<Point>.End() => _end;
    }
}