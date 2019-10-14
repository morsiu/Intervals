namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct Points : IPoints<Point>
    {
        public Point Maximum() => Point.Maximum;

        public Point Minimum() => Point.Minimum;

        public (bool HasValue, Point Value) Next(in Point value) => value.Next();

        public (bool HasValue, Point Value) Previous(in Point value) => value.Previous();
    }
}