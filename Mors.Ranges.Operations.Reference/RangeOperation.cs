// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Linq;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal static class RangeOperations
    {
        public static IRange<int> Zip(
            IRange<int> rangeA,
            IRange<int> rangeB,
            StateTable<PointTypePair, PointType> stateTable)
        {
            return RangeFromSequence(
                new ZippedPointSequence(
                    !rangeA.IsEmpty
                        ? new PointSequenceFromRange(rangeA.Start, rangeA.End, rangeA.HasOpenStart, rangeA.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    !rangeB.IsEmpty
                        ? new PointSequenceFromRange(rangeB.Start, rangeB.End, rangeB.HasOpenStart, rangeB.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    stateTable.Match));
        }

        public static IRange<int> Zip<TState>(
            IRange<int> rangeA,
            IRange<int> rangeB,
            TState initialState,
            StateTable<(TState, PointTypePair), (TState, PointType)> stateTable)
        {
            return RangeFromSequence(
                new ZippedPointSequence<TState>(
                    !rangeA.IsEmpty
                        ? new PointSequenceFromRange(rangeA.Start, rangeA.End, rangeA.HasOpenStart, rangeA.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    !rangeB.IsEmpty
                        ? new PointSequenceFromRange(rangeB.Start, rangeB.End, rangeB.HasOpenStart, rangeB.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    initialState,
                    stateTable.Match));
        }

        private static IRange<int> RangeFromSequence(
            IPointSequence sequence)
        {
            var result =
                new RangesInPointSequence(sequence)
                    .FirstOrDefault();
            return result == null
                ? Range.Empty<int>()
                : Range.Create(result.Start, result.End, result.HasOpenStart, result.HasOpenEnd);
        }

        public static bool Any<TValue>(
            IRange<int> rangeA,
            IRange<int> rangeB,
            Func<TValue, bool> predicate,
            StateTable<PointTypePair, TValue> stateTable)
        {
            var result =
                new ZipOfPointSequences<TValue>(
                    !rangeA.IsEmpty
                        ? new PointSequenceFromRange(rangeA.Start, rangeA.End, rangeA.HasOpenStart, rangeA.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    !rangeB.IsEmpty
                        ? new PointSequenceFromRange(rangeB.Start, rangeB.End, rangeB.HasOpenStart, rangeB.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    stateTable.Match);
            return result.Any(predicate);
        }
    }
}
