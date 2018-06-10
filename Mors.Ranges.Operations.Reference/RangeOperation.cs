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
        public static Range? Zip(
            Range? rangeA,
            Range? rangeB,
            StateTable<PointTypePair, PointType> stateTable)
        {
            return RangeFromSequence(
                new ZippedPointSequence(
                    rangeA is Range x
                        ? new PointSequenceFromRange(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    rangeB is Range y
                        ? new PointSequenceFromRange(y.Start, y.End, y.HasOpenStart, y.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    stateTable.Match));
        }

        public static Range? Zip<TState>(
            Range? rangeA,
            Range? rangeB,
            TState initialState,
            StateTable<(TState, PointTypePair), (TState, PointType)> stateTable)
        {
            return RangeFromSequence(
                new ZippedPointSequence<TState>(
                    rangeA is Range x
                        ? new PointSequenceFromRange(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    rangeB is Range y
                        ? new PointSequenceFromRange(y.Start, y.End, y.HasOpenStart, y.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    initialState,
                    stateTable.Match));
        }

        private static Range? RangeFromSequence(
            IPointSequence sequence)
        {
            return new RangesInPointSequence(sequence)
                .Cast<Range?>()
                .FirstOrDefault();
        }

        public static bool Any<TValue>(
            Range? rangeA,
            Range? rangeB,
            Func<TValue, bool> predicate,
            StateTable<PointTypePair, TValue> stateTable)
        {
            return new ZipOfPointSequences<TValue>(
                    rangeA is Range x
                        ? new PointSequenceFromRange(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    rangeB is Range y
                        ? new PointSequenceFromRange(y.Start, y.End, y.HasOpenStart, y.HasOpenEnd)
                        : (IPointSequence)new EmptyPointSequence(),
                    stateTable.Match)
                .Any(predicate);
        }
    }
}
