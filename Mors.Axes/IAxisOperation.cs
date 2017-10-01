namespace Mors.Ranges.Test.Support.RangeOperations
{
    public interface IAxisOperation<TState, TResult>
            where TState : IAxisOperationState<TState, TResult>
    {
        TState Start();
    }
}