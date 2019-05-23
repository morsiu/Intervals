using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Mors.Ranges.Sequences.Tests
{
    public class TestsOfRangesInPointSequence
    {
        [Test]
        [TestCaseSource(nameof(ValidStrings))]
        public IEnumerable<Range> ShouldReturnExpectedRangeForValidString(string @string)
        {
            return new RangesInPointSequence(new TestablePointSequence(@string, 1));
        }

        [Test]
        [TestCaseSource(nameof(StringsWithUnexpectedInput))]
        public void ShouldThrowExceptionForUnexpectedInput(string @string)
        {
            var ranges = new RangesInPointSequence(new TestablePointSequence(@string, 1));
            Assert.Throws<UnexpectedInputException>(
                () => ranges.LastOrDefault());
        }

        [Test]
        [TestCaseSource(nameof(StringsWithUnexpectedEndOfInput))]
        public void ShouldThrowExceptionForUnexpectedEndOfInput(string @string)
        {
            var ranges = new RangesInPointSequence(new TestablePointSequence(@string, 1));
            Assert.Throws<UnexpectedEndOfInputException>(
                () => ranges.LastOrDefault());
        }

        public static IEnumerable<TestCaseData> ValidStrings()
        {
            yield return new TestCaseData("").Returns(NoRanges());
            yield return new TestCaseData("-").Returns(NoRanges());

            yield return new TestCaseData("#").Returns(Ranges(Closed(1, 1)));
            yield return new TestCaseData("-#").Returns(Ranges(Closed(2, 2)));
            yield return new TestCaseData("#-").Returns(Ranges(Closed(1, 1)));
            yield return new TestCaseData("-#-").Returns(Ranges(Closed(2, 2)));
            yield return new TestCaseData("[]").Returns(Ranges(Closed(1, 2)));
            yield return new TestCaseData("-[]").Returns(Ranges(Closed(2, 3)));
            yield return new TestCaseData("[]-").Returns(Ranges(Closed(1, 2)));
            yield return new TestCaseData("-[]-").Returns(Ranges(Closed(2, 3)));
            yield return new TestCaseData("[=]").Returns(Ranges(Closed(1, 3)));
            yield return new TestCaseData("-[=]").Returns(Ranges(Closed(2, 4)));
            yield return new TestCaseData("[=]-").Returns(Ranges(Closed(1, 3)));
            yield return new TestCaseData("-[=]-").Returns(Ranges(Closed(2, 4)));

            yield return new TestCaseData("()").Returns(Ranges(Open(1, 2)));
            yield return new TestCaseData("-()").Returns(Ranges(Open(2, 3)));
            yield return new TestCaseData("()-").Returns(Ranges(Open(1, 2)));
            yield return new TestCaseData("-()-").Returns(Ranges(Open(2, 3)));
            yield return new TestCaseData("(=)").Returns(Ranges(Open(1, 3)));
            yield return new TestCaseData("-(=)").Returns(Ranges(Open(2, 4)));
            yield return new TestCaseData("(=)-").Returns(Ranges(Open(1, 3)));
            yield return new TestCaseData("-(=)-").Returns(Ranges(Open(2, 4)));

            yield return new TestCaseData("[)").Returns(Ranges(RightOpen(1, 2)));
            yield return new TestCaseData("-[)").Returns(Ranges(RightOpen(2, 3)));
            yield return new TestCaseData("[)-").Returns(Ranges(RightOpen(1, 2)));
            yield return new TestCaseData("-[)-").Returns(Ranges(RightOpen(2, 3)));
            yield return new TestCaseData("[=)").Returns(Ranges(RightOpen(1, 3)));
            yield return new TestCaseData("-[=)").Returns(Ranges(RightOpen(2, 4)));
            yield return new TestCaseData("[=)-").Returns(Ranges(RightOpen(1, 3)));
            yield return new TestCaseData("-[=)-").Returns(Ranges(RightOpen(2, 4)));

            yield return new TestCaseData("(]").Returns(Ranges(LeftOpen(1, 2)));
            yield return new TestCaseData("-(]").Returns(Ranges(LeftOpen(2, 3)));
            yield return new TestCaseData("(]-").Returns(Ranges(LeftOpen(1, 2)));
            yield return new TestCaseData("-(]-").Returns(Ranges(LeftOpen(2, 3)));
            yield return new TestCaseData("(=]").Returns(Ranges(LeftOpen(1, 3)));
            yield return new TestCaseData("-(=]").Returns(Ranges(LeftOpen(2, 4)));
            yield return new TestCaseData("(=]-").Returns(Ranges(LeftOpen(1, 3)));
            yield return new TestCaseData("-(=]-").Returns(Ranges(LeftOpen(2, 4)));
            
            yield return new TestCaseData("#(]").Returns(Ranges(Closed(1, 1), LeftOpen(2, 3)));
            yield return new TestCaseData("[)#").Returns(Ranges(RightOpen(1, 2), Closed(3, 3)));

            IEnumerable<Range> NoRanges() => Enumerable.Empty<Range>();
            IEnumerable<Range> Ranges(params Range[] ranges) => ranges;
            Range Open(int start, int end) => new Range(start, end, true, true);
            Range Closed(int start, int end) => new Range(start, end, false, false);
            Range LeftOpen(int start, int end) => new Range(start, end, true, false);
            Range RightOpen(int start, int end) => new Range(start, end, false, true);
        }

        public static IEnumerable<string> StringsWithUnexpectedInput()
        {
            yield return "=";
            yield return ")";
            yield return "]";
            yield return "(#";
            yield return "[#";
            yield return "(-";
            yield return "((";
            yield return "##";
            yield return "#[]";
            yield return "[]#";
        }

        public static IEnumerable<string> StringsWithUnexpectedEndOfInput()
        {
            yield return "[";
            yield return "(";
            yield return "[=";
            yield return "(=";
        }
    }
}
