// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    internal sealed class PairsOfRangesOfMixedKinds<TRanges, TRange> : IEnumerable<RangePair<TRange>>
        where TRanges : struct, IRanges<TRange>
    {
        public IEnumerator<RangePair<TRange>> GetEnumerator()
        {
            var emptyRange = default(TRanges).Empty();
            foreach (var rangeEnds in new AllRangeEnds())
            {
                var nonEmptyRange =
                    default(TRanges).NonEmpty(
                        1,
                        3,
                        rangeEnds == RangeEnds.LeftOpen || rangeEnds == RangeEnds.Open,
                        rangeEnds == RangeEnds.RightOpen || rangeEnds == RangeEnds.Open);
                yield return new RangePair<TRange>(emptyRange, nonEmptyRange);
                yield return new RangePair<TRange>(nonEmptyRange, emptyRange);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
