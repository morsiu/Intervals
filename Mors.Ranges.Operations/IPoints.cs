namespace Mors.Ranges.Operations
{
    public interface IPoints<T>
    {
        bool Next(T current, out T next);

        bool Previous(T current, out T previous);
    }
}