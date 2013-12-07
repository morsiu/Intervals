using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.Ranges
{
    [TestFixture]
    public class RangeOperationsTests
    {
        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void IntersectsWithShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.IntersectsWith(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs")]
        public void IntersectsWithShouldReturnExpectedResult(RangePairTestCases.TestCase testCase)
        {
            Assert.AreEqual(
                testCase.AIntersectsWithB,
                RangeOperations.IntersectsWith(testCase.A, testCase.B));
        }
    }
}
