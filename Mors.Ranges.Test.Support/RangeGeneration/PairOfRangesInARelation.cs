using System;
using Mors.Ranges.Test.Support.RangeGeneration.Options;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    internal sealed class PairOfRangesInARelation
    {
        private readonly RangeRelation _rangeRelation;

        public PairOfRangesInARelation(RangeRelation rangeRelation)
        {
            _rangeRelation = rangeRelation;
        }

        public RangePair RangePair(RangeEnds rangeAEnds, RangeEnds rangeBEnds)
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

        private static RangePair RangePair(
            int rangeAStart, int rangeAEnd, RangeEnds rangeAEnds,
            int rangeBStart, int rangeBEnd, RangeEnds rangeBEnds)
        {
            var rangeA = Range(rangeAStart, rangeAEnd, rangeAEnds);
            var rangeB = Range(rangeBStart, rangeBEnd, rangeBEnds);
            return new RangePair(rangeA, rangeB);
        }

        private static IRange<int> Range(int start, int end, RangeEnds ends)
        {
            var hasOpenStart = ends == RangeEnds.LeftOpen;
            var hasOpenEnd = ends == RangeEnds.RightOpen;
            var range = Ranges.Range.Create(start, end, hasOpenStart, hasOpenEnd);
            return range;
        }
    }
}
