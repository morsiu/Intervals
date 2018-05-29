namespace Mors.Ranges.Sequences
{
    public readonly struct Element<TValue>
    {
        public Element(TValue value, Position position)
        {
            Value = value;
            Position = position;
        }

        public TValue Value { get; }
        public Position Position { get; }

        public Element<TValue> Next(TValue input) =>
            new Element<TValue>(input, Position.Next());
    }
}