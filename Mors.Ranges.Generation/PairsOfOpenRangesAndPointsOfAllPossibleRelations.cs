// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfOpenRangesAndPointsOfAllPossibleRelations<
        TRange,
        TRanges,
        TPairOfRangeAndPoint,
        TPairsOfRangesAndPoints>
    : IEnumerable<TPairOfRangeAndPoint>
    where TRanges : IOpenRanges<TRange>
    where TPairsOfRangesAndPoints : IPairs<TRange, int, TPairOfRangeAndPoint>
    {
        public IEnumerator<TPairOfRangeAndPoint> GetEnumerator()
        {
            var a = default(TPairsOfRangesAndPoints);
            return Enumerable.Range(1, 7)
                .SelectMany(x => Ranges(), (x, y) => a.Pair(y, x))
                .GetEnumerator();
        }

        private static IEnumerable<TRange> Ranges()
        {
            yield return default(TRanges).Range(3, 5, false, false);
            yield return default(TRanges).Range(3, 5, false, true);
            yield return default(TRanges).Range(3, 5, true, false);
            yield return default(TRanges).Range(3, 5, true, true);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
