namespace Mors.Ranges.Operations
{
    public readonly struct Integers : IPoints<int>
    {
        public bool Next(int current, out int next)
        {
            next = current + 1;
            return current != int.MaxValue;
        }

        public bool Previous(int current, out int previous)
        {
            previous = current - 1;
            return current != int.MinValue;
        }
    }
}
