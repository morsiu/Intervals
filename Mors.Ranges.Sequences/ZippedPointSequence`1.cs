using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class ZippedPointSequence<TState> : IPointSequence
    {
        private readonly IPointSequence _first;
        private readonly IPointSequence _second;
        private readonly TState _initialState;
        private readonly Func<(TState, PairOfPointTypes), (TState, PointType)> _zip;

        public ZippedPointSequence(
            IPointSequence first,
            IPointSequence second,
            TState initialState,
            Func<(TState, PairOfPointTypes), (TState, PointType)> zip)
        {
            _first = new AlignedPointSequence(first, second);
            _second = new AlignedPointSequence(second, first);
            _initialState = initialState;
            _zip = zip;
        }

        public int Start => _first.Start;

        public int Length => _second.Start;

        public IEnumerator<PointType> GetEnumerator()
        {
            var state = _initialState;
            foreach (var pairOfPointTypes in _first.Zip(_second, PairOfPointTypes.Create))
            {
                var (nextState, output) = _zip((state, pairOfPointTypes));
                state = nextState;
                yield return output;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
