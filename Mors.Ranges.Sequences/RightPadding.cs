using System;

namespace Mors.Ranges.Sequences
{
    public struct RightPadding
    {
        private readonly int _mainStart;
        private readonly int _mainLength;
        private readonly int _otherStart;
        private readonly int _otherLength;

        public RightPadding(int mainStart, int mainLength, int otherStart, int otherLength)
        {
            _mainStart = mainStart;
            _mainLength = mainLength;
            _otherStart = otherStart;
            _otherLength = otherLength;
        }

        public int Length => Math.Max(0, _otherStart + _otherLength - _mainStart - _mainLength);
    }
}