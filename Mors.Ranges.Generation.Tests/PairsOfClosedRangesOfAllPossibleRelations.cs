// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation.Tests
{
    internal sealed class PairsOfClosedRangesOfAllPossibleRelations : IEnumerable<ClosedRangePair>
    {
        public IEnumerator<ClosedRangePair> GetEnumerator()
        {
            var x = new ClosedRanges();
            yield return x.Pair(x.Empty(), x.Empty()); // both empty
            yield return x.Pair(x.Range(1, 5), x.Empty()); // second empty
            yield return x.Pair(x.Empty(), x.Range(1, 5)); // first empty
            yield return x.Pair(x.Range(1, 5), x.Range(7, 11)); // first before second
            yield return x.Pair(x.Range(2, 7), x.Range(7, 11)); // first before second, first ends where second starts
            yield return x.Pair(x.Range(5, 9), x.Range(7, 11)); // first before second, intersecting
            yield return x.Pair(x.Range(7, 11), x.Range(7, 11)); // first equal to second
            yield return x.Pair(x.Range(9, 13), x.Range(7, 11)); // first after second, intersecting
            yield return x.Pair(x.Range(11, 15), x.Range(7, 11)); // first after second, first starts where second ends
            yield return x.Pair(x.Range(13, 18), x.Range(7, 11)); // first after second
            yield return x.Pair(x.Range(1, 8), x.Range(1, 4)); // second inside first, first starts where second starts
            yield return x.Pair(x.Range(1, 8), x.Range(3, 6)); // second inside first
            yield return x.Pair(x.Range(1, 8), x.Range(5, 8)); // second inside first, first ends where second ends
            yield return x.Pair(x.Range(1, 4), x.Range(1, 8)); // first inside second, first starts where second starts
            yield return x.Pair(x.Range(3, 6), x.Range(1, 8)); // first inside second
            yield return x.Pair(x.Range(5, 8), x.Range(1, 8)); // first inside second, first ends where second ends
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}