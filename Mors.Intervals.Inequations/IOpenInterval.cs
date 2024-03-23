namespace Mors.Intervals.Inequations
{
    public interface IOpenInterval<out T>
    {
        bool Empty();

        bool ClosedStart();

        bool ClosedEnd();

        T Start();

        T End();
    }
}
