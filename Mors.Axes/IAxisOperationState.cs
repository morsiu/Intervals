namespace Mors.Ranges.Test.Support.RangeOperations
{
    public interface IAxisOperationState<TSelf, TResult>
    {
        TSelf Next(AxisPoint left, AxisPoint right);

        TResult Result();
    }
}