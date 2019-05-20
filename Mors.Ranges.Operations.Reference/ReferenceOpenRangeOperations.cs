// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceOpenRangeOperations<TOpenRange, TOpenRanges>
        where TOpenRange : IRange<int>, IOpenRange, IEmptyRange
        where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
    {
        public static object Contains(in TOpenRange range, int point) =>
            new ContainsOperation(PointSequence(range), point).Result();

        public static bool Covers(TOpenRange first, TOpenRange second) =>
            new CoversOperation(PointSequence(first), PointSequence(second)).Result();

        public static bool IntersectsWith(TOpenRange first, TOpenRange second) =>
            new IntersectsWithOperation(PointSequence(first), PointSequence(second)).Result();

        public static TOpenRange Intersect(TOpenRange first, TOpenRange second) =>
            new IntersectOperation(PointSequence(first), PointSequence(second))
                .AtMostOneOpenRange<TOpenRange, TOpenRanges>();

        public static bool IsCoveredBy(TOpenRange first, TOpenRange second) =>
            new IsCoveredByOperation(PointSequence(first), PointSequence(second)).Result();

        public static TOpenRange Span(TOpenRange first, TOpenRange second) =>
            new SpanOperation(PointSequence(first), PointSequence(second))
                .AtMostOneOpenRange<TOpenRange, TOpenRanges>();

        private static IPointSequence PointSequence(in TOpenRange range) =>
            new PointSequenceOfOpenRange<TOpenRange>(range).Value();
    }
}
