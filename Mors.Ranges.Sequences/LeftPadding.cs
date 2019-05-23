using System;

namespace Mors.Ranges.Sequences
{
    public struct LeftPadding
    {
        private readonly int _mainStart;
        private readonly int _otherStart;

        public LeftPadding(int mainStart, int otherStart)
        {
            _mainStart = mainStart;
            _otherStart = otherStart;
        }

        public int Length => Math.Max(0, _mainStart - _otherStart);
    }
}