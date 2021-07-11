using System;

namespace Chroma8NeverDoneBefore.Chip
{
    public class InstructionAttribute : Attribute
    {
        public const string ACCEPTED_CHARS = "0123456789ABCDEF";
        
        public readonly string MatchStr;
        public readonly bool IncrementPc;

        public InstructionAttribute(string matchStrStr, bool incrementPc = true)
        {
            MatchStr = matchStrStr;
            IncrementPc = incrementPc;
        }
    }
}