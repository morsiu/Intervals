using NUnit.Framework;

namespace Mors.Ranges.Sequences.Tests
{
    [TestFixture]
    internal class TestsOfPointTypeCharacters
    {
        [Test]
        [TestCase('-', ExpectedResult = PointType.Outside)]
        [TestCase('(', ExpectedResult = PointType.OpenStart)]
        [TestCase('[', ExpectedResult = PointType.ClosedStart)]
        [TestCase('#', ExpectedResult = PointType.ClosedStartAndEnd)]
        [TestCase('=', ExpectedResult = PointType.Inside)]
        [TestCase(')', ExpectedResult = PointType.OpenEnd)]
        [TestCase(']', ExpectedResult = PointType.ClosedEnd)]
        public PointType? PointTypeShouldReturnCorrespondingPointTypeGivenCharacterWithAssignedPointType(char pointType)
        {
            return PointTypeCharacters.MaybePointType(pointType);
        }

        [Test]
        public void PointTypeShouldReturnNullGivenCharacterWithoutAssignedPointType()
        {
            Assert.AreEqual(
                default(PointType?),
                PointTypeCharacters.MaybePointType('_'));
        }
    }
}
