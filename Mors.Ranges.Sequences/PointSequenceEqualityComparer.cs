using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class PointSequenceEqualityComparer : IEqualityComparer<IPointSequence>
    {
        public bool Equals(IPointSequence x, IPointSequence y)
        {
            return x == null && y == null
                || (x != null && y != null && x.Start == y.Start && x.SequenceEqual(y));
        }

        public int GetHashCode(IPointSequence obj)
        {
            var hashCode = obj.Start;
            return obj.Aggregate(hashCode, (x, y) => unchecked((x * 397) ^ y.GetHashCode()));
        }
    }
}
