using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    internal readonly struct Integers : IPoints<int>
    {
        public int Maximum() => int.MaxValue;

        public int Minimum() => int.MinValue;

        public (bool HasValue, int Value) Next(in int value) =>
            value == int.MaxValue
                ? (false, default)
                : (true, value + 1);

        public (bool HasValue, int Value) Previous(in int value) =>
            value == int.MinValue
                ? (false, default)
                : (true, value - 1);
    }
}
