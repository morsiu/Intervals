﻿using System;

namespace Mors.Intervals.Operations.Reference
{
    internal readonly struct OpenInterval<T> : Inequations.IOpenInterval<T>
        where T : IComparable<T>
    {
        private readonly T _start;
        private readonly T _end;
        private readonly bool _openStart;
        private readonly bool _openEnd;
        private readonly bool _notEmpty;

        public OpenInterval(in T start, in T end, bool openStart, bool openEnd)
        {
            _start = start;
            _end = end;
            _openStart = openStart;
            _openEnd = openEnd;
            _notEmpty = true;
        }

        public bool ClosedEnd() => !_openEnd;

        public bool ClosedStart() => !_openStart;

        public bool Empty() => !_notEmpty;

        public T End() => _end;

        public T Start() => _start;
    }
}
