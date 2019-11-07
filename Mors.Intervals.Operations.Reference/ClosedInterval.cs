using System;

namespace Mors.Intervals.Operations.Reference
{
    internal readonly struct ClosedInterval<T> : Inequations.IClosedInterval<T>
        where T : IComparable<T>
    {
        private readonly T _start;
        private readonly T _end;
        private readonly bool _notEmpty;

        public ClosedInterval(in T start, in T end)
        {
            _start = start;
            _end = end;
            _notEmpty = true;
        }

        public bool Empty() => !_notEmpty;

        public T End() => _end;

        public T Start() => _start;
    }
}
