// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Walrus.Ranges.Test.Cases.Generation.Options;
using Walrus.Ranges.Test.Extensions;

namespace Walrus.Ranges.Test.Cases.Generation
{
    internal static class RangePairGenerator
    {
        public static IEnumerable<RangePair> GeneratePairs(RangePairGeneratorOptions options)
        {
            foreach (var abRangesRelation in options.ABRangesRelations.EnumerateFlags())
            {
                foreach (var rangeAEnds in options.RangeAEnds.EnumerateFlags())
                {
                    foreach (var rangeBEnds in options.RangeBEnds.EnumerateFlags())
                    {
                        yield return GeneratePair(abRangesRelation, rangeAEnds, rangeBEnds);
                    }
                }
            }
        }

        private static RangePair GeneratePair(RangeRelations abRangesRelation, RangeEnds rangeAEnds, RangeEnds rangeBEnds)
        {
            switch (abRangesRelation)
            {
                case RangeRelations.ABeforeB:
                    return GeneratePair(1, 5, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.ABeforeBTouching:
                    return GeneratePair(2, 7, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.ABeforeBIntersecting:
                    return GeneratePair(5, 9, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.ASpanningB:
                    return GeneratePair(7, 11, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.AAfterBIntersecting:
                    return GeneratePair(9, 13, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.AAfterBTouching:
                    return GeneratePair(11, 15, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.AAfterB:
                    return GeneratePair(13, 18, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelations.ACoversBTouchingLeft:
                    return GeneratePair(1, 8, rangeAEnds, 1, 4, rangeBEnds);
                case RangeRelations.ACoversB:
                    return GeneratePair(1, 8, rangeAEnds, 3, 6, rangeBEnds);
                case RangeRelations.ACoversBTouchingRight:
                    return GeneratePair(1, 8, rangeAEnds, 5, 8, rangeBEnds);
                case RangeRelations.AInsideBTouchingLeft:
                    return GeneratePair(1, 4, rangeAEnds, 1, 8, rangeBEnds);
                case RangeRelations.AInsideB:
                    return GeneratePair(3, 6, rangeAEnds, 1, 8, rangeBEnds);
                case RangeRelations.AInsideBTouchingRight:
                    return GeneratePair(5, 8, rangeAEnds, 1, 8, rangeBEnds);
                default:
                    throw new ArgumentOutOfRangeException("abRangesRelation");
            }
        }

        private static RangePair GeneratePair(
            int rangeAStart, int rangeAEnd, RangeEnds rangeAEnds,
            int rangeBStart, int rangeBEnd, RangeEnds rangeBEnds)
        {
            var rangeAHasOpenStart = rangeAEnds == RangeEnds.LeftOpen;
            var rangeAHasOpenEnd = rangeAEnds == RangeEnds.RightOpen;
            var rangeA = Range.Create(rangeAStart, rangeAEnd, rangeAHasOpenStart, rangeAHasOpenEnd);
            var rangeBHasOpenStart = rangeBEnds == RangeEnds.LeftOpen;
            var rangeBHasOpenEnd = rangeBEnds == RangeEnds.RightOpen;
            var rangeB = Range.Create(rangeBStart, rangeBEnd, rangeBHasOpenStart, rangeBHasOpenEnd);
            return new RangePair(rangeA, rangeB);
        }
    }
}
