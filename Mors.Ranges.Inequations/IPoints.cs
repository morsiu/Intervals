namespace Mors.Ranges.Inequations
{
    public interface IPoints<T>
    {
        T Minimum();

        T Maximum();

        (bool HasValue, T Value) Next(in T value);

        (bool HasValue, T Value) Previous(in T value);
    }
}
