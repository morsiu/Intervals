﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CsCheck;
using Mors.Intervals.Operations.Reference;
using NUnit.Framework;

namespace Mors.Intervals.Operations.Test
{
    [TestFixture]
    public sealed class TestsOfOperationsOnClosedIntervalUnions
    {
        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalUnionsOfAllPossibleRelations))]
        [TestCaseSource(nameof(TestCasesForUnion))]
        public void UnionReturnsExpectedResult(
            (ClosedIntervalUnion IntervalUnionA, ClosedIntervalUnion IntervalUnionB) pairOfIntervalUnions)
        {
            var expected =
                ReferenceClosedIntervalUnionOperations<
                        ClosedInterval,
                        ClosedIntervalUnion,
                        ClosedIntervals,
                        ClosedIntervalUnions>
                    .Union(pairOfIntervalUnions.IntervalUnionA, pairOfIntervalUnions.IntervalUnionB);
            var actual =
                ClosedIntervalUnionOperations.Union<
                        ClosedIntervalUnion,
                        IEnumerator<ClosedInterval>,
                        ClosedInterval,
                        int,
                        Integers,
                        ClosedIntervalUnionBuilder,
                        ClosedIntervals>(
                    pairOfIntervalUnions.IntervalUnionA,
                    pairOfIntervalUnions.IntervalUnionB);
            Assert.That(actual, Is.EqualTo(expected), $"Expected {actual} to be {expected}");
        }

        [Test]
        public void UnionReturnsExpectedResultGivenGeneratedIntervalUnions()
        {
            new GeneratedPairsOfClosedIntervalUnions(
                    GeneratedPairsOfClosedIntervalUnions.Algorithm.TreeOfAllCombinations)
                .Value()
                .Sample(UnionReturnsExpectedResult);
            new GeneratedPairsOfClosedIntervalUnions(
                    GeneratedPairsOfClosedIntervalUnions.Algorithm.RandomIntervals)
                .Value()
                .Sample(UnionReturnsExpectedResult);
        }

        private static IEnumerable<(ClosedIntervalUnion, ClosedIntervalUnion)> TestCasesForUnion()
        {
            foreach (var (first, second) in All())
            {
                yield return (first, second);
                yield return (second, first);
            }

            static IEnumerable<(ClosedIntervalUnion, ClosedIntervalUnion)> All()
            {
                yield return Pair([], []);
                yield return Pair([(1, 3), (5, 7), (9, 11)], []);
                yield return Pair([(1, 3), (5, 7)], [(9, 11), (13, 15)]);

                yield return Pair([(1, 3), (9, 11)], [(5, 7), (13, 15)]);
                yield return Pair([(5, 7), (13, 15)], [(1, 3), (9, 11)]);

                yield return Pair([(3, 8)], [(1, 5)]);

                yield return Pair([(1, 3), (5, 7)], [(2, 6)]);
                yield return Pair([(1, 3), (5, 9)], [(2, 6), (8, 10)]);
                yield return Pair([(1, 3), (5, 9), (11, 15)], [(2, 6), (8, 12), (14, 16)]);

                yield return Pair([(1, 13)], [(2, 4), (6, 8), (10, 12)]);

                yield return Pair([(1, 3)], [(4, 6)]);
                yield return Pair([(1, 3), (7, 9)], [(4, 6), (11, 13)]);
                yield return Pair([(1, 3), (7, 9), (14, 16)], [(4, 6), (11, 13), (17, 19)]);

                static (ClosedIntervalUnion, ClosedIntervalUnion) Pair(
                    (int Start, int End)[] first,
                    (int Start, int End)[] second)
                {
                    return (Union(first), Union(second));

                    static ClosedIntervalUnion Union((int Start, int End)[] intervals)
                    {
                        return new ClosedIntervalUnion(
                            intervals.Select(x => new ClosedInterval(x.Start, x.End)).ToImmutableArray());
                    }
                }
            }
        }
    }
}
