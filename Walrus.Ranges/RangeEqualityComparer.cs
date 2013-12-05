using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.Ranges
{
    public sealed class RangeEqualityComparer<T> : IEqualityComparer<IRange<T>>
        where T : IComparable<T>
    {
        public static readonly RangeEqualityComparer<T> Instance = new RangeEqualityComparer<T>();

        private RangeEqualityComparer()
        {
        }

        public bool Equals(IRange<T> x, IRange<T> y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (ReferenceEquals(x, null) ||
                ReferenceEquals(y, null))
            {
                return false;
            }
            return x.HasOpenStart == y.HasOpenStart
                && x.HasOpenEnd == y.HasOpenEnd
                && x.Start.CompareTo(y.End) == 0
                && x.End.CompareTo(y.End) == 0;
        }

        public int GetHashCode(IRange<T> obj)
        {
            if (obj == null)
            {
                return 0;
            }
            unchecked
            {
                var hashCode = ReferenceEquals(obj.Start, null) ? 0 : obj.Start.GetHashCode();
                hashCode = (hashCode * 397) ^ (ReferenceEquals(obj.End, null) ? 0 : obj.End.GetHashCode());
                hashCode = (hashCode * 397) ^ obj.HasOpenStart.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.HasOpenEnd.GetHashCode();
                return hashCode;
            }
        }
    }
}
