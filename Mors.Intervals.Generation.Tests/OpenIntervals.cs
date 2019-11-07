namespace Mors.Intervals.Generation.Tests
{
    internal struct OpenIntervals : IOpenIntervals<int, OpenInterval>
    {
        public OpenInterval Empty() => new OpenInterval();

        public OpenInterval Interval(int start, int end, bool isStartOpen, bool isEndOpen) =>
            new OpenInterval(start, end, isStartOpen, isEndOpen);
    }

}
