namespace Mors.Ranges
{
    internal static class RangeExtensions
    {
        public static Sequences.Range? SequencesRange(this IRange<int> range)
        {
            return range != null && !range.IsEmpty
                ? new Sequences.Range(range.Start, range.End, range.HasOpenStart, range.HasOpenEnd)
                : default(Sequences.Range?);
        }

        public static IRange<int> Range(this Sequences.Range? range)
        {
            return range is Sequences.Range x
                ? Ranges.Range.Create(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd)
                : Ranges.Range.Empty<int>();
        }
    }
}