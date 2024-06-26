﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Mors.Intervals.Inequations
{
    internal sealed class Closure<T> : IInequation<T>
        where T : IComparable<T>
    {
        private readonly IInequation<T> _inequation;

        public Closure(IInequation<T> inequation) => _inequation = inequation;

        public IEnumerable<T> Boundaries() =>
            _inequation.Boundaries().FirstUnionLast();

        public bool IsSatisfiedBy(in T value)
        {
            var firstAndLast = _inequation.Boundaries().FirstUnionLast().ToImmutableArray();
            if (firstAndLast.Length == 0) return _inequation.IsSatisfiedBy(value);
            var first = firstAndLast.First();
            var last = firstAndLast.Last();
            return 
                (value.CompareTo(first), value.CompareTo(last)) switch
                {
                    (< 0, _) => false,
                    (0, _) => _inequation.IsSatisfiedBy(value),
                    (_, < 0) => true,
                    (_, 0) => _inequation.IsSatisfiedBy(value),
                    _ => false
                };
        }

        public override string ToString() => $"Closure({_inequation})";
    }
}
