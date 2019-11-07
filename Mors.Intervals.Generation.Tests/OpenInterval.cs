namespace Mors.Intervals.Generation.Tests
{
    internal readonly struct OpenInterval
    {
        private readonly Point _start;
        private readonly Point _end;
        private readonly bool _nonEmpty;

        public OpenInterval(int start, int end, bool isStartOpen, bool isEndOpen)
        {
            _nonEmpty = true;
            _start = new Point(isStartOpen, start);
            _end = new Point(isEndOpen, end);
        }

        public override string ToString() =>
            _nonEmpty
                ? $"{_start.ToString(isStartPoint: true)}, {_end.ToString(isStartPoint: false)}"
                : "∅";

        private readonly struct Point
        {
            public Point(bool isOpen, int value) { _isOpen = isOpen; _value = value; }
            private readonly bool _isOpen;
            private readonly int _value;
            public string ToString(bool isStartPoint) =>
                isStartPoint
                    ? $"{(_isOpen ? '(' : '[')}{_value}"
                    : $"{_value}{(_isOpen ? ')' : ']')}";
        }
    }
}
