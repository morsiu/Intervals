using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    internal sealed class NonEmptyRanges : IEnumerable<IRange<int>>
    {
        public IEnumerator<IRange<int>> GetEnumerator()
        {
            foreach (var rangeEnd in new AllRangeEnds())
            {
                yield return GenerateNonEmptyRange(1, 3, rangeEnd);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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