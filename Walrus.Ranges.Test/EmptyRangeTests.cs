using NUnit.Framework;
using System;

namespace Walrus.Ranges.Test
{
    public class EmptyRangeTests
    {
        [TestFixture]
        public class AfterConstruction
        {
            private EmptyRange<int> emptyRange;

            [TestFixtureSetUp]
            public void SetUp()
            {
                emptyRange = new EmptyRange<int>();
            }

            [Test]
            public void ShouldBeEmpty()
            {
                Assert.IsTrue(emptyRange.IsEmpty);
            }

            [Test]
            public void StartShouldThrowInvalidOperationException()
            {
                Assert.Throws<InvalidOperationException>(
                    () => { var foo = emptyRange.Start; });
            }

            [Test]
            public void EndShouldThrowInvalidOperationException()
            {
                Assert.Throws<InvalidOperationException>(
                    () => { var foo = emptyRange.End; });
            }

            [Test]
            public void HasOpenStartShouldThrowInvalidOperationException()
            {
                Assert.Throws<InvalidOperationException>(
                    () => { var foo = emptyRange.HasOpenStart; });
            }

            [Test]
            public void HasOpenEndShouldThrowInvalidOperationException()
            {
                Assert.Throws<InvalidOperationException>(
                    () => { var foo = emptyRange.HasOpenEnd; });
            }
        }
    }
}
