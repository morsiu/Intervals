using System;

namespace Mors.Ranges.Sequences
{
    public sealed class UnexpectedEndOfInputException : Exception
    {
        private readonly string _stateName;

        public UnexpectedEndOfInputException(string stateName)
        {
            _stateName = stateName;
        }

        public override string Message =>
            $"Unexpected end of input in state {_stateName}";
    }
}