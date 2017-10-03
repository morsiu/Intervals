using NUnit.Framework;

namespace Mors.Axes.Test
{
    public abstract class ObjectEqualityTests
    {
        private readonly object _left;
        private readonly object _right;
        private readonly bool _leftEqualsRight;

        public ObjectEqualityTests(bool leftEqualsRight, object left, object right)
        {
            _left = left;
            _right = right;
            _leftEqualsRight = leftEqualsRight;
        }

        [Test]
        public void LeftEqualsRight()
        {
            Assert.That(Equals(_left, _right), Is.EqualTo(_leftEqualsRight), $"Left is <{_left}>, right is <{_right}>.");
        }

        [Test]
        public void RightEqualsLeft()
        {
            Assert.That(Equals(_right, _left), Is.EqualTo(_leftEqualsRight), $"Left is <{_right}>, right is <{_left}>.");
        }

        [Test]
        public void HashCodesAreEqualGivenValuesAreEqual()
        {
            if (_leftEqualsRight && _left != null && _right != null)
            {
                Assert.That(_left.GetHashCode(), Is.EqualTo(_right.GetHashCode()), $"Left is <{_left}>, right is <{_right}>.");
            }
            else
            {
                Assert.Pass();
            }
        }
    }
}
