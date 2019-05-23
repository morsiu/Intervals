using NUnit.Framework;

namespace Mors.Ranges.Sequences.Tests
{
    public class TestsOfRightPadding
    {
        [Test]
        [TestCase(0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(0, 0, 0, 1, ExpectedResult = 1)]
        [TestCase(0, 1, 0, 0, ExpectedResult = 0)]
        [TestCase(1, 1, 0, 0, ExpectedResult = 0)]
        [TestCase(1, 1, 1, 0, ExpectedResult = 0)]
        [TestCase(0, 1, 0, 1, ExpectedResult = 0)]
        [TestCase(0, 2, 1, 2, ExpectedResult = 1)]
        [TestCase(0, 2, 1, 3, ExpectedResult = 2)]
        public int LengthTest(int mainStart, int mainLength, int otherStart, int otherLength)
        {
            return new RightPadding(mainStart, mainLength, otherStart, otherLength).Length;
        }
    }
}
