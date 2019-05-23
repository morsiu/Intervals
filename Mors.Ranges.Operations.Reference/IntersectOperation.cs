using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class IntersectOperation : IPointSequence
    {
        private static readonly StateTable<PairOfPointTypes, PointType> States =
            new StateTableBuilder<char, char, char>()
            .AssumingHeader('-', '(', '[', '#', '=', ')', ']')
            .AppendRow('-', '-', '-', '-', '-', '-', '-', '-')
            .AppendRow('(', '-', '(', '(', '(', '(', '-', '-')
            .AppendRow('[', '-', '(', '[', '[', '[', '-', '#')
            .AppendRow('#', '-', '(', '[', '#', '=', ')', ']')
            .AppendRow('=', '-', '(', '[', '=', '=', ')', ']')
            .AppendRow(')', '-', '-', '-', ')', ')', ')', ')')
            .AppendRow(']', '-', '-', '#', ']', ']', ')', ']')
            .Build(PointTypeCharacters.PairOfPointTypes, PointTypeCharacters.PointType);

        private readonly ZippedPointSequence _result;

        public IntersectOperation(IPointSequence first, IPointSequence second) =>
            _result =
                new ZippedPointSequence(
                    new AlignedPointSequence(first, second),
                    new AlignedPointSequence(second, first),
                    States.Match);

        public IEnumerator<PointType> GetEnumerator() => _result.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Start => _result.Start;
        public int Length => _result.Length;
    }
}
