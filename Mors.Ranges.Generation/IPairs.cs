namespace Mors.Ranges.Generation
{
    public interface IPairs<TFirst, TSecond, TPair>
    {
        TPair Pair(TFirst first, TSecond second);
    }
}
