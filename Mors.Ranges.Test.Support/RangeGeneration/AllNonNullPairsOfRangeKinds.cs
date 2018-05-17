using System;
using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    public sealed class AllNonNullPairsOfRangeKinds : IEnumerable<Tuple<RangeKind, RangeKind>>
    {
        public IEnumerator<Tuple<RangeKind, RangeKind>> GetEnumerator()
        {
            yield return Tuple.Create(RangeKind.Empty, RangeKind.Empty);
            yield return Tuple.Create(RangeKind.NonEmpty, RangeKind.Empty);
            yield return Tuple.Create(RangeKind.Empty, RangeKind.NonEmpty);
            yield return Tuple.Create(RangeKind.NonEmpty, RangeKind.NonEmpty);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
