namespace Mors.Ranges.Inequations
{
    public interface IOpenRange<T>
    {
        bool Empty();

        bool ClosedStart();

        bool ClosedEnd();

        T Start();

        T End();
    }
}
