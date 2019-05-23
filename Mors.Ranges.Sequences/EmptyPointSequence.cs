using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Sequences
{
    public sealed class EmptyPointSequence : IPointSequence
    {
        public int Start => 0;

        public int Length => 0;

        public IEnumerator<PointType> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}