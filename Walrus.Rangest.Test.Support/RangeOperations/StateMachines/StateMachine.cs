// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using Walrus.Ranges.Text;

namespace Walrus.Ranges.Test.Support.RangeOperations.StateMachines
{
    internal static class StateMachine
    {
        public static IRange<int> Zip(IRange<int> rangeA, IRange<int> rangeB, StateTable<PointTypePair, PointType> stateTable)
        {
            var rangePair = new PointSequencePair(
                PointSequence.FromRange(rangeA),
                PointSequence.FromRange(rangeB));
            var output = rangePair.Zip(stateTable.Match);
            return output.ToRange();
        }

        public static bool Any<TValue>(IRange<int> rangeA, IRange<int> rangeB, Func<TValue, bool> predicate, StateTable<PointTypePair, TValue> stateTable)
        {
            var rangePair = new PointSequencePair(
                PointSequence.FromRange(rangeA),
                PointSequence.FromRange(rangeB));
            var output = rangePair.Zip<TValue>(stateTable.Match);
            return output.Any(predicate);

        }

        public static IRange<int> Zip<TState>(IRange<int> rangeA, IRange<int> rangeB, TState initialState, StateTable<PointTypePair, TState, PointType> stateTable)
        {
            var rangePair = new PointSequencePair(
                PointSequence.FromRange(rangeA),
                PointSequence.FromRange(rangeB));
            var state = new State<TState, PointType>(initialState, stateTable);
            var output = rangePair.Zip(state.Match);
            return output.ToRange();
        }

        public static bool Any<TValue, TState>(IRange<int> rangeA, IRange<int> rangeB, Func<TValue, bool> predicate, TState initialState, StateTable<PointTypePair, TState, TValue> stateTable)
        {
            var rangePair = new PointSequencePair(
                PointSequence.FromRange(rangeA),
                PointSequence.FromRange(rangeB));
            var state = new State<TState, TValue>(initialState, stateTable);
            var output = rangePair.Zip<TValue>(state.Match);
            return output.Any(predicate);

        }

        private class State<TState, TOutput>
        {
            private readonly StateTable<PointTypePair, TState, TOutput> _stateTable;
            private TState _state;

            public State(TState initial, StateTable<PointTypePair, TState, TOutput> stateTable)
            {
                _state = initial;
                _stateTable = stateTable;
            }

            public TOutput Match(PointTypePair input)
            {
                var outputWithState = _stateTable.Match(input, _state);
                _state = outputWithState.State;
                var output = outputWithState.Value;
                return output;
            }
        }
    }
}
