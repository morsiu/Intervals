using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    internal sealed class NullRanges : IEnumerable<IRange<int>>
    {
        public IEnumerator<IRange<int>> GetEnumerator()
        {
            yield return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
