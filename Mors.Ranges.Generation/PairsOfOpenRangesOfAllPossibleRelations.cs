// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfOpenRangesOfAllPossibleRelations<TRange, TRangePair, TRanges, TRangePairs> : IEnumerable<TRangePair>
        where TRanges : IOpenRanges<TRange>
        where TRangePairs : IPairs<TRange, TRange, TRangePair>
    {
        public IEnumerator<TRangePair> GetEnumerator()
        {
            return new PairsOfClosedRangesOfAllPossibleRelations<IEnumerable<TRange>, IEnumerable<TRangePair>, Ranges, RangePairs>()
                .SelectMany(x => x)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly struct Ranges : IClosedRanges<IEnumerable<TRange>>
        {
            public IEnumerable<TRange> Empty()
            {
                yield return default(TRanges).Empty();
            }

            public IEnumerable<TRange> Range(int start, int end)
            {
                var x = default(TRanges);
                yield return x.Range(start, end, isStartOpen: true, isEndOpen: true);
                yield return x.Range(start, end, isStartOpen: true, isEndOpen: false);
                yield return x.Range(start, end, isStartOpen: false, isEndOpen: true);
                yield return x.Range(start, end, isStartOpen: false, isEndOpen: false);
            }
        }
        
        private readonly struct RangePairs : IPairs<IEnumerable<TRange>, IEnumerable<TRange>, IEnumerable<TRangePair>>
        {
            public IEnumerable<TRangePair> Pair(IEnumerable<TRange> first, IEnumerable<TRange> second)
            {
                return first.SelectMany(x => second, (x, y) => default(TRangePairs).Pair(x, y));
            }
        }
    }
}
