namespace Mors.Ranges.Inequations
{
    public interface IOpenRange<out T>
    {
        bool Empty();

        bool ClosedStart();

        bool ClosedEnd();

        T Start();

        T End();
    }
}
