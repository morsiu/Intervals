using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    internal sealed class AllRangeRelations : IEnumerable<RangeRelation>
    {
        public IEnumerator<RangeRelation> GetEnumerator()
        {
            yield return RangeRelation.ABeforeB;
            yield return RangeRelation.ABeforeBTouching;
            yield return RangeRelation.ABeforeBIntersecting;
            yield return RangeRelation.ASpanningB;
            yield return RangeRelation.AAfterBIntersecting;
            yield return RangeRelation.AAfterBTouching;
            yield return RangeRelation.AAfterB;
            yield return RangeRelation.ACoversBTouchingLeft;
            yield return RangeRelation.ACoversB;
            yield return RangeRelation.ACoversBTouchingRight;
            yield return RangeRelation.AInsideBTouchingLeft;
            yield return RangeRelation.AInsideB;
            yield return RangeRelation.AInsideBTouchingRight;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
