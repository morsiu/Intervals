using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.Ranges.Test
{
    // Tests static Range class.
    [TestFixture]
    public class RangeFactoryTests
    {
        [Test]
        public void ClosedShouldThrowArgumentExceptionWhenStartIsGreaterThanEnd()
        {
            Assert.Throws<ArgumentException>(
                () => Range.Closed(10, 5));
        }

        [TestFixture]
        public class RangeCreatedWithClosed
        {
            private readonly int start = 5;
            private readonly int end = 10;
            private IRange<int> range;

            [TestFixtureSetUp]
            public void SetUp()
            {
                range = Range.Closed(start, end);
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
            public void ShouldHaveClosedEnd()
            {
                Assert.IsFalse(range.HasOpenEnd);
            }
        }
    }
}
