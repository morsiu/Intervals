using Mors.Ranges.Test.Support.RangeOperations;
using NUnit.Framework;

namespace Mors.Axes.Test
{
    public static class AxisVectorTests
    {
        [Test]
        [TestCase(-10, true)]
        [TestCase(0, false)]
        [TestCase(10, false)]
        public static void LeftIsTrueForNegativeMangitudeAndDirection(int magnitudeAndDirection, bool expectedLeft)
        {
            Assert.That(new AxisVector(magnitudeAndDirection).Left(), Is.EqualTo(expectedLeft));
        }
    }
}
