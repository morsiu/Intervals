// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal static class RangeExtensions
    {
        public static TOpenRange ToOpenRange<TOpenRange, TOpenRanges>(this Range? range)
            where TOpenRange : IRange<int>, IOpenRange
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
        {
            var ranges = default(TOpenRanges);
            return range is Range x
                ? ranges.Range(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd)
                : ranges.Empty();
        }

        public static TClosedRange ToClosedRange<TClosedRange, TClosedRanges>(this Range? range)
            where TClosedRange : IRange<int>
            where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
        {
            var ranges = default(TClosedRanges);
            return range is Range x
                ? !x.HasOpenStart && !x.HasOpenEnd
                    ? ranges.Range(x.Start, x.End)
                    : throw new ArgumentException("The range has open ends.", nameof(range))
                : ranges.Empty();
        }
    }
}