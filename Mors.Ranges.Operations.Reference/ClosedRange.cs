using System;

namespace Mors.Ranges.Operations.Reference
{
    internal readonly struct ClosedRange<T> : Inequations.IClosedRange<T>
        where T : IComparable<T>
    {
        private readonly T _start;
        private readonly T _end;
        private readonly bool _notEmpty;

        public ClosedRange(in T start, in T end)
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
