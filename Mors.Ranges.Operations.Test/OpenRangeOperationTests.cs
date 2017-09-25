// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Mors.Ranges.Test.Support.RangeGeneration;
using Mors.Ranges.Test.Support.RangeOperations;
using NUnit.Framework;

namespace Mors.Ranges.Operations
{
    [TestFixture]
    public sealed class OpenRangeOperationTests
    {
        [Test]
        [TestCaseSource(typeof(PairsOfOpenRanges), nameof(PairsOfOpenRanges.OfAllPossibleRelations))]
        public void IntersectsWithReturnsExpectedResult(RangePair pairOfRanges)
        {
            new ReferenceImplementationTest<bool>(
                    IntersectsWithOperation.Calculate,
                    new OpenRangeOperationReturningBool(OpenRangeOperations.IntersectsWith<int, OpenRange>))
                .Run(pairOfRanges);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRanges), nameof(PairsOfOpenRanges.OfAllPossibleRelations))]
        public void IntersectReturnsExpectedResult(RangePair pairOfRanges)
        {
            new ReferenceImplementationTest<IRange<int>>(
                    IntersectOperation.Calculate,
                    new OpenRangeOperationReturningRange(OpenRangeOperations.Intersect<int, OpenRange, OpenRanges>))
                .Run(pairOfRanges);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRanges), nameof(PairsOfOpenRanges.OfAllPossibleRelations))]
        public void CoversReturnsExpectedResult(RangePair pairOfRanges)
        {
            new ReferenceImplementationTest<bool>(
                    CoversOperation.Calculate,
                    new OpenRangeOperationReturningBool(OpenRangeOperations.Covers<int, OpenRange>))
                .Run(pairOfRanges);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRanges), nameof(PairsOfOpenRanges.OfAllPossibleRelations))]
        public void IsCoveredByReturnsExpectedResult(RangePair pairOfRanges)
        {
            new ReferenceImplementationTest<bool>(
                    IsCoveredByOperation.Calculate,
                    new OpenRangeOperationReturningBool(OpenRangeOperations.IsCoveredBy<int, OpenRange>))
                .Run(pairOfRanges);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRanges), nameof(PairsOfOpenRanges.OfAllPossibleRelations))]
        public void SpanReturnsExpectedResult(RangePair pairOfRanges)
        {
            new ReferenceImplementationTest<IRange<int>>(
                    SpanOperation.Calculate,
                    new OpenRangeOperationReturningRange(OpenRangeOperations.Span<int, OpenRange, OpenRanges>))
                .Run(pairOfRanges);
        }
    }
}
