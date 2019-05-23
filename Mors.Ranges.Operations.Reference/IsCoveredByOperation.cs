using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class IsCoveredByOperation
    {
        private readonly CoversOperation _result;

        public IsCoveredByOperation(IPointSequence first, IPointSequence second) =>
            _result = new CoversOperation(second, first);

        public bool Result() => _result.Result();
    }
}
