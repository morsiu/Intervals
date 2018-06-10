// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Generation
{
    internal struct PairOfRangesInARelation<TRanges, TRange>
        where TRanges : struct, IRanges<TRange>
    {
        private readonly RangeRelation _rangeRelation;

        public PairOfRangesInARelation(RangeRelation rangeRelation)
        {
            _rangeRelation = rangeRelation;
        }

        public RangePair<TRange> RangePair(RangeEnds rangeAEnds, RangeEnds rangeBEnds)
        {
            switch (_rangeRelation)
            {
                case RangeRelation.ABeforeB:
                    return RangePair(1, 5, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelation.ABeforeBTouching:
                    return RangePair(2, 7, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelation.ABeforeBIntersecting:
                    return RangePair(5, 9, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelation.ASpanningB:
                    return RangePair(7, 11, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelation.AAfterBIntersecting:
                    return RangePair(9, 13, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelation.AAfterBTouching:
                    return RangePair(11, 15, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelation.AAfterB:
                    return RangePair(13, 18, rangeAEnds, 7, 11, rangeBEnds);
                case RangeRelation.ACoversBTouchingLeft:
                    return RangePair(1, 8, rangeAEnds, 1, 4, rangeBEnds);
                case RangeRelation.ACoversB:
                    return RangePair(1, 8, rangeAEnds, 3, 6, rangeBEnds);
                case RangeRelation.ACoversBTouchingRight:
                    return RangePair(1, 8, rangeAEnds, 5, 8, rangeBEnds);
                case RangeRelation.AInsideBTouchingLeft:
                    return RangePair(1, 4, rangeAEnds, 1, 8, rangeBEnds);
                case RangeRelation.AInsideB:
                    return RangePair(3, 6, rangeAEnds, 1, 8, rangeBEnds);
                case RangeRelation.AInsideBTouchingRight:
                    return RangePair(5, 8, rangeAEnds, 1, 8, rangeBEnds);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static RangePair<TRange> RangePair(
            int rangeAStart, int rangeAEnd, RangeEnds rangeAEnds,
            int rangeBStart, int rangeBEnd, RangeEnds rangeBEnds)
        {
            var rangeA = Range(rangeAStart, rangeAEnd, rangeAEnds);
            var rangeB = Range(rangeBStart, rangeBEnd, rangeBEnds);
            return new RangePair<TRange>(rangeA, rangeB);
        }

        private static TRange Range(int start, int end, RangeEnds ends)
        {
            var hasOpenStart = ends == RangeEnds.LeftOpen;
            var hasOpenEnd = ends == RangeEnds.RightOpen;
            var range = default(TRanges).NonEmpty(start, end, hasOpenStart, hasOpenEnd);
            return range;
        }
    }
}
