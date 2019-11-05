namespace Mors.Ranges.Operations
{
    public interface IPoints<T>
    {
        T UnsafeNext(T current);

        T UnsafePrevious(T current);
    }
}