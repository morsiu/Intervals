using System;
using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    public sealed class AllNullPairsOfRangeKinds : IEnumerable<Tuple<RangeKind, RangeKind>>
    {
        public IEnumerator<Tuple<RangeKind, RangeKind>> GetEnumerator()
        {
            yield return Tuple.Create(RangeKind.Null, RangeKind.Null);
            yield return Tuple.Create(RangeKind.Null, RangeKind.Empty);
            yield return Tuple.Create(RangeKind.Empty, RangeKind.Null);
            yield return Tuple.Create(RangeKind.Null, RangeKind.NonEmpty);
            yield return Tuple.Create(RangeKind.NonEmpty, RangeKind.Null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
