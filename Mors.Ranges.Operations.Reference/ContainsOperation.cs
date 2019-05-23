using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class ContainsOperation
    {
        private readonly IntersectsWithOperation _result;

        public ContainsOperation(IPointSequence sequence, int point) =>
            _result =
                new IntersectsWithOperation(
                    sequence,
                    new PointSequenceFromRange(point, point, false, false));

        public bool Result() => _result.Result();
    }
}