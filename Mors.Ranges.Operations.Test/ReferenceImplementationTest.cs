using Mors.Ranges.Test.Support.RangeGeneration;
using NUnit.Framework;
using System;

namespace Mors.Ranges.Operations
{
    public sealed class ReferenceImplementationTest<TResult>
    {
        private readonly Func<IRange<int>, IRange<int>, TResult> _referenceOperation;
        private readonly Func<IRange<int>, IRange<int>, TResult> _testedOperation;

        public ReferenceImplementationTest(
            Func<IRange<int>, IRange<int>, TResult> referenceOperation,
            Func<IRange<int>, IRange<int>, TResult> testedOperation)
        {
            _referenceOperation = referenceOperation;
            _testedOperation = testedOperation;
        }

        public void Run(RangePair pairOfRanges)
        {
            Run(pairOfRanges.RangeA, pairOfRanges.RangeB);
        }

        public void Run(IRange<int> first, IRange<int> second)
        {
            var expected = _referenceOperation(first, second);
            var actual = _testedOperation(first, second);
            Assert.AreEqual(expected, actual);
        }
    }
}
