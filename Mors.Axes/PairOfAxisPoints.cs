namespace Mors.Ranges.Test.Support.RangeOperations
{
    public struct PairOfAxisPoints
    {
        private readonly AxisPoint _left;
        private readonly AxisPoint _right;

        public PairOfAxisPoints(AxisPoint left, AxisPoint right)
        {
            _left = left;
            _right = right;
        }

        public TState NextState<TState, TResult>(TState state)
            where TState : IAxisOperationState<TState, TResult>
        {
            return state.Next(_left, _right);
        }
    }
}