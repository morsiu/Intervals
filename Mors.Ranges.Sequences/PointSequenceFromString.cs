using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences
{
    public sealed class PointSequenceFromString : IPointSequence
    {
        private readonly string _string;

        public PointSequenceFromString(string @string, int start)
        {
            _string = @string;
            Start = start;
        }

        public int Start { get; }

        public int Length => _string.Length;

        public IEnumerator<PointType> GetEnumerator()
        {
            return _string
                .Cast<char>()
                .Select(PointTypeCharacters.PointType)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
