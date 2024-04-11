using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using CsCheck;

namespace Mors.Intervals.Operations.Test;

internal sealed class GeneratedPairsOfClosedIntervalUnions
{
    private readonly Algorithm _algorithm;

    public GeneratedPairsOfClosedIntervalUnions(Algorithm algorithm)
    {
        _algorithm = algorithm;
    }

    public Gen<(ClosedIntervalUnion, ClosedIntervalUnion)> Value()
    {
        return _algorithm switch
        {
            Algorithm.RandomIntervals => RandomIntervals(),
            Algorithm.TreeOfAllCombinations => TreeOfAllCombinations(),
        };
    }

    private static Gen<(ClosedIntervalUnion, ClosedIntervalUnion)> RandomIntervals()
    {
        var intervalUnion =
            Gen.UInt16.SelectMany(
                maxDepth =>
                Gen.Recursive<LinkedList<ClosedInterval>>(
                    (depth, list) =>
                    {
                        return depth == maxDepth
                            ? from start in Gen.UInt64
                              from length in Gen.UInt64
                              select new LinkedList<ClosedInterval>(
                                  new ClosedInterval((int)start, (int)start + (int)length),
                                  null)
                            : list.SelectMany(x =>
                                from space in Gen.UInt64
                                from length in Gen.UInt64
                                let start = x.Value.End + (int)space + 2
                                let end = start + (int)length
                                select new LinkedList<ClosedInterval>(
                                    new ClosedInterval(start, end),
                                    x));
                    }))
            .Select(
                x =>
                {
                    var result = new ClosedIntervalUnionBuilder();
                    var intervals = x.ToEnumerable().Reverse();
                    foreach (var interval in intervals)
                    {
                        result.Append(interval);
                    }
                    return result.Build();
                });
        return intervalUnion.SelectMany(x => intervalUnion.Select(y => (x, y)));
    }

    private static Gen<(ClosedIntervalUnion, ClosedIntervalUnion)> TreeOfAllCombinations()
    {
        return GeneratedCollection(States.None, InstructionAndNextState)
            .Select(ToIntervalUnionPair);

        static Gen<(Instruction?, States?)> InstructionAndNextState(States state)
        {
            var canStartA =
                !state.HasFlag(States.InsideA)
                && !state.HasFlag(States.RightAfterEndA);
            var canStartB =
                !state.HasFlag(States.InsideB)
                && !state.HasFlag(States.RightAfterEndB);
            var canEndA = state.HasFlag(States.InsideA);
            var canEndB = state.HasFlag(States.InsideB);
            var canMoveToAdjacent =
                state != States.None
                && !state.HasFlag(States.MovedPreviously)
                && !(state.HasFlag(States.RightAfterEndA)
                    && state.HasFlag(States.RightAfterEndB));
            var canMovePastAdjacent =
                state != States.None
                && !state.HasFlag(States.MovedPreviously);
            var canStop =
                !state.HasFlag(States.InsideA)
                && !state.HasFlag(States.InsideB)
                && !state.HasFlag(States.MovedPreviously);
            var allowedInstructions =
                Enumerable.Empty<Instruction?>()
                    .Concat(canStartA ? [Instruction.StartA] : [])
                    .Concat(canStartB ? [Instruction.StartB] : [])
                    .Concat(canEndA ? [Instruction.EndA] : [])
                    .Concat(canEndB ? [Instruction.EndB] : [])
                    .Concat(canMoveToAdjacent ? [Instruction.MoveToAdjacent] : [])
                    .Concat(canMovePastAdjacent ? [Instruction.MovePastAdjacent] : [])
                    .Concat(canStop ? [default(Instruction?)] : [])
                    .ToArray();
            Debug.Assert(allowedInstructions.Length > 0, "allowedInstructions.Length > 0");
            return Gen.OneOfConst(allowedInstructions).Select(x =>
                x is not { } a
                    ? (x, default(States?))
                    :
                    (a,
                    a switch
                    {
                        Instruction.StartA =>
                            States.InsideA
                            | (state & (States.InsideB | States.RightAfterEndB)),
                        Instruction.StartB =>
                            States.InsideB
                            | (state & (States.InsideA | States.RightAfterEndA)),
                        Instruction.EndA =>
                            States.RightAfterEndA
                            | (state & (States.InsideB | States.RightAfterEndB)),
                        Instruction.EndB =>
                            States.RightAfterEndB
                            | (state & (States.InsideA | States.RightAfterEndA)),
                        Instruction.MoveToAdjacent =>
                            States.MovedPreviously
                            | (state &
                                (States.InsideA
                                | States.InsideB
                                | States.RightAfterEndA
                                | States.RightAfterEndB)),
                        Instruction.MovePastAdjacent =>
                            States.MovedPreviously
                            | (state & (States.InsideA | States.InsideB)),
                    }));
        }

        static (ClosedIntervalUnion, ClosedIntervalUnion) ToIntervalUnionPair(
            ImmutableArray<Instruction?> instructions)
        {
            var a = new ClosedIntervalUnionBuilder();
            var b = new ClosedIntervalUnionBuilder();
            var position = 0;
            var aStart = default(int?);
            var bStart = default(int?);
            foreach (var instruction in instructions)
            {
                switch (instruction)
                {
                    case Instruction.StartA:
                        aStart = position;
                        break;
                    case Instruction.StartB:
                        bStart = position;
                        break;
                    case Instruction.EndA:
                        Debug.Assert(aStart != null, "aStart != null");
                        a.Append(new ClosedInterval(aStart.Value, position));
                        aStart = null;
                        break;
                    case Instruction.EndB:
                        Debug.Assert(bStart != null, "bStart != null");
                        b.Append(new ClosedInterval(bStart.Value, position));
                        bStart = null;
                        break;
                    case Instruction.MoveToAdjacent:
                        position += 1;
                        break;
                    case Instruction.MovePastAdjacent:
                        position += 2;
                        break;
                    case null:
                        Debug.Assert(aStart == null, "aStart == null");
                        Debug.Assert(bStart == null, "bStart == null");
                        break;
                }
            }
            return (a.Build(), b.Build());
        }
    }

    private static Gen<ImmutableArray<T>> GeneratedCollection<T, TState>(
        TState initial,
        Func<TState, Gen<(T Item, TState? State)>> resultAndNextState)
        where TState : struct
    {
        return resultAndNextState(initial)
            .SelectMany(x => GenerateAnotherItem((x.State, ImmutableArray.Create(x.Item))))
            .Select(x => x.Result);

        Gen<(TState? State, ImmutableArray<T> Result)> GenerateAnotherItem(
            (TState? State, ImmutableArray<T> Result) a)
        {
            if (a.State is not { } state)
            {
                return Gen.Const(a);
            }
            return resultAndNextState(state)
                .Select(b => (b.State, a.Result.Add(b.Item)))
                .SelectMany(GenerateAnotherItem);
        }
    }

    public enum Algorithm
    {
        RandomIntervals,
        TreeOfAllCombinations,
    }

    [Flags]
    private enum States
    {
        None = 0,
        InsideA = 1,
        InsideB = 2,
        RightAfterEndA = 4,
        RightAfterEndB = 8,
        MovedPreviously = 16,
    }

    private enum Instruction
    {
        StartA,
        StartB,
        EndA,
        EndB,
        MoveToAdjacent,
        MovePastAdjacent,
    }

    private sealed record class LinkedList<T>(T Value, LinkedList<T>? Next)
    {
        public IEnumerable<T> ToEnumerable()
        {
            yield return Value;
            var next = Next;
            while (next != null)
            {
                yield return next.Value;
                next = next.Next;
            }
        }
    }
}
