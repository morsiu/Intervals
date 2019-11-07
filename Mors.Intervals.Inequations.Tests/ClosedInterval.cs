using System;
using System.Collections.Generic;

namespace Mors.Intervals.Inequations.Tests
{
    using Implementation = Inequations.Inequation;

    public readonly struct ClosedInterval : IClosedInterval<Point>, IEquatable<ClosedInterval>
    {
        private readonly bool _isNonEmpty;
        private readonly Point _start;
        private readonly Point _end;

        private ClosedInterval(in Point start, in Point end)
        {
            _start = start;
            _end = end;
            _isNonEmpty = true;
        }

        public static ClosedInterval Closed(in Point start, in Point end) =>
            new ClosedInterval(start, end);

        public static ClosedInterval Empty() => new ClosedInterval();

        public bool Equals(ClosedInterval other) =>
            _isNonEmpty == other._isNonEmpty
            && EqualityComparer<Point>.Default.Equals(_start, other._start)
            && EqualityComparer<Point>.Default.Equals(_end, other._end);

        public override bool Equals(object obj) =>
            obj is ClosedInterval other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(_start, _end, _isNonEmpty);

        public bool IsEmpty => !_isNonEmpty;

        public Inequation ToInequation() =>
            new Inequation(Implementation.FromClosedInterval<Point, ClosedInterval>(this));

        public override string ToString() =>
            _isNonEmpty 
                ? $"[{_start}; {_end}]"
                : "∅";

        public ClosedIntervalUnion ToUnion() => ClosedIntervalUnion.FromInterval(this);

        bool IClosedInterval<Point>.Empty() => !_isNonEmpty;

        Point IClosedInterval<Point>.Start() => _start;

        Point IClosedInterval<Point>.End() => _end;
    }
}