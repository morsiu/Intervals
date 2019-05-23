using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class ZipOfPointSequences<TResult> : IEnumerable<TResult>
    {
        private readonly IPointSequence _first;
        private readonly IPointSequence _second;
        private readonly Func<PairOfPointTypes, TResult> _zip;

        public ZipOfPointSequences(
            IPointSequence first,
            IPointSequence second,
            Func<PairOfPointTypes, TResult> zip)
        {
            _first = new AlignedPointSequence(first, second);
            _second = new AlignedPointSequence(second, first);
            _zip = zip;
        }

        public IEnumerator<TResult> GetEnumerator()
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