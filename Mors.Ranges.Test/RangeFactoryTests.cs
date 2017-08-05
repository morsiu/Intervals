// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;
using System;

namespace Mors.Ranges
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

        [Test]
        [TestCase(5, 5)]
        [TestCase(10, 5)]
        public void OpenShouldThrowArgumentExceptionWhenStartIsGreaterThanOrEqualToEnd(int start, int end)
        {
            Assert.Throws<ArgumentException>(
                () => Range.Open(start, end));
        }

        [Test]
        [TestCase(5, 5)]
        [TestCase(10, 5)]
        public void LeftOpenShouldThrowArgumentExceptionWhenStartIsGreaterThanOrEqualToEnd(int start, int end)
        {
            Assert.Throws<ArgumentException>(
                () => Range.LeftOpen(start, end));
        }

        [Test]
        [TestCase(5, 5)]
        [TestCase(10, 5)]
        public void RightOpenShouldThrowArgumentExceptionWhenStartIsGreaterThanOrEqualToEnd(int start, int end)
        {
            Assert.Throws<ArgumentException>(
                () => Range.RightOpen(start, end));
        }

        [Test]
        [TestCase(5, 5)]
        [TestCase(10, 5)]
        public void LeftClosedShouldThrowArgumentExceptionWhenStartIsGreaterThanOrEqualToEnd(int start, int end)
        {
            Assert.Throws<ArgumentException>(
                () => Range.LeftClosed(start, end));
        }

        [Test]
        [TestCase(5, 5)]
        [TestCase(10, 5)]
        public void RightClosedShouldThrowArgumentExceptionWhenStartIsGreaterThanOrEqualToEnd(int start, int end)
        {
            Assert.Throws<ArgumentException>(
                () => Range.RightClosed(start, end));
        }

        [TestFixture]
        public class RangeCreatedWithEmpty
        {
            private IRange<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = EmptyRange<int>.Value;
            }

            [Test]
            public void ShouldBeEmpty()
            {
                Assert.IsTrue(_range.IsEmpty);
            }

            [Test]
            public void StartShouldThrowInvalidOperationException()
            {
                Assert.Throws<InvalidOperationException>(
                    () => { var unused = _range.Start; });
            }

            [Test]
            public void EndShouldThrowInvalidOperationException()
            {
                Assert.Throws<InvalidOperationException>(
                    () => { var unused = _range.End; });
            }

            [Test]
            public void HasOpenStartShouldThrowInvalidOperationException()
            {
                Assert.Throws<InvalidOperationException>(
                    () => { var unused = _range.HasOpenStart; });
            }

            [Test]
            public void HasOpenEndShouldThrowInvalidOperationException()
            {
                Assert.Throws<InvalidOperationException>(
                    () => { var unused = _range.HasOpenEnd; });
            }
        }

        [TestFixture]
        public class RangeCreatedWithClosed
        {
            private readonly int _start = 5;
            private readonly int _end = 10;
            private IRange<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = Range.Closed(_start, _end);
            }

            [Test]
            public void ShouldHaveExpectedStart()
            {
                Assert.AreEqual(_end, _range.End);
            }

            [Test]
            public void ShouldHaveExpectedEnd()
            {
                Assert.AreEqual(_start, _range.Start);
            }

            [Test]
            public void ShouldNotBeEmpty()
            {
                Assert.IsFalse(_range.IsEmpty);
            }

            [Test]
            public void ShouldHaveClosedStart()
            {
                Assert.IsFalse(_range.HasOpenStart);
            }

            [Test]
            public void ShouldHaveClosedEnd()
            {
                Assert.IsFalse(_range.HasOpenEnd);
            }
        }

        [TestFixture]
        public class RangeCreatedWithOpen
        {
            private readonly int _start = 5;
            private readonly int _end = 10;
            private IRange<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = Range.Open(_start, _end);
            }

            [Test]
            public void ShouldHaveExpectedStart()
            {
                Assert.AreEqual(_end, _range.End);
            }

            [Test]
            public void ShouldHaveExpectedEnd()
            {
                Assert.AreEqual(_start, _range.Start);
            }

            [Test]
            public void ShouldNotBeEmpty()
            {
                Assert.IsFalse(_range.IsEmpty);
            }

            [Test]
            public void ShouldHaveOpenStart()
            {
                Assert.IsTrue(_range.HasOpenStart);
            }

            [Test]
            public void ShouldHaveOpenEnd()
            {
                Assert.IsTrue(_range.HasOpenEnd);
            }
        }

        [TestFixture]
        public class RangeCreatedWithLeftOpen
        {
            private readonly int _start = 5;
            private readonly int _end = 10;
            private IRange<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = Range.LeftOpen(_start, _end);
            }

            [Test]
            public void ShouldHaveExpectedStart()
            {
                Assert.AreEqual(_end, _range.End);
            }

            [Test]
            public void ShouldHaveExpectedEnd()
            {
                Assert.AreEqual(_start, _range.Start);
            }

            [Test]
            public void ShouldNotBeEmpty()
            {
                Assert.IsFalse(_range.IsEmpty);
            }

            [Test]
            public void ShouldHaveOpenStart()
            {
                Assert.IsTrue(_range.HasOpenStart);
            }

            [Test]
            public void ShouldHaveClosedEnd()
            {
                Assert.IsFalse(_range.HasOpenEnd);
            }
        }

        [TestFixture]
        public class RangeCreatedWithPoint
        {
            private readonly int _point = 5;
            private IRange<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = Range.Point(_point);
            }

            [Test]
            public void ShouldHaveStartEqualToPoint()
            {
                Assert.AreEqual(_point, _range.End);
            }

            [Test]
            public void ShouldHaveEndEqualToPoint()
            {
                Assert.AreEqual(_point, _range.Start);
            }

            [Test]
            public void ShouldNotBeEmpty()
            {
                Assert.IsFalse(_range.IsEmpty);
            }

            [Test]
            public void ShouldHaveClosedStart()
            {
                Assert.IsFalse(_range.HasOpenStart);
            }

            [Test]
            public void ShouldHaveClosedEnd()
            {
                Assert.IsFalse(_range.HasOpenEnd);
            }
        }

        [TestFixture]
        public class RangeCreatedWithRightOpen
        {
            private readonly int _start = 5;
            private readonly int _end = 10;
            private IRange<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = Range.RightOpen(_start, _end);
            }

            [Test]
            public void ShouldHaveExpectedStart()
            {
                Assert.AreEqual(_end, _range.End);
            }

            [Test]
            public void ShouldHaveExpectedEnd()
            {
                Assert.AreEqual(_start, _range.Start);
            }

            [Test]
            public void ShouldNotBeEmpty()
            {
                Assert.IsFalse(_range.IsEmpty);
            }

            [Test]
            public void ShouldHaveClosedStart()
            {
                Assert.IsFalse(_range.HasOpenStart);
            }

            [Test]
            public void ShouldHaveOpenEnd()
            {
                Assert.IsTrue(_range.HasOpenEnd);
            }
        }

        [TestFixture]
        public class RangeCreatedWithLeftClosed
        {
            private readonly int _start = 5;
            private readonly int _end = 10;
            private IRange<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = Range.LeftClosed(_start, _end);
            }

            [Test]
            public void ShouldHaveExpectedStart()
            {
                Assert.AreEqual(_end, _range.End);
            }

            [Test]
            public void ShouldHaveExpectedEnd()
            {
                Assert.AreEqual(_start, _range.Start);
            }

            [Test]
            public void ShouldNotBeEmpty()
            {
                Assert.IsFalse(_range.IsEmpty);
            }

            [Test]
            public void ShouldHaveClosedStart()
            {
                Assert.IsFalse(_range.HasOpenStart);
            }

            [Test]
            public void ShouldHaveOpenEnd()
            {
                Assert.IsTrue(_range.HasOpenEnd);
            }
        }

        [TestFixture]
        public class RangeCreatedWithRightClosed
        {
            private readonly int _start = 5;
            private readonly int _end = 10;
            private IRange<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = Range.RightClosed(_start, _end);
            }

            [Test]
            public void ShouldHaveExpectedStart()
            {
                Assert.AreEqual(_end, _range.End);
            }

            [Test]
            public void ShouldHaveExpectedEnd()
            {
                Assert.AreEqual(_start, _range.Start);
            }

            [Test]
            public void ShouldNotBeEmpty()
            {
                Assert.IsFalse(_range.IsEmpty);
            }

            [Test]
            public void ShouldHaveOpenStart()
            {
                Assert.IsTrue(_range.HasOpenStart);
            }

            [Test]
            public void ShouldHaveClosedEnd()
            {
                Assert.IsFalse(_range.HasOpenEnd);
            }
        }
    }
}
