// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    internal static class OpenRangeExtensions
    {
        public static TOpenRange ToReferenceRange<TOpenRange, TOpenRanges>(
            this OpenRange<int> range)
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
            where TOpenRange : IRange<int>, IOpenRange, IEmptyRange
        {
            var ranges = default(TOpenRanges);
            return !range.Empty()
                ? ranges.Range(range.Start(), range.End(), !range.ClosedStart(), !range.ClosedEnd())
                : ranges.Empty();
        }

        public static Inequation<int> ToInequationFromOpen<TOpenRange>(this TOpenRange range)
            where TOpenRange : IRange<int>, IOpenRange, IEmptyRange =>
            Inequation.FromOpenRange<int, OpenRange<int>>(
                range.Empty
                    ? new OpenRange<int>()
                    : new OpenRange<int>(range.Start, range.End, range.OpenStart, range.OpenEnd));
    }
}
