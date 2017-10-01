using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeOperations
{
    public sealed class EmptyAxis : IAxisSegment
    {
        public IEnumerator<AxisPoint> GetEnumerator()
        {
            yield break;
        }

        public AxisDistance Length()
            => new AxisDistance();

        public AxisPosition Start()
            => new AxisPosition();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

    }
}