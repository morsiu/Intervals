using System.Linq;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class IsEmptyOperation
    {
        private readonly IPointSequence _pointSequence;

        public IsEmptyOperation(IPointSequence pointSequence) => _pointSequence = pointSequence;

        public bool Result() => _pointSequence.All(x => x == PointType.Outside);
    }
}