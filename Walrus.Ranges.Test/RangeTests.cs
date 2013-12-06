using NUnit.Framework;

namespace Walrus.Ranges.Test
{
    public class RangeTests
    {
        [TestFixture]
        public class AfterConstruction
        {
            private readonly int start = 5;
            private readonly bool hasOpenStart = false;
            private readonly int end = 10;
            private readonly bool hasOpenEnd = true;
            private Range<int> range;

            [TestFixtureSetUp]
            public void SetUp()
            {
                range = new Range<int>(start, end, hasOpenStart, hasOpenEnd);
            }

            [Test]
            public void ShouldNotBeEmpty()
            {
                Assert.IsFalse(range.IsEmpty);
            }

            [Test]
            public void ShouldHaveClosedStart()
            {
                Assert.IsFalse(range.HasOpenStart);
            }

            [Test]
            public void ShouldHaveOpenEnd()
            {
                Assert.IsTrue(range.HasOpenEnd);
            }

            [Test]
            public void ShouldHaveExpectedStart()
            {
                Assert.AreEqual(end, range.End);
            }

            [Test]
            public void ShouldHaveExpectedEnd()
            {
                Assert.AreEqual(start, range.Start);
            }
        }
    }
}
