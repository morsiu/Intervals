// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Walrus.Ranges
{
    [TestFixture]
    public class RangeBuilderTests
    {
        [Test]
        public void BuildAfterConstructionShouldReturnEmptyRange()
        {
            var builder = new RangeBuilder<int>();
        }

        [Test]
        public void BuildAfterSettingStartShouldThrowException()
        {
            var builder = new RangeBuilder<int>();
            builder.SetStart(5, false);
            Assert.Throws<InvalidOperationException>(
                () => builder.Build());
        }

        [Test]
        public void BuildAfterSettingEndShouldThrowException()
        {
            var builder = new RangeBuilder<int>();
            builder.SetEnd(5, false);
            Assert.Throws<InvalidOperationException>(
                () => builder.Build());
        }

        [Test]
        public void BuildAfterSettingStartAndEndShouldReturnRangeWithSetValues()
        {
            var builder = new RangeBuilder<int>();
            builder.SetStart(5, false);
            builder.SetEnd(10, false);
            var range = builder.Build();
            Assert.AreEqual(Range.Closed(5, 10), range);
        }

        [Test]
        public void BuildAfterClearShouldReturnEmptyRange()
        {
            var builder = new RangeBuilder<int>();
            builder.Clear();
            var range = builder.Build();
            Assert.AreEqual(Range.Empty<int>(), range);
        }

        [Test]
        public void BuildAfterSetStartThenClearShouldReturnEmptyRange()
        {
            var builder = new RangeBuilder<int>();
            builder.SetStart(5, true);
            builder.Clear();
            var range = builder.Build();
            Assert.AreEqual(Range.Empty<int>(), range);
        }

        [Test]
        public void BuildAfterSetEndThenClearShouldReturnEmptyRange()
        {
            var builder = new RangeBuilder<int>();
            builder.SetEnd(5, true);
            builder.Clear();
            var range = builder.Build();
            Assert.AreEqual(Range.Empty<int>(), range);
        }
    }
}
