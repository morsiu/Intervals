// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
