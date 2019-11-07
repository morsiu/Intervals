using System;
using System.Collections.Generic;

namespace Mors.Intervals.Inequations.Tests
{
    using Implementation = Inequations.Inequation;

    public readonly struct OpenInterval : IOpenInterval<Point>, IEquatable<OpenInterval>
    {
        private readonly Point _start;
        private readonly Point _end;
        private readonly bool _isNonEmpty;
        private readonly bool _isStartClosed;
        private readonly bool _isEndClosed;

        private OpenInterval(in Point start, in Point end, bool isStartClosed, bool isEndClosed)
        {
            _start = start;
            _end = end;
            _isStartClosed = isStartClosed;
            _isEndClosed = isEndClosed;
            _isNonEmpty = true;
        }

        public static OpenInterval LeftOpen(in Point start, in Point end) =>
            new OpenInterval(start, end, isStartClosed: false, isEndClosed: true);

        public static OpenInterval RightOpen(in Point start, in Point end) =>
            new OpenInterval(start, end, isStartClosed: true, isEndClosed: false);

        public static OpenInterval Closed(in Point start, in Point end) =>
            new OpenInterval(start, end, isStartClosed: true, isEndClosed: true);

        public static OpenInterval Open(in Point start, in Point end) =>
            new OpenInterval(start, end, isStartClosed: false, isEndClosed: false);

        public static OpenInterval Empty() => new OpenInterval();

        public static OpenIntervalUnion Union(in OpenInterval first, in OpenInterval other) =>
            Inequation.Or(first.ToInequation(), other.ToInequation()).ToOpenIntervalUnion();

        public bool IsEmpty() => !_isNonEmpty;

        public bool Equals(OpenInterval other) =>
            _isNonEmpty == other._isNonEmpty
            && (!_isNonEmpty
                || (_isStartClosed == other._isStartClosed
                    && _isEndClosed == other._isEndClosed
                    && EqualityComparer<Point>.Default.Equals(_start, other._start)
                    && EqualityComparer<Point>.Default.Equals(_end, other._end)));

        public override bool Equals(object obj) =>
            obj is OpenInterval other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(_start, _end, _isNonEmpty, _isStartClosed, _isEndClosed);

        public Inequation ToInequation() =>
            new Inequation(Implementation.FromOpenInterval<Point, OpenInterval>(this));

        public override string ToString() =>
            (_isNonEmpty, _isStartClosed, _isEndClosed) switch
            {
                (true, false, false) => $"({_start}; {_end})",
                (true, false, true) => $"({_start}; {_end}]",
                (true, true, false) => $"[{_start}; {_end})",
                (true, true, true) => $"[{_start}; {_end}]",
                _ => "∅",
            };

        public OpenIntervalUnion ToUnion() => OpenIntervalUnion.FromInterval(this);

        bool IOpenInterval<Point>.Empty() => !_isNonEmpty;

        bool IOpenInterval<Point>.ClosedStart() => _isStartClosed;

        bool IOpenInterval<Point>.ClosedEnd() => _isEndClosed;

        Point IOpenInterval<Point>.Start() => _start;

        Point IOpenInterval<Point>.End() => _end;
    }
}