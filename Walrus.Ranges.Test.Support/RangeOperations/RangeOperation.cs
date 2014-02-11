// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Linq;
using Walrus.Ranges.Test.Support.RangeOperations.StateMachines;
using Walrus.Ranges.Text;

namespace Walrus.Ranges.Test.Support.RangeOperations
{
    internal static class RangeOperations
    {
        public static IRange<int> Zip(IRange<int> rangeA, IRange<int> rangeB, StateTable<PointTypePair, PointType> stateTable)
        {
            var rangePair = new PointSequencePair(
                PointSequence.FromRange(rangeA),
                PointSequence.FromRange(rangeB));
            var stateMachine = new StateMachine<PointType>(stateTable);
            var output = rangePair.Zip(stateMachine.Run);
            return output.ToRange();
        }

        public static bool Any<TValue>(IRange<int> rangeA, IRange<int> rangeB, Func<TValue, bool> predicate, StateTable<PointTypePair, TValue> stateTable)
        {
            var rangePair = new PointSequencePair(
                PointSequence.FromRange(rangeA),
                PointSequence.FromRange(rangeB));
            var stateMachine = new StateMachine<TValue>(stateTable);
            var output = rangePair.Zip<TValue>(stateMachine.Run);
            return output.Any(predicate);
        }
    }
}
