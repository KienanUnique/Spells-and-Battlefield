using System;

namespace Player.Spell_Manager
{
    public class UnrecognizedSpellTypeException : Exception
    {
        public UnrecognizedSpellTypeException() : base("Unrecognized Spell Type Exception")
        {
        }
    }
}