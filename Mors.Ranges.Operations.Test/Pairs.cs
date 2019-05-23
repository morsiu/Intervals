using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    internal readonly struct Pairs
        : IPairs<ClosedRange, ClosedRange, (ClosedRange, ClosedRange)>,
        IPairs<OpenRange, OpenRange, (OpenRange, OpenRange)>,
        IPairs<ClosedRange, int, (ClosedRange, int)>,
        IPairs<OpenRange, int, (OpenRange, int)>
    {
        public (OpenRange, OpenRange) Pair(OpenRange first, OpenRange second) => (first, second);

        public (ClosedRange, ClosedRange) Pair(ClosedRange first, ClosedRange second) => (first, second);

        public (OpenRange, int) Pair(OpenRange first, int second) => (first, second);

        public (ClosedRange, int) Pair(ClosedRange first, int second) => (first, second);
    }
}
