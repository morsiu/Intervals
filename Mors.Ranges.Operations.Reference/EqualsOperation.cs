using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class EqualsOperation
    {
        private readonly IPointSequence _first;
        private readonly IPointSequence _second;

        public EqualsOperation(IPointSequence first, IPointSequence second)
        {
            _first = new AlignedPointSequence(first, second);
            _second = new AlignedPointSequence(second, first);
        }

        public bool Result() =>
            new PointSequenceEqualityComparer().Equals(_first, _second);
    }
}
