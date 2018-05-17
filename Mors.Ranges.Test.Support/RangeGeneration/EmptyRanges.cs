using System;
using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    internal sealed class EmptyRanges : IEnumerable<IRange<int>>
    {
        public IEnumerator<IRange<int>> GetEnumerator()
        {
            yield return Range.Empty<int>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
