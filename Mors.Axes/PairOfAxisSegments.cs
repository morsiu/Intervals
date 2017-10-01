namespace Mors.Ranges.Test.Support.RangeOperations
{
    public struct PairOfAxisSegments
    {
        private readonly IAxisSegment _left;
        private readonly IAxisSegment _right;

        public PairOfAxisSegments(IAxisSegment left, IAxisSegment right)
        {
            _left = left;
            _right = right;
        }

        public TResult Transform<TState, TResult>(IAxisOperation<TState, TResult> operation)
            where TState : IAxisOperationState<TState, TResult>
        {
            return
                new TransformationOfPairsOfAxisSegments<TState, TResult>(
                        new ZipOfAxisSegments(_left, _right),
                        operation)
                    .Result();
        }
    }
}