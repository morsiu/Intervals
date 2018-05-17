// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Mors.Ranges.Test.Support.RangeGeneration.Options;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    public static class RangePairGenerator
    {
        public static IEnumerable<RangePair> GeneratePairs(RangePairKinds pairKinds)
        {
            foreach (var pairKind in pairKinds)
            {
                var rangeAKind = pairKind.Item1;
                var rangeBKind = pairKind.Item2;
                foreach (var pair in GeneratePairs(rangeAKind, rangeBKind))
                {
                    yield return pair;
                }
            }
        }

        private static IEnumerable<RangePair> GeneratePairs(RangeKind rangeAKind, RangeKind rangeBKind)
        {
            if (rangeAKind == RangeKind.NonEmpty && rangeBKind == RangeKind.NonEmpty)
                return GenerateNonEmptyPairs();
            return GenerateMixedPairs(rangeAKind, rangeBKind);
        }

        private static IEnumerable<RangePair> GenerateMixedPairs(RangeKind rangeAKind, RangeKind rangeBKind)
        {
            var aRanges = GenerateRanges(rangeAKind);
            foreach (var rangeA in aRanges)
            {
                var bRanges = GenerateRanges(rangeBKind);
                foreach (var rangeB in bRanges)
                {
                    yield return new RangePair(rangeA, rangeB);
                }
            }
        }

        private static IEnumerable<IRange<int>> GenerateRanges(RangeKind rangeKind)
        {
            switch (rangeKind)
            {
                case RangeKind.NonEmpty:
                    foreach (var range in GenerateNonEmptyRanges()) yield return range;
                    break;
                case RangeKind.Empty:
                    yield return Range.Empty<int>();
                    break;
                case RangeKind.Null:
                    yield return null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rangeKind));
            }
        }

        private static IEnumerable<IRange<int>> GenerateNonEmptyRanges()
        {
            foreach (var rangeEnd in new AllRangeEnds())
            {
                yield return GenerateNonEmptyRange(1, 3, rangeEnd);
            }
        }

        private static IEnumerable<RangePair> GenerateNonEmptyPairs()
        {
            foreach (var abRangesRelation in new AllRangeRelations())
            {
                foreach (var rangeAEnds in new AllRangeEnds())
                {
                    foreach (var rangeBEnds in new AllRangeEnds())
                    {
                        yield return GenerateNonEmptyPair(abRangesRelation, rangeAEnds, rangeBEnds);
                    }
                }
            }
        }

        private static RangePair GenerateNonEmptyPair(RangeRelation abRangesRelation, RangeEnds rangeAEnds, RangeEnds rangeBEnds)
        {
            return new PairOfRangesInARelation(abRangesRelation).RangePair(rangeAEnds, rangeBEnds);
        }

        private static RangePair GenerateNonEmptyPair(
            int rangeAStart, int rangeAEnd, RangeEnds rangeAEnds,
            int rangeBStart, int rangeBEnd, RangeEnds rangeBEnds)
        {
            var rangeA = GenerateNonEmptyRange(rangeAStart, rangeAEnd, rangeAEnds);
            var rangeB = GenerateNonEmptyRange(rangeBStart, rangeBEnd, rangeBEnds);
            return new RangePair(rangeA, rangeB);
        }

        private static IRange<int> GenerateNonEmptyRange(int start, int end, RangeEnds ends)
        {
            var hasOpenStart = ends == RangeEnds.LeftOpen;
            var hasOpenEnd = ends == RangeEnds.RightOpen;
            var range = Range.Create(start, end, hasOpenStart, hasOpenEnd);
            return range;
        }
    }
}
