namespace Mors.Ranges.Inequations
{
    public interface IClosedRange<T>
    {
        bool Empty();

        T Start();

        T End();
    }
}