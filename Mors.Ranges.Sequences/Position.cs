namespace Mors.Ranges.Sequences
{
    public readonly struct Position
    {
        private readonly int _position;
        public Position(int position) => _position = position;
        public Position Next() => new Position(_position + 1);
        public override string ToString() => _position.ToString();
        public static implicit operator int(Position x) => x._position;
    }
}