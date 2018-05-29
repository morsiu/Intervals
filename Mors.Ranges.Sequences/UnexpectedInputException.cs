using System;

namespace Mors.Ranges.Sequences
{
    public sealed class UnexpectedInputException : Exception
    {
        private readonly object _value;
        private readonly int _position;
        private readonly string _stateName;

        public UnexpectedInputException(object value, int position, string stateName)
        {
            _value = value;
            _position = position;
            _stateName = stateName;
        }

        public override string Message =>
            $"Unexpected input {_value} at position {_position} in state {_stateName}";
    }
}