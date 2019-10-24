﻿using System;
using System.Collections.Generic;

namespace Mors.Ranges.Inequations.Tests
{
    using Implementation = Inequations.Inequation;

    public readonly struct OpenRange : IOpenRange<Point>, IEquatable<OpenRange>
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

        public bool Equals(OpenRange other) =>
            _isNonEmpty == other._isNonEmpty
            && (!_isNonEmpty
                || (_isStartClosed == other._isStartClosed
                    && _isEndClosed == other._isEndClosed
                    && EqualityComparer<Point>.Default.Equals(_start, other._start)
                    && EqualityComparer<Point>.Default.Equals(_end, other._end)));

        public override bool Equals(object obj) =>
            obj is OpenRange other && Equals(other);

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

        public OpenRangeUnion ToUnion() => OpenRangeUnion.FromRange(this);

        bool IOpenRange<Point>.Empty() => !_isNonEmpty;

        bool IOpenRange<Point>.ClosedStart() => _isStartClosed;

        bool IOpenRange<Point>.ClosedEnd() => _isEndClosed;

        Point IOpenRange<Point>.Start() => _start;

        Point IOpenRange<Point>.End() => _end;
    }
}