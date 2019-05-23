using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class IntersectsWithOperation
    {
        private static readonly BoolCharacters BoolCharacters = new BoolCharacters('#', ' ');

        private static readonly StateTable<PairOfPointTypes, bool> States =
            new StateTableBuilder<char, char, char>()
            .AssumingHeader('-', '(', '[', '#', '=', ')', ']')
            .AppendRow('-', ' ', ' ', ' ', ' ', ' ', ' ', ' ')
            .AppendRow('(', ' ', ' ', ' ', ' ', ' ', ' ', ' ')
            .AppendRow('[', ' ', ' ', '#', '#', '#', ' ', '#')
            .AppendRow('#', ' ', ' ', '#', '#', '#', ' ', '#')
            .AppendRow('=', ' ', ' ', '#', '#', '#', ' ', '#')
            .AppendRow(')', ' ', ' ', ' ', ' ', ' ', ' ', ' ')
            .AppendRow(']', ' ', ' ', '#', '#', '#', ' ', '#')
            .Build(PointTypeCharacters.PairOfPointTypes, BoolCharacters.Bool);

        private readonly IEnumerable<bool> _result;

        public IntersectsWithOperation(IPointSequence first, IPointSequence second) =>
            _result =
                new ZipOfPointSequences<bool>(
                    new AlignedPointSequence(first, second),
                    new AlignedPointSequence(second, first),
                    States.Match);

        public bool Result() => _result.Any(x => x);
    }
}
