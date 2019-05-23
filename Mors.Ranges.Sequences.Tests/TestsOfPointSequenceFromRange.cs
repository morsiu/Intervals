using System.Collections.Generic;
using NUnit.Framework;

namespace Mors.Ranges.Sequences.Tests
{
    public class TestsOfPointSequenceFromRange
    {
        public static IEnumerable<TestCaseData> EnumerationTestCases()
        {
            yield return new TestCaseData((0, 0, true, true)).Returns(new TestablePointSequence("-", 0));
            yield return new TestCaseData((0, 0, false, true)).Returns(new TestablePointSequence("-", 0));
            yield return new TestCaseData((0, 0, true, false)).Returns(new TestablePointSequence("-", 0));
            yield return new TestCaseData((0, 0, false, false)).Returns(new TestablePointSequence("#", 0));
            yield return new TestCaseData((0, 1, false, false)).Returns(new TestablePointSequence("[]", 0));
            yield return new TestCaseData((0, 1, true, false)).Returns(new TestablePointSequence("(]", 0));
            yield return new TestCaseData((0, 1, false, true)).Returns(new TestablePointSequence("[)", 0));
            yield return new TestCaseData((0, 1, true, true)).Returns(new TestablePointSequence("()", 0));
            yield return new TestCaseData((0, 2, false, false)).Returns(new TestablePointSequence("[=]", 0));
            yield return new TestCaseData((0, 2, true, false)).Returns(new TestablePointSequence("(=]", 0));
            yield return new TestCaseData((0, 2, false, true)).Returns(new TestablePointSequence("[=)", 0));
            yield return new TestCaseData((0, 2, true, true)).Returns(new TestablePointSequence("(=)", 0));
        }

        [Test]
        [TestCaseSource(nameof(EnumerationTestCases))]
        public TestablePointSequence EnumerationTest(in (int Start, int End, bool StartOpen, bool EndOpen) input)
        {
            return new TestablePointSequence(
                new PointSequenceFromRange(input.Start, input.End, input.StartOpen, input.EndOpen));
        }
    }
}
