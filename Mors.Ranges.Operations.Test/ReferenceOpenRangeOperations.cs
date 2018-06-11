// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Linq;
using Mors.Ranges.Operations.Reference;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations
{
    internal static class ReferenceOpenRangeOperations
    {
        public static bool Covers(OpenRange first, OpenRange second)
        {
            return CoversOperation.Calculate(first.Range(), second.Range());
        }

        public static bool IntersectsWith(OpenRange first, OpenRange second)
        {
            return IntersectsWithOperation.Calculate(first.Range(), second.Range());
        }

        public static OpenRange Intersect(OpenRange first, OpenRange second)
        {
            return IntersectOperation.Calculate(first.Range(), second.Range()).OpenRange();
        }

        public static bool IsCoveredBy(OpenRange first, OpenRange second)
        {
            return IsCoveredByOperation.Calculate(first.Range(), second.Range());
        }

        public static OpenRange Span(OpenRange first, OpenRange second)
        {
            return
                new RangesInPointSequence(
                        new SpanOperation(first.PointSequence(), second.PointSequence()))
                    .Cast<Range?>()
                    .SingleOrDefault()
                    .OpenRange();
        }
    }
}
