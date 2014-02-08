// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;

namespace Walrus.Ranges.Test.Support.RangeOperations.StateMachines
{
    internal struct ValueWithState<TValue, TState> : IEquatable<ValueWithState<TValue, TState>>
    {
        private readonly TState _state;
        private readonly TValue _value;

        public ValueWithState(TValue value, TState state)
        {
            _value = value;
            _state = state;
        }

        public TState State { get { return _state; } }

        public TValue Value { get { return _value; } }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ReferenceEquals(_value, null) ? 0 : (_value.GetHashCode() * 137))
                    ^ (ReferenceEquals(_state, null) ? 0 : _state.GetHashCode());
            }
        }

        public override bool Equals(object obj)
        {
            return obj is ValueWithState<TValue, TState>
                && Equals((ValueWithState<TValue, TState>)obj);
        }

        public bool Equals(ValueWithState<TValue, TState> other)
            {
                return EqualityComparer<TValue>.Default.Equals(_value, other._value)
                    && EqualityComparer<TState>.Default.Equals(_state, other._state);
            }
    }
}
