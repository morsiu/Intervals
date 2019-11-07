namespace Mors.Intervals.Operations.Test
{
    internal readonly struct OpenIntervals
        : IOpenIntervals<int, OpenInterval>,
        Reference.IOpenIntervals<int, OpenInterval>,
        IEmptyIntervals<OpenInterval>,
        Reference.IEmptyIntervals<OpenInterval>,
        Generation.IOpenIntervals<int, OpenInterval>
    {
        public OpenInterval Empty() => new OpenInterval();

        public OpenInterval Interval(int start, int end, bool isStartOpen, bool isEndOpen) =>
            new OpenInterval(start, end, isStartOpen, isEndOpen);
    }
}
