using NUnit.Framework;
using Walrus.Ranges;

namespace Walrus.Ranges.Test
{
    [TestFixture]
    public class RangeEqualityComparerTests
    {
        [Test]
        public void InstanceShouldReturnNonNullReference()
        {
            var reference = RangeEqualityComparer<int>.Instance;
            Assert.IsNotNull(reference);
        }
    }
}
