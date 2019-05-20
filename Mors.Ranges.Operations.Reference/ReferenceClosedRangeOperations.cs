// Copyright (C) 2019 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceClosedRangeOperations<TClosedRange, TClosedRanges>
        where TClosedRange : IRange<int>, IEmptyRange
        where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
    {
        public static object Contains(in TClosedRange range, int point) =>
            new ContainsOperation(PointSequence(range), point).Result();

        public static bool Covers(in TClosedRange first, in TClosedRange second) =>
            new CoversOperation(PointSequence(first), PointSequence(second)).Result();

        public static bool IntersectsWith(TClosedRange first, TClosedRange second) =>
            new IntersectsWithOperation(PointSequence(first), PointSequence(second)).Result();

        public static TClosedRange Intersect(TClosedRange first, TClosedRange second) =>
            AtMostOneClosedRange(new IntersectOperation(PointSequence(first), PointSequence(second)));

        public static bool IsCoveredBy(TClosedRange first, TClosedRange second) =>
            new IsCoveredByOperation(PointSequence(first), PointSequence(second)).Result();

        public static TClosedRange Span(TClosedRange first, TClosedRange second) =>
            AtMostOneClosedRange(new SpanOperation(PointSequence(first), PointSequence(second)));

        private static TClosedRange AtMostOneClosedRange(IPointSequence pointSequence) =>
            pointSequence.AtMostOneClosedRange<TClosedRange, TClosedRanges>();

        private static IPointSequence PointSequence(in TClosedRange range) =>
            new PointSequenceOfClosedRange<TClosedRange>(range).Value();
    }
}
