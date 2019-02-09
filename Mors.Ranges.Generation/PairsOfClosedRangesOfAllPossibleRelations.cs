// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfClosedRangesOfAllPossibleRelations<TRange, TRangePair, TRanges, TRangePairs> : IEnumerable<TRangePair>
        where TRanges : IClosedRanges<TRange>
        where TRangePairs : IPairs<TRange, TRange, TRangePair>
    {
        public IEnumerator<TRangePair> GetEnumerator()
        {
            var x = default(TRanges);
            var y = default(TRangePairs);
            yield return y.Pair(x.Empty(), x.Empty());
            yield return y.Pair(x.Range(1, 5), x.Empty());
            yield return y.Pair(x.Empty(), x.Range(1, 5));
            yield return y.Pair(x.Range(1, 5), x.Range(7, 11));
            yield return y.Pair(x.Range(2, 7), x.Range(7, 11));
            yield return y.Pair(x.Range(5, 9), x.Range(7, 11));
            yield return y.Pair(x.Range(7, 11), x.Range(7, 11));
            yield return y.Pair(x.Range(9, 13), x.Range(7, 11));
            yield return y.Pair(x.Range(11, 15), x.Range(7, 11));
            yield return y.Pair(x.Range(13, 18), x.Range(7, 11));
            yield return y.Pair(x.Range(1, 8), x.Range(1, 4));
            yield return y.Pair(x.Range(1, 8), x.Range(3, 6));
            yield return y.Pair(x.Range(1, 8), x.Range(5, 8));
            yield return y.Pair(x.Range(1, 4), x.Range(1, 8));
            yield return y.Pair(x.Range(3, 6), x.Range(1, 8));
            yield return y.Pair(x.Range(5, 8), x.Range(1, 8));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
