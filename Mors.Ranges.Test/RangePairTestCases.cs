﻿// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Test
{
    public static class RangePairTestCases
    {
        public static IEnumerable<RangePair<IRange<int>>> AllRangePairs
        {
            get
            {
                var pairs = new PairsOfRangesOfKinds<Ranges, IRange<int>>(new AllPairsOfRangeKinds());
                return pairs;
            }
        }

        public static IEnumerable<RangePair<IRange<int>>> AllNonNullRangePairs
        {
            get
            {
                var pairs = new PairsOfRangesOfKinds<Ranges, IRange<int>>(new AllNonNullPairsOfRangeKinds());
                return pairs;
            }
        }

        public static IEnumerable<RangePair<IRange<int>>> AllNullRangePairs
        {
            get
            {
                var pairs = new PairsOfRangesOfKinds<Ranges, IRange<int>>(new AllNullPairsOfRangeKinds());
                return pairs;
            }
        }

        private struct Ranges : IRanges<IRange<int>>
        {
            public IRange<int> Empty() => Range.Empty<int>();

            public IRange<int> NonEmpty(int start, int end, bool isStartOpen, bool isEndOpen) =>
                Range.Create(start, end, isStartOpen, isEndOpen);
        }
    }
}
