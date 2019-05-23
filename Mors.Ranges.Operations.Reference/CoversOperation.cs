using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class CoversOperation
    {
        private readonly IPointSequence _first;
        private readonly IPointSequence _second;

        public CoversOperation(IPointSequence first, IPointSequence second)
        {
            _first = new AlignedPointSequence(first, second);
            _second = new AlignedPointSequence(second, first);
        }

        public bool Result() =>
            !new IsEmptyOperation(_first).Result()
            && !new IsEmptyOperation(_second).Result()
            && new EqualsOperation(
                    new IntersectOperation(_first, _second),
                    _second)
                .Result();
    }
}
