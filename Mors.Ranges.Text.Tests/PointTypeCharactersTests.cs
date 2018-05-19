// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;
using System;

namespace Mors.Ranges.Text
{
    [TestFixture]
    internal class PointTypeCharactersTests
    {
        [Test]
        [TestCase('#', '#', '3', '4')]
        [TestCase('#', '2', '#', '4')]
        [TestCase('#', '2', '3', '#')]
        [TestCase('1', '#', '#', '4')]
        [TestCase('1', '#', '3', '#')]
        [TestCase('1', '2', '#', '#')]
        [TestCase('#', '#', '#', '4')]
        [TestCase('#', '#', '3', '#')]
        [TestCase('#', '2', '#', '#')]
        [TestCase('1', '#', '#', '#')]
        [TestCase('#', '#', '#', '#')]
        public void ConstructorShouldThrowArgumentExceptionGivenIdenticalCharactersForDifferentPointTypes(
            char uncoveredPoint,
            char coveredPoint,
            char closedEndPoint,
            char openEndPoint)
        {
            Assert.Throws<ArgumentException>(
                () => { var unused = new PointTypeCharacters(uncoveredPoint, coveredPoint, closedEndPoint, openEndPoint); });
        }

        [Test]
        [TestCase('a', ExpectedResult = PointType.Uncovered)]
        [TestCase('b', ExpectedResult = PointType.Covered)]
        [TestCase('c', ExpectedResult = PointType.ClosedEnd)]
        [TestCase('d', ExpectedResult = PointType.OpenEnd)]
        public PointType? PointTypeShouldReturnCorrespondingPointTypeGivenCharacterWithAssignedPointType(char character)
        {
            var characters = new PointTypeCharacters('a', 'b', 'c', 'd');
            return characters.MaybePointType(character);
        }

        [Test]
        public void PointTypeShouldReturnNullGivenCharacterWithoutAssignedPointType()
        {
            var characters = new PointTypeCharacters('a', 'b', 'c', 'd');
            Assert.AreEqual(
                default(PointType?),
                characters.MaybePointType('#'));
        }

        [Test]
        // This test is for character to point type lookup array used inside matcher,
        // which assumes specific integer values for point types.
        [TestCase(PointType.Uncovered, ExpectedResult = 0)]
        [TestCase(PointType.Covered, ExpectedResult = 1)]
        [TestCase(PointType.ClosedEnd, ExpectedResult = 2)]
        [TestCase(PointType.OpenEnd, ExpectedResult = 3)]
        public int PointTypeShouldHaveSpecificIntegerValue(PointType type)
        {
            return (int)type;
        }
    }
}
