namespace Mors.Intervals.Generation
{
    public interface IPairs<in TFirst, in TSecond, out TPair>
    {
        TPair Pair(TFirst first, TSecond second);
    }
}
