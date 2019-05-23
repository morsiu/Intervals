using System;

namespace Mors.Ranges.Operations.Reference
{
    internal sealed class BoolCharacters
    {
        private readonly char _trueCharacter;
        private readonly char _falseCharacter;

        public BoolCharacters(
            char trueCharacter,
            char falseCharacter)
        {
            if (trueCharacter == falseCharacter)
            {
                throw new ArgumentException("The characters for true and false are identical.");
            }
            _trueCharacter = trueCharacter;
            _falseCharacter = falseCharacter;
        }

        public bool Bool(char character)
        {
            if (character == _trueCharacter) return true;
            if (character == _falseCharacter) return false;
            throw new ArgumentException("The character does not represent any boolean value.", nameof(character));
        }
    }
}
