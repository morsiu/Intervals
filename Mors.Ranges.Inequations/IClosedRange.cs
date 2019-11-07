namespace Mors.Ranges.Inequations
{
    public interface IClosedRange<out T>
    {
        bool Empty();

        T Start();

        T End();
    }
}