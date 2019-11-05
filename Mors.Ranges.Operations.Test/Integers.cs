using System;

namespace Mors.Ranges.Operations
{
    public readonly struct Integers : IPoints, IPoints<int>
    {
        public IPoints<T> For<T>() =>
            typeof(T) == typeof(int)
                ? (IPoints<T>)(object)this
                : throw new NotSupportedException();

        public int UnsafeNext(int current) => current + 1;

        public int UnsafePrevious(int current) => current - 1;
    }
}
