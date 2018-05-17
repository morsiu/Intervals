using System;
using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Test.Support.RangeGeneration.Options;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    public sealed class AllPairsOfRangeKinds : IEnumerable<Tuple<RangeKind, RangeKind>>
    {
        public IEnumerator<Tuple<RangeKind, RangeKind>> GetEnumerator()
        {
            yield return Tuple.Create(RangeKind.Empty, RangeKind.Empty);
            yield return Tuple.Create(RangeKind.NonEmpty, RangeKind.Empty);
            yield return Tuple.Create(RangeKind.Empty, RangeKind.NonEmpty);
            yield return Tuple.Create(RangeKind.NonEmpty, RangeKind.NonEmpty);
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
