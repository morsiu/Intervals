// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation.Tests
{
    internal sealed class PairsOfClosedRangesAndPointsOfAllPossibleRelations
        : IEnumerable<PairOfClosedRangeAndPoint>
    {
        public IEnumerator<PairOfClosedRangeAndPoint> GetEnumerator()
        {
            var x = new ClosedRange(3, 5);
            yield return new PairOfClosedRangeAndPoint(x, 1); // range starts after the point
            yield return new PairOfClosedRangeAndPoint(x, 2); // range starts after the point, with adjacent start
            yield return new PairOfClosedRangeAndPoint(x, 3); // range starts with the point
            yield return new PairOfClosedRangeAndPoint(x, 4); // range contains the point
            yield return new PairOfClosedRangeAndPoint(x, 5); // range ends with the point
            yield return new PairOfClosedRangeAndPoint(x, 6); // range ends before the point, with adjacent end
            yield return new PairOfClosedRangeAndPoint(x, 7); // range ends before the point
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
