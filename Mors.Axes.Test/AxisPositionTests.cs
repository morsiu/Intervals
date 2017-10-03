using Mors.Ranges.Test.Support.RangeOperations;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mors.Axes.Test
{
    public static class AxisPositionTests
    {
        [TestFixtureSource(typeof(AxisPositionTests), nameof(EqualityTestFixtures))]
        public sealed class ObjectEquality : ObjectEqualityTests
        {
            public ObjectEquality(bool leftEqualsRight, AxisPosition left, AxisPosition right) : base(leftEqualsRight, left, right)
            {
            }
        }

        public static IEnumerable<TestFixtureData> EqualityTestFixtures()
        {
            yield return new TestFixtureData(true, new AxisPosition(), new AxisPosition());
            yield return new TestFixtureData(true, new AxisPosition(-1), new AxisPosition(-1));
            yield return new TestFixtureData(true, new AxisPosition(1), new AxisPosition(1));
            yield return new TestFixtureData(false, new AxisPosition(-1), new AxisPosition(1));
            yield return new TestFixtureData(false, new AxisPosition(1), new AxisPosition(0));
            yield return new TestFixtureData(false, new AxisPosition(-1), new AxisPosition(0));
            yield return new TestFixtureData(false, new AxisPosition(1), new AxisPosition(2));
            yield return new TestFixtureData(false, new AxisPosition(-1), new AxisPosition(-2));
        }
    }
}
