// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    public static class PairsOfOpenRanges
    {
        public static IEnumerable<(OpenRange, OpenRange)> OfAllPossibleRelations()
        {
            return new PairsOfRangesOfKinds<Ranges, Range>(new AllPairsOfRangeKinds())
                .Where(x => x.RangeA != null && x.RangeB != null)
                .Select(x => (x.RangeA.OpenRange(), x.RangeB.OpenRange()));
        }

        private sealed class Range
        {
            private readonly bool _empty;
            private readonly int _start;
            private readonly int _end;
            private readonly bool _openStart;
            private readonly bool _openEnd;

            public Range(bool empty, int start, int end, bool openStart, bool openEnd)
            {
                _empty = empty;
                _start = start;
                _end = end;
                _openStart = openStart;
                _openEnd = openEnd;
            }
            
            public OpenRange OpenRange() => _empty ? new OpenRange() : new OpenRange(_start, _end, _openStart, _openEnd);
        }

        private struct Ranges : IRanges<Range>
        {
            public Range Empty()
            {
                return new Range(true, 0, 0, false, false);
            }

            public Range NonEmpty(int start, int end, bool isStartOpen, bool isEndOpen)
            {
                return new Range(false, start, end, isStartOpen, isEndOpen);
            }
        }
    }
}
