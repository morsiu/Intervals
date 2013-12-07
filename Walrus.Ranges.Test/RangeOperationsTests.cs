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

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void IntersectShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.Intersect(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs")]
        public void IntersectShouldReturnExpectedResult(RangePairTestCases.TestCase testCase)
        {
            Assert.AreEqual(
                testCase.ABIntersection,
                RangeOperations.Intersect(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void CoversShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.Covers(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs")]
        public void CoversShouldReturnExpectedResult(RangePairTestCases.TestCase testCase)
        {
            Assert.AreEqual(
                testCase.ACoversB,
                RangeOperations.Covers(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void IsCoveredByShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.IsCoveredBy(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs")]
        public void IsCoveredByShouldReturnExpectedResult(RangePairTestCases.TestCase testCase)
        {
            Assert.AreEqual(
                testCase.BCoversA,
                RangeOperations.IsCoveredBy(testCase.A, testCase.B));
        }
    }
}
