using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class ZippedPointSequence : IPointSequence
    {
        private readonly IPointSequence _first;
        private readonly IPointSequence _second;
        private readonly Func<PairOfPointTypes, PointType> _zip;

        public ZippedPointSequence(
            IPointSequence first,
            IPointSequence second,
            Func<PairOfPointTypes, PointType> zip)
        {
            _first = new AlignedPointSequence(first, second);
            _second = new AlignedPointSequence(second, first);
            _zip = zip;
        }

        public int Start => _first.Start;

        public int Length => _first.Length;

        public IEnumerator<PointType> GetEnumerator()
        {
            return _first
                .Zip(_second, PairOfPointTypes.Create)
                .Select(_zip)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}