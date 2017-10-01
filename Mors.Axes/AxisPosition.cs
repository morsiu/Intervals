namespace Mors.Ranges.Test.Support.RangeOperations
{
    public struct AxisPosition
    {
        private readonly int _startPosition;

        public AxisPosition(int startPosition)
            => _startPosition = startPosition;

        public AxisVector Subtract(AxisPosition other)
            => new AxisVector(_startPosition - other._startPosition);

        public AxisPosition Add(AxisVector other)
        {
            var otherMagnitude = other.Distance().Magnitude();
            return new AxisPosition(
                _startPosition + (other.Left() ? -otherMagnitude : otherMagnitude));
        }
    }
}