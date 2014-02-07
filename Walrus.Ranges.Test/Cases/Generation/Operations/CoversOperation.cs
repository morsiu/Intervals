using Walrus.Ranges.Test.Cases.Generation.Operations.Parsers;
using Walrus.Ranges.Test.Cases.Generation.Operations.StateMachines;

namespace Walrus.Ranges.Test.Cases.Generation.Operations
{
    internal static class CoversOperation
    {
        private static readonly StateTable<PointTypePair, bool> _states =
            new StateTableBuilder<char, char, char>()
            .AssumingHeader('=', 'x', 'o', '-')
            .AppendRow('=', 'f', 'f', 'f', 'f')
            .AppendRow('x', 't', 'f', 'f', 'f')
            .AppendRow('o', 't', 't', 'f', 'f')
            .AppendRow('-', 't', 't', 't', 'f')
            .Build(PointTypeParser.ToPointPair, BoolParser.ToBool);

        public static bool Calculate(IRange<int> rangeA, IRange<int> rangeB)
        {
            if (rangeA.IsEmpty || rangeB.IsEmpty) return false;
            var isAnyPointUncovered = StateMachine.Any(rangeA, rangeB, output => output == true, _states);
            return !isAnyPointUncovered;
        }
    }
}
