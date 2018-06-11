// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;

namespace Mors.Ranges.Test
{
    public class RangeTests
    {
        [TestFixture]
        public class AfterConstruction
        {
            private readonly int _start = 5;
            private readonly bool _hasOpenStart = false;
            private readonly int _end = 10;
            private readonly bool _hasOpenEnd = true;
            private Range<int> _range;

            [OneTimeSetUp]
            public void SetUp()
            {
                _range = new Range<int>(_start, _end, _hasOpenStart, _hasOpenEnd);
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
        }
    }
}
