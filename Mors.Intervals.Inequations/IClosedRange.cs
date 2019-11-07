namespace Mors.Intervals.Inequations
{
    public interface IClosedInterval<out T>
    {
        bool Empty();

        T Start();

        T End();
    }
}