// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Mors.Ranges.Test.Support.Extensions;
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
            var bRanges = GenerateRanges(rangeBKind);
            foreach (var rangeA in aRanges)
            {
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
                    throw new ArgumentOutOfRangeException("rangeType");
            }
        }

        private static IEnumerable<IRange<int>> GenerateNonEmptyRanges()
        {
            foreach (var rangeEnd in RangeEnds.All.EnumerateFlags())
            {
                yield return GenerateNonEmptyRange(1, 3, rangeEnd);
            }
        }

        private static IEnumerable<RangePair> GenerateNonEmptyPairs()
        {
            foreach (var abRangesRelation in RangeRelations.All.EnumerateFlags())
            {
                foreach (var rangeAEnds in RangeEnds.All.EnumerateFlags())
                {
                    foreach (var rangeBEnds in RangeEnds.All.EnumerateFlags())
                    {
                        yield return GenerateNonEmptyPair(abRangesRelation, rangeAEnds, rangeBEnds);
                    }
                }
            }
        }

        private static RangePair GenerateNonEmptyPair(RangeRelations abRangesRelation, RangeEnds rangeAEnds, RangeEnds rangeBEnds)
        {
            switch (abRangesRelation)
            {
                case RangeRelations.ABeforeB:
                    return GenerateNonEmptyPair(1, 5, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.ABeforeBTouching:
                    return GenerateNonEmptyPair(2, 7, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.ABeforeBIntersecting:
                    return GenerateNonEmptyPair(5, 9, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.ASpanningB:
                    return GenerateNonEmptyPair(7, 11, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.AAfterBIntersecting:
                    return GenerateNonEmptyPair(9, 13, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.AAfterBTouching:
                    return GenerateNonEmptyPair(11, 15, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.AAfterB:
                    return GenerateNonEmptyPair(13, 18, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.ACoversBTouchingLeft:
                    return GenerateNonEmptyPair(1, 8, rangeAEnds, 1, 4, rangeBEnds);
                case RangeRelations.ACoversB:
                    return GenerateNonEmptyPair(1, 8, rangeAEnds, 3, 6, rangeBEnds);
                case RangeRelations.ACoversBTouchingRight:
                    return GenerateNonEmptyPair(1, 8, rangeAEnds, 5, 8, rangeBEnds);
                case RangeRelations.AInsideBTouchingLeft:
                    return GenerateNonEmptyPair(1, 4, rangeAEnds, 1, 8, rangeBEnds);
                case RangeRelations.AInsideB:
                    return GenerateNonEmptyPair(3, 6, rangeAEnds, 1, 8, rangeBEnds);
                case RangeRelations.AInsideBTouchingRight:
                    return GenerateNonEmptyPair(5, 8, rangeAEnds, 1, 8, rangeBEnds);
                default:
                    throw new ArgumentOutOfRangeException("abRangesRelation");
            }
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
