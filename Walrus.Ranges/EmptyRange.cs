using System;

namespace Walrus.Ranges
{
    internal sealed class EmptyRange<T> : IRange<T>
        where T : IComparable<T>
    {
        public bool IsEmpty
        {
            get { return true; }
        }

        public T Start
        {
            get { throw new InvalidOperationException(); }
        }

        public T End
        {
            get { throw new InvalidOperationException(); }
        }

        public bool HasOpenStart
        {
            get { throw new InvalidOperationException(); }
        }

        public bool HasOpenEnd
        {
            get { throw new InvalidOperationException(); }
        }

        public bool Equals(IRange<T> other)
        {
            return RangeEqualityComparer<T>.Instance.Equals(this, other);
        }

        public override bool Equals(object obj)
        {
            return RangeEqualityComparer<T>.Instance.Equals(this, obj as IRange<T>);
        }

        public override int GetHashCode()
        {
            return RangeEqualityComparer<T>.Instance.GetHashCode(this);
        }
    }
}
