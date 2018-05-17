using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    internal sealed class AllRangeEnds : IEnumerable<RangeEnds>
    {
        public IEnumerator<RangeEnds> GetEnumerator()
        {
            yield return RangeEnds.Open;
            yield return RangeEnds.Closed;
            yield return RangeEnds.LeftOpen;
            yield return RangeEnds.RightOpen;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
