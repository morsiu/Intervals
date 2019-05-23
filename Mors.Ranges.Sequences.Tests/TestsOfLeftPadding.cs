using NUnit.Framework;

namespace Mors.Ranges.Sequences.Tests
{
    public class TestsOfLeftPadding
    {
        [Test]
        [TestCase(-1, 0, ExpectedResult = 0)]
        [TestCase(-1, 1, ExpectedResult = 0)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, 1, ExpectedResult = 0)]
        [TestCase(1, -1, ExpectedResult = 2)]
        [TestCase(1, 0, ExpectedResult = 1)]
        public int LengthTest(int mainStart, int otherStart)
        {
            return new LeftPadding(mainStart, otherStart).Length;
        }
    }
}
